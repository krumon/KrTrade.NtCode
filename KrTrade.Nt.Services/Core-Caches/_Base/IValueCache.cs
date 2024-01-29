namespace KrTrade.Nt.Services
{
    public interface IValueCache<TElement,TInput> : ISeriesCache<TElement,TInput>
        where TElement : struct
    {
    }
}
