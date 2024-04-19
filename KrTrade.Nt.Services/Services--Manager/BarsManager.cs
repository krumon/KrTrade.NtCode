using KrTrade.Nt.Core.Bars;
using KrTrade.Nt.Services.Series;
using System.Collections.Generic;

namespace KrTrade.Nt.Services
{
    public class BarsManager : BaseNinjascriptService<BarsManagerInfo, BarsManagerOptions>, IBarsManager
    {
        #region Private members

        private bool _isRunning;
        private readonly BarsServiceCollection _barsServices;

        //private IList<IIndicatorService> _indicators;
        //private IList<IStatisticsService> _stats;
        //private IList<IFilterService> _filters;

        #endregion

        #region Public properties

        public int DefaultCachesCapacity { get; protected set; }
        public int DefaultRemovedCachesCapacity { get; protected set; }

        public CurrentBarSeries CurrentBar => _barsServices[0].CurrentBar;
        public TimeSeries Time => _barsServices[0].Time;
        public PriceSeries Open => _barsServices[0].Open;
        public PriceSeries High => _barsServices[0].High;
        public PriceSeries Low => _barsServices[0].Low;
        public PriceSeries Close => _barsServices[0].Close;
        public VolumeSeries Volume => _barsServices[0].Volume;
        public TickSeries Tick => _barsServices[0].Tick;

        public CurrentBarSeries[] CurrentBars { get; protected set; }
        public TimeSeries[] Times { get; protected set; }
        public PriceSeries[] Opens { get; protected set; }
        public PriceSeries[] Highs { get; protected set; }
        public PriceSeries[] Lows { get; protected set; }
        public PriceSeries[] Closes { get; protected set; }
        public VolumeSeries[] Volumes { get; protected set; }
        public TickSeries[] Ticks { get; protected set; }

        public int Count => _barsServices == null ? -1 : _barsServices.Count;
        public IBarsService this[string name] => _barsServices[name];
        public IBarsService this[int idx] => _barsServices[idx];

        public bool IsUpdated => _barsServices[0].IsUpdated;
        public bool IsClosed => _barsServices[0].IsClosed;
        public bool LastBarIsRemoved => _barsServices[0].LastBarIsRemoved;
        public bool IsTick => _barsServices[0].IsTick;
        public bool IsFirstTick => _barsServices[0].IsFirstTick;
        public bool IsPriceChanged => _barsServices[0].IsPriceChanged;

        #endregion

        #region Constructors

        public BarsManager(NinjaTrader.NinjaScript.NinjaScriptBase ninjascript) : this(ninjascript, null, null, null) { }
        public BarsManager(NinjaTrader.NinjaScript.NinjaScriptBase ninjascript, IPrintService printService) : this(ninjascript, printService,null,null) { }
        public BarsManager(NinjaTrader.NinjaScript.NinjaScriptBase ninjascript, IPrintService printService, BarsManagerInfo info) : this(ninjascript, printService, info,null) { }
        public BarsManager(NinjaTrader.NinjaScript.NinjaScriptBase ninjascript, IPrintService printService, BarsManagerOptions options) : this(ninjascript, printService,null, options) { }
        public BarsManager(NinjaTrader.NinjaScript.NinjaScriptBase ninjascript, IPrintService printService, BarsManagerInfo info, BarsManagerOptions options) : base(ninjascript, printService, info, options)
        {
            _barsServices = new BarsServiceCollection();
            Info = new List<BarsServiceInfo>();
        }

        //internal BarsManager(NinjaTrader.NinjaScript.NinjaScriptBase ninjascript, IPrintService printService, Action<BarsManagerOptions> configureOptions) : this(ninjascript, printService, configureOptions, null) { }
        //internal BarsManager(NinjaTrader.NinjaScript.NinjaScriptBase ninjascript, IPrintService printService, BarsManagerOptions options) : this(ninjascript, printService, null,options) { }
        //protected BarsManager(NinjaTrader.NinjaScript.NinjaScriptBase ninjascript, IPrintService printService, Action<BarsManagerOptions> configureOptions, BarsManagerOptions options) : base(ninjascript, printService, configureOptions,options) 
        //{
        //    _dataSeries = new BarsServiceCollection(this);
        //    Info = new List<DataSeriesInfo>();
        //}

        #endregion

        #region Implementation

        //public override string Name => "BarsManager";
        protected override string GetKey()
        {
            string key = "Bars";
            if (_barsServices.Count > 0)
            {
                key += "(";
                for (int i = 0; i < _barsServices.Count; i++)
                {
                    key += _barsServices[i].Key;
                    if (i == _barsServices.Count - 1)
                        key += ")";
                    else
                        key += ",";
                }
            }

            return key;

        }
        public int BarsInProgress => Ninjascript.BarsInProgress;
        public new IList<BarsServiceInfo> Info { get; internal set; }
        //public int Capacity => throw new NotImplementedException();
        //public int RemovedCacheCapacity => throw new NotImplementedException();

