using KrTrade.Nt.Core.Infos;
using System;

namespace KrTrade.Nt.Core.Series
{

    public static class SeriesInfoExtensions
    {
        public static void AddInputSeries_Period(this IInputSeriesInfo info, Action<PeriodSeriesInfo> configureSeriesInfo)
            => info?.AddInputSeries(configureSeriesInfo);

        public static void AddInputSeries_Swing(this IInputSeriesInfo info, Action<SwingSeriesInfo> configureSeriesInfo)
            => info?.AddInputSeries(configureSeriesInfo);

    }
}
