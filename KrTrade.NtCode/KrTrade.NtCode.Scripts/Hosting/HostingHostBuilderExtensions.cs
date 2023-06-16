using KrTrade.NtCode.Attributes;
using KrTrade.NtCode.Configuration;
using KrTrade.NtCode.Hosting;
using KrTrade.NtCode.Logging;
using KrTrade.NtCode.MasterScripts;
using KrTrade.NtCode.Ninjascripts;
using KrTrade.NtCode.Ninjascripts.Indicators;
using KrTrade.NtCode.NinjatraderObjects;
using System.IO;

namespace KrTrade.NtCode.Hosting
{
    public static class HostingHostBuilderExtensions
    {

        /// <summary>
        /// Configures an existing <see cref="IHostBuilder"/> instance with pre-configured defaults. This will overwrite
        /// previously configured values and is intended to be called before additional configuration calls.
        /// </summary>
        /// <remarks>
        ///   The following defaults are applied to the <see cref="IHostBuilder"/>:
        ///     * set the <see cref="IHostEnvironment.ContentRootPath"/> to the result of <see cref="Directory.GetCurrentDirectory()"/>
        ///     * load host <see cref="IConfiguration"/> from "DOTNET_" prefixed environment variables
        ///     * load host <see cref="IConfiguration"/> from supplied command line args
        ///     * load app <see cref="IConfiguration"/> from 'appsettings.json' and 'appsettings.[<see cref="IHostEnvironment.EnvironmentName"/>].json'
        ///     * load app <see cref="IConfiguration"/> from User Secrets when <see cref="IHostEnvironment.EnvironmentName"/> is 'Development' using the entry assembly
        ///     * load app <see cref="IConfiguration"/> from environment variables
        ///     * load app <see cref="IConfiguration"/> from supplied command line args
        ///     * configure the <see cref="ILoggerFactory"/> to log to the console, debug, and event source output
        ///     * enables scope validation on the dependency injection container when <see cref="IHostEnvironment.EnvironmentName"/> is 'Development'
        /// </remarks>
        /// <param name="builder">The existing builder to configure.</param>
        /// <param name="ninjatraderObjects">The command line args.</param>
        /// <returns>The same instance of the <see cref="IHostBuilder"/> for chaining.</returns>
        public static IHostBuilder NinjaHostConfigureDefaults<T>(this IHostBuilder builder, object[] ninjatraderObjects, bool isInDesignMode = false)
        {
            // Configure the content root of the Host.
            if (!isInDesignMode)
                ConfigureContentRoot(builder);
            else
                builder.UseContentRoot(Directory.GetCurrentDirectory());

            // Configure the host launch settings
            builder.ConfigureHostConfiguration(config =>
                {
                    config.AddJsonFile("launchsettings.json", optional: true, reloadOnChange: false);
                });

            // Configure the application to launch in ninjatrader.
            builder.ConfigureAppConfiguration((hostingContext, config) =>
            {
                IHostEnvironment env = hostingContext.Environment;
                bool reloadOnChange = GetReloadConfigOnChangeValue(hostingContext);

                config
                    .AddJsonFile("ntsettings.json", optional: true, reloadOnChange: reloadOnChange)
                    .AddJsonFile($"ntsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: reloadOnChange);
            })

            // Configure the ninjatrader logging.
            .ConfigureLogging((hostingContext, logging) =>
            {
                logging
                    .AddConfiguration(hostingContext.Configuration.GetSection("Logging"))
                    .AddOutputWindowLogger();
            })

            // Configure the default service provider.
            .UseDefaultServiceProvider((context, options) =>
            {
                bool isDevelopment = context.Environment.IsDevelopment();
                options.ValidateScopes = isDevelopment;
                options.ValidateOnBuild = isDevelopment;
            })

            //Configure the ninjatrader services
            .ConfigureServices((context, services) =>
            {
                if (!context.Environment.IsInDesignMode)
                    services.AddNinjatraderObjects(ninjatraderObjects);
                else 
                    services.AddDesignNinjatraderObjects();
            })

            // Configure the Master scripts
            .ConfigureMasterScripts(context => { })

            // Configure the ninjascripts
            .ConfigureNinjascript((context, ninjascriptBuilder) =>
            {
                ninjascriptBuilder.AddConfiguration(context.Configuration.GetSection("Ninjascripts"));
                if (!context.Environment.IsInDesignMode)
                    ninjascriptBuilder.AddSessions(ninjatraderObjects);
                else
                    ninjascriptBuilder.AddDesignSessions();

            });
            
            return builder;

        }

        [UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026:RequiresUnreferencedCode", Justification = "Calling IConfiguration.GetValue is safe when the T is bool.")]
        private static bool GetReloadConfigOnChangeValue(HostBuilderContext hostingContext) => hostingContext.Configuration.GetValue("hostBuilder:reloadConfigOnChange", defaultValue: true);

        // Method necesary for design mode.
        private static void ConfigureContentRoot(IHostBuilder builder)
        {
            builder.UseContentRoot(NinjaTrader.Core.Globals.UserDataDir);
        }
    }
}
