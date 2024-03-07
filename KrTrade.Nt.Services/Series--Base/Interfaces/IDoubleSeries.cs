namespace KrTrade.Nt.Services
{
    public interface IDoubleSeries<TInput> : IValueSeries<double,TInput>, IHasCalculatedValues<double>
    {
    }
}
