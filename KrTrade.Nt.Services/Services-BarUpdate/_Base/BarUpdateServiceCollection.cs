using System;

namespace KrTrade.Nt.Services
{
    public class BarUpdateServiceCollection<TService> : BaseNinjascriptServiceCollection<TService>, IBarUpdateService
        where TService : IBarUpdateService
    {
        protected new BarUpdateServiceCollectionOptions _options;
        public new BarUpdateServiceCollectionOptions Options { get => _options ?? new BarUpdateServiceCollectionOptions(); protected set { _options = value; } }

        protected BarUpdateServiceCollection(IBarsService barsService) : this(barsService, new BarUpdateServiceCollectionOptions()) { }
        protected BarUpdateServiceCollection(IBarsService barsService, Action<BarUpdateServiceCollectionOptions> configureOptions) : base(barsService?.Ninjascript, barsService?.PrintService, new BarUpdateServiceCollectionOptions())
        {
            Bars = barsService ?? throw new ArgumentNullException(nameof(barsService));
            //Options = new BarUpdateServiceCollectionOptions();
            configureOptions?.Invoke(Options);
            Options.BarsIndex = BarsIndex;
        }
        protected BarUpdateServiceCollection(IBarsService barsService, BarUpdateServiceCollectionOptions options) : base(barsService.Ninjascript, barsService.PrintService, null, options)
        {
            Bars = barsService ?? throw new ArgumentNullException(nameof(barsService));
            Options = options ?? new BarUpdateServiceCollectionOptions();
            options.BarsIndex = barsService.Index;
        }

        //public BarUpdateServiceCollection(NinjaScriptBase ninjascript) : base(ninjascript) { }
        //public BarUpdateServiceCollection(NinjaScriptBase ninjascript, IPrintService printService) : base(ninjascript, printService) { }
        //public BarUpdateServiceCollection(NinjaScriptBase ninjascript, IPrintService printService, BarUpdateServiceCollectionOptions options) : base(ninjascript, printService, options) { }
        //public BarUpdateServiceCollection(NinjaScriptBase ninjascript, IPrintService printService, Action<BarUpdateServiceCollectionOptions> configureOptions, BarUpdateServiceCollectionOptions options) : base(ninjascript, printService, configureOptions, options) { }

        #region Implementation

        public int BarsIndex => Bars.Index;
        public IBarsService Bars { get; protected set; }

        public override string Key => throw new NotImplementedException();

        public void Update() => Execute((service) => service.Update());
        public void Update(IBarsService updatedBarsSeries) => Execute((service) => service.Update(updatedBarsSeries));

        #endregion

    }

    public class BarUpdateServiceCollection<TService,TOptions> : BarUpdateServiceCollection<TService>, IBarUpdateService<TOptions>
        where TService : IBarUpdateService
        where TOptions : BarUpdateServiceCollectionOptions, new()
    {
        protected new TOptions _options;
        public new TOptions Options { get => _options ?? new TOptions(); protected set { _options = value; } }

        protected BarUpdateServiceCollection(IBarsService barsService) : this(barsService, new TOptions()) { }
        protected BarUpdateServiceCollection(IBarsService barsService, Action<TOptions> configureOptions) : base(barsService, new TOptions())
        {
            //Bars = barsService ?? throw new ArgumentNullException(nameof(barsService));
            //Options = new TOptions();
            configureOptions?.Invoke(Options);
            Options.BarsIndex = BarsIndex;
        }
        protected BarUpdateServiceCollection(IBarsService barsService, TOptions options) : base(barsService, options)
        {
            //Bars = barsService ?? throw new ArgumentNullException(nameof(barsService));
            Options = options ?? new TOptions();
            options.BarsIndex = barsService.Index;
        }

    }
}
