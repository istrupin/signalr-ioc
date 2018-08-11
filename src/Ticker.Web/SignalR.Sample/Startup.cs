using Microsoft.AspNet.SignalR.Hubs;
using Owin;

namespace Microsoft.AspNet.SignalR.StockTicker
{
    public static class Startup
    {
        public static void ConfigureSignalR(IAppBuilder app, HubConfiguration config)
        {
            // For more information on how to configure your application using OWIN startup, visit http://go.microsoft.com/fwlink/?LinkID=316888

             app.MapSignalR("/signalr",config);

        }
    }
}