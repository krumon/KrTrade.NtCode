using KrTrade.Nt.Core.Elements;
using NinjaTrader.NinjaScript;
using System.Collections.Generic;
using Bar = KrTrade.Nt.Core.Bars.Bar;

namespace KrTrade.Nt.Services.Series
{
    public class BarSeries : DoubleSeries<NinjaTrader.NinjaScript.ISeries<double>,NinjaScriptBase>, IBarSeries
    {

        #region Public properties

        public CurrentBarSeries CurrentBar { get; set; }
        public TimeSeries Time { get; set; }
        public PriceSeries Open {  get; private set; }
        public PriceSeries High {  get; private set; }
        public PriceSeries Low {  get; private set; }
        public PriceSeries Close {  get; private set; }
        public VolumeSeries Volume {  get; private set; }
        public TickSeries Tick { get; protected set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Create <see cref="BarSeries"/> default instance with specified properties.
        /// </summary>
        /// <param name="input">The <see cref="IBarsManager"/> instance used to gets <see cref="NinjaScriptBase"/> object necesary for <see cref="BarsCache"/>.</param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        public BarSeries(IBarsService input) : this(input?.Ninjascript, input?.CacheCapacity ?? DEFAULT_CAPACITY, input?.RemovedCacheCapacity ?? DEFAULT_OLD_VALUES_CAPACITY, input?.Index ?? 0)
        {
        }

        //public BarSeries(int period, int capacity, int oldValuesCapacity) 
        //{
        //}

        /// <summary>
        /// Create <see cref="BarSeries"/> default instance with specified properties.
        /// </summary>
        /// <param name="entry">The <see cref="NinjaScriptBase"/> instance used to gets <see cref="BarSeries"/> series.</param>
        /// <param name="capacity">The <see cref="ICache{T}"/> capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
        /// <param name="oldValuesCapacity">The length of the old values cache. This values are at the end of cache.</param>
        /// <param name="barsIndex">The index of NinjaScript.Bars used to gets cache elements.</param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="entry"/> cannot be null.</exception>
        public BarSeries(NinjaScriptBase entry, int capacity, int oldValuesCapacity, int barsIndex) : base(entry, period: 1, capacity, oldValuesCapacity, barsIndex)
        {
            CurrentBar = new CurrentBarSeries(entry,capacity,oldValuesCapacity,barsIndex);
            Time = new TimeSeries(entry, capacity, oldValuesCapacity, barsIndex);
            Open = new HighSeries(entry, capacity, oldValuesCapacity, barsIndex);
            High = new HighSeries(entry, capacity, oldValuesCapacity, barsIndex);
            Low = new HighSeries(entry, capacity, oldValuesCapacity, barsIndex);
            Close = new HighSeries(entry, capacity, oldValuesCapacity, barsIndex);
            Volume = new VolumeSeries(entry, capacity, oldValuesCapacity, barsIndex);
            Tick = new TickSeries(entry, capacity, oldValuesCapacity, barsIndex);
        }

        #endregion

        #region Implementation

        public override string Name => "BarsSeries";
        public override string Key => $"{Name.ToUpper()}(Capacity:{Capacity})";

        //public override IElementInfo Info => throw new System.NotImplementedException();

        public override NinjaTrader.NinjaScript.ISeries<double> GetInput(NinjaScriptBase entry)
            => entry.Inputs[BarsIndex];

        protected override double GetCandidateValue(int barsAgo, bool isCandidateValueForUpdate)
            => Input[barsAgo];

        protected override bool CheckAddConditions(double lastValue, double candidateValue)
            => true;

        protected override bool CheckUpdateConditions(double currentValue, double candidateValue)
            => true;

        protected override void OnElementAdded(double addedElement)
        {
            CurrentBar.Add();
            Time.Add();
            Open.Add();
            High.Add();
            Low.Add();
            Close.Add();
            Volume.Add();
            Tick.Add(); 
        }
        protected override void OnElementUpdated(double oldValue, double newValue)
        {
            CurrentBar.Update();
            Time.Update();
            Open.Update();
            High.Update();
            Low.Update();
            Close.Update();
            Volume.Update();
            Tick.Update();
        }
        protected override void OnLastElementRemoved(double removedValue)
        {
            CurrentBar.RemoveLastElement();
            Time.RemoveLastElement();
            Open.RemoveLastElement();
            High.RemoveLastElement();
            Low.RemoveLastElement();
            Close.RemoveLastElement();
            Volume.RemoveLastElement();
            Tick.RemoveLastElement();
        }

        #endregion

        #region Public methods

        public Bar GetBar(int barsAgo)
        {
            if (!IsValidIndex(barsAgo))
                return null;

            Bar bar = new Bar() 
            { 
                Idx = CurrentBar[barsAgo],
                Open = Open[barsAgo],
                High = High[barsAgo],
                Low = Low[barsAgo],
                Close = Close[barsAgo],
                Volume = Volume[barsAgo],
                Ticks = (long)Tick[barsAgo],
             };
            return bar;
        }
        public Bar GetBar(int barsAgo, int period)
        {
            IsValidIndex(barsAgo, period);

            Bar bar = new Bar();
            for (int i = barsAgo + period - 1; i >= 0; i--)
                bar += new Bar()
                {
                    Idx = CurrentBar[i],
                    Open = Open[i],
                    High = High[i],
                    Low = Low[i],
                    Close = Close[i],
                    Volume = Volume[i],
                    Ticks = (long)Tick[i],
                };

            return bar;
        }
        public IList<Bar> GetBars(int barsAgo, int period)
        {
            if (!IsValidIndex(barsAgo, period))
                return null;
            IList<Bar> bars = new List<Bar>();
            for (int i = barsAgo + period - 1; i >= 0; i--)
                bars.Add(new Bar()
                {
                    Idx = CurrentBar[i],
                    Open = Open[i],
                    High = High[i],
                    Low = Low[i],
                    Close = Close[i],
                    Volume = Volume[i],
                    Ticks = (long)Tick[i],
                });
            return bars;
        }

        //public int GetIndex(int barsAgo) => IsValidIndex(barsAgo) ? CurrentBar[barsAgo] : default;
        //public DateTime GetTime(int barsAgo) => IsValidIndex(barsAgo) ? Time[barsAgo] : default;
        //public double GetOpen(int barsAgo) => IsValidIndex(barsAgo) ? Open[barsAgo] : default;
        //public double GetHigh(int barsAgo) => IsValidIndex(barsAgo) ? High[barsAgo] : default;
        //public double GetLow(int barsAgo) => IsValidIndex(barsAgo) ? Low[barsAgo] : default;
        //public double GetClose(int barsAgo) => IsValidIndex(barsAgo) ? Close[barsAgo] : default;
        //public double GetVolume(int barsAgo) => IsValidIndex(barsAgo) ? Volume[barsAgo] : default;
        //public long GetTicks(int barsAgo) => IsValidIndex(barsAgo) ? Tick[barsAgo] : default;

        //public double GetRange(int barsAgo) => IsValidIndex(barsAgo) ? High[barsAgo] - Low[barsAgo] : default;
        
        public override string ToString() => 
            $"{Name}[0]: Open:{Open[0]:#,0.00} - High:{High[0]:#,0.00} - Low:{Low[0]:#,0.00} - Close:{Close[0]:#,0.00} - Volume:{Volume[0]:#,0.##} - Ticks:{Tick[0]:#,0.##}";

        #endregion

    }
}
