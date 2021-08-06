using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows.Threading;
using IQFeed.CSharpApiClient.Streaming.Level1.Messages;
using IQFeed.CSharpApiClient.WPF.Annotations;
using IQFeed.CSharpApiClient.WPF.Common;
using IQFeed.CSharpApiClient.WPF.Models;
using IQFeed.CSharpApiClient.WPF.Services;

namespace IQFeed.CSharpApiClient.WPF.ViewModels
{
    public class QuoteViewModel : INotifyPropertyChanged
    {
        #region Symbol
        private string _symbol;
        public string Symbol
        {
            get => _symbol;
            set
            {
                _symbol = value;
                OnPropertyChanged(nameof(Symbol));
            }
        }
        #endregion

        public ObservableCollection<QuoteViewRow> QuoteRows { get; }
        public ICommand AddCommand { get; }
        public ICommand RemoveCommand { get; }

        private readonly Dictionary<string, QuoteViewRow> _quoteViewRowsBySymbol;
        private readonly RealTimeMarketDataService _realTimeMarketDataService;

        public QuoteViewModel()
        {
            _quoteViewRowsBySymbol = new Dictionary<string, QuoteViewRow>();
            _realTimeMarketDataService = new RealTimeMarketDataService();
            _realTimeMarketDataService.Level1Msg += RealTimeMarketDataServiceOnLevel1Msg;

            QuoteRows = new ObservableCollection<QuoteViewRow>();

            AddCommand = new RelayCommand<string>(s => AddCommandExecute(s.ToUpper()), s => true);
            RemoveCommand = new RelayCommand<string>(s => RemoveCommandExecute(s.ToUpper()), s => true);

            InitializeSymbols();
        }

        private void InitializeSymbols()
        {
            var symbols = new List<string>()
            {
                "AAPL",
                "MSFT",
                "NFLX",
                "AMZN",
                "FB",
            };

            foreach (var symbol in symbols)
            {
                AddCommandExecute(symbol);
            }
        }

        private void RealTimeMarketDataServiceOnLevel1Msg(IUpdateSummaryMessage updateSummaryMessage)
        {
            Dispatcher.CurrentDispatcher.Invoke(() => UpdateQuoteViewRow(updateSummaryMessage));
        }

        private void UpdateQuoteViewRow(IUpdateSummaryMessage updateSummaryMessage)
        {
            if (!_quoteViewRowsBySymbol.TryGetValue(updateSummaryMessage.Symbol, out var quoteViewRow))
                return;

            quoteViewRow.Last = updateSummaryMessage.MostRecentTrade;
            quoteViewRow.LastSize = updateSummaryMessage.MostRecentTradeSize;
            quoteViewRow.TotalVolume = updateSummaryMessage.TotalVolume;
            quoteViewRow.Bid = updateSummaryMessage.Bid;
            quoteViewRow.BidSize = updateSummaryMessage.BidSize;
            quoteViewRow.Ask = updateSummaryMessage.Ask;
            quoteViewRow.AskSize = updateSummaryMessage.AskSize;
            quoteViewRow.Open = updateSummaryMessage.Open;
            quoteViewRow.High = updateSummaryMessage.Low;
            quoteViewRow.Low = updateSummaryMessage.Low;
            quoteViewRow.Close = updateSummaryMessage.Close;
            quoteViewRow.UpdateTime = updateSummaryMessage.MostRecentTradeTime;
        }

        private void AddCommandExecute(string symbol)
        {
            var quoteViewRow = new QuoteViewRow(symbol);
            _quoteViewRowsBySymbol.Add(symbol, quoteViewRow);
            _realTimeMarketDataService.Subscribe(symbol);
            QuoteRows.Add(quoteViewRow);
            Symbol = string.Empty;
        }

        private void RemoveCommandExecute(string symbol)
        {
            if (_quoteViewRowsBySymbol.TryGetValue(symbol, out var quoteViewRow))
            {
                QuoteRows.Remove(quoteViewRow);
                _realTimeMarketDataService.Unsubscribe(symbol);
            }
            Symbol = string.Empty;
        }

        #region OnPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}