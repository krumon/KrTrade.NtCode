using System;

namespace KrTrade.Nt.Core.Data
{

    /// <summary>
    /// Extensions methods of <see cref="ElementType"/> enum.
    /// </summary>
    public static class ElementTypesExtensions
    {

        public static string ToShortString(this ElementType type)
        {
            string[] subStrings = type.ToString().Split('_');
            string text = subStrings[0];
            if (subStrings.Length > 1)
                for (int i = 1; i < subStrings.Length - 1; i++)
                {
                    text += "_" + subStrings[i];
                }
            return text;
        }

        /// <summary>
        /// Converts from <typeparamref name="T"/> enum to <see cref="ElementType"/> enum.
        /// </summary>
        /// <param name="type">The specified enum to convert.</param>
        /// <returns>A <see cref="ServiceType"/> thats represents the specified <paramref name="type"/>.</returns>
        public static ElementType ToElementType<T>(this T type)
            where T : Enum
        {
            if (type is ElementType krTradeType)
                return krTradeType;

            if (type is SeriesType seriesType)
                return seriesType.ToElementType();

            if (type is BarsSeriesType barsSeriesType)
                return barsSeriesType.ToElementType();

            if (type is PeriodSeriesType periodSeriesType)
                return periodSeriesType.ToElementType();

            if (type is StrengthSeriesType swingSeriesType)
                return swingSeriesType.ToElementType();

            if (type is SeriesCollectionType seriesCollectionType)
                return seriesCollectionType.ToElementType();

            if (type is ServiceType serviceType)
                return serviceType.ToElementType();

            if (type is ServiceCollectionType serviceCollectionType)
                return serviceCollectionType.ToElementType();

            return ElementType.UNKNOWN;
        }

