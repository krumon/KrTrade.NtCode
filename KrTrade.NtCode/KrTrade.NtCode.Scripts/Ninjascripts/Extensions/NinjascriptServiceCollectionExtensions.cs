using KrTrade.NtCode.DependencyInjection;
using KrTrade.NtCode.Options;
using KrTrade.NtCode.Ninjascripts.Internal;
using System;

namespace KrTrade.NtCode.Ninjascripts
{
    /// <summary>
    /// Extension methods for setting up ninjascripts services in an <see cref="IServiceCollection" />.
    /// </summary>
    public static class NinjascriptServiceCollectionExtensions
    {

        /// <summary>
        /// Adds ninjascript services to the specified <see cref="IServiceCollection" />.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
        /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
        public static IServiceCollection AddNinjascript(this IServiceCollection services)
        {
            return AddNinjascript(services, builder => { });
        }

        /// <summary>
        /// Adds ninjascript services to the specified <see cref="IServiceCollection" />.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
        /// <param name="configure">The <see cref="INinjascriptBuilder"/> configuration delegate.</param>
        /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
        public static IServiceCollection AddNinjascript(this IServiceCollection services, Action<INinjascriptBuilder> configure)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.TryAdd(ServiceDescriptor.Singleton<INinjascriptFactory, NinjascriptFactory>());
            services.TryAdd(ServiceDescriptor.Singleton(typeof(INinjascript<>), typeof(Ninjascript<>)));

            services.TryAddEnumerable(ServiceDescriptor.Singleton<IConfigureOptions<NinjascriptFilterOptions>>(
                new DefaultNinjascriptLevelConfigureOptions(NinjascriptLevel.Active)));

            configure(new NinjascriptBuilder(services));

            return services;
        }

    }
}
