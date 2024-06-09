//using System;

//namespace KrTrade.Nt.Core.Series
//{

//    /// <summary>
//    /// Extensions methods of <see cref="SeriesType"/> object.
//    /// </summary>
//    public static class SeriesTypeExtensions
//    {
//        ///// <summary>
//        ///// Converts from <typeparamref name="T"/> enum to <see cref="SeriesType"/> enum.
//        ///// </summary>
//        ///// <param name="type">The specified enum to convert.</param>
//        ///// <returns>A <see cref="SeriesType"/> thats represents the specified <paramref name="type"/>.</returns>
//        //public static SeriesType ToSeriesType<T>(this T type)
//        //    where T : Enum
//        //{
//        //    if (type is SeriesType seriesType)
//        //        return seriesType;

//        //    if (type is BarsSeriesType barsSeriesType)
//        //        return barsSeriesType.ToSeriesType();

//        //    if (type is PeriodSeriesType periodSeriesType)
//        //        return periodSeriesType.ToSeriesType();

//        //    if (type is SwingSeriesType swingSeriesType)
//        //        return swingSeriesType.ToSeriesType();

//        //    return SeriesType.UNKNOWN;
//        //}

//        //public static SeriesType ToSeriesType(this BarsSeriesType type)
//        //{
//        //    switch (type)
//        //    {
//        //        case BarsSeriesType.CURRENT_BAR:
//        //            return SeriesType.CURRENT_BAR;
//        //        case BarsSeriesType.TIME:
//        //            return SeriesType.TIME;
//        //        case BarsSeriesType.INPUT:
//        //            return SeriesType.INPUT;
//        //        case BarsSeriesType.OPEN:
//        //            return SeriesType.OPEN;
//        //        case BarsSeriesType.HIGH:
//        //            return SeriesType.HIGH;
//        //        case BarsSeriesType.LOW:
//        //            return SeriesType.LOW;
//        //        case BarsSeriesType.CLOSE:
//        //            return SeriesType.CLOSE;
//        //        case BarsSeriesType.VOLUME:
//        //            return SeriesType.VOLUME;
//        //        case BarsSeriesType.TICK:
//        //            return SeriesType.TICK;
//        //        default:
//        //            return SeriesType.UNKNOWN;
//        //    }
//        //}
//        //public static BarsSeriesType ToBarsSeriesType(this SeriesType type)
//        //{
//        //    switch (type)
//        //    {
//        //        case SeriesType.CURRENT_BAR:
//        //            return BarsSeriesType.CURRENT_BAR;
//        //        case SeriesType.TIME:
//        //            return BarsSeriesType.TIME;
//        //        case SeriesType.INPUT:
//        //            return BarsSeriesType.INPUT;
//        //        case SeriesType.OPEN:
//        //            return BarsSeriesType.OPEN;
//        //        case SeriesType.HIGH:
//        //            return BarsSeriesType.HIGH;
//        //        case SeriesType.LOW:
//        //            return BarsSeriesType.LOW;
//        //        case SeriesType.CLOSE:
//        //            return BarsSeriesType.CLOSE;
//        //        case SeriesType.VOLUME:
//        //            return BarsSeriesType.VOLUME;
//        //        case SeriesType.TICK:
//        //            return BarsSeriesType.TICK;
//        //        default:
//        //            throw new NotImplementedException();
//        //    }
//        //}

//        //public static SeriesType ToSeriesType(this PeriodSeriesType type)
//        //{
//        //    switch (type)
//        //    {
//        //        case PeriodSeriesType.AVG:
//        //            return SeriesType.AVG;
//        //        case PeriodSeriesType.DEVSTD:
//        //            return SeriesType.DEVSTD;
//        //        case PeriodSeriesType.MAX:
//        //            return SeriesType.MAX;
//        //        case PeriodSeriesType.MIN:
//        //            return SeriesType.MIN;
//        //        case PeriodSeriesType.SUM:
//        //            return SeriesType.SUM;
//        //        case PeriodSeriesType.RANGE:
//        //            return SeriesType.RANGE;
//        //        default:
//        //            return SeriesType.UNKNOWN;
//        //    }
//        //}
//        //public static PeriodSeriesType ToPeriodSeriesType(this SeriesType type)
//        //{
//        //    switch (type)
//        //    {
//        //        case SeriesType.AVG:
//        //            return PeriodSeriesType.AVG;
//        //        case SeriesType.DEVSTD:
//        //            return PeriodSeriesType.DEVSTD;
//        //        case SeriesType.MAX:
//        //            return PeriodSeriesType.MAX;
//        //        case SeriesType.MIN:
//        //            return PeriodSeriesType.MIN;
//        //        case SeriesType.SUM:
//        //            return PeriodSeriesType.SUM;
//        //        case SeriesType.RANGE:
//        //            return PeriodSeriesType.RANGE;
//        //        default:
//        //            throw new NotImplementedException();
//        //    }
//        //}

//        //public static SeriesType ToSeriesType(this SwingSeriesType type)
//        //{
//        //    switch (type)
//        //    {
//        //        case SwingSeriesType.SWING_HIGH :
//        //            return SeriesType.SWING_HIGH;
//        //        case SwingSeriesType.SWING_LOW :
//        //            return SeriesType.SWING_LOW;
//        //        default:
//        //            return SeriesType.UNKNOWN;
//        //    }
//        //}
//        //public static SwingSeriesType ToSwingSeriesType(this SeriesType type)
//        //{
//        //    switch (type)
//        //    {
//        //        case SeriesType.SWING_HIGH :
//        //            return SwingSeriesType.SWING_HIGH;
//        //        case SeriesType.SWING_LOW :
//        //            return SwingSeriesType.SWING_LOW;
//        //        default:
//        //            throw new NotImplementedException();
//        //    }
//        //}



//    }
//}