        internal override void Configure(out bool isConfigured)
        {
            _barsServices.Configure();
            isConfigured = _barsServices.IsConfigure;
        }
        internal override void DataLoaded(out bool isDataLoaded)
        {
            isDataLoaded = true;

            if (Count == 0 || Count != Ninjascript.BarsArray.Length)
                isDataLoaded = false;

            BarsServiceInfo info = new BarsServiceInfo();
            for (int i=0; i < Ninjascript.BarsArray.Length; i++)
            {
                info.SetNinjascriptValues(Ninjascript,i);
                if (Info[0] == info)
                    (_barsServices[i] as BarsService).Index = i;
                else
                    isDataLoaded = false;
            }

            _barsServices.DataLoaded();
            isDataLoaded = isDataLoaded && _barsServices.IsDataLoaded;

            CurrentBars = new CurrentBarSeries[_barsServices.Count];
            Times = new TimeSeries[_barsServices.Count];
            Opens = new PriceSeries[_barsServices.Count];
            Highs = new HighSeries[_barsServices.Count];
            Lows = new PriceSeries[_barsServices.Count];
            Closes = new PriceSeries[_barsServices.Count];
            Volumes = new VolumeSeries[_barsServices.Count];
            Ticks = new TickSeries[_barsServices.Count];

            for (int i=0; i< _barsServices.Count; i++)
            {
                CurrentBars[i] = _barsServices[i].CurrentBar;
                Times[i] = _barsServices[i].Time;
                Opens[i] = _barsServices[i].Open;
                Highs[i] = _barsServices[i].High;
                Lows[i] = _barsServices[i].Low;
                Closes[i] = _barsServices[i].Close;
                Volumes[i] = _barsServices[i].Volume;
                Ticks[i] = _barsServices[i].Tick;
            }
        }
        public void OnBarUpdate()
        {
            if (_isRunning)
                Update(BarsInProgress);
            else
            {
                if (!IsConfigureAll)
                    LoggingHelpers.ThrowIsNotConfigureException(Name);

                if (!IsInRunningStates())
                    LoggingHelpers.ThrowOutOfRunningStatesException(Name);

                _isRunning = true;
                Update(BarsInProgress);
            }
        }
        public override string ToLogString() => Count > 0 ? _barsServices[BarsInProgress].ToLogString() : string.Empty;
        public Bar GetBar(int barsAgo) => _barsServices[0].GetBar(barsAgo);
        public Bar GetBar(int barsAgo, int period) => _barsServices[0].GetBar(barsAgo, period);
        public IList<Bar> GetBars(int barsAgo, int period) => _barsServices[0].GetBars(barsAgo, period);

        #endregion

        #region Public methods

        internal void Add(IBarsService service) 
        {
            _barsServices.Add(service);
            Info.Add(service.Info);
        }
        //internal void Add(string key, IBarsService service) => _barsServices.Add(key, service);

        #endregion

        #region Virtual methods

        protected virtual void OnLastBarRemoved() { }
        protected virtual void OnBarClosed() { }
        protected virtual void OnPriceChanged() { }
        protected virtual void OnEachTick() { }

        #endregion

        #region Implementation methods

        public void Update(int barsInProgress = 0)
        {
            if (!Options.IsEnable || BarsInProgress != barsInProgress)
                return;

            // Check FILTERS
            _barsServices[BarsInProgress].Update(BarsInProgress);
        }
        public void Update(IBarsService updatedBarsSeries, int barsInProgress = 0)
        {
            if (!Options.IsEnable || BarsInProgress != barsInProgress)
                return;

            _barsServices[BarsInProgress].Update(updatedBarsSeries,BarsInProgress);
        }
        public void MarketData(int barsInProgress = 0)
        {
            if (!Options.IsEnable || BarsInProgress != barsInProgress)
                return;

            _barsServices[BarsInProgress].MarketData(BarsInProgress);
        }
        public void MarketData(IBarsService updatedBarsSeries, int barsInProgress = 0)
        {
            if (!Options.IsEnable || BarsInProgress != barsInProgress)
                return;

            _barsServices[BarsInProgress].MarketData(updatedBarsSeries, BarsInProgress);
        }
        public void MarketDepth(int barsInProgress = 0)
        {
            if (!Options.IsEnable || BarsInProgress != barsInProgress)
                return;

            _barsServices[BarsInProgress].MarketDepth(BarsInProgress);
        }
        public void MarketDepth(IBarsService updatedBarsSeries, int barsInProgress = 0)
        {
            if (!Options.IsEnable || BarsInProgress != barsInProgress)
                return;

            _barsServices[BarsInProgress].MarketDepth(updatedBarsSeries, BarsInProgress);
        }
        public void Render(int barsInProgress = 0)
        {
            _barsServices[BarsInProgress].Render(BarsInProgress);
        }
        public void Render(IBarsService updatedBarsSeries, int barsInProgress = 0)
        {
            _barsServices[BarsInProgress].Render(updatedBarsSeries, BarsInProgress);
        }

        #endregion

    }
}
