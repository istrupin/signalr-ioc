using System.Collections.Generic;

namespace Microsoft.AspNet.SignalR.StockTicker
{
    public interface IStockTicker
    {
        IEnumerable<Stock> GetAllStocks();
        void OpenMarket();
        void CloseMarket();
        void Reset();
        MarketState MarketState { get; }
    }
}