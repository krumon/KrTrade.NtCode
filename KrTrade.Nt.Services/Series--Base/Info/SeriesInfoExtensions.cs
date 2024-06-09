using KrTrade.Nt.Core.Elements;
using System;

namespace KrTrade.Nt.Services.Series
{

    public static class SeriesInfoExtensions
    {
        public static void AddInputSeries_Period(this IInputSeriesInfo info, Action<PeriodSeriesInfo> configureSeriesInfo)
            => info?.AddInputSeries(configureSeriesInfo);

        public static void AddInputSeries_Swing(this IInputSeriesInfo info, Action<SwingSeriesInfo> configureSeriesInfo)
            => info?.AddInputSeries(configureSeriesInfo);

    }
}
