using KrTrade.Nt.Core.Data;
using KrTrade.Nt.Core.Infos;
using KrTrade.Nt.Core.Options;
using KrTrade.Nt.Core.Services;
using NinjaTrader.Data;
using System;

namespace KrTrade.Nt.Core.Series
{

    public abstract class BaseSeriesCollection<TSeries> : BaseCollection<TSeries,SeriesType,ISeriesInfo,ISeriesCollectionInfo,SeriesCollectionType>, ISeriesCollection<TSeries>
        where TSeries : ISeries
    {

        protected IBarsService Bars { get; set; }

        protected BaseSeriesCollection(IBarsService barsService) : this(barsService, new SeriesCollectionInfo(), new ServiceOptions()) { }
        protected BaseSeriesCollection(IBarsService barsService,ISeriesCollectionInfo info, IServiceOptions options) : base(barsService.Ninjascript, barsService.PrintService, info, options) 
        {
            Bars = barsService ?? throw new ArgumentNullException($"Error in 'BaseNinjascriptServiceCollection' constructor. The {nameof(barsService)} argument cannot be null.");
        }

        #region Implementation
        
        public void Reset()
        {
            if (_collection == null || _collection.Count == 0)
                return;
            foreach (var series in _collection)
                series.Reset();
        }

        public string ToString(
            string name, string description, string valuesSeparator, string elementsSeparator, 
            int tabOrder, int barsAgo,
            bool inLine, bool displayIndex, bool displayValues)
        {
            string text = string.Empty;
            string tab = string.Empty;
            valuesSeparator = string.IsNullOrEmpty(valuesSeparator) || valuesSeparator == Environment.NewLine ? ": " : valuesSeparator;
            inLine = inLine || elementsSeparator == Environment.NewLine;
            elementsSeparator = string.IsNullOrEmpty(elementsSeparator) || elementsSeparator == Environment.NewLine ? ", " : elementsSeparator;
            
            if(!inLine)
                elementsSeparator += Environment.NewLine;
            
            for (int i = 0; i < tabOrder; i++)
                tab += "\t";
            string inlineString = inLine ? string.Empty : Environment.NewLine + tab;

            if (!string.IsNullOrEmpty(name))
            {
                text += tab + name;
                if (!string.IsNullOrEmpty(description))
                    text += description;
            }

            if (_collection == null)
                return $"{text}[NULL]";
            if (_collection.Count == 0)
                return $"{text}[EMPTY]";

            text += inlineString + "[" + inlineString;
            for (int i = 0; i < _collection.Count; i++)
            {
                text += _collection[i].ToString(
                    tabOrder: inLine ? 0 : tabOrder + 1,
                    state: string.Empty,
                    isIndexVisible: !displayIndex,
                    index: barsAgo,
                    separator: valuesSeparator,
                    isTitleVisible: true,
                    isSubtitleVisible: true,
                    isDescriptionVisible: false
                    //displayValues: displayValues,
                    );
                if (i != _collection.Count - 1)
                    text += elementsSeparator;
                else
                    text += inlineString + "]";
            }

            return text;
        }

        public override string ToString() => ToString(
            name: Name,
            description: Bars.ToString(),
            valuesSeparator: ": ",
            elementsSeparator: ", ",
            tabOrder: 0,
            barsAgo: 0,
            inLine: false,
            displayIndex: false,
            displayValues: false);
        public string ToString(int tabOrder, int barsAgo,
            string valuesSeparator = ": ", string elementsSeparator = ", ",
            bool displayIndex = true, bool displayValues = true, bool displayName = true, bool displayDescription = false)
            => ToString(
            name: displayName ? Name : string.Empty,
            description: displayDescription ? Bars.ToString() : string.Empty,
            valuesSeparator: valuesSeparator,
            elementsSeparator: elementsSeparator,
            tabOrder: tabOrder,
            barsAgo: barsAgo,
            inLine: tabOrder > 0,
            displayIndex: displayIndex,
            displayValues: displayValues);

        public virtual void BarUpdate() => ForEach(x => { if (x is IBarUpdate ok) ok.BarUpdate(); });
        public virtual void BarUpdate(IBarsService updatedBarsService) => ForEach(x => { if (x is IBarUpdate ok) ok.BarUpdate(updatedBarsService); });
        public virtual void MarketData(MarketDataEventArgs args) => ForEach(x => { if (x is IMarketData ok) ok.MarketData(args); });
        public virtual void MarketData(IBarsService updatedBarsService) => ForEach(x => { if (x is IMarketData ok) ok.MarketData(updatedBarsService); });
        public virtual void MarketDepth(MarketDepthEventArgs args) => ForEach(x => { if (x is IMarketDepth ok) ok.MarketDepth(args); });
        public virtual void MarketDepth(IBarsService updatedBarsService) => ForEach(x => { if (x is IMarketDepth ok) ok.MarketDepth(updatedBarsService); });

        #endregion

    }
}