        public static ElementType ToElementType(this SeriesType type)
        {
            switch (type)
            {
                case SeriesType.CURRENT_BAR:
                    return ElementType.CURRENT_BAR_SERIES;
                case SeriesType.TIME:
                    return ElementType.TIME_SERIES;
                case SeriesType.INPUT:
                    return ElementType.INPUT_SERIES;
                case SeriesType.OPEN:
                    return ElementType.OPEN_SERIES;
                case SeriesType.HIGH:
                    return ElementType.HIGH_SERIES;
                case SeriesType.LOW:
                    return ElementType.LOW_SERIES;
                case SeriesType.CLOSE:
                    return ElementType.CLOSE_SERIES;
                case SeriesType.VOLUME:
                    return ElementType.VOLUME_SERIES;
                case SeriesType.TICK:
                    return ElementType.TICK_SERIES;
                case SeriesType.MEDIAN:
                    return ElementType.MEDIAN_SERIES;
                case SeriesType.TYPICAL:
                    return ElementType.TYPICAL_SERIES;
                case SeriesType.WEIGHTED:
                    return ElementType.WEIGHTED_SERIES;
                case SeriesType.AVG:
                    return ElementType.AVG_SERIES;
                case SeriesType.DEVSTD:
                    return ElementType.DEVSTD_SERIES;
                case SeriesType.MAX:
                    return ElementType.MAX_SERIES;
                case SeriesType.MIN:
                    return ElementType.MIN_SERIES;
                case SeriesType.SUM:
                    return ElementType.SUM_SERIES;
                case SeriesType.RANGE:
                    return ElementType.RANGE_SERIES;
                case SeriesType.SWING_HIGH:
                    return ElementType.SWING_HIGH_SERIES;
                case SeriesType.SWING_LOW:
                    return ElementType.SWING_LOW_SERIES;

                default:
                    return ElementType.UNKNOWN;
            }
        }
        public static ElementType ToElementType(this BarsSeriesType type)
        {
            switch (type)
            {
                case BarsSeriesType.CURRENT_BAR:
                    return ElementType.CURRENT_BAR_SERIES;
                case BarsSeriesType.TIME:
                    return ElementType.TIME_SERIES;
                case BarsSeriesType.INPUT:
                    return ElementType.INPUT_SERIES;
                case BarsSeriesType.OPEN:
                    return ElementType.OPEN_SERIES;
                case BarsSeriesType.HIGH:
                    return ElementType.HIGH_SERIES;
                case BarsSeriesType.LOW:
                    return ElementType.LOW_SERIES;
                case BarsSeriesType.CLOSE:
                    return ElementType.CLOSE_SERIES;
                case BarsSeriesType.VOLUME:
                    return ElementType.VOLUME_SERIES;
                case BarsSeriesType.TICK:
                    return ElementType.TICK_SERIES;
                default:
                    return ElementType.UNKNOWN;
            }
        }
        public static ElementType ToElementType(this PeriodSeriesType type)
        {
            switch (type)
            {
                case PeriodSeriesType.AVG:
                    return ElementType.AVG_SERIES;
                case PeriodSeriesType.DEVSTD:
                    return ElementType.DEVSTD_SERIES;
                case PeriodSeriesType.MAX:
                    return ElementType.MAX_SERIES;
                case PeriodSeriesType.MIN:
                    return ElementType.MIN_SERIES;
                case PeriodSeriesType.SUM:
                    return ElementType.SUM_SERIES;
                case PeriodSeriesType.RANGE:
                    return ElementType.RANGE_SERIES;
                default:
                    return ElementType.UNKNOWN;
            }
        }
        public static ElementType ToElementType(this StrengthSeriesType type)
        {
            switch (type)
            {
                case StrengthSeriesType.SWING_HIGH:
                    return ElementType.SWING_HIGH_SERIES;
                case StrengthSeriesType.SWING_LOW:
                    return ElementType.SWING_LOW_SERIES;
                default:
                    return ElementType.UNKNOWN;
            }
        }
        public static ElementType ToElementType(this SeriesCollectionType type)
        {
            switch (type)
            {
                case SeriesCollectionType.BARS:
                    return ElementType.BARS_SERIES_COLLECTION;
                case SeriesCollectionType.SERIES:
                    return ElementType.SERIES_COLLECTION;
                case SeriesCollectionType.STATS:
                    return ElementType.STATS_SERIES_COLLECTION;
                case SeriesCollectionType.INDICATORS:
                    return ElementType.INDICATORS_SERIES_COLLECTION;
                default:
                    return ElementType.UNKNOWN;
            }
        }
        public static ElementType ToElementType(this ServiceType type)
        {
            switch (type)
            {
                case ServiceType.BARS_MANAGER:
                    return ElementType.BARS_MANAGER_SERVICE;
                case ServiceType.BARS:
                    return ElementType.BARS_SERVICE;
                case ServiceType.PLOT:
                    return ElementType.PLOT_SERVICE;
                case ServiceType.PRINT:
                    return ElementType.PRINT_SERVICE;
                default:
                    return ElementType.UNKNOWN;
            }
        }
        public static ElementType ToElementType(this ServiceCollectionType type)
        {
            switch (type)
            {
                case ServiceCollectionType.BARS_COLLECTION:
                    return ElementType.BARS_SERVICE_COLLECTION;
                default:
                    return ElementType.UNKNOWN;
            }
        }

