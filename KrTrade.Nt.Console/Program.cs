using KrTrade.Nt.Core.Bars;
using KrTrade.Nt.Core.Data;
using KrTrade.Nt.Core;
using KrTrade.Nt.Core.Logging;
using KrTrade.Nt.Services;
using KrTrade.Nt.Services.Series;
using KrTrade.Nt.Core.Services;
using KrTrade.Nt.Core.Series;

namespace KrTrade.Nt.Console.Console
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
            IBarsService barsService = new BarsServiceBuilder(null, null, null)
                .Configure((info,options) =>
                {
                    // Information
                    info.Name = "KRUMON_BARS";
                    info.InstrumentCode = InstrumentCode.Default;
                    info.TimeFrame = TimeFrame.m1;
                    info.TradingHoursCode = TradingHoursCode.Default;
                    info.MarketDataType = MarketDataType.Last;
                    // Options
                    options.IsEnable = true;
                    options.IsLogEnable = true;
                    options.CalculateMode = NinjaTrader.NinjaScript.Calculate.OnBarClose;
                    options.MultiSeriesCalculateMode = MultiSeriesCalculateMode.PrimarySeriesClosed;
                    options.CacheCapacity = 10;
                    options.RemovedCacheCapacity = 2;
                })
                .Build();

            // Configure
            IBarsManager bars = new BarsManagerBuilder()
                .AddPrintService((printSvc) =>
                {
                    printSvc.IsEnable = true;
                    printSvc.IsLogInfoVisible = true;
                    printSvc.IsDataSeriesInfoVisible = true;
                    printSvc.IsNumOfBarVisible = true;
                    printSvc.IsTimeVisible = true;
                    printSvc.FormatLength = FormatLength.Short;
                    printSvc.LogLevel = LogLevel.Information;
                    printSvc.NinjascriptLogLevel = NinjascriptLogLevel.Configuration;
                    printSvc.BarsLogLevel = BarsLogLevel.BarClosed;
                })
                .ConfigureOptions((options) =>
                {
                    options.DefaultCachesCapacity = 2;
                    options.DefaultRemovedCachesCapacity = 1;
                })
                .ConfigurePrimaryBars((builder) =>
                {
                    //builder.ConfigureOptions((info, options) =>
                    //{
                    //    info.InstrumentCode = InstrumentCode.MES;
                    //});
                    //builder.AddSeries_Period(
                    //    (info) => {
                    //        info.Name = "Max5";
                    //        info.Type = PeriodSeriesType.AVG;
                    //        info.Period = 5;
                    //        info.Capacity = 7;
                    //        info.AddInputSeries_Period(max =>
                    //        {
                    //            max.Type = PeriodSeriesType.MAX;
                    //            max.AddInputSeries<BarsSeriesInfo>(high =>
                    //            {
                    //                high.Type = BarsSeriesType.HIGH;
                    //            });
                    //        });
                    //        info.AddInputSeries_Swing(swing =>
                    //        {
                    //            swing.AddInputSeries<PeriodSeriesInfo>(min =>
                    //            {
                    //                min.Type = PeriodSeriesType.MIN;
                    //                min.OldValuesCapacity = 5;
                    //                min.Period = 5;
                    //            });
                    //        });
                    //        info.AddInputSeries<SwingSeriesInfo>(high =>
                    //        {
                    //            high.Type = StrengthSeriesType.SWING_HIGH;
                    //            high.LeftStrength = 3;
                    //            high.RightStrength = 3;
                    //        });
                    //    });
                })
                // Add bars service.
                .AddDataSeries(builder =>
                {
                    // Configure the BarsService options.
                    builder.Configure((info,op) =>
                    {
                        // Configure the data series.
                        info.InstrumentCode = InstrumentCode.MES;
                        info.TradingHoursCode = TradingHoursCode.Default;
                        info.TimeFrame = TimeFrame.m5;
                        info.MarketDataType = MarketDataType.Last;
                        // Configure the service
                        op.IsEnable = true;
                        op.IsLogEnable = true;
                        op.CalculateMode = NinjaTrader.NinjaScript.Calculate.OnEachTick;
                    });
                    //// Add series to the BarsService().
                    //builder.AddSeries_Period((info) =>
                    //{
                    //    info.Type = PeriodSeriesType.DEVSTD;
                    //    info.Period = 20;
                    //}); 
                })
                .AddDataSeries(builder =>
                { 
                    builder.Configure((info,op) =>
                    {
                        info.InstrumentCode = InstrumentCode.MES;
                        info.TradingHoursCode = TradingHoursCode.Default;
                        info.TimeFrame = TimeFrame.m5;
                        info.MarketDataType = MarketDataType.Last;
                    });
                })
                .Build(null,null);


            bars.Configure();

            // DataLoaded
            bars.DataLoaded();

            // OnBarUpdate
            bars.OnBarUpdate();

            System.Console.ReadKey();

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
