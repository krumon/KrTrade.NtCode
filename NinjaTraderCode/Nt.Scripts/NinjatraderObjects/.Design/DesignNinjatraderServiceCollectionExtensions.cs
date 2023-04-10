using NinjaTrader.Gui.Chart;
using Nt.Core.DependencyInjection;
using System;

namespace Nt.Scripts.NinjatraderObjects.Design
{
    public static class DesignNinjatraderServiceCollectionExtensions
    {

        public static IServiceCollection AddDesignGlobalsData(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.TryAdd(ServiceDescriptor.Singleton<IGlobalsData, DesignGlobalsData>());

            return services;
        }
        public static IServiceCollection TryAddDesignGlobalsData(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));
            try
            {
                AddDesignGlobalsData(services);
            }
            catch { }

            return services;
        }
        public static IServiceCollection AddDesignNinjascript(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.TryAdd(ServiceDescriptor.Singleton<INinjaScriptBase>(new DesignNinjascriptBase()));

            return services;
        }
        public static IServiceCollection TryAddDesignNinjascript(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));
            try
            {
                AddDesignNinjascript(services);
            }
            catch 
            {
            }

            return services;
        }
        public static IServiceCollection AddDesignChartBarsData(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));
             
            services.TryAdd(ServiceDescriptor.Singleton<IChartBarsProperties>(new DesignChartBarsProperties()));

            return services;
        }
        public static IServiceCollection TryAddDesignChartBarsData(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));
            try
            {
                AddDesignChartBarsData(services);
            }
            catch 
            {
            }

            return services;
        }
    }
}
