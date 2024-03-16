namespace KrTrade.Nt.Services
{
    public interface IDoubleSeries : INumericSeries<double> { }
    public interface IDoubleSeries<TInput> : IDoubleSeries, INumericSeries<double, TInput> { }
    public interface IDoubleSeries<TInput,TEntry> : IDoubleSeries, INumericSeries<double,TInput,TEntry> { }
    public interface IDoubleSeries<TInput1,TInput2,TEntry> : IDoubleSeries, INumericSeries<double,TInput1,TInput2,TEntry> { }
    public interface IDoubleSeries<TInput1,TInput2,TEntry1,TEntry2> : IDoubleSeries, INumericSeries<double,TInput1,TInput2,TEntry1,TEntry2> { }
}
