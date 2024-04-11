using KrTrade.Nt.Core.Bars;
using KrTrade.Nt.Core.Data;
using KrTrade.Nt.Services.Series;
using System;
using System.Collections.Generic;

namespace KrTrade.Nt.Services
{
    public class BarsManager : BaseNinjascriptService<BarsManagerOptions>, IBarsManager
    {
        #region Private members

        private bool _isRunning;
        private BarsServiceCollection _dataSeries;
        //private IList<IIndicatorService> _indicators;
        //private IList<IStatisticsService> _stats;
        //private IList<IFilterService> _filters;

        #endregion

        #region Public properties

        public int DefaultCachesCapacity { get; protected set; }
        public int DefaultRemovedCachesCapacity { get; protected set; }

        public CurrentBarSeries CurrentBar => _dataSeries[0].CurrentBar;
        public TimeSeries Time => _dataSeries[0].Time;
        public PriceSeries Open => _dataSeries[0].Open;
        public PriceSeries High => _dataSeries[0].High;
        public PriceSeries Low => _dataSeries[0].Low;
        public PriceSeries Close => _dataSeries[0].Close;
        public VolumeSeries Volume => _dataSeries[0].Volume;
        public TickSeries Tick => _dataSeries[0].Tick;

        public CurrentBarSeries[] CurrentBars { get; protected set; }
        public TimeSeries[] Times { get; protected set; }
        public PriceSeries[] Opens { get; protected set; }
        public PriceSeries[] Highs { get; protected set; }
        public PriceSeries[] Lows { get; protected set; }
        public PriceSeries[] Closes { get; protected set; }
        public VolumeSeries[] Volumes { get; protected set; }
        public TickSeries[] Ticks { get; protected set; }

        public int Count => _dataSeries == null ? -1 : _dataSeries.Count;
        public IBarsService this[string name] => _dataSeries[name];
        public IBarsService this[int idx] => _dataSeries[idx];

        public bool IsUpdated => _dataSeries[0].IsUpdated;
        public bool IsClosed => _dataSeries[0].IsClosed;
        public bool LastBarIsRemoved => _dataSeries[0].LastBarIsRemoved;
        public bool IsTick => _dataSeries[0].IsTick;
        public bool IsFirstTick => _dataSeries[0].IsFirstTick;
        public bool IsPriceChanged => _dataSeries[0].IsPriceChanged;

        #endregion

        #region Constructors

        internal BarsManager(NinjaTrader.NinjaScript.NinjaScriptBase ninjascript, IPrintService printService, Action<BarsManagerOptions> configureOptions) : this(ninjascript, printService, configureOptions, null) { }
        internal BarsManager(NinjaTrader.NinjaScript.NinjaScriptBase ninjascript, IPrintService printService, BarsManagerOptions options) : this(ninjascript, printService, null,options) { }
        protected BarsManager(NinjaTrader.NinjaScript.NinjaScriptBase ninjascript, IPrintService printService, Action<BarsManagerOptions> configureOptions, BarsManagerOptions options) : base(ninjascript, printService, configureOptions,options) 
        {
            _dataSeries = new BarsServiceCollection(this);
            Info = new List<DataSeriesInfo>();
        }

        #endregion

        #region Implementation

        public override string Name => "BarsManager";
        public override string GetKey()
        {
            string key = "Bars";
            if (_dataSeries.Count > 0)
            {
                key += "(";
                for (int i = 0; i < _dataSeries.Count; i++)
                {
                    key += _dataSeries[i].Key;
                    if (i == _dataSeries.Count - 1)
                        key += ")";
                    else
                        key += ",";
                }
            }

            return key;

        }
        public int BarsInProgress => Ninjascript.BarsInProgress;
        public IList<DataSeriesInfo> Info { get; internal set; }
        //public int Capacity => throw new NotImplementedException();
        //public int RemovedCacheCapacity => throw new NotImplementedException();

