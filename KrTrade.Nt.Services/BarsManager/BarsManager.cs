using KrTrade.Nt.Core.Bars;
using KrTrade.Nt.Core.Data;
using KrTrade.Nt.Core.Helpers;
using KrTrade.Nt.Core.Logging;
using KrTrade.Nt.Core.Series;
using KrTrade.Nt.Core.Services;
using System.Collections.Generic;

namespace KrTrade.Nt.Services
{
    public class BarsManager : BaseService<BarsManagerInfo, BarsManagerOptions>, IBarsManager
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

        public ICurrentBarSeries CurrentBar => _barsServiceCollection[0].CurrentBar;
        public ITimeSeries Time => _barsServiceCollection[0].Time;
        public IPriceSeries Open => _barsServiceCollection[0].Open;
        public IPriceSeries High => _barsServiceCollection[0].High;
        public IPriceSeries Low => _barsServiceCollection[0].Low;
        public IPriceSeries Close => _barsServiceCollection[0].Close;
        public IVolumeSeries Volume => _barsServiceCollection[0].Volume;
        public ITickSeries Tick => _barsServiceCollection[0].Tick;

        public ICurrentBarSeries[] CurrentBars => _barsServiceCollection.CurrentBars;
        public ITimeSeries[] Times => _barsServiceCollection.Times;
        public IPriceSeries[] Opens => _barsServiceCollection.Opens;
        public IPriceSeries[] Highs => _barsServiceCollection.Highs;
        public IPriceSeries[] Lows => _barsServiceCollection.Lows;
        public IPriceSeries[] Closes => _barsServiceCollection.Closes;
        public IVolumeSeries[] Volumes => _barsServiceCollection.Volumes;
        public ITickSeries[] Ticks => _barsServiceCollection.Ticks;

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

        internal BarsManager(NinjaTrader.NinjaScript.NinjaScriptBase ninjascript) : 
            this(
                ninjascript: ninjascript, 
                printService: null, 
                info: new BarsManagerInfo(ServiceType.BARS_MANAGER), 
                options: null) 
        { }
        internal BarsManager(NinjaTrader.NinjaScript.NinjaScriptBase ninjascript, IPrintService printService) : 
            this(
                ninjascript: ninjascript,
                printService: printService,
                info: new BarsManagerInfo(ServiceType.BARS_MANAGER),
                options: null) 
        { }
        internal BarsManager(NinjaTrader.NinjaScript.NinjaScriptBase ninjascript, IPrintService printService, BarsManagerInfo info) : 
            this(
                ninjascript: ninjascript, 
                printService: printService, 
                info: info,
                options: null
                ) 
        { }
        internal BarsManager(NinjaTrader.NinjaScript.NinjaScriptBase ninjascript, IPrintService printService, BarsManagerOptions options) : 
            this(
                ninjascript: ninjascript,
                printService: printService,
                info: new BarsManagerInfo(ServiceType.BARS_MANAGER),
                options: options
                ) 
        { }
        internal BarsManager(NinjaTrader.NinjaScript.NinjaScriptBase ninjascript, IPrintService printService, BarsManagerInfo info, BarsManagerOptions options) : 
            base(ninjascript, printService, info, options)
        {
            _barsServiceCollection = new BarsServiceCollection(this);
            Info = new List<BarsServiceInfo>();
        }

        #endregion

        #region Implementation

