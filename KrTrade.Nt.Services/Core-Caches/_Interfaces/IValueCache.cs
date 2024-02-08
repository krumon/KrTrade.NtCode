namespace KrTrade.Nt.Services
{
    public interface IValueCache<TElement,TInput> : INinjaCache<TElement,TInput>
        where TElement : struct
    {
    }
}
