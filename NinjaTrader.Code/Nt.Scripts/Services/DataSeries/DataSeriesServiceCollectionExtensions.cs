using Nt.Core.DependencyInjection;
using System;

namespace Nt.Scripts.Services
{
    public static class DataSeriesServiceCollectionExtensions
    {
        /// <summary>
        /// Adds <see cref="IDataSeries"/> service to the <see cref="IServiceCollection"/> container.
        /// </summary>
        /// <param name="services">The service container.</param>
        /// <returns>The <see cref="IServiceCollection"/> to continue adding services.</returns>
        /// <exception cref="ArgumentNullException">The <see cref="IServiceCollection"/> cannot be null.</exception>
        public static IServiceCollection AddDataSeries(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));
            
            services.AddSingleton<IDataSeries, DataSeries>();

            return services;
        }
    }
}
