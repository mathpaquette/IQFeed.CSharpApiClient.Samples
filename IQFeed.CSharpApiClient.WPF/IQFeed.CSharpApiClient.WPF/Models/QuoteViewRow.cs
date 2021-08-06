using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using IQFeed.CSharpApiClient.WPF.Annotations;

namespace IQFeed.CSharpApiClient.WPF.Models
{
    public class QuoteViewRow : INotifyPropertyChanged
    {
        public QuoteViewRow(string symbol)
        {
            Symbol = symbol;
        }
        
        public string Symbol { get; }

        #region Last
        private double _last;
        public double Last
        {
            get => _last;
            set
            {
                if (_last == value) return;
                _last = value;
                OnPropertyChanged(nameof(Last));
            }
        }
        #endregion

        #region LastSize
        private int _lastSize;
        public int LastSize
        {
            get => _lastSize;
            set
            {
                if (_lastSize == value) return;
                _lastSize = value;
                OnPropertyChanged(nameof(LastSize));
            }
        } 
        #endregion

        #region TotalVolume
        private int _totalVolume;
        public int TotalVolume
        {
            get => _totalVolume;
            set
            {
                if (_totalVolume == value) return;
                _totalVolume = value;
                OnPropertyChanged(nameof(TotalVolume));
            }
        } 
        #endregion

        #region Bid
        private double _bid;
        public double Bid
        {
            get => _bid;
            set
            {
                if (_bid == value) return;
                _bid = value;
                OnPropertyChanged(nameof(Bid));
            }
        } 
        #endregion

        #region BidSize
        private int _bidSize;
        public int BidSize
        {
            get => _bidSize;
            set
            {
                if (_bidSize == value) return;
                _bidSize = value;
                OnPropertyChanged(nameof(BidSize));
            }
        } 
        #endregion

        #region Ask
        private double _ask;
        public double Ask
        {
            get => _ask;
            set
            {
                if (_ask == value) return;
                _ask = value;
                OnPropertyChanged(nameof(Ask));
            }
        } 
        #endregion

        #region AskSize
        private int _askSize;
        public int AskSize
        {
            get => _askSize;
            set
            {
                if (_askSize == value) return;
                _askSize = value;
                OnPropertyChanged(nameof(AskSize));
            }
        } 
        #endregion

        #region Open
        private double _open;
        public double Open
        {
            get => _open;
            set
            {
                if (_open == value) return;
                _open = value;
                OnPropertyChanged(nameof(Open));
            }
        }
        #endregion

        #region High
        private double _high;
        public double High
        {
            get => _high;
            set
            {
                if (_high == value) return;
                _high = value;
                OnPropertyChanged(nameof(High));
            }
        }
        #endregion

        #region Low
        private double _low;
        public double Low
        {
            get => _low;
            set
            {
                if (_low == value) return;
                _low = value;
                OnPropertyChanged(nameof(Low));
            }
        }
        #endregion

        #region Close
        private double _close;
        public double Close
        {
            get => _close;
            set
            {
                if (_close == value) return;
                _close = value;
                OnPropertyChanged(nameof(Close));
            }
        }
        #endregion

        #region UpdateTime
        private TimeSpan _updateTime;
        public TimeSpan UpdateTime
        {
            get => _updateTime;
            set
            {
                if (_updateTime == value) return;
                _updateTime = value;
                OnPropertyChanged(nameof(UpdateTime));
            }
        } 
        #endregion

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        } 
        #endregion
    }
}