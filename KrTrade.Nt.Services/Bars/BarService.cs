using KrTrade.Nt.Core.Extensions;
using KrTrade.Nt.Core.Helpers;
using KrTrade.Nt.Core.Interfaces;
using NinjaTrader.NinjaScript;
using System;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Represents the service of only one bar.
    /// </summary>
    public class BarService : BaseBarService, INeedBarsService, IOnBarUpdateService
    {

        #region Private members


        #endregion

        #region Constructors

        /// <summary>
        /// Create <see cref="BarService"/> instance.
        /// </summary>
        /// <param name="ninjascript">The NinjaScript where the service is being executed.</param>
        /// <param name="printService">The print service to write in the NinjaScript output window.</param>
        /// <param name="barsAgo">The bars ago of the bar in the data series.</param>
        /// <param name="barsIdx">The bars index of the bar data series.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="ninjascript"/>cannot be null.</exception>
        public BarService(NinjaScriptBase ninjascript, PrintService printService, int barsAgo, int barsIdx) : base(ninjascript, printService, barsAgo, barsIdx)
        {
        }

        #endregion

        #region Implementation methods

        /// <summary>
        /// Gets the name of the service.
        /// </summary>
        public override string Name => nameof(BarService);

        /// <inheritdoc/>
        public void OnBarUpdate()
        {
            if (!Ninjascript.State.IsInProgress())
                ThrowHelper.ThrowOnBarUpdateInvalidStateException(Ninjascript.State);

            if (Ninjascript.BarsInProgress < 0 || Ninjascript.CurrentBars[Ninjascript.BarsInProgress] < 0)
                return;

            UpdateValues();
        }

        #endregion

    }
}
