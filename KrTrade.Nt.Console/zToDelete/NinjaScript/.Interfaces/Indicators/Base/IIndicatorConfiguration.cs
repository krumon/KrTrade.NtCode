using NinjaTrader.Gui.Chart;

namespace KrTrade.Nt.Console
{

    /// <summary>
    /// Interface for any indicator configuration.
    /// </summary>
    public interface IIndicatorConfiguration : IConfiguration
    {

        #region Properties

        /// <summary>
        /// Indicates if the data is overlay in the graphics.
        /// </summary>
        bool IsOverlay { get; set; }

        /// <summary>
        /// Indicates if the ninjascript display in the data box.
        /// </summary>
        bool DisplayInDataBox { get; set; }

        /// <summary>
        /// Represents the scale justification of the chart.
        /// </summary>
        ScaleJustification ScaleJustification { get; set; }

        //Disable this property if your indicator requires custom values that cumulate with each new market data event. 
        //See Help Guide for additional information.
        /// <summary>
        /// Indicates if the ninjascript is suspended when the ninjascript is inactive.
        /// </summary>
        bool IsSuspendedWhileInactive { get; set; }

        /// <summary>
        /// Indicates if the ninjascript draw in the price panel.
        /// </summary>
        bool DrawOnPricePanel { get; set; }

        /// <summary>
        /// Indicates if the ninjscript draw the horizontal grid lines.
        /// </summary>
        bool DrawHorizontalGridLines { get; set; }

        /// <summary>
        /// Indicates if the ninjscript draw the vertical grid lines.
        /// </summary>
        bool DrawVerticalGridLines { get; set; }

        /// <summary>
        /// Indicates if the ninjscript paint the price markers.
        /// </summary>
        bool PaintPriceMarkers { get; set; }

        #endregion


    }

}
