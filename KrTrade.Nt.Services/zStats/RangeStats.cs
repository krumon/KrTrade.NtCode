﻿using NinjaTrader.NinjaScript;

namespace KrTrade.Nt.Services
{
    public class RangeStats : BaseBarStats
    {
        public RangeStats(NinjaScriptBase ninjascript) : base(ninjascript)
        {
        }

        public RangeStats(NinjaScriptBase ninjascript, int period) : base(ninjascript, period)
        {
        }

        public RangeStats(NinjaScriptBase ninjascript, int period, int displacement) : base(ninjascript, period, displacement)
        {
        }

        public RangeStats(NinjaScriptBase ninjascript, int period, int displacement, int barsIdx) : base(ninjascript, period, displacement, barsIdx)
        {
        }

        public override string Key => throw new System.NotImplementedException();

        public override void OnCalculate()
        {
            Avg = sum / Period;

        }

        public override string ToLogString()
        {
            throw new System.NotImplementedException();
        }

        public override double UpdateOnBarClosed(bool isPeriodFull)
        {
            if (isPeriodFull)
            {
                return Ninjascript.Highs[BarsIdx][0] - Ninjascript.Highs[BarsIdx][Period + Displacement];
            }
            else
            {
                return Ninjascript.Highs[BarsIdx][0];
            }
        }

        public override double UpdateOnEachTick()
        {
            throw new System.NotImplementedException();
        }
    }
}
