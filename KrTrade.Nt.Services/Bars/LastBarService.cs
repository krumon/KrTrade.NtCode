using NinjaTrader.NinjaScript;
using System;

namespace KrTrade.Nt.Services.Bars
{
    /// <summary>
    /// Represents the service of only one bar.
    /// </summary>
    public class LastBarService : BarService
    {

        #region Private members

        private readonly BarsService _barsService;

        protected BarsService BarsService { get { return _barsService; } }

        #endregion

        #region Public properties

        /// <summary>
        /// Indicates if the last bar of the bars in progress is closed.
        /// </summary>
        public bool IsClosed => _barsService.GetIsClosed(BarsIdx);

        /// <summary>
        /// Indicates if the last bar of the bars in progress is removed.
        /// </summary>
        public bool IsRemoved => _barsService.GetIsRemoved(BarsIdx);

        /// <summary>
        /// Indicates if success the first tick in the current bar of the bars in progress.
        /// </summary>
        public bool IsFirstTick => _barsService.GetIsFirstTick(BarsIdx);

        /// <summary>
        /// Indicates if success a new tick in the current bar of the bars in progress.
        /// </summary>
        public bool IsNewTick => _barsService.GetHasNewTick(BarsIdx);

        /// <summary>
        /// Indicates if the price changed in the current bar of the bars in progress.
        /// </summary>
        public bool IsNewPrice => _barsService.GetHasNewPrice(BarsIdx);

        #endregion

        #region Constructors

        /// <summary>
        /// Create <see cref="LastBarService"/> instance.
        /// </summary>
        /// <param name="ninjascript">The NinjaScript where the service is being executed.</param>
        /// <param name="barsService">The bars service necessary for the service to function correctly.</param>
        /// <param name="printService">The print service to write in the NinjaScript output window.</param>
        /// <exception cref="ArgumentNullException">The NinjaScript or BarsService arguments cannot be null anyone.</exception>
        public LastBarService(NinjaScriptBase ninjascript, BarsService barsService, PrintService printService) : base(ninjascript, printService,0, barsService != null ? barsService.Idx : -1) 
        {
            _barsService = barsService ?? throw new ArgumentNullException(nameof(barsService));
        }
        
        #endregion
        
    }
}
