using KrTrade.NtCode.DependencyInjection;
using KrTrade.NtCode.Ninjascripts.Indicators;
using System;

namespace KrTrade.NtCode.NinjatraderObjects.Design
{
    public static class DesignServicesServiceCollectionExtensions
    {
        /// <summary>
        /// Adds ninjatrader objects passed by the builder.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="ninjatraderObjects">The ninjatrader objects.</param>
        /// <returns>The <see cref="IServiceCollection"/> to continue builder the host.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IServiceCollection AddDesignNinjatraderObjects(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services
                .AddDesignNinjascript()
                .AddDesignGlobalsData()
                .AddDesignChartBarsData();

            return services;
        }

        /// <summary>
        /// Adds ninjatrader objects passed by the builder.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="ninjatraderObjects">The ninjatrader objects.</param>
        /// <returns>The <see cref="IServiceCollection"/> to continue builder the host.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IServiceCollection TryAddDesignNinjatraderObjects(this IServiceCollection services, object[] ninjatraderObjects)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services
                .TryAddDesignNinjascript()
                .TryAddDesignGlobalsData()
                .TryAddDesignChartBarsData();

            return services;
        }

        public static IServiceCollection AddSessions(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            //services.AddSessionsIterator();
            //services.AddSessionsFilters();
            services.AddSingleton<ISessions, Sessions>();

            return services;
        }

    }
}
