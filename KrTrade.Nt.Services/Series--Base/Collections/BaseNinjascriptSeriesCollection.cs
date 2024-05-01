using KrTrade.Nt.Core.Collections;
using KrTrade.Nt.Core.Series;
using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using System;

namespace KrTrade.Nt.Services.Series
{
    // TODO: IMPLEMETAR GET SERIES METHOD. LINE:145.

    public abstract class BaseNinjascriptSeriesCollection<TSeries> : BaseKeyCollection<TSeries>, INinjascriptSeriesCollection<TSeries>
        where TSeries : INinjascriptSeries
    {

        private readonly NinjaScriptBase _ninjascript;
        private readonly IPrintService _printService;
        protected SeriesCollectionOptions _options;

        public NinjaScriptBase Ninjascript => _ninjascript;
        public IPrintService PrintService => _printService;
        public SeriesCollectionOptions Options { get => _options ?? new SeriesCollectionOptions(); protected set { _options = value; } }

        public bool IsConfigure { get;protected set; }
        public bool IsDataLoaded { get; protected set; }

        public string Name { get; protected set; }
        public bool IsEnable => Options.IsEnable;
        public bool IsLogEnable => Options.IsLogEnable;


        protected BaseNinjascriptSeriesCollection(IBarsService barsService) : this(barsService.Ninjascript, barsService.PrintService, null, new SeriesCollectionOptions()) { }
        protected BaseNinjascriptSeriesCollection(IBarsService barsService, string name) : this(barsService.Ninjascript, barsService.PrintService, name, new SeriesCollectionOptions()) { }
        protected BaseNinjascriptSeriesCollection(IBarsService barsService, string name, SeriesCollectionOptions options) : this(barsService.Ninjascript, barsService.PrintService, name, options) { }
        protected BaseNinjascriptSeriesCollection(IBarsService barsService, string name, SeriesCollectionOptions options, int capacity) : this(barsService.Ninjascript, barsService.PrintService, name, options, capacity) { }
        
        protected BaseNinjascriptSeriesCollection(NinjaScriptBase ninjascript, IPrintService printService, string name, SeriesCollectionOptions options)
        {
            _ninjascript = ninjascript ?? throw new ArgumentNullException($"Error in 'BaseNinjascriptServiceCollection' constructor. The {nameof(ninjascript)} argument cannot be null.");
            Name = name;
            _options = options;
            _printService = printService;
        }
        protected BaseNinjascriptSeriesCollection(NinjaScriptBase ninjascript, IPrintService printService, string name, SeriesCollectionOptions options, int capacity) : base(capacity) 
        {
            _ninjascript = ninjascript ?? throw new ArgumentNullException($"Error in 'BaseNinjascriptServiceCollection' constructor. The {nameof(ninjascript)} argument cannot be null.");
            Name = string.IsNullOrEmpty(name) ? "SeriesCollection" : name;
            _options = options;
            _printService = printService;
        }

        #region Implementation

        public void Configure()
        {
            if (_collection == null)
                throw new NullReferenceException($"The service collection is null.");
            if (_collection.Count == 0)
                throw new Exception($"The service collection is empty.");

            IsConfigure = true;
            foreach (var series in _collection)
            {
                series.Configure();
                if (!series.IsConfigure)
                    IsConfigure = false;
            }
        }
        public void DataLoaded()
        {
            if (_collection == null)
                throw new NullReferenceException($"The service collection is null.");
            if (_collection.Count == 0)
                throw new Exception($"The service collection is empty.");

            IsDataLoaded = true;
            foreach (var series in _collection)
            {
                series.DataLoaded();
                if (!series.IsDataLoaded)
                    IsDataLoaded = false;
            }
        }
        public void Terminated()
        {
            if (_collection == null)
                return;
            if (_collection.Count == 0)
            {
                _collection = null; 
                return;
            }

            ForEach(x => x.Terminated());
            _collection.Clear();
            _collection = null;
        }
        public void Dispose() => Terminated();
        public void Reset()
        {
            if (_collection == null || _collection.Count == 0)
                return;
            foreach (var series in _collection)
                series.Reset();
        }

        public void Add<TInfo>(TInfo info)
            where TInfo : ISeriesInfo
        {
            try
            {
                if (info == null)
                    throw new ArgumentNullException(nameof(info));

                string key = info.Key;
                string name = info.Name;

                // El servicio no existe
                if (!ContainsKey(key))
                {
                    TSeries series = GetSeries(info);
                    if (series == null)
                        throw new Exception($"The series with key:{info.Key} could not be added to the collection");
                    else
                        base.Add(series);
                }
            }
            catch (Exception e)
            {
                throw new Exception($"The series with name:{info.Name} and key:{info.Key} could not be added to the collection.", e);
            }
        }
        public void TryAdd<TInfo>(TInfo info)
            where TInfo : ISeriesInfo
        {
            try
            {
                Add(info);
            }
            catch 
            {
                PrintService.LogWarning($"The series with key:{info.Key} could not be added to the collection");
            }
        }

        // TODO: IMPLEMETAR GET SERIES METHOD.
        public TSeries GetSeries(ISeriesInfo info)
        {
            throw new NotImplementedException();
        }

        public void BarUpdate() => ForEach(x => { if (x is IBarUpdate ok) ok.BarUpdate(); });
        public void BarUpdate(IBarsService updatedBarsService) => ForEach(x => { if (x is IBarUpdate ok) ok.BarUpdate(updatedBarsService); });
        public void MarketData(MarketDataEventArgs args) => ForEach(x => { if (x is IMarketData ok) ok.MarketData(args); });
        public void MarketData(IBarsService updatedBarsService) => ForEach(x => { if (x is IMarketData ok) ok.MarketData(updatedBarsService); });
        public void MarketDepth(MarketDepthEventArgs args) => ForEach(x => { if (x is IMarketDepth ok) ok.MarketDepth(args); });
        public void MarketDepth(IBarsService updatedBarsService) => ForEach(x => { if (x is IMarketDepth ok) ok.MarketDepth(updatedBarsService); });

        #endregion

    }
}
