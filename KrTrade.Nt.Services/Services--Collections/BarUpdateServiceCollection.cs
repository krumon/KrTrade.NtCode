using System;

namespace KrTrade.Nt.Services
{
    public abstract class BarUpdateServiceCollection<TService> : BaseNinjascriptServiceCollection<TService>
        where TService : IBarUpdateService
    {
        protected BarUpdateServiceCollection(IBarsService barsService, NinjascriptServiceInfo info, BarUpdateServiceCollectionOptions options) : base(barsService.Ninjascript, barsService.PrintService, info, options) 
        {
            LogInitStart();
            Bars = barsService ?? throw new ArgumentNullException(nameof(barsService));
            LogInitEnd();
        }
        protected BarUpdateServiceCollection(IBarsService barsService, NinjascriptServiceInfo info, BarUpdateServiceCollectionOptions options, int capacity) : base(barsService.Ninjascript, barsService.PrintService, info, options, capacity) 
        {
            LogInitStart();
            Bars = barsService ?? throw new ArgumentNullException(nameof(barsService));
            LogInitEnd();
        }

        new public BarUpdateServiceCollectionOptions Options => (BarUpdateServiceCollectionOptions)base.Options;

        #region Implementation

        public int BarsIndex => Bars.Index;
        public IBarsService Bars { get; protected set; }

        public void Update() => ForEach((service) => { if (service.IsEnable) service.BarUpdate(); });
        public void Update(IBarsService updatedBarsSeries) => ForEach((service) => { if (service.IsEnable) service.BarUpdate(updatedBarsSeries); });

        #endregion

    }

    //public abstract class BarUpdateServiceCollection<TService,TOptions> : BarUpdateServiceCollection<TService>, IBarUpdateService<TOptions>
    //    where TService : IBarUpdateService
    //    where TOptions : BarUpdateServiceCollectionOptions, new()
    //{
    //    protected new TOptions _options;
    //    public new TOptions Options { get => _options ?? new TOptions(); protected set { _options = value; } }

    //    protected BarUpdateServiceCollection(IBarsService barsService) : this(barsService, null, null) { }
    //    protected BarUpdateServiceCollection(IBarsService barsService, Action<TOptions> configureOptions) : this(barsService, configureOptions, null) { }
    //    protected BarUpdateServiceCollection(IBarsService barsService, TOptions options) : this(barsService, null, options) { }
    //    protected BarUpdateServiceCollection(IBarsService barsService, Action<TOptions> configureOptions, TOptions options) : base(barsService, null, null)
    //    {
    //        Options = options ?? new TOptions();
    //        configureOptions?.Invoke(Options);
    //    }

    //}
}
