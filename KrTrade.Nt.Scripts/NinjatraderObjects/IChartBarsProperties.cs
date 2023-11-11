using KrTrade.Nt.Core.Bars;

namespace KrTrade.Nt.Scripts.NinjatraderObjects
{
    /// <summary>
    /// Represents the properties and methods to create a default implementation of <see cref="ChartBarsProperties"/>.
    /// </summary>
    public interface IChartBarsProperties
    {
        
        /// <summary>
        /// Gets or sets the period of the chart bars.
        /// </summary>
        BarsPeriod BarsPeriod { get; set; }

        /// <summary>
        /// The chart instrument (primary instrument) name.
        /// </summary>
        string InstrumentName { get; set; }

        /// <summary>
        /// The chart instrument (primary instrument) trading hours.
        /// The trading hours is configured by the user in "Traging Hours Template" UI.
        /// </summary>
        string TradingHoursName { get; set; }

    }
}
