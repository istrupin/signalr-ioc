using System;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;
using Autofac.Integration.SignalR;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNet.SignalR.Infrastructure;
using Microsoft.AspNet.SignalR.StockTicker;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Ticker.Web.Startup))]

namespace Ticker.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var builder = new ContainerBuilder();
            var config = new HubConfiguration();

            //builder.RegisterHubs(Assembly.GetExecutingAssembly());


            builder.Register(i => config.Resolver.Resolve<IConnectionManager>().GetHubContext<StockTickerHub>().Clients)
                .Named<IHubConnectionContext<dynamic>>("StockTickerContext")
                .ExternallyOwned();

            //builder.Register(i => new StockTicker(
            //    config.Resolver.Resolve<IConnectionManager>().GetHubContext<StockTickerHub>().Clients)).As<IStockTicker>().SingleInstance();
            builder.RegisterType<StockTicker>()
                .WithParameter(ResolvedParameter.ForNamed<IHubConnectionContext<dynamic>>("StockTickerContext"))
                .As<IStockTicker>().SingleInstance();
            builder.RegisterType<StockTickerHub>().ExternallyOwned();


            var container = builder.Build();
            config.Resolver = new AutofacDependencyResolver(container);


            app.UseAutofacMiddleware(container);

            Microsoft.AspNet.SignalR.StockTicker.Startup.ConfigureSignalR(app, config);
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
        }
    }
}
