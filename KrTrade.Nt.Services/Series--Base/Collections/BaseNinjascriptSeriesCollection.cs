using KrTrade.Nt.Core.Collections;
using KrTrade.Nt.Core.Series;
using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using System;

namespace KrTrade.Nt.Services.Series
{

    public abstract class BaseNinjascriptSeriesCollection<TSeries> : BaseKeyCollection<TSeries>, INinjascriptSeriesCollection<TSeries>
        where TSeries : INinjascriptSeries
    {

        protected SeriesCollectionInfo _info;
        protected IBarsService Bars { get; set; }

        public NinjaScriptBase Ninjascript => Bars.Ninjascript;
        public IPrintService PrintService => Bars.PrintService;
        public SeriesCollectionInfo Info { get => _info ?? new SeriesCollectionInfo(); protected set { _info = value; } }

        public SeriesCollectionType Type { get => Info.Type; protected set => Info.Type = value; }

        public bool IsConfigure { get;protected set; }
        public bool IsDataLoaded { get; protected set; }

        public string Name { get => string.IsNullOrEmpty(Info.Name) ? Info.Key : Info.Name; internal set { Info.Name = value; } }

        protected BaseNinjascriptSeriesCollection(IBarsService barsService) : this(barsService, new SeriesCollectionInfo()) { }
        protected BaseNinjascriptSeriesCollection(IBarsService barsService,SeriesCollectionInfo info) : base() 
        {
            this.Bars = barsService ?? throw new ArgumentNullException($"Error in 'BaseNinjascriptServiceCollection' constructor. The {nameof(barsService)} argument cannot be null.");
            Info = info;
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
                throw new NullReferenceException($"{Name} inner collection is null.");
            if (_collection.Count == 0)
            {
                if (_collection is BarsSeriesCollection)
                    throw new Exception($"{Name} inner collection is empty.");
            }

            IsConfigure = true;
            foreach (var series in _collection)
            {
                series.Configure();
                if (!series.IsConfigure)
                    IsConfigure = false;
            }

            if (!IsConfigure)
                PrintService.LogError($"'{Name}' cannot be configured because one 'Series' could not be configured.");
        }
        public void DataLoaded()
        {
            if (_collection == null)
                throw new NullReferenceException($"{Name} inner collection is null.");
            if (_collection.Count == 0)
            {
                if (_collection is BarsSeriesCollection)
                {
                    if (_collection is BarsSeriesCollection)
                        throw new Exception($"{Name} inner collection is empty.");
                }
            }

            IsDataLoaded = true;
            foreach (var series in _collection)
            {
                series.DataLoaded();
                if (!series.IsDataLoaded)
                    IsDataLoaded = false;
            }

            if (!IsDataLoaded)
                PrintService.LogError($"'{Name}' cannot be configured when data loaded because one 'Series' could not be configured.");
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

        public virtual string ToLogString() => ToLogString(Name, 0, ", ",0);
        public virtual string ToLogString(string header, int tabOrder, string separator, int barsAgo)
        {
            string text = string.Empty;
            string tab = string.Empty;
            separator = string.IsNullOrEmpty(separator) ? ", " : separator;

            for (int i = 0; i < tabOrder; i++)
                tab += "\t";

            if (!string.IsNullOrEmpty(header))
                text += tab + header;

            if (_collection == null)
                return $"{text}[NULL]";
            if (_collection.Count == 0)
                return $"{text}[EMPTY]";

            text += (separator == Environment.NewLine ? Environment.NewLine + tab : string.Empty) + "[" + (separator == Environment.NewLine ? Environment.NewLine + tab : string.Empty);
            for (int i = 0; i < _collection.Count; i++)
            {
                text += _collection[i].ToString(tabOrder + 1, barsAgo);
                if (i == _collection.Count - 1)
                    text += (separator == Environment.NewLine ? Environment.NewLine + tab : string.Empty) + "]";
                else
                    text += (separator != Environment.NewLine ? separator : string.Empty) + (separator == Environment.NewLine ? Environment.NewLine : string.Empty);
            }

            return text;
        }
        
        public virtual string ToLogString(bool isMultiline) => ToLogString(Name, 0, isMultiline ? Environment.NewLine : ", ",0);
        public virtual string ToLogString(int tabOrder, string separator) => ToLogString(Name, tabOrder, separator,0);

        public override string ToString() => ToString(Type.ToString(), 0, false);
        public override string ToLongString() => ToString(Type.ToString(), 0, true);
        public override string ToLongString(int tabOrder) => ToString(Type.ToString(), tabOrder, true);

        public virtual void BarUpdate() => ForEach(x => { if (x is IBarUpdate ok) ok.BarUpdate(); });
        public virtual void BarUpdate(IBarsService updatedBarsService) => ForEach(x => { if (x is IBarUpdate ok) ok.BarUpdate(updatedBarsService); });
        public virtual void MarketData(MarketDataEventArgs args) => ForEach(x => { if (x is IMarketData ok) ok.MarketData(args); });
        public virtual void MarketData(IBarsService updatedBarsService) => ForEach(x => { if (x is IMarketData ok) ok.MarketData(updatedBarsService); });
        public virtual void MarketDepth(MarketDepthEventArgs args) => ForEach(x => { if (x is IMarketDepth ok) ok.MarketDepth(args); });
        public virtual void MarketDepth(IBarsService updatedBarsService) => ForEach(x => { if (x is IMarketDepth ok) ok.MarketDepth(updatedBarsService); });

        #endregion

    }
}
