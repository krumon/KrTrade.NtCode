using NinjaTrader.NinjaScript;

namespace ConsoleApp
{

    /// <summary>
    /// The interface for any ninjascript configuration.
    /// </summary>
    public interface IConfiguration 
    {

        #region Properties

        /// <summary>
        /// Represents the ninjascript description.
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// Represents the ninjascript name.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Represents the ninjascript calculate mode.
        /// </summary>
        Calculate Calculate { get; set; }

        /// <summary>
        /// Represents the minimum bars required to plot.
        /// </summary>
        int BarsRequiredToPlot { get; set; }

        #endregion

    }
}
