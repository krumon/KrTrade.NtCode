using NinjaTrader.Core;
using NinjaTrader.Gui.Chart;
using NinjaTrader.NinjaScript;
using NinjaTrader.NinjaScript.DrawingTools;
using KrTrade.Nt.DI.DependencyInjection;
using System;

namespace KrTrade.Nt.Scripts.NinjatraderObjects
{
    public static class NinjatraderObjectsServiceCollectionExtensions
    {

        public static IServiceCollection AddGlobalsData(this IServiceCollection services, Globals globals)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            if (globals != null)
                services.TryAdd(ServiceDescriptor.Singleton<IGlobalsData, GlobalsData>()); ;

            return services;
        }
        public static IServiceCollection AddNinjaScriptBase(this IServiceCollection services, NinjaTrader.NinjaScript.NinjaScriptBase ninjascript)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            if (ninjascript == null)
                throw new ArgumentNullException(nameof(ninjascript));

            // TODO: Delete this condition in future when develop the indicator, strategy,... services.
            if (ninjascript is NinjaTrader.NinjaScript.NinjaScriptBase)
                services.TryAdd(ServiceDescriptor.Singleton<INinjaScriptBase>(new NinjaScriptBase(ninjascript)));
            else if (ninjascript is IndicatorBase indicator)
                return services;
            else if (ninjascript is StrategyBase strategy)
                return services;
            else if (ninjascript is IDrawingTool drawingTool)
                return services;

            return services;
        }
        public static IServiceCollection AddChartBarsProperties(this IServiceCollection services, ChartBars chartBars)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));
            if (chartBars != null)
                services.TryAdd(ServiceDescriptor.Singleton<IChartBarsProperties>(new ChartBarsProperties(chartBars)));

            return services;
        }

        public static IServiceCollection AddDesignGlobalsData(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.TryAdd(ServiceDescriptor.Singleton<IGlobalsData>(new GlobalsData()));

            return services;
        }
        public static IServiceCollection AddDesignNinjaScriptBase(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.TryAdd(ServiceDescriptor.Singleton<INinjaScriptBase>(new NinjaScriptBase()));

            return services;
        }
        public static IServiceCollection AddDesignChartBarsProperties(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));
             
            services.TryAdd(ServiceDescriptor.Singleton<IChartBarsProperties>(new ChartBarsProperties()));

            return services;
        }

    }
}
