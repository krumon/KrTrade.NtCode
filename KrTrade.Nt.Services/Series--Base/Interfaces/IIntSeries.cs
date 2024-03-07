namespace KrTrade.Nt.Services
{
    public interface IIntSeries<TInput> : IValueSeries<int,TInput>, IHasCalculatedValues<int>
    {
    }
}