        protected bool IsConfigureAll => IsConfigure && IsDataLoaded;
        protected string GetKey()
        {
            string key = ServiceType.BARS_MANAGER.ToString();
            if (_barsServiceCollection != null && _barsServiceCollection.Count > 0)
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
        new public IList<BarsServiceInfo> Info { get; internal set; }

        protected override void Configure(out bool isConfigured)
        {
            _barsServiceCollection.Configure();
            isConfigured = _barsServiceCollection.IsConfigure;
        }
        protected override void DataLoaded(out bool isDataLoaded)
        {
            _barsServiceCollection.DataLoaded();
            isDataLoaded = _barsServiceCollection.IsDataLoaded;
        }
        public void OnBarUpdate()
        {
            if (_isRunning)
                BarUpdate();
            else
            {
                if (!IsConfigureAll)
                    NinjascriptThrowHelpers.ThrowIsNotConfigureException(Name);

                if (!IsInRunningStates())
                    NinjascriptThrowHelpers.ThrowOutOfRunningStatesException(Name);

                _isRunning = true;
                BarUpdate();
            }
        }
        public void OnMarketData(NinjaTrader.Data.MarketDataEventArgs args)
        {
            if (_isRunning)
                MarketData(args);
            else
            {
                if (!IsConfigureAll)
                    NinjascriptThrowHelpers.ThrowIsNotConfigureException(Name);

                if (!IsInRunningStates())
                    NinjascriptThrowHelpers.ThrowOutOfRunningStatesException(Name);

                _isRunning = true;
                MarketData(args);
            }
        }
        public void OnMarketDepth(NinjaTrader.Data.MarketDepthEventArgs args)
        {
            if (_isRunning)
                MarketDepth(args);
            else
            {
                if (!IsConfigureAll)
                    NinjascriptThrowHelpers.ThrowIsNotConfigureException(Name);

                if (!IsInRunningStates())
                    NinjascriptThrowHelpers.ThrowOutOfRunningStatesException(Name);

                _isRunning = true;
                MarketDepth(args);
            }
        }

        protected ServiceType GetServiceType() => ServiceType.BARS_MANAGER;
        public override string ToString() => Count > 0 ? _barsServiceCollection.ToString() : string.Empty;
        public Bar GetBar(int barsAgo) => _barsServiceCollection[0].GetBar(barsAgo);
        public Bar GetBar(int barsAgo, int period) => _barsServiceCollection[0].GetBar(barsAgo, period);
        public IList<Bar> GetBars(int barsAgo, int period) => _barsServiceCollection[0].GetBars(barsAgo, period);

        #endregion

        #region Public methods

        internal void Add(IBarsService service) 
        {
            _barsServiceCollection.Add(service);
            Info.Add((BarsServiceInfo)service.Info);
        }

        //public override string ToString(int tabOrder) => _barsServiceCollection[BarsInProgress].ToString();
        //protected override string ToHeader() => "BARS";
        //protected override string ToDescription() => _barsServiceCollection.ToString();

        protected override string GetHeaderString() => "BARS_MANAGER";
        protected override string GetParentString() => null;
        protected override string GetDescriptionString() => _barsServiceCollection.ToString();
        protected override string GetLogString(string state) 
            => ToLogString(
                tabOrder: 0, 
                label: GetLabelString(
                    isLabelVisible: true, 
                    isHeaderVisible: true, 
                    isParentVisible: false, 
                    isDescriptionVisible: true, 
                    isDescriptionBracketsVisible: false, 
                    isIndexVisible: false), 
                state: state);

        

        #endregion

        #region Virtual methods

        protected virtual void OnLastBarRemoved() { }
        protected virtual void OnBarClosed() { }
        protected virtual void OnPriceChanged() { }
        protected virtual void OnEachTick() { }

        #endregion

        #region Protected methods

        protected void BarUpdate()
        {
            if (!Options.IsEnable || !IsDataLoaded)
                return;

            // Check FILTERS
            _barsServiceCollection[BarsInProgress].BarUpdate();
        }
        protected void BarUpdate(IBarsService updatedBarsSeries)
        {
            if (!Options.IsEnable || !IsDataLoaded)
                return;

            _barsServiceCollection[BarsInProgress].BarUpdate(updatedBarsSeries);
        }
        protected void MarketData(NinjaTrader.Data.MarketDataEventArgs args)
        {
            if (!Options.IsEnable || !IsDataLoaded)
                return;

            _barsServiceCollection[BarsInProgress].MarketData(args);
        }
        protected void MarketData(IBarsService updatedBarsSeries)
        {
            if (!Options.IsEnable || !IsDataLoaded)
                return;

            _barsServiceCollection[BarsInProgress].MarketData(updatedBarsSeries);
        }
        protected void MarketDepth(NinjaTrader.Data.MarketDepthEventArgs args)
        {
            if (!Options.IsEnable || !IsDataLoaded)
                return;

            _barsServiceCollection[BarsInProgress].MarketDepth(args);
        }
        protected void MarketDepth(IBarsService updatedBarsSeries)
        {
            if (!Options.IsEnable || !IsDataLoaded)
                return;

            _barsServiceCollection[BarsInProgress].MarketDepth(updatedBarsSeries);
        }
        protected void Render()
        {
            _barsServiceCollection[BarsInProgress].Render();
        }
        protected void Render(IBarsService updatedBarsSeries)
        {
            _barsServiceCollection[BarsInProgress].Render(updatedBarsSeries);
        }

        protected override ServiceType ToElementType() => ServiceType.BARS_MANAGER;


        #endregion

    }
}
