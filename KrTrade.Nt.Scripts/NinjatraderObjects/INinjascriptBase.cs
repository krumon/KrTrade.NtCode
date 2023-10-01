using NinjaTrader.NinjaScript;
using System;

namespace KrTrade.Nt.Scripts.NinjatraderObjects
{
    /// <summary>
    /// Represents properties and methods of <see cref="NinjaTrader.NinjaScript.NinjaScriptBase"/>.
    /// </summary>
    public interface INinjaScriptBase
    {
        /// <summary>
        /// Gets the ninjascript instance.
        /// </summary>
        NinjaTrader.NinjaScript.NinjaScriptBase Instance { get; }

        /// <summary>
        /// Gets the NinjaScript type name.
        /// </summary>
        string TypeName { get; }

        /// <summary>
        /// Gets the <see cref="NinjaScript.State"/> of the ninjascript object.
        /// </summary>
        State State { get; }

        /// <summary>
        /// Gets the delegate that print in the ninjatrader output window.
        /// </summary>
        Action<object> Print { get; }

        /// <summary>
        /// Gets methods thats clear the output window.
        /// </summary>
        Action ClearOutputWindow { get; }

    }
}
