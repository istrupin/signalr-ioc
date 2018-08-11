using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Microsoft.AspNet.SignalR.Hubs;

namespace Microsoft.AspNet.SignalR.StockTicker
{
    [HubName("stockTicker")]
    public class StockTickerHub : Hub
    {
        private readonly ILifetimeScope _lifetimeScope;
        private readonly IStockTicker _stockTicker;

        //public StockTickerHub() :
        //    this(StockTicker.Instance)
        //{

        //}

        public StockTickerHub(IStockTicker stockTicker, ILifetimeScope lifetimeScope)
        {
            _stockTicker = stockTicker;
            _lifetimeScope = lifetimeScope.BeginLifetimeScope();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _lifetimeScope != null)
            {
                _lifetimeScope.Dispose();
            }
            base.Dispose(disposing);
        }

        public IEnumerable<Stock> GetAllStocks()
        {
            return _stockTicker.GetAllStocks();
        }

        public string GetMarketState()
        {
            return _stockTicker.MarketState.ToString();
        }

        public void OpenMarket()
        {
            _stockTicker.OpenMarket();
        }

        public void CloseMarket()
        {
            _stockTicker.CloseMarket();
        }

        public void Reset()
        {
            _stockTicker.Reset();
        }
    }
}