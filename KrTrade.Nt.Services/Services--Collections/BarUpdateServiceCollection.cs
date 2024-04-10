using System;

namespace KrTrade.Nt.Services
{
    public abstract class BarUpdateServiceCollection<TService> : BaseNinjascriptServiceCollection<TService>, IBarUpdateService
        where TService : IBarUpdateService
    {
        //protected new BarUpdateServiceCollectionOptions _options;
        //public new BarUpdateServiceCollectionOptions Options { get => _options ?? new BarUpdateServiceCollectionOptions(); protected set { _options = value; } }

        protected BarUpdateServiceCollection(IBarsService barsService) : this(barsService, null, null) { }
        protected BarUpdateServiceCollection(IBarsService barsService, Action<NinjascriptServiceCollectionOptions> configureOptions) : this(barsService, configureOptions,null) { }
        protected BarUpdateServiceCollection(IBarsService barsService, NinjascriptServiceCollectionOptions options) : this(barsService, null, options) { }
        protected BarUpdateServiceCollection(IBarsService barsService, Action<NinjascriptServiceCollectionOptions> configureOptions, NinjascriptServiceCollectionOptions options) : base(barsService.Ninjascript, barsService.PrintService, null, null)
        {
            Bars = barsService ?? throw new ArgumentNullException(nameof(barsService));
            Options = options ?? new NinjascriptServiceCollectionOptions();
            configureOptions?.Invoke(Options);
        }

        #region Implementation

        public int BarsIndex => Bars.Index;
        public IBarsService Bars { get; protected set; }

        //public override string Key => throw new NotImplementedException();

        public void Update() => Execute((service) => { if (service.Options.IsEnable) service.Update(); });
        public void Update(IBarsService updatedBarsSeries) => Execute((service) => { if (service.Options.IsEnable) service.Update(updatedBarsSeries); });

        #endregion

    }

    public abstract class BarUpdateServiceCollection<TService,TOptions> : BarUpdateServiceCollection<TService>, IBarUpdateService<TOptions>
        where TService : IBarUpdateService
        where TOptions : BarUpdateServiceCollectionOptions, new()
    {
        protected new TOptions _options;
        public new TOptions Options { get => _options ?? new TOptions(); protected set { _options = value; } }

        protected BarUpdateServiceCollection(IBarsService barsService) : this(barsService, null, null) { }
        protected BarUpdateServiceCollection(IBarsService barsService, Action<TOptions> configureOptions) : this(barsService, configureOptions, null) { }
        protected BarUpdateServiceCollection(IBarsService barsService, TOptions options) : this(barsService, null, options) { }
        protected BarUpdateServiceCollection(IBarsService barsService, Action<TOptions> configureOptions, TOptions options) : base(barsService, null, null)
        {
            Options = options ?? new TOptions();
            configureOptions?.Invoke(Options);
        }

    }
}
