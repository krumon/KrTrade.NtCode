using System;

namespace KrTrade.Nt.Services
{
    public abstract class BarUpdateService<TOptions> : BaseNinjascriptService<TOptions>, IBarUpdateService<TOptions>
        where TOptions : BarUpdateServiceOptions, new()
    {
        public IBarsService Bars { get; protected set; }
        public int BarsIndex => Bars.Index;

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
            barsService.PrintService.LogTrace("BarUpdateService constructor.");
            Bars = barsService ?? throw new ArgumentNullException(nameof(barsService));
            Options = options ?? new TOptions();
            options.BarsIndex = barsService.Index;
        }
        
        public abstract void Update();
        public abstract void Update(IBarsService updatedSeries);

    }
}
