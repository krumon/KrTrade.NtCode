using KrTrade.Nt.Core;
using NinjaTrader.NinjaScript;
using System;

namespace KrTrade.Nt.Services
{
    public interface INinjascript : IConfigure, IDataLoaded, ITerminated
    {
        /// <summary>
        /// Gets Ninjatrader NinjaScript instance.
        /// </summary>
        NinjaScriptBase Instance { get; }

        /// <summary>
        /// Gets or sets the <see cref="Ninjascript"/> options. This options will be configured when
        /// 'Ninjatrader.NinjaScript' state is 'Configure'.
        /// </summary>
        NinjascriptOptions Options { get; }

        /// <summary>
        /// The ninjascripts service provider.
        /// </summary>
        IServiceProvider Services {  get; }

        /// <summary>
        /// Method used to update the ninjascript when the last bar has been updated.        
        /// </summary>
        void OnBarUpdate();

    }
}
