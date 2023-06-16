using KrTrade.NtCode.Data;
using KrTrade.NtCode.Tests;

namespace KrTrade.NtCode
{
    internal class NinjascriptHostingTests : BaseConsoleTests
    {

        #region Private members


        #endregion

        #region Public Properties


        #endregion

        #region Constructor

        /// <summary>
        /// Create a <see cref="TradingSessionTests"/> default instance.
        /// </summary>
        public NinjascriptHostingTests()
        {

        }

        #endregion

        #region Public methods

        public override async void Run()
        {
            //await InstanceTestsAsync();
            WaitAndClear();
        }

        #endregion

        #region Private methods

        //private async Task InstanceTestsAsync()
        //{
        //    // Create a custom instance.
        //    Title("Instance tests.");

        //    using (IHost host = Host.CreateDefaultBuilder().ConfigureAppConfiguration((hostingContext, configurationBuilder) =>
        //    {
        //        configurationBuilder.Sources.Clear();

        //        IHostEnvironment env = hostingContext.HostingEnvironment;
        //        var path = "appsettings.json"; // Path.Combine(env.ContentRootPath, "appsettings.json");

        //        configurationBuilder.SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

        //        configurationBuilder
        //            .AddJsonFile(path, optional: false, reloadOnChange: true)
        //            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
        //            .AddJsonFile($"appsettingsNt.json", optional: true, reloadOnChange: true)
        //            .AddEnvironmentVariables((configure) =>
        //            {
        //                configure.Prefix = "PROCESSOR_";
        //            });

        //        IConfigurationRoot configurationRoot = configurationBuilder.Build();

        //        //TransientFaultHandlingOptions options = new TransientFaultHandlingOptions();
        //        //configurationRoot.GetSection(nameof(TransientFaultHandlingOptions))
        //        //                 .Bind(options);

        //        //Console.WriteLine($"TransientFaultHandlingOptions.Enabled={options.Enabled}");
        //        //Console.WriteLine($"TransientFaultHandlingOptions.AutoRetryDelay={options.AutoRetryDelay}");

        //    }).Build())
        //    {
        //        //Enter code

        //        await host.RunAsync();
        //    }


        //}

        private void ToStringTests(SessionType type)
        {
        }

        #endregion

    }
}
