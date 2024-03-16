namespace KrTrade.Nt.Services
{
    public interface IValueSeries<TElement> : ISeries<TElement>
        where TElement : struct
    {
    }
}
