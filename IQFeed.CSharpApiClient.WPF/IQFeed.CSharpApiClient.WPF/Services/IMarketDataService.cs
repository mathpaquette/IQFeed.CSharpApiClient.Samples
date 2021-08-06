using System;
using IQFeed.CSharpApiClient.Streaming.Level1.Messages;

namespace IQFeed.CSharpApiClient.WPF.Services
{
    public interface IMarketDataService
    {
        event Action<IUpdateSummaryMessage> Level1Msg;
        void Subscribe(string symbol);
        void Unsubscribe(string symbol);
    }
}