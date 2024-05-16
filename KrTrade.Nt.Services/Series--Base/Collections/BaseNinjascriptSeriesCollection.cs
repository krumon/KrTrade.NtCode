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

        //private readonly NinjaScriptBase _ninjascript;
        //private readonly IPrintService _printService;
        protected SeriesCollectionInfo _info;
        protected IBarsService Bars { get; set; }

        public NinjaScriptBase Ninjascript => Bars.Ninjascript;
        public IPrintService PrintService => Bars.PrintService;
        public SeriesCollectionInfo Info { get => _info ?? new SeriesCollectionInfo(); protected set { _info = value; } }

        public bool IsConfigure { get;protected set; }
        public bool IsDataLoaded { get; protected set; }

        public string Name { get => string.IsNullOrEmpty(Info.Name) ? Info.Key : Info.Name; internal set { Info.Name = value; } }

        protected BaseNinjascriptSeriesCollection(IBarsService barsService) : this(barsService, new SeriesCollectionInfo()) { }
        protected BaseNinjascriptSeriesCollection(IBarsService barsService,SeriesCollectionInfo info) : base() 
        {
            barsService.PrintService.LogTrace("NinjascriptSeriesCollection constructor. Init...");
            this.Bars = barsService ?? throw new ArgumentNullException($"Error in 'BaseNinjascriptServiceCollection' constructor. The {nameof(barsService)} argument cannot be null.");
            Info = info;
            barsService.PrintService.LogTrace("NinjascriptSeriesCollection constructor. ...End");
        }
        protected BaseNinjascriptSeriesCollection(IBarsService barsService, SeriesCollectionInfo info, int capacity) : base(capacity) 
        {
            this.Bars = barsService ?? throw new ArgumentNullException($"Error in 'BaseNinjascriptServiceCollection' constructor. The {nameof(barsService)} argument cannot be null.");
            Info = info;
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

        //public void Add<TInfo>(TInfo info)
        //    where TInfo : ISeriesInfo
        //{
        //    try
        //    {
        //        if (info == null)
        //            throw new ArgumentNullException(nameof(info));

        //        // El servicio no existe
        //        if (!ContainsKey(info.Key))
        //        {
        //            TSeries series = GetSeries(info);
        //            if (series == null)
        //                throw new Exception($"The series with key:{info.Key} could not be added to the collection");
        //            else
        //                base.Add(series);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception($"The series with key:{info.Key} could not be added to the collection.", e);
        //    }
        //}
        //public void TryAdd<TInfo>(TInfo info)
        //    where TInfo : ISeriesInfo
        //{
        //    try
        //    {
        //        Add(info);
        //    }
        //    catch 
        //    {
        //        PrintService.LogWarning($"The series with key:{info.Key} could not be added to the collection");
        //    }
        //}

        //// TODO: IMPLEMETAR GET SERIES METHOD.
        //public TSeries GetSeries<TInfo>(TInfo info)
        //    where TInfo : ISeriesInfo
        //{
        //    if (info == null)
        //        return default;
        //    INinjascriptSeries series = null;
        //    if (info.Type == SeriesType.MAX)
        //    {
        //        if (info.Inputs == null || info.Inputs.Count == 0)
        //        {
        //            PrintService.LogWarning(
        //                $"The {info.Type} series nedds one Input series to calculate the values. " +
        //                $"The {info.Type} series could not be created.");
        //        }
        //        else if (info.Inputs.Count > 1)
        //        {
        //            PrintService.LogWarning(
        //                $"The {info.Type} series only nedds one Input series to calculate the values. " +
        //                $"The rest of the series will be eliminated and will not be taken into consideration.");
        //            info.Inputs.RemoveRange(1, info.Inputs.Count - 2);
        //        }
        //        else if (info.Inputs[0] is PeriodSeriesInfo periodInfo)
        //            series = new MaxSeries(Bars, periodInfo);
        //    }
        //    base.Add(series);
        //    return this[info.Key];
        //}
        //public TSeries GetOrAdd<TInfo>(TInfo info)
        //    where TInfo : ISeriesInfo
        //{
        //    return default;
        //}

        public virtual void BarUpdate() => ForEach(x => { if (x is IBarUpdate ok) ok.BarUpdate(); });
        public virtual void BarUpdate(IBarsService updatedBarsService) => ForEach(x => { if (x is IBarUpdate ok) ok.BarUpdate(updatedBarsService); });
        public virtual void MarketData(MarketDataEventArgs args) => ForEach(x => { if (x is IMarketData ok) ok.MarketData(args); });
        public virtual void MarketData(IBarsService updatedBarsService) => ForEach(x => { if (x is IMarketData ok) ok.MarketData(updatedBarsService); });
        public virtual void MarketDepth(MarketDepthEventArgs args) => ForEach(x => { if (x is IMarketDepth ok) ok.MarketDepth(args); });
        public virtual void MarketDepth(IBarsService updatedBarsService) => ForEach(x => { if (x is IMarketDepth ok) ok.MarketDepth(updatedBarsService); });

        #endregion

    }
}
