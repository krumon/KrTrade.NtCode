using NinjaTrader.NinjaScript;
using System;

namespace KrTrade.Nt.Services
{
    public interface ITimeSeries : IDateTimeSeries<ISeries<DateTime>>
    {
    }
}
