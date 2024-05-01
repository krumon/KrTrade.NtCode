using NinjaTrader.NinjaScript;

namespace KrTrade.Nt.Services.Series
{

    public class NumericSeriesCollection<TSeries> : BaseNinjascriptSeriesCollection<TSeries>, INumericSeriesCollection<TSeries>
    where TSeries : INumericSeries
    {

        public NumericSeriesCollection(IBarsService barsService) : base(barsService) { }
        public NumericSeriesCollection(IBarsService barsService, string name) : base(barsService, name) { }
        public NumericSeriesCollection(IBarsService barsService, string name, SeriesCollectionOptions options) : base(barsService, name, options) { }
        public NumericSeriesCollection(IBarsService barsService, string name, SeriesCollectionOptions options, int capacity) : base(barsService, name, options, capacity) { }
        public NumericSeriesCollection(NinjaScriptBase ninjascript, IPrintService printService, string name, SeriesCollectionOptions options) : base(ninjascript, printService, name, options) { }
        public NumericSeriesCollection(NinjaScriptBase ninjascript, IPrintService printService, string name, SeriesCollectionOptions options, int capacity) : base(ninjascript, printService, name, options, capacity) { }

    }

    public abstract class NumericSeriesCollection<TSeries,TOptions> : NumericSeriesCollection<TSeries>
        where TSeries : INumericSeries
        where TOptions : SeriesCollectionOptions, new()
    {
        protected new TOptions _options;
        public new TOptions Options { get => _options ?? new TOptions(); protected set { _options = value; } }

        protected NumericSeriesCollection(IBarsService barsService) : base(barsService) { }
        protected NumericSeriesCollection(IBarsService barsService, string name) : base(barsService, name) { }
        protected NumericSeriesCollection(IBarsService barsService, string name, SeriesCollectionOptions options) : base(barsService, name, options) { }
        protected NumericSeriesCollection(IBarsService barsService, string name, SeriesCollectionOptions options, int capacity) : base(barsService, name, options, capacity) { }
        protected NumericSeriesCollection(NinjaScriptBase ninjascript, IPrintService printService, string name, SeriesCollectionOptions options) : base(ninjascript, printService, name, options) { }
        protected NumericSeriesCollection(NinjaScriptBase ninjascript, IPrintService printService, string name, SeriesCollectionOptions options, int capacity) : base(ninjascript, printService, name, options, capacity) { }

    }
}
