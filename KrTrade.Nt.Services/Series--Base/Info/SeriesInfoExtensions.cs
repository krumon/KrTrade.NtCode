using KrTrade.Nt.Core.Series;
using System;

namespace KrTrade.Nt.Services.Series
{

    public static class SeriesInfoExtensions
    {
        public static void AddInputSeries_Period(this ISeriesInfo info, Action<PeriodSeriesInfo> configureSeriesInfo)
            => info?.AddInputSeries(configureSeriesInfo);

        public static void AddInputSeries_Swing(this ISeriesInfo info, Action<SwingSeriesInfo> configureSeriesInfo)
            => info?.AddInputSeries(configureSeriesInfo);

    }
}
