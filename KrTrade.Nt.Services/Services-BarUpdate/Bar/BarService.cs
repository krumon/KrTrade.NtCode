﻿using KrTrade.Nt.Core.Bars;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Represents the service of only one bar.
    /// </summary>
    public class BarService : BarUpdateService<BarOptions>, IBarUpdateService
    {
        //public BarService(IBarsService barsService) : base(barsService)
        //{
        //}

        //public BarService(IBarsService barsService, IConfigureOptions<BarOptions> configureOptions) : base(barsService, configureOptions)
        //{
        //}

        //public BarService(IBarsService barsService, int period) : base(barsService, period)
        //{
        //}

        //public BarService(IBarsService barsService, int period, int displacement) : base(barsService, period, displacement)
        //{
        //}

        public BarService(IBarsService barsService, int period, int displacement, int lengthOfRemovedValues) : base(barsService, period, displacement, lengthOfRemovedValues)
        {
        }

        #region Private members

        private Bar _currentBar;

        #endregion

        #region Public properties

        Bar CurrentBar { get; set; }

        #endregion

        #region Implementation

        public override string Name => "Bar(" + Displacement + ")";
        internal override void Configure(out bool isConfigured)
        {
            _currentBar = new Bar();
            isConfigured = true;
        }
        internal override void DataLoaded(out bool isDataLoaded)
        {
            isDataLoaded = true;
        }

        public override void Update()
        {
            _currentBar = new Bar();
            
        }

        public override string ToLogString()
        {
            return $"{Name} Last:{CurrentBar.Close}.";
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Copy the bar values to other bar object.
        /// </summary>
        public void CopyTo(Bar bar)
        {
            bar.Idx = CurrentBar.Idx;
            bar.Open = CurrentBar.Open;
            bar.High = CurrentBar.High;
            bar.Low = CurrentBar.Low;
            bar.Close = CurrentBar.Close;
            bar.Volume = CurrentBar.Volume;
            bar.Time = CurrentBar.Time;
            bar.Ticks = CurrentBar.Ticks;
            // TODO: Copiar todas las propiedades
        }

        /// <summary>
        /// Copy the bar values to other bar object.
        /// </summary>
        public Bar GetCurrentBar()
        {
            return new Bar
            {
                Idx = CurrentBar.Idx,
                Open = CurrentBar.Open,
                High = CurrentBar.High,
                Low = CurrentBar.Low,
                Close = CurrentBar.Close,
                Volume = CurrentBar.Volume,
                Time = CurrentBar.Time,
                Ticks = CurrentBar.Ticks
                // TODO: Copiar todas las propiedades
            };
        }

        #endregion

        #region Protected methods

        protected virtual void OnLastBarRemoved()
        {
            UpdateBarClosedValues();
        }
        protected virtual void OnBarClosed()
        {
            UpdateBarClosedValues();
        }
        protected virtual void OnFirstTick()
        {

        }
        protected virtual void OnPriceChanged()
        {

        }
        protected virtual void OnEachTick()
        {
            UpdateTickValues();
        }

        #endregion

        #region Private methods


        protected void UpdateBarClosedValues()
        {
            CurrentBar.Idx = GetBarIdx(Bars.Index, Displacement);
            CurrentBar.Open = GetOpen(Bars.Index, Displacement);
            CurrentBar.High = GetHigh(Bars.Index, Displacement);
            CurrentBar.Low = GetLow(Bars.Index, Displacement);
            CurrentBar.Close = GetClose(Bars.Index, Displacement);
            CurrentBar.Volume = GetVolume(Bars.Index, Displacement);
            CurrentBar.Time = GetTime(Bars.Index, Displacement);
            CurrentBar.Ticks = Ninjascript.BarsArray[Bars.Index].TickCount;
        }

        protected void SetBarUpdateValues()
        {
            CurrentBar.Idx = Ninjascript.CurrentBars[Bars.Index] - Displacement;
            CurrentBar.Open = Ninjascript.Opens[Bars.Index][Displacement];
            CurrentBar.High = Ninjascript.Highs[Bars.Index][Displacement];
            CurrentBar.Low = Ninjascript.Lows[Bars.Index][Displacement];
            CurrentBar.Close = Ninjascript.Closes[Bars.Index][Displacement];
            CurrentBar.Volume = Ninjascript.Volumes[Bars.Index][Displacement];
            CurrentBar.Time = Ninjascript.Times[Bars.Index][Displacement];
            CurrentBar.Ticks = Ninjascript.BarsArray[Bars.Index].TickCount;
        }

        protected void UpdateTickValues()
        {
            CurrentBar.Ticks = Ninjascript.BarsArray[Bars.Index].TickCount;
        }



        #endregion

    }
}