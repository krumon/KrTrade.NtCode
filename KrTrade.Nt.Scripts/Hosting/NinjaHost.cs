using KrTrade.Nt.DI.Hosting;

namespace KrTrade.Nt.Scripts.Hosting
{
    public static class NinjaHost
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HostBuilder"/> class with pre-configured defaults.
        /// </summary>
        /// <remarks>
        ///   The following defaults are applied to the returned <see cref="HostBuilder"/>:
        ///   <list type="bullet">
        ///     <item><description>set the <see cref="IHostEnvironment.ContentRootPath"/> to the result of <see cref="Directory.GetCurrentDirectory()"/></description></item>
        ///     <item><description>load host <see cref="Core.Configuration.IConfiguration"/> from "DOTNET_" prefixed environment variables</description></item>
        ///     <item><description>load app <see cref="Core.Configuration.IConfiguration"/> from 'appsettings.json' and 'appsettings.[<see cref="IHostEnvironment.EnvironmentName"/>].json'</description></item>
        ///     <item><description>load app <see cref="Core.Configuration.IConfiguration"/> from User Secrets when <see cref="IHostEnvironment.EnvironmentName"/> is 'Development' using the entry assembly</description></item>
        ///     <item><description>load app <see cref="Core.Configuration.IConfiguration"/> from environment variables</description></item>
        ///     <item><description>configure the <see cref="ILoggerFactory"/> to log to the console, debug, and event source output</description></item>
        ///     <item><description>enables scope validation on the dependency injection container when <see cref="IHostEnvironment.EnvironmentName"/> is 'Development'</description></item>
        ///   </list>
        /// </remarks>
        /// <returns>The initialized <see cref="IHostBuilder"/>.</returns>
        public static IHostBuilder CreateNinjaHostDefaultBuilder<T>(params object[] ninjatraderObjects)
        {
            IHostBuilder builder = new NinjaHostBuilder();
            return builder.NinjaHostConfigureDefaults<T>(ninjatraderObjects, true);
        }

        public static IHostBuilder CreateDesignNinjaHostDefaultBuilder<T>(params object[] ninjatraderObjects)
        {
            IHostBuilder builder = new NinjaHostBuilder();
            return CreateNinjaHostDefaultBuilder<T>(builder, ninjatraderObjects);
        }

    }
}
