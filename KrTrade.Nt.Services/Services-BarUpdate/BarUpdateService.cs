using KrTrade.Nt.Core.Caches;
using System;

namespace KrTrade.Nt.Services
{
    public abstract class BarUpdateService<TOptions> : BaseNinjascriptService<TOptions>, IBarUpdateService
        where TOptions : BarUpdateServiceOptions, new()
    {
        public IBarsService Bars { get; protected set; }
        public int Period => Options.Period;
        public int Displacement => Options.Displacement;
        public int LengthOfRemovedValuesCache => Options.LengthOfRemovedValuesCache;
        public int BarsIndex => Options.BarsIndex;

        protected BarUpdateService(IBarsService barsService) : base(barsService?.Ninjascript, barsService?.PrintService, null, null) { InitializeService(barsService, Options); }
        protected BarUpdateService(IBarsService barsService, IConfigureOptions<TOptions> configureOptions) : base(barsService?.Ninjascript, barsService?.PrintService, configureOptions) { InitializeService(barsService, Options); }
        protected BarUpdateService(IBarsService barsService, Action<TOptions> configureOptions) : base(barsService?.Ninjascript, barsService?.PrintService, configureOptions) { InitializeService(barsService, Options); }
        protected BarUpdateService(IBarsService barsService, TOptions options): base(barsService.Ninjascript, barsService.PrintService, null, options) { InitializeService(barsService, Options); }
        protected BarUpdateService(IBarsService barsService, int period = Cache.DEFAULT_PERIOD, int displacement = Cache.DEFAULT_DISPLACEMENT, int lengthOfRemovedValuesCache = Cache.DEFAULT_LENGTH_REMOVED_CACHE, int barsIndex = 0) : base(barsService.Ninjascript, barsService.PrintService, null,null)
        {
            TOptions options = new TOptions();
            options.Period = period;
            options.Displacement = displacement;
            options.LengthOfRemovedValuesCache = lengthOfRemovedValuesCache;
            options.BarsIndex = barsIndex;
            InitializeService(barsService,options);
        }
        
        protected void InitializeService(IBarsService barsService, TOptions options)
        {
            Bars = barsService ?? throw new ArgumentNullException(nameof(barsService));
            if (options.Displacement < 0)
                options.Displacement = Cache.DEFAULT_DISPLACEMENT;
            if (options.LengthOfRemovedValuesCache < 0)
                options.LengthOfRemovedValuesCache = Cache.DEFAULT_LENGTH_REMOVED_CACHE;
            if (options.Period <= 0)
                options.Period = Cache.DEFAULT_PERIOD;
            else if (options.Period > (int.MaxValue - options.Period - options.Displacement))
                options.Period = int.MaxValue - options.Period - options.Displacement;
        }

        public abstract void Update();


    }
}
