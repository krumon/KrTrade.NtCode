using KrTrade.Nt.DI.DependencyInjection;
using KrTrade.Nt.Scripts.Ninjascripts;
using KrTrade.Nt.Scripts.Ninjascripts.Configuration;
using KrTrade.Nt.Scripts.Ninjascripts.Options;
using KrTrade.Nt.Scripts.NinjatraderObjects;
using System;

namespace KrTrade.Nt.Scripts.Ninjascripts.Indicators
{
    public static class SessionsNinjascriptBuilderExtensions
    {
        /// <summary>
        /// Adds a sessions ninjascript named 'SessionsIndicator' to the factory.
        /// </summary>
        /// <param name="builder">The <see cref="INinjascriptBuilder"/> to use.</param>
        /// <param name="ninjatraderObjects">The ninjatrader objects necesary to create the dependency services.</param>
        /// <returns>The <see cref="INinjascriptBuilder"/> so that additional calls can be chained.</returns>
        public static INinjascriptBuilder AddSessions(this INinjascriptBuilder builder, object[] ninjatraderObjects)
        {
            // If the configuration serives is not create..
            builder.AddConfiguration();
            // If the ninjatrader services is not create...
            builder.Services.AddNinjatraderObjects(ninjatraderObjects);
            // Create the sessions service.
            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<INinjascriptProvider, SessionsProvider>());
            NinjascriptProviderOptions.RegisterProviderOptions<SessionsOptions, SessionsProvider>(builder.Services);

            return builder;
        }

        /// <summary>
        /// Adds a sessions ninjascript named 'SessionsIndicator' to the factory.
        /// </summary>
        /// <param name="builder">The <see cref="INinjascriptBuilder"/> to use.</param>
        /// <param name="ninjatraderObjects">The ninjatrader objects necesary to create the dependency services.</param>
        /// <param name="configure">A delegate to configure the <see cref="Sessions"/>.</param>
        /// <returns>The <see cref="INinjascriptBuilder"/> so that additional calls can be chained.</returns>
        public static INinjascriptBuilder AddSessions(this INinjascriptBuilder builder, object[] ninjatraderObjects, Action<SessionsOptions> configure)
        {
            if (configure == null)
                throw new ArgumentNullException(nameof(configure));

            builder.AddSessions(ninjatraderObjects);
            builder.Services.Configure(configure);

            return builder;
        }

        /// <summary>
        /// Adds a sessions ninjascript named 'SessionsIndicator' to the factory.
        /// </summary>
        /// <param name="builder">The <see cref="INinjascriptBuilder"/> to use.</param>
        /// <param name="ninjatraderObjects">The ninjatrader objects necesary to create the dependency services.</param>
        /// <returns>The <see cref="INinjascriptBuilder"/> so that additional calls can be chained.</returns>
        public static INinjascriptBuilder AddDesignSessions(this INinjascriptBuilder builder)
        {
            // If the configuration serives is not create..
            builder.AddConfiguration();
            // If the ninjatrader services is not create...
            builder.Services.AddDesignNinjatraderObjects();
            // Create the sessions service.
            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<INinjascriptProvider, SessionsProvider>());
            NinjascriptProviderOptions.RegisterProviderOptions<SessionsOptions, SessionsProvider>(builder.Services);

            return builder;
        }

        /// <summary>
        /// Adds a sessions ninjascript named 'SessionsIndicator' to the factory.
        /// </summary>
        /// <param name="builder">The <see cref="INinjascriptBuilder"/> to use.</param>
        /// <param name="configure">A delegate to configure the <see cref="Sessions"/>.</param>
        /// <returns>The <see cref="INinjascriptBuilder"/> so that additional calls can be chained.</returns>
        public static INinjascriptBuilder AddDesignSessions(this INinjascriptBuilder builder, Action<SessionsOptions> configure)
        {
            if (configure == null)
                throw new ArgumentNullException(nameof(configure));

            builder.AddDesignSessions();
            builder.Services.Configure(configure);

            return builder;
        }


    }
}
