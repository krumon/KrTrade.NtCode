namespace KrTrade.Nt.Services
{
    public interface IValueSeries<TElement,TInput> : INinjaSeries<TElement,TInput>
        where TElement : struct
    {
    }
}
