using System;
using IQFeed.CSharpApiClient.Streaming.Level1;
using IQFeed.CSharpApiClient.Streaming.Level1.Messages;

namespace IQFeed.CSharpApiClient.WPF.Services
{
    public class RealTimeMarketDataService : IMarketDataService
    {
        private readonly Level1Client _level1Client;

        public event Action<IUpdateSummaryMessage> Level1Msg
        {
            add
            {
                _level1Client.Summary += value;
                _level1Client.Update += value;
            }
            remove
            {
                _level1Client.Summary -= value;
                _level1Client.Update -= value;
            }
        }

        public RealTimeMarketDataService()
        {
            IQFeedLauncher.Start("MY_LOGIN", "MY_PASSWORD", "MY_PRODUCT_ID", "MY_PRODUCT_VERSION");

            _level1Client = Level1ClientFactory.CreateNew();
            _level1Client.Connect();
        }

        public void Subscribe(string symbol)
        {
            _level1Client.ReqWatch(symbol);
        }

        public void Unsubscribe(string symbol)
        {
            _level1Client.ReqUnwatch(symbol);
        }
    }
}