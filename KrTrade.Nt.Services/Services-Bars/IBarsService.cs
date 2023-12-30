using KrTrade.Nt.Core.Bars;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Defines methods that are necesary to be executed when the bar is updated.
    /// </summary>
    public interface IBarsService : INinjascriptService<BarsOptions>
    {

        ///// <summary>
        ///// Gets <see cref="BarsCache"/> used by the <see cref="IBarsService"/> with the last bars information.
        ///// </summary>
        //BarsCache Cache { get; }

        /// <summary>
        /// Gets the index of the data series to which it belongs. 
        /// </summary>
        int ParentBarsIdx { get; }

        /// <summary>
        /// Gets the current <see cref="BarDataModel"/> of the <see cref="IBarsService"/>.
        /// </summary>
        BarDataModel CurrentBar { get; }

        /// <summary>
        /// Indicates <see cref="IBarsService"/> is updated.
        /// </summary>
        bool IsUpdated {get;} 

        /// <summary>
        /// Indicates the last bar of 'Ninjatrader.ChartBars' is closed.
        /// </summary>
        bool IsLastBarClosed {get;}

        /// <summary>
        /// Indicates the last bar of 'Ninjatrader.ChartBars' is removed.
        /// </summary>
        bool IsLastBarRemoved {get;}

        /// <summary>
        /// Indicates new tick success in 'Ninjatrader.ChartBars'.
        /// If calculate mode is 'BarClosed', this value is always false.
        /// </summary>
        bool NewTick {get;}

        /// <summary>
        /// Indicates first tick success in 'Ninjatrader.ChartBars'.
        /// If calculate mode is 'BarClosed', this value is always false.
        /// </summary>
        bool FirstTick {get;}

        /// <summary>
        /// Indicates new price success in 'Ninjatrader.ChartBars'.
        /// If calculate mode is 'BarClosed', this value is unique true when a gap success between two bars.
        /// </summary>
        bool NewPrice {get;}

        /// <summary>
        /// Method to be executed when 'Ninjatrader.ChartBars' is updated.
        /// </summary>
        void Update();

        /// <summary>
        /// Adds new <see cref="IBarUpdateService"/> to <see cref="IBarsService"/>.
        /// This services needs <see cref="IBarsService"/> to be executed because they are executed in <see cref="IBarsService"/>
        /// after the bars have been updated.
        /// </summary>
        void Add(IBarUpdateService service);

    }
}
