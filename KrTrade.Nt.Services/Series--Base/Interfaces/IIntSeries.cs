namespace KrTrade.Nt.Services.Series
{

    public interface IIntSeries : INumericSeries<int> { }
    public interface IIntSeries<TInput> : IIntSeries, INumericSeries<int, TInput> { }
    public interface IIntSeries<TInput, TEntry> : IIntSeries<TInput>, INumericSeries<int, TInput, TEntry> { }
    public interface IIntSeries<TInput1, TInput2, TEntry> : IIntSeries, INumericSeries<int, TInput1, TInput2, TEntry> { }
    public interface IIntSeries<TInput1, TInput2, TEntry1, TEntry2> : IIntSeries, INumericSeries<int, TInput1, TInput2, TEntry1, TEntry2> { }

}
