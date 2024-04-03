namespace KrTrade.Nt.Services.Series
{
    public interface ILongSeries : INumericSeries<long> { }
    public interface ILongSeries<TInput> : ILongSeries, INumericSeries<long, TInput> { }
    public interface ILongSeries<TInput, TEntry> : ILongSeries, INumericSeries<long, TInput, TEntry> { }
    public interface ILongSeries<TInput1, TInput2, TEntry> : ILongSeries, INumericSeries<long, TInput1, TInput2, TEntry> { }
    public interface ILongSeries<TInput1, TInput2, TEntry1, TEntry2> : ILongSeries, INumericSeries<long, TInput1, TInput2, TEntry1, TEntry2> { }
}