        public static SeriesType ToSeriesType(this ElementType type)
        {
            switch (type)
            {
                case ElementType.CURRENT_BAR_SERIES:
                    return SeriesType.CURRENT_BAR;
                case ElementType.TIME_SERIES:
                    return SeriesType.TIME;
                case ElementType.INPUT_SERIES:
                    return SeriesType.INPUT;
                case ElementType.OPEN_SERIES:
                    return SeriesType.OPEN;
                case ElementType.HIGH_SERIES:
                    return SeriesType.HIGH;
                case ElementType.LOW_SERIES:
                    return SeriesType.LOW;
                case ElementType.CLOSE_SERIES:
                    return SeriesType.CLOSE;
                case ElementType.VOLUME_SERIES:
                    return SeriesType.VOLUME;
                case ElementType.TICK_SERIES:
                    return SeriesType.TICK;
                case ElementType.MEDIAN_SERIES:
                    return SeriesType.MEDIAN;
                case ElementType.TYPICAL_SERIES:
                    return SeriesType.TYPICAL;
                case ElementType.WEIGHTED_SERIES:
                    return SeriesType.WEIGHTED;
                case ElementType.AVG_SERIES:
                    return SeriesType.AVG;
                case ElementType.DEVSTD_SERIES:
                    return SeriesType.DEVSTD;
                case ElementType.MAX_SERIES:
                    return SeriesType.MAX;
                case ElementType.MIN_SERIES:
                    return SeriesType.MIN;
                case ElementType.SUM_SERIES:
                    return SeriesType.SUM;
                case ElementType.RANGE_SERIES:
                    return SeriesType.RANGE;
                case ElementType.SWING_HIGH_SERIES:
                    return SeriesType.SWING_HIGH;
                case ElementType.SWING_LOW_SERIES:
                    return SeriesType.SWING_LOW;

                default:
                    throw new NotImplementedException();
            }
        }
        public static BarsSeriesType ToBarsSeriesType(this ElementType type)
        {
            switch (type)
            {
                case ElementType.CURRENT_BAR_SERIES:
                    return BarsSeriesType.CURRENT_BAR;
                case ElementType.TIME_SERIES:
                    return BarsSeriesType.TIME;
                case ElementType.INPUT_SERIES:
                    return BarsSeriesType.INPUT;
                case ElementType.OPEN_SERIES:
                    return BarsSeriesType.OPEN;
                case ElementType.HIGH_SERIES:
                    return BarsSeriesType.HIGH;
                case ElementType.LOW_SERIES:
                    return BarsSeriesType.LOW;
                case ElementType.CLOSE_SERIES:
                    return BarsSeriesType.CLOSE;
                case ElementType.VOLUME_SERIES:
                    return BarsSeriesType.VOLUME;
                case ElementType.TICK_SERIES:
                    return BarsSeriesType.TICK;
                default:
                    throw new NotImplementedException();
            }
        }
        public static PeriodSeriesType ToPeriodSeriesType(this ElementType type)
        {
            switch (type)
            {
                case ElementType.AVG_SERIES:
                    return PeriodSeriesType.AVG;
                case ElementType.DEVSTD_SERIES:
                    return PeriodSeriesType.DEVSTD;
                case ElementType.MAX_SERIES:
                    return PeriodSeriesType.MAX;
                case ElementType.MIN_SERIES:
                    return PeriodSeriesType.MIN;
                case ElementType.SUM_SERIES:
                    return PeriodSeriesType.SUM;
                case ElementType.RANGE_SERIES:
                    return PeriodSeriesType.RANGE;
                default:
                    throw new NotImplementedException();
            }
        }
        public static StrengthSeriesType ToStrengthSeriesType(this ElementType type)
        {
            switch (type)
            {
                case ElementType.SWING_HIGH_SERIES:
                    return StrengthSeriesType.SWING_HIGH;
                case ElementType.SWING_LOW_SERIES:
                    return StrengthSeriesType.SWING_LOW;
                default:
                    throw new NotImplementedException();
            }
        }
        public static SeriesCollectionType ToSeriesCollectionType(this ElementType type)
        {
            switch (type)
            {
                case ElementType.BARS_SERIES_COLLECTION:
                    return SeriesCollectionType.BARS;
                case ElementType.STATS_SERIES_COLLECTION:
                    return SeriesCollectionType.STATS;
                case ElementType.INDICATORS_SERIES_COLLECTION:
                    return SeriesCollectionType.INDICATORS;
                default:
                    throw new NotImplementedException();
            }
        }
        public static ServiceType ToServiceType(this ElementType type)
        {
            switch (type)
            {
                case ElementType.BARS_MANAGER_SERVICE:
                    return ServiceType.BARS_MANAGER;
                case ElementType.BARS_SERVICE:
                    return ServiceType.BARS;
                case ElementType.PLOT_SERVICE:
                    return ServiceType.PLOT;
                case ElementType.PRINT_SERVICE:
                    return ServiceType.PRINT;
                default:
                    throw new NotImplementedException();
            }
        }
        public static ServiceCollectionType ToServiceCollectionType(this ElementType type)
        {
            switch (type)
            {
                case ElementType.BARS_SERVICE_COLLECTION:
                    return ServiceCollectionType.BARS_COLLECTION;
                default:
                    throw new NotImplementedException();
            }
        }

    }
}
