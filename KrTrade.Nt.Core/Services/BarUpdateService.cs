using KrTrade.Nt.Core.Infos;
using KrTrade.Nt.Core.Options;
using System;

namespace KrTrade.Nt.Core.Services
{
    public abstract class BarUpdateService<TInfo,TOptions> : BaseService<TInfo, TOptions>, IBarUpdateService<TInfo, TOptions>
        where TInfo : BarUpdateServiceInfo, new()
        where TOptions : BarUpdateServiceOptions, new()
    {
        //protected BarUpdateService(IBarsService barsService) : this(barsService, null,null) { }
        //protected BarUpdateService(IBarsService barsService, TInfo info) : this(barsService, info,null) { }
        //protected BarUpdateService(IBarsService barsService, TOptions options) : this(barsService, null, options) { }
        protected BarUpdateService(IBarsService barsService, TInfo info, TOptions options) : base(barsService.Ninjascript, barsService.PrintService, info, options)
        {
            Bars = barsService ?? throw new ArgumentNullException(nameof(barsService));
        }

        public IBarsService Bars { get; protected set; }
        public int BarsIndex => Bars.Index;

        //protected BarUpdateService(IBarsService barsService) : this(barsService, new TOptions()) { }
        //protected BarUpdateService(IBarsService barsService, Action<TOptions> configureOptions) : base(barsService?.Ninjascript, barsService?.PrintService, configureOptions) 
        //{
        //    Bars = barsService ?? throw new ArgumentNullException(nameof(barsService));
        //    Options = new TOptions();
        //    configureOptions?.Invoke(Options);
        //    Options.BarsIndex = BarsIndex;
        //}
        //protected BarUpdateService(IBarsService barsService, TOptions options): base(barsService.Ninjascript, barsService.PrintService, null, options) 
        //{
        //    Bars = barsService ?? throw new ArgumentNullException(nameof(barsService));
        //    Options = options ?? new TOptions();
        //    options.BarsIndex = barsService.Index;
        //}
        
        public abstract void BarUpdate();
        //public abstract void BarUpdate(IBarsService updatedBarsSeries);
    }
}
