namespace KrTrade.Nt.Services
{
    public interface INumericSeries<TElement> : IValueSeries<TElement>, IHasNumericCalculateValues<TElement>
        where TElement : struct
    {
    }

    public interface INumericSeries<TElement,TInput> : INumericSeries<TElement>
        where TElement : struct
    {
        /// <summary>
        /// The <typeparamref name="TInput"/> object necesary to get or calculate the cache values.
        /// </summary>
        TInput Input { get; }

        /// <summary>
        /// Gets the <typeparamref name="TInput"/> object, necesary to get or calculate the cache values.
        /// </summary>
        /// <returns>The instance of the input series.</returns>
        TInput GetInput(object input);
    }
}
