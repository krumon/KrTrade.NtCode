using System;

namespace KrTrade.Nt.Services
{
    public abstract class BarUpdateService<TOptions> : BaseNinjascriptService<TOptions>, IBarUpdateService
        where TOptions : NinjascriptServiceOptions, new()
    {
        // TODO: BORRAR ESTAS CONSTANTES
        private const int DEFAULT_PERIOD = 11;
        private const int DEFAULT_DISPLACEMENT = 0;

        protected BarUpdateService(IBarsService barsService) : this(
            barsService, 
            barsService != null ? barsService.Period : BarsOptions.DEFAULT_PERIOD,
            barsService != null ? barsService.Displacement : BarsOptions.DEFAULT_DISPLACEMENT, 
            null
            )
        {
        }

        protected BarUpdateService(IBarsService barsService, IConfigureOptions<TOptions> configureOptions) : this(barsService, DEFAULT_PERIOD, DEFAULT_DISPLACEMENT, configureOptions)
        {
        }

        protected BarUpdateService(IBarsService barsService, int period) : this(barsService, period, DEFAULT_DISPLACEMENT, null)
        {
        }

        protected BarUpdateService(IBarsService barsService, int period, int displacement) : this(barsService, period, displacement, null)
        {
        }

        protected BarUpdateService(IBarsService barsService, int period, int displacement, IConfigureOptions<TOptions> configureOptions) : base(barsService.Ninjascript, barsService.PrintService, configureOptions)
        {
            Bars = barsService ?? throw new ArgumentNullException(nameof(barsService));
            //Period = period <= 0 ? DEFAULT_PERIOD : period > BarsCache.MAX_CAPACITY ? DEFAULT_PERIOD : period;
            Displacement = displacement < 0 ? DEFAULT_DISPLACEMENT : displacement >= Period ? DEFAULT_DISPLACEMENT : displacement;
            //if (Period + Displacement > BarsCache.MAX_CAPACITY) 
            //    Period = BarsCache.MAX_CAPACITY - Displacement;
            //Bars.Add(this);
        }
        protected BarUpdateService(IBarsService barsService, TOptions options) : base(barsService.Ninjascript, barsService.PrintService, null)
        {
            Bars = barsService ?? throw new ArgumentNullException(nameof(barsService));
            Options = options ?? new TOptions();
            //Period = period <= 0 ? DEFAULT_PERIOD : period > BarsCache.MAX_CAPACITY ? DEFAULT_PERIOD : period;
            //Displacement = displacement < 0 ? DEFAULT_DISPLACEMENT : displacement >= Period ? DEFAULT_DISPLACEMENT : displacement;
            //if (Period + Displacement > BarsCache.MAX_CAPACITY) 
            //    Period = BarsCache.MAX_CAPACITY - Displacement;
            //Bars.Add(this);
        }

        public IBarsService Bars { get; protected set; }
        public int Period { get; protected set; }
        public int Displacement { get; protected set; }

        public abstract void Update();
    }
}
