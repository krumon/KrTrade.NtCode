using NinjaTrader.Core;
using NinjaTrader.Gui.Chart;
using KrTrade.Nt.DI.DependencyInjection;
using System;

namespace KrTrade.Nt.Scripts.NinjatraderObjects
{
    public static class ServicesServiceCollectionExtensions
    {
        /// <summary>
        /// Adds ninjatrader objects passed by the builder.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="ninjatraderObjects">The ninjatrader objects.</param>
        /// <returns>The <see cref="IServiceCollection"/> to continue builder the host.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IServiceCollection AddNinjatraderObjects(this IServiceCollection services, object[] ninjatraderObjects)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            if (ninjatraderObjects == null || ninjatraderObjects.Length == 0)
                return services;

            services.AddGlobalsData(TryGetNinjatraderObject<Globals>(ninjatraderObjects));
            services.AddNinjaScriptBase(TryGetNinjatraderObject<NinjaTrader.NinjaScript.NinjaScriptBase>(ninjatraderObjects));
            services.AddChartBarsProperties(TryGetNinjatraderObject<ChartBars>(ninjatraderObjects));

            return services;
        }

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

            services.AddDesignGlobalsData();
            services.AddDesignNinjaScriptBase();
            services.AddDesignChartBarsProperties();

            return services;
        }

        private static T TryGetNinjatraderObject<T>(object[] ninjatraderObjects)
            where T : class
        {
            T searchObject = null;
            try
            {
                if (ninjatraderObjects == null)
                    // TODO: Changes for return.
                    throw new ArgumentNullException(nameof(ninjatraderObjects));

                if (ninjatraderObjects.Length == 0)
                    // TODO: Changes for return.
                    throw new ArgumentException("The ninjascript objects array cannot be empty.");

                foreach (var o in ninjatraderObjects)
                    if (o is T t)
                    {
                        searchObject = t;
                        break;
                    }
            }
            catch
            {
            }

            return searchObject;
        }
    }
}
