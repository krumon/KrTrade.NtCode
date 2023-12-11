using NinjaTrader.Data;
using System.Collections.Generic;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Defines properties and methods to be executed in 'Ninjatrader.NinjaScript' primary data series.
    /// </summary>
    public interface IDataSeriesService : INinjascriptService<DataSeriesOptions>
    {
        /// <summary>
        /// Method to be executed when 'Ninjatrader.CurrentBar' is updated.        
        /// </summary>
        void OnBarUpdate();

        /// <summary>
        /// Method to be executed when 'Ninjatrader.CurrentBar' is updated and is not necesary pass previous filters.        
        /// </summary>
        void Update();

        /// <summary>
        /// <see cref="IBarUpdateService"/> collection to be executed when 'Ninjatrader.CurrentBar' is updated.
        /// </summary>
        IList<IBarUpdateService> Services { get; }

        /// <summary>
        /// Gets or sets the trading hours name of the bars series.
        /// </summary>
        string TradingHoursName { get; }

        /// <summary>
        /// Gets or sets the market data type of the bars series.
        /// </summary>
        Core.Data.MarketDataType MarketDataType { get; }

        /// <summary>
        /// Gets the instument name.
        /// </summary>
        string InstrumentName { get; }

        /// <summary>
        /// The bars period of the DataSeries.
        /// </summary>
        BarsPeriod BarsPeriod { get; }

        /// <summary>
        /// Gets the bars index in the 'NinjaScript.BarsArray'.
        /// </summary>
        int Idx { get; }

        /// <summary>
        /// Gets the last bar of the bars in progress.
        /// </summary>
        LastBarService LastBar { get; }

        /// <summary>
        /// Gets the last bar of the bars in progress.
        /// </summary>
        LastBarService CurrentBar { get; }

    }
}
