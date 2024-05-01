using KrTrade.Nt.Core.Bars;
using KrTrade.Nt.Services.Series;
using System.Collections.Generic;

namespace KrTrade.Nt.Services
{
    public class BarsManager : BaseNinjascriptService<BarsManagerInfo, BarsManagerOptions>, IBarsManager
    {
        #region Private members

        private bool _isRunning;
        private readonly BarsServiceCollection _barsServiceCollection;

        //private IList<IIndicatorService> _indicators;
        //private IList<IStatisticsService> _stats;
        //private IList<IFilterService> _filters;

        #endregion

        #region Public properties

        public int DefaultCachesCapacity { get; protected set; }
        public int DefaultRemovedCachesCapacity { get; protected set; }

        public CurrentBarSeries CurrentBar => _barsServiceCollection[0].CurrentBar;
        public TimeSeries Time => _barsServiceCollection[0].Time;
        public PriceSeries Open => _barsServiceCollection[0].Open;
        public PriceSeries High => _barsServiceCollection[0].High;
        public PriceSeries Low => _barsServiceCollection[0].Low;
        public PriceSeries Close => _barsServiceCollection[0].Close;
        public VolumeSeries Volume => _barsServiceCollection[0].Volume;
        public TickSeries Tick => _barsServiceCollection[0].Tick;

        public CurrentBarSeries[] CurrentBars { get; protected set; }
        public TimeSeries[] Times { get; protected set; }
        public PriceSeries[] Opens { get; protected set; }
        public PriceSeries[] Highs { get; protected set; }
        public PriceSeries[] Lows { get; protected set; }
        public PriceSeries[] Closes { get; protected set; }
        public VolumeSeries[] Volumes { get; protected set; }
        public TickSeries[] Ticks { get; protected set; }

        public int Count => _barsServiceCollection == null ? -1 : _barsServiceCollection.Count;
        public IBarsService this[string name] => _barsServiceCollection[name];
        public IBarsService this[int idx] => _barsServiceCollection[idx];

        public bool IsUpdated => _barsServiceCollection[0].IsUpdated;
        public bool IsClosed => _barsServiceCollection[0].IsClosed;
        public bool LastBarIsRemoved => _barsServiceCollection[0].LastBarIsRemoved;
        public bool IsTick => _barsServiceCollection[0].IsTick;
        public bool IsFirstTick => _barsServiceCollection[0].IsFirstTick;
        public bool IsPriceChanged => _barsServiceCollection[0].IsPriceChanged;

        #endregion

        #region Constructors

