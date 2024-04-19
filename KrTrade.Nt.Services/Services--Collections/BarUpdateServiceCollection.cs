using System;
using System.Collections.Generic;

namespace KrTrade.Nt.Services
{
    public abstract class BarUpdateServiceCollection<TService> : BaseNinjascriptServiceCollection<TService>
        where TService : IBarUpdateService
    {
        protected BarUpdateServiceCollection(IBarsService barsService)
        {
            Bars = barsService ?? throw new ArgumentNullException(nameof(barsService));
        }
        protected BarUpdateServiceCollection(IBarsService barsService, IEnumerable<TService> elements) : base(elements)
        {
            Bars = barsService ?? throw new ArgumentNullException(nameof(barsService));
        }
        protected BarUpdateServiceCollection(IBarsService barsService, int capacity) : base(capacity)
        {
            Bars = barsService ?? throw new ArgumentNullException(nameof(barsService));
        }

        //protected new BarUpdateServiceCollectionOptions _options;
        //public new BarUpdateServiceCollectionOptions Options { get => _options ?? new BarUpdateServiceCollectionOptions(); protected set { _options = value; } }

        //protected BarUpdateServiceCollection(IBarsService barsService) : this(barsService, null, null) { }
        //protected BarUpdateServiceCollection(IBarsService barsService, Action<NinjascriptServiceCollectionOptions> configureOptions) : this(barsService, configureOptions,null) { }
        //protected BarUpdateServiceCollection(IBarsService barsService, NinjascriptServiceCollectionOptions options) : this(barsService, null, options) { }
        //protected BarUpdateServiceCollection(IBarsService barsService, Action<NinjascriptServiceCollectionOptions> configureOptions, NinjascriptServiceCollectionOptions options) : base(barsService.Ninjascript, barsService.PrintService, null, null)
        //{
        //    Bars = barsService ?? throw new ArgumentNullException(nameof(barsService));
        //    Options = options ?? new NinjascriptServiceCollectionOptions();
        //    configureOptions?.Invoke(Options);
        //}

        #region Implementation

        public int BarsIndex => Bars.Index;
        public IBarsService Bars { get; protected set; }

        public void Update() => ForEach((service) => { if (service.IsEnable) service.Update(); });
        public void Update(IBarsService updatedBarsSeries) => ForEach((service) => { if (service.IsEnable) service.Update(updatedBarsSeries); });

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
