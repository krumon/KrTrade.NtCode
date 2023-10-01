using KrTrade.Nt.DI.DependencyInjection;
using System;

namespace KrTrade.Nt.Scripts.MasterScripts
{
    /// <summary>
    /// Extension methods for setting up ninjascripts services in an <see cref="IServiceCollection" />.
    /// </summary>
    public static class MasterScriptServiceCollectionExtensions
    {

        /// <summary>
        /// Adds ninjascript services to the specified <see cref="IServiceCollection" />.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
        /// <param name="configure">The <see cref="INinjascriptBuilder"/> configuration delegate.</param>
        /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
        public static IServiceCollection AddMasterScripts(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.AddSingleton<MasterScriptFactory>();

            return services;
        }
    }
}