        public BarsManager(NinjaTrader.NinjaScript.NinjaScriptBase ninjascript) : this(ninjascript, null, null, null) { }
        public BarsManager(NinjaTrader.NinjaScript.NinjaScriptBase ninjascript, IPrintService printService) : this(ninjascript, printService,null,null) { }
        public BarsManager(NinjaTrader.NinjaScript.NinjaScriptBase ninjascript, IPrintService printService, BarsManagerInfo info) : this(ninjascript, printService, info,null) { }
        public BarsManager(NinjaTrader.NinjaScript.NinjaScriptBase ninjascript, IPrintService printService, BarsManagerOptions options) : this(ninjascript, printService,null, options) { }
        public BarsManager(NinjaTrader.NinjaScript.NinjaScriptBase ninjascript, IPrintService printService, BarsManagerInfo info, BarsManagerOptions options) : base(ninjascript, printService, info, options)
        {
            _barsServiceCollection = new BarsServiceCollection(this);
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
            if (_barsServiceCollection.Count > 0)
            {
                key += "(";
                for (int i = 0; i < _barsServiceCollection.Count; i++)
                {
                    key += _barsServiceCollection[i].Key;
                    if (i == _barsServiceCollection.Count - 1)
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
            _barsServiceCollection.Configure();
            isConfigured = _barsServiceCollection.IsConfigure;
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
                if (Info[i] == info)
                    (_barsServiceCollection[i] as BarsService).Index = i;
                else
                {
                    isDataLoaded = false;
                    PrintService.LogInformation($"The ninjascript key: {info.Key} don't match with configure data series key: {Info[i].Key}.");
                }
            }

            if (!isDataLoaded)
            {
                PrintService.LogError($"The {Name} cannot be configured.");
                return;
            }

            _barsServiceCollection.DataLoaded();
            isDataLoaded = isDataLoaded && _barsServiceCollection.IsDataLoaded;

            if (!isDataLoaded)
            {
                PrintService.LogError($"The {Name} cannot be configured.");
                return;
            }

            CurrentBars = new CurrentBarSeries[_barsServiceCollection.Count];
            Times = new TimeSeries[_barsServiceCollection.Count];
            Opens = new PriceSeries[_barsServiceCollection.Count];
            Highs = new HighSeries[_barsServiceCollection.Count];
            Lows = new PriceSeries[_barsServiceCollection.Count];
            Closes = new PriceSeries[_barsServiceCollection.Count];
            Volumes = new VolumeSeries[_barsServiceCollection.Count];
            Ticks = new TickSeries[_barsServiceCollection.Count];

            for (int i=0; i< _barsServiceCollection.Count; i++)
            {
                CurrentBars[i] = _barsServiceCollection[i].CurrentBar;
                Times[i] = _barsServiceCollection[i].Time;
                Opens[i] = _barsServiceCollection[i].Open;
                Highs[i] = _barsServiceCollection[i].High;
                Lows[i] = _barsServiceCollection[i].Low;
                Closes[i] = _barsServiceCollection[i].Close;
                Volumes[i] = _barsServiceCollection[i].Volume;
                Ticks[i] = _barsServiceCollection[i].Tick;
            }
        }
        public void OnBarUpdate()
        {
            if (_isRunning)
                BarUpdate();
            else
            {
                if (!IsConfigureAll)
                    LoggingHelpers.ThrowIsNotConfigureException(Name);

                if (!IsInRunningStates())
                    LoggingHelpers.ThrowOutOfRunningStatesException(Name);

                _isRunning = true;
                BarUpdate();
            }
        }
        public override string ToLogString() => Count > 0 ? _barsServiceCollection[BarsInProgress].ToLogString() : string.Empty;
        public Bar GetBar(int barsAgo) => _barsServiceCollection[0].GetBar(barsAgo);
        public Bar GetBar(int barsAgo, int period) => _barsServiceCollection[0].GetBar(barsAgo, period);
        public IList<Bar> GetBars(int barsAgo, int period) => _barsServiceCollection[0].GetBars(barsAgo, period);

        #endregion

        #region Public methods

        internal void Add(IBarsService service) 
        {
            _barsServiceCollection.Add(service);
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

        public void BarUpdate()
        {
            if (!Options.IsEnable || !IsDataLoaded)
                return;

            // Check FILTERS
            _barsServiceCollection[BarsInProgress].BarUpdate();
        }
        public void BarUpdate(IBarsService updatedBarsSeries)
        {
            if (!Options.IsEnable || !IsDataLoaded)
                return;

            _barsServiceCollection[BarsInProgress].BarUpdate(updatedBarsSeries);
        }
        public void MarketData(NinjaTrader.Data.MarketDataEventArgs args)
        {
            if (!Options.IsEnable || !IsDataLoaded)
                return;

            _barsServiceCollection[BarsInProgress].MarketData(args);
        }
        public void MarketData(IBarsService updatedBarsSeries)
        {
            if (!Options.IsEnable || !IsDataLoaded)
                return;

            _barsServiceCollection[BarsInProgress].MarketData(updatedBarsSeries);
        }
        public void MarketDepth(NinjaTrader.Data.MarketDepthEventArgs args)
        {
            if (!Options.IsEnable || !IsDataLoaded)
                return;

            _barsServiceCollection[BarsInProgress].MarketDepth(args);
        }
        public void MarketDepth(IBarsService updatedBarsSeries)
        {
            if (!Options.IsEnable || !IsDataLoaded)
                return;

            _barsServiceCollection[BarsInProgress].MarketDepth(updatedBarsSeries);
        }
        public void Render()
        {
            _barsServiceCollection[BarsInProgress].Render();
        }
        public void Render(IBarsService updatedBarsSeries)
        {
            _barsServiceCollection[BarsInProgress].Render(updatedBarsSeries);
        }

        #endregion

    }
}
