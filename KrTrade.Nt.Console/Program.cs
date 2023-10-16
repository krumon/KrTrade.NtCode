using KrTrade.Nt.Core.Print;

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
            //BasePrint printSvc = new BasePrint(System.Console.WriteLine, System.Console.Clear);
            //printSvc.WriteTitle("Este es el encabezado de prueba");
            //printSvc.WriteLine(lineChar: '*');
            //printSvc.WriteBlankLine();
            //printSvc.Write("High", 1525);
            //printSvc.Write("Open, High, Low, Close ", 3215.25,3217.75,3213,3214.5);
            //System.Console.ReadKey();
            
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
