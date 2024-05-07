using NinjaTrader.NinjaScript;

namespace KrTrade.Nt.Services.Series
{
    public interface IVolumeSeries : INumericSeries<ISeries<double>>, IBarsSeries
    {
    }
}
