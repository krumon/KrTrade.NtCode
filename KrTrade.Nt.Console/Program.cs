using KrTrade.Nt.Core.Bars;
using KrTrade.Nt.Core.Data;
using KrTrade.Nt.Core.Logging;
using KrTrade.Nt.Services;

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
                    options.DefaultCachesCapacity = 14;
                    options.DefaultRemovedCachesCapacity = 1;
                })
                .ConfigurePrimaryDataSeries((builder) =>
                {
                    builder.AddSeries(
                        (max_info, max_op) => {
                            max_op.IsEnable = true;
                            max_op.IsLogEnable = true;
                            max_info.Code = "Max5";
                            max_info.Type = SeriesType.AVG;
                            max_info.Period = 5;
                            max_info.Capacity = 7;
                            max_info.AddInputSeries<SwingSeriesInfo>(high =>
                            {

                            });
                        });
                })
                // Add bars service.
                .AddDataSeries(
                // Configure the data series.
                info =>
                {
                    info.InstrumentCode = InstrumentCode.MES;
                    info.TradingHoursCode = TradingHoursCode.Default;
                    info.TimeFrame = TimeFrame.m5;
                    info.MarketDataType = MarketDataType.Last;
                }, 
                // Built the bars service.
                builder =>
                {
                    // Configure the BarsService options.
                    builder.ConfigureOptions(op =>
                    {

                    });
                    // Add series to the BarsService().
                    builder.AddSeries((info,op) =>
                    {

                    }); 
                })
                .AddDataSeries(
                info =>
                {
                    info.InstrumentCode = InstrumentCode.MES;
                    info.TradingHoursCode = TradingHoursCode.Default;
                    info.TimeFrame = TimeFrame.m5;
                    info.MarketDataType = MarketDataType.Last;
                }, 
                builder =>
                {
                    builder.ConfigureOptions(bsOptions =>
                    {

                    });
                })
                .Configure(null,null);


            //bars
            //.AddService<CacheService<MaxCache>, CacheServiceOptions>("MAX",(options) =>
            //{
            //    options.IsLogEnable = true;
            //    options.Capacity = 5;
            //},bars.Ninjascript.High)
            //.AddService<CacheService<MaxCache>, CacheServiceOptions>("MIN",(options) =>
            //{
            //    options.IsLogEnable = true;
            //    options.Capacity = 5;
            //});

            bars.Configure();

            // DataLoaded
            bars.DataLoaded();

            // OnBarUpdate
            bars.OnBarUpdate();

            //var displacement = 0;
            //var strength = 4;

            //double swingHighValue = bars.Series.Close.SwingHigh(displacement, strength);
            //var max = bars.GetCache<MaxCache>();
            //var currentMax = max[0];
            //if (swingHighValue > 0)
            //    bars.GetBars(displacement, strength * 2 + 1);

            //var high = bars.Series.High[0];

            //NinjaScriptBase ninjascript = null;
            //PrintService printService;
            //BarsService barsSvc = new BarsService(ninjascript, printService);
            //barsSvc.LogOptions.BarsLogLevel = Core.Bars.BarsLogLevel.PriceChanged;

            //int capacity = 10;
            //DoubleCache cache = new DoubleCache(capacity);
            //Random random = new Random();

            //for (int i = 0; i < capacity*2; i++)
            //{
            //    cache.Add(random.Next(0,1000));
            //}

            //for (int i = 0; i < capacity; i++)
            //    System.Console.WriteLine(string.Format("Cache[{0}]: {1}", i, cache[i]));

            //System.Console.WriteLine();

            //System.Console.WriteLine(string.Format("Last: {0}", cache.Last));
            //System.Console.WriteLine(string.Format("High: {0}", cache.High));
            //System.Console.WriteLine(string.Format("Low: {0}", cache.Low));

            //cache.Replace(69, 0);
            //for (int i = 0; i < capacity; i++)
            //    System.Console.WriteLine(string.Format("Cache[{0}]: {1}", i, cache[i]));

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
