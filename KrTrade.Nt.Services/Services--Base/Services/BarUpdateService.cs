using System;

namespace KrTrade.Nt.Services
{
    public abstract class BarUpdateService<TOptions> : BaseNinjascriptService<TOptions>, IBarUpdateService<TOptions>
        where TOptions : BarUpdateServiceOptions, new()
    {
        public IBarsService Bars { get; protected set; }
        public int BarsIndex => Bars.Index;
        //public int BarsIndex => Options.BarsIndex;
        //public int Period => Options.Period;
        //public int Displacement => Options.Displacement;
        //public int LengthOfRemovedValuesCache => Options.LengthOfRemovedValuesCache;
        

        protected BarUpdateService(IBarsService barsService) : this(barsService, new TOptions()) { }
        protected BarUpdateService(IBarsService barsService, Action<TOptions> configureOptions) : base(barsService?.Ninjascript, barsService?.PrintService, configureOptions) 
        {
            Bars = barsService ?? throw new ArgumentNullException(nameof(barsService));
            Options = new TOptions();
            configureOptions?.Invoke(Options);
            Options.BarsIndex = BarsIndex;
        }
        protected BarUpdateService(IBarsService barsService, TOptions options): base(barsService.Ninjascript, barsService.PrintService, null, options) 
        {
            Bars = barsService ?? throw new ArgumentNullException(nameof(barsService));
            Options = options ?? new TOptions();
            options.BarsIndex = barsService.Index;
        }
        
        //protected void InitializeService(IBarsService barsService, TOptions options)
        //{
        //    //if (options.Displacement < 0)
        //    //    options.Displacement = Cache.DEFAULT_DISPLACEMENT;
        //    //if (options.LengthOfRemovedValuesCache < 0)
        //    //    options.LengthOfRemovedValuesCache = Cache.DEFAULT_OLD_VALUES_CAPACITY;
        //    //if (options.Period <= 0)
        //    //    options.Period = Cache.DEFAULT_PERIOD;
        //    //else if (options.Period > (int.MaxValue - options.Period - options.Displacement))
        //    //    options.Period = int.MaxValue - options.Period - options.Displacement;
        //}

        public abstract void Update();
        public abstract void BarsSeriesUpdate(IBarsService updatedSeries);

    }
}
