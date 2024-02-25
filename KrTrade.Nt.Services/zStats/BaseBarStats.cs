using NinjaTrader.NinjaScript;
using System;

namespace KrTrade.Nt.Services
{
    public abstract class BaseBarStats : BaseStats
    {
        protected double sum = 0;

        public const int DefaultPeriod = 14;

        protected int BarsIdx { get; set; }
        public int Period { get; set; }
        public int Displacement { get; set; }

        //public double[] NormalizeValues {  get; set; }
        //public double[] NormalizeStdValues {  get; set; }

        public BaseBarStats(NinjaScriptBase ninjascript) : this(ninjascript, DefaultPeriod,0,0)
        {
        }

        public BaseBarStats(NinjaScriptBase ninjascript, int period) : this(ninjascript, period, 0,0)
        {
        }

        public BaseBarStats(NinjaScriptBase ninjascript, int period, int displacement) : this(ninjascript, period, displacement,0)
        {
        }

        public BaseBarStats(NinjaScriptBase ninjascript, int period, int displacement, int barsIdx) : base(ninjascript)
        {
            Period = period;
            Displacement = displacement;
            BarsIdx = barsIdx;
        }

        internal override void Configure(out bool isConfigured)
        {
            isConfigured = Period > 0 && Displacement > 0;
        }

        internal override void DataLoaded(out bool isDataLoaded)
        {
            isDataLoaded = BarsIdx < Ninjascript.BarsArray.Length;
        }

        public void Calculate(Func<double> value)
        {
            if (Ninjascript.BarsInProgress != BarsIdx)
                return;
            if (Ninjascript.CurrentBars[BarsIdx] < Displacement)
                return;

            if (Ninjascript.CurrentBars[BarsIdx] < Period + Displacement)
            {
                sum += UpdateOnBarClosed(isPeriodFull: false);
            }
            else
            {
                sum += UpdateOnBarClosed(isPeriodFull: true);
            }
            OnCalculate();
        }

        public void Update()
        {
            if (Ninjascript.BarsInProgress != BarsIdx)
                return;
            if (Ninjascript.CurrentBars[BarsIdx] < Displacement)
                return;
            UpdateOnEachTick();
            OnCalculate();
        }

        public abstract double UpdateOnBarClosed(bool isPeriodFull);
        public abstract double UpdateOnEachTick();
        public abstract void OnCalculate();
    }
}
