using NinjaTrader.Data;
using NinjaTrader.NinjaScript;

namespace KrTrade.Nt.Services
{
    public interface ITickSeries : IDoubleSeries<Bars,NinjaScriptBase>
    {
    }
}
