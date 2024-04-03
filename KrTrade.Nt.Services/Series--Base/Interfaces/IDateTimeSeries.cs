using System;

namespace KrTrade.Nt.Services.Series
{

    public interface IDateTimeSeries : IValueSeries<DateTime>, IHasDateTimeCalculateValues { }
    public interface IDateTimeSeries<TInput> : IDateTimeSeries
    {
        /// <summary>
        /// The <typeparamref name="TInput"/> object necesary to get or calculate the cache values.
        /// </summary>
        TInput Input { get; }
    }
    public interface IDateTimeSeries<TInput,TEntry> : IDateTimeSeries
    {
        /// <summary>
        /// The <typeparamref name="TInput"/> object necesary to get or calculate the cache values.
        /// </summary>
        TInput Input { get; }

        /// <summary>
        /// Gets the <typeparamref name="TInput"/> object, necesary to get or calculate the cache values.
        /// </summary>
        /// <param name="entry">The entry object necesary to gets the input series.</param>
        /// <returns>The input series.</returns>
        TInput GetInput(TEntry entry);
    }
    public interface IDateTimeSeries<TInput1, TInput2, TEntry> : IDateTimeSeries
    {
        /// <summary>
        /// The <typeparamref name="TInput1"/> object necesary to get or calculate the cache values.
        /// </summary>
        TInput1 Input1 { get; }

        /// <summary>
        /// The <typeparamref name="TInput2"/> object necesary to get or calculate the cache values.
        /// </summary>
        TInput2 Input2 { get; }

        /// <summary>
        /// Gets the <typeparamref name="TInput1"/> object, necesary to get or calculate the cache values.
        /// </summary>
        /// <param name="entry">The entry object necesary to gets the input series.</param>
        /// <returns>The input series.</returns>
        TInput1 GetInput1(TEntry entry);

        /// <summary>
        /// Gets the <typeparamref name="TInput2"/> object, necesary to get or calculate the cache values.
        /// </summary>
        /// <param name="entry">The entry object necesary to gets the input series.</param>
        /// <returns>The input series.</returns>
        TInput2 GetInput2(TEntry entry);

    }
    public interface IDateTimeSeries<TInput1, TInput2, TEntry1, TEntry2> : IDateTimeSeries
    {
        /// <summary>
        /// The <typeparamref name="TInput1"/> object necesary to get or calculate the cache values.
        /// </summary>
        TInput1 Input1 { get; }

        /// <summary>
        /// The <typeparamref name="TInput2"/> object necesary to get or calculate the cache values.
        /// </summary>
        TInput2 Input2 { get; }

        /// <summary>
        /// Gets the <typeparamref name="TInput1"/> object, necesary to get or calculate the cache values.
        /// </summary>
        /// <param name="entry1">The entry object necesary to gets the input series.</param>
        /// <returns>The input series.</returns>
        TInput1 GetInput1(TEntry1 entry1);

        /// <summary>
        /// Gets the <typeparamref name="TInput2"/> object, necesary to get or calculate the cache values.
        /// </summary>
        /// <param name="entry2">The entry object necesary to gets the input series.</param>
        /// <returns>The input series.</returns>
        TInput2 GetInput2(TEntry2 entry2);

    }

}
