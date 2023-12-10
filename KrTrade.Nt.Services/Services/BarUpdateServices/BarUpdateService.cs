using NinjaTrader.NinjaScript;

namespace KrTrade.Nt.Services
{
    public abstract class BarUpdateService<TOptions> : NinjascriptService<TOptions>, IBarUpdateService
    where TOptions : NinjascriptServiceOptions, new()
    {
        /// <summary>
        /// Create <see cref="BarService"/> instance and configure it.
        /// </summary>
        /// <see cref="IBarsService"/> necesary for the <see cref="BarService"/>.
        /// <exception cref="ArgumentNullException">The <see cref="IBarsService"/> cannot be null.</exception>
        public BarUpdateService(IBarsService barsService) : base(barsService?.Ninjascript, barsService?.PrintService)
        {
        }

        /// <summary>
        /// Create <see cref="BarService"/> instance and configure it.
        /// </summary>
        /// <see cref="IBarsService"/> necesary for the <see cref="BarService"/>.
        /// <exception cref="ArgumentNullException">The <see cref="IBarsService"/> cannot be null.</exception>
        /// <param name="configureOptions">The configure options of the service.</param>
        /// <exception cref="ArgumentNullException">The <see cref="IBarsService"/> cannot be null.</exception>
        public BarUpdateService(IBarsService barsService, IConfigureOptions<TOptions> configureOptions) : base(barsService?.Ninjascript, barsService?.PrintService, configureOptions)
        {
        }

        public abstract IBarsService BarsService { get; }

        public abstract void LogUpdatedState();

        public abstract void Update();
    }
}