        internal override void Configure(out bool isConfigured)
        {
            _dataSeries.Configure();
            isConfigured = _dataSeries.IsConfigure;
        }
        internal override void DataLoaded(out bool isDataLoaded)
        {
            isDataLoaded = true;

            if (Count == 0 || Count != Ninjascript.BarsArray.Length)
                isDataLoaded = false;

            DataSeriesInfo info = new DataSeriesInfo();
            for (int i=0; i < Ninjascript.BarsArray.Length; i++)
                if (Info[0] == info.GetNinjascriptValues(Ninjascript, i))
                    (_dataSeries[i] as BarsService).Index = i;
                else
                    isDataLoaded = false;

            _dataSeries.DataLoaded();
            isDataLoaded = isDataLoaded && _dataSeries.IsDataLoaded;

            CurrentBars = new CurrentBarSeries[_dataSeries.Count];
            Times = new TimeSeries[_dataSeries.Count];
            Opens = new PriceSeries[_dataSeries.Count];
            Highs = new HighSeries[_dataSeries.Count];
            Lows = new PriceSeries[_dataSeries.Count];
            Closes = new PriceSeries[_dataSeries.Count];
            Volumes = new VolumeSeries[_dataSeries.Count];
            Ticks = new TickSeries[_dataSeries.Count];

            for (int i=0; i< _dataSeries.Count; i++)
            {
                CurrentBars[i] = _dataSeries[i].CurrentBar;
                Times[i] = _dataSeries[i].Time;
                Opens[i] = _dataSeries[i].Open;
                Highs[i] = _dataSeries[i].High;
                Lows[i] = _dataSeries[i].Low;
                Closes[i] = _dataSeries[i].Close;
                Volumes[i] = _dataSeries[i].Volume;
                Ticks[i] = _dataSeries[i].Tick;
            }
        }
        public void OnBarUpdate()
        {
            if (_isRunning)
                Update();
            else
            {
                if (!IsConfigureAll)
                    LoggingHelpers.ThrowIsNotConfigureException(Name);

                if (!IsInRunningStates())
                    LoggingHelpers.ThrowOutOfRunningStatesException(Name);

                _isRunning = true;
                Update();
            }
        }
        public override string ToLogString() => Count > 0 ? _dataSeries[BarsInProgress].ToLogString() : string.Empty;
        public Bar GetBar(int barsAgo) => _dataSeries[0].GetBar(barsAgo);
        public Bar GetBar(int barsAgo, int period) => _dataSeries[0].GetBar(barsAgo, period);
        public IList<Bar> GetBars(int barsAgo, int period) => _dataSeries[0].GetBars(barsAgo, period);

        #endregion

        #region Public methods

        internal void Add(IBarsService service) => _dataSeries.Add(service);
        internal void Add(string key, IBarsService service) => _dataSeries.Add(key, service);

        #endregion

        #region Virtual methods

        protected virtual void OnLastBarRemoved() { }
        protected virtual void OnBarClosed() { }
        protected virtual void OnPriceChanged() { }
        protected virtual void OnEachTick() { }

        #endregion

        #region Implementation methods

        public void Update()
        {
            if (!Options.IsEnable || Ninjascript.BarsInProgress != BarsInProgress)
                return;

            // Check FILTERS
            _dataSeries[BarsInProgress].Update();
        }
        public void Update(IBarsService updatedBarsSeries)
        {
            _dataSeries[BarsInProgress].Update(updatedBarsSeries);
        }
        public void MarketData()
        {
            _dataSeries[BarsInProgress].MarketData();
        }
        public void MarketData(IBarsService updatedBarsSeries)
        {
            _dataSeries[BarsInProgress].MarketData(updatedBarsSeries);
        }
        public void MarketDepth()
        {
            _dataSeries[BarsInProgress].MarketDepth();
        }
        public void MarketDepth(IBarsService updatedBarsSeries)
        {
            _dataSeries[BarsInProgress].MarketDepth(updatedBarsSeries);
        }
        public void Render()
        {
            _dataSeries[BarsInProgress].Render();
        }
        public void Render(IBarsService updatedBarsSeries)
        {
            _dataSeries[BarsInProgress].Render(updatedBarsSeries);
        }

        #endregion

    }
}
