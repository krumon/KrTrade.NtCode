namespace KrTrade.Nt.Services
{
    public interface ILongSeries<TInput> : IValueSeries<long,TInput>, IHasCalculatedValues<long>
    {
    }
}
