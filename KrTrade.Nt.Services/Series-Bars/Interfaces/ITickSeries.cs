using NinjaTrader.Data;

namespace KrTrade.Nt.Services.Series
{
    public interface ITickSeries : INumericSeries<Bars>, IBarsSeries
    {
    }
}
