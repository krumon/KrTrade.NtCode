using KrTrade.Nt.Core;
using System;

namespace KrTrade.Nt.Services.Series
{
    public interface IValueSeries<T> : INinjascriptSeries, ISeries<T> where T : struct {  }

    public interface INumericSeries : IValueSeries<double>, IHasNumericCalculateValues<double> { }
    public interface INumericSeries<TInput> : INumericSeries 
    {
        /// <summary>
        /// Gets the input series to calculate the series values.
        /// </summary>
        TInput Input { get; }
    }
    public interface INumericSeries<TInput1, TInput2> : INumericSeries 
    {
        /// <summary>
        /// Gets the input series to calculate the series values.
        /// </summary>
        TInput1 Input1 { get; }

        /// <summary>
        /// Gets the input series to calculate the series values.
        /// </summary>
        TInput2 Input2 { get; }
    }

    public interface IDateTimeSeries : IValueSeries<DateTime>, IHasDateTimeCalculateValues { }
    public interface IDateTimeSeries<TInput> : IDateTimeSeries
    {
        /// <summary>
        /// Gets the input series to calculate the series values.
        /// </summary>
        TInput Input { get; }
    }
    public interface IDateTimeSeries<TInput1, TInput2> : IDateTimeSeries
    {
        /// <summary>
        /// Gets the input series to calculate the series values.
        /// </summary>
        TInput1 Input1 { get; }

        /// <summary>
        /// Gets the input series to calculate the series values.
        /// </summary>
        TInput2 Input2 { get; }
    }

    public interface IIntSeries : IValueSeries<int>, IHasNumericCalculateValues<int> { }
    public interface IIntSeries<TInput> : IIntSeries
    {
        /// <summary>
        /// Gets the input series to calculate the series values.
        /// </summary>
        TInput Input { get; }
    }
    public interface IIntSeries<TInput1, TInput2> : IIntSeries
    {
        /// <summary>
        /// Gets the input series to calculate the series values.
        /// </summary>
        TInput1 Input1 { get; }

        /// <summary>
        /// Gets the input series to calculate the series values.
        /// </summary>
        TInput2 Input2 { get; }
    }

    public interface ILongSeries : IValueSeries<long>, IHasNumericCalculateValues<long> { }
    public interface ILongSeries<TInput> : ILongSeries
    {
        /// <summary>
        /// Gets the input series to calculate the series values.
        /// </summary>
        TInput Input { get; }
    }
    public interface ILongSeries<TInput1, TInput2> : ILongSeries
    {
        /// <summary>
        /// Gets the input series to calculate the series values.
        /// </summary>
        TInput1 Input1 { get; }

        /// <summary>
        /// Gets the input series to calculate the series values.
        /// </summary>
        TInput2 Input2 { get; }
    }

}
