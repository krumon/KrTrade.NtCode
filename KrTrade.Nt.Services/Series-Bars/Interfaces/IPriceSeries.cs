using NinjaTrader.NinjaScript;

namespace KrTrade.Nt.Services.Series
{
    public interface IPriceSeries : INumericSeries<ISeries<double>>, IBarsSeries
    {
    }
}
