using Kr.Core.Helpers;
using Nt.Core.Configuration;
using Nt.Core.DependencyInjection;
using Nt.Core.Hosting;
using Nt.Core.Logging;
using Nt.Core.Logging.Configuration;
using Nt.Scripts.Hosting;
using Nt.Scripts.MasterScripts;
using Nt.Scripts.Ninjascripts;
using Nt.Scripts.Ninjascripts.Indicators;
using Nt.Scripts.Services;
using System;

namespace ConsoleApp
{
    internal class Program
    {
        ////private static IHost _host;
        //private static ILogger _logger;

        private static void FindingObjectsInMS()
        {
            //Microsoft.Extensions.DependencyInjection.ServiceDescriptor sd;
            //Microsoft.Extensions.DependencyInjection.ServiceProvider sp;
            //Microsoft.Extensions.DependencyInjection.ServiceCollection src = new Microsoft.Extensions.DependencyInjection.ServiceCollection();
            //Microsoft.Extensions.Logging.Console.
            //Microsoft.Extensions.Hosting.Internal.Host
            //Microsoft.Extensions.Hosting.HostBuilder
            //Microsoft.Extensions.Configuration.ConfigurationProvider
            //Microsoft.Extensions.Hosting.Internal.ConsoleLifetime
            //Microsoft.Extensions.Primitives.ChangeToken
        }

        public static void Main(string[] args)
        {
            var ninjascript = new KrTradeStats();

            ninjascript.Configure();

            for (int i = 0; i < 100; i++)
            {
                ninjascript.OnBarUpdate();
                if (i > 0 && i%10 == 0)
                    ninjascript.OnSessionUpdate();
                    
            }

            string name = TypeNameHelper.GetTypeDisplayName(typeof(KrTradeStats),fullName:false, includeGenericParameterNames: false,nestedTypeDelimiter:'.');


            //UseDesignNinjascriptHostingServices();

            //if (DesignNinjaHost.Host == null)
            //    throw new ArgumentNullException(nameof(DesignNinjaHost.Host));

            //_logger = DesignNinjaHost.Logger<Program>();
            //_logger.LogInformation("Hello World!");
            ////_logger.LogInformation(DesignNinjaHost.SessionsIterator.ToString());
            ////_logger.LogInformation(DesignNinjaHost.SessionsFilters.IsEnabled.ToString());
            ////_logger.LogInformation(DesignNinjaHost.Ninjascript.ToString());
            //IConfigurationRoot config = (IConfigurationRoot)(DesignNinjaHost.Host?.Services.GetService<HostBuilderContext>().Configuration);
            //IConfiguration configure = DesignNinjaHost.Host?.Services.GetService<IConfiguration>();
            //IDataSeries dataSeries = DesignNinjaHost.Host?.Services.GetService<IDataSeries>();
            //INinjascript<Program> ninjascripts = DesignNinjaHost.Host?.Services.GetService<INinjascript<Program>>();
            //INinjascript<Sessions> ninjascripts2 = DesignNinjaHost.Host?.Services.GetService<INinjascript<Sessions>>();
            //MasterScriptFactory master = DesignNinjaHost.Host?.Services.GetService<MasterScriptFactory>();
        }

        private static void UseDesignNinjascriptHostingServices()
        {
            //DesignNinjaHost.Create(DesignHosting.CreateDesignNinjaDefaultBuilder("Krumon").Build());
        }
        private static void UseNinjascriptHost()
        {
            //if (NinjaHost.Host == null)
            //    return;

            //NinjaHost.Create(Host.CreateDefaultBuilder()
            //.ConfigureAppConfiguration((context,builder) =>
            //{
            //    builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            //})
            //.ConfigureServices((context, services) =>
            //{
            //    services.AddLogging((builder) =>
            //    {
            //        builder.SetMinimumLevel(LogLevel.Debug);
            //        builder.AddConsole();
            //        builder.AddDebug();
            //        builder.AddConfiguration(context.Configuration.GetSection("Logging"));
            //    });
            //})
            //.Build());

            //NinjaHost.Host.Configure(null);
            //NinjaHost.Host.DataLoaded(null);
            //NinjaHost.Host.OnBarUpdate();
            //NinjaHost.Host.OnMarketData();
            //NinjaHost.Host.OnSessionUpdate();
            //NinjaHost.Host.Dispose();

        }
        private static void UseNinjascriptHost2()
        {
            //NinjaHost.Create(Host.CreateDefaultBuilder()
            //    //.ConfigureAppConfiguration((context, builder) =>
            //    //{
            //    //    builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            //    //})
            //    .ConfigureServices((context, services) =>
            //    {
            //        //services.AddLogging(builder =>
            //        //{
            //        //    builder.SetMinimumLevel(LogLevel.Information);
            //        //    builder.AddConsole();
            //        //    builder.AddDebug();
            //        //});
            //        services.AddSingleton<IChartBarsProperties, ChartBarsProperties>()
            //        //.AddSessions<ISessionsService>((builder) =>
            //        //{
            //        //    builder.ConfigureFilters((options) =>
            //        //    {
            //        //        options.IncludePartialHolidays = false;
            //        //        options.IncludeHolidays = false;
            //        //        options.ExcludeHistoricalData = true;
            //        //    });
            //        //})
            //        ;
            //    })
            //    .Build());
        }
        private static void UseColorConsoleLogger()
        {
            //NinjaHost.Create(Host.CreateDefaultBuilder()
            //    .ConfigureServices((context, services) =>
            //    {
            //        services.AddLogging(builder =>
            //        {
            //            builder.ClearProviders();
            //            builder.AddConsole();
            //            builder.AddFile();
            //        })
            //        ;
            //    })
            //    .Build());
        }
    }
}
