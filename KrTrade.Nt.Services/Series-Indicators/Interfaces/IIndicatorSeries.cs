namespace KrTrade.Nt.Services
{
    public interface IIndicatorSeries : IDoubleSeries<INumericSeries<double>>
    {
    }
    public interface IIndicatorSeries<TInput1> : IIndicatorSeries
    {

        /// <summary>
        /// The <typeparamref name="TInput1"/> object necesary to get or calculate the cache values.
        /// </summary>
        TInput1 Input1 { get; }

    }

    public interface IIndicatorSeries<TInput1,TInput2> : IIndicatorSeries<TInput1>
    {

        /// <summary>
        /// The <typeparamref name="TInput1"/> object necesary to get or calculate the cache values.
        /// </summary>
        TInput2 Input2 { get; }

    }
}
