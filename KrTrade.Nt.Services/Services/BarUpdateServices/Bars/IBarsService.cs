using NinjaTrader.Data;
using System.Collections.Generic;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Defines properties and methods to be executed in 'Ninjatrader.NinjaScript' primary data series.
    /// </summary>
    public interface IBarsService : INinjascriptService<BarsOptions>
    {
        /// <summary>
        /// Method to be executed when 'Ninjatrader.CurrentBar' is updated.        
        /// </summary>
        void OnBarUpdate();

        /// <summary>
        /// Method to be executed when 'Ninjatrader.CurrentBar' is closed.
        /// </summary>
        void OnBarClosed();

        /// <summary>
        /// Method to be executed when new tick success in 'Ninjatrader.CurrentBar'.
        /// </summary>
        void OnTick();

        /// <summary>
        /// Method to be executed when first tick success in 'Ninjatrader.CurrentBar'.
        /// </summary>
        void OnFirstTick();

        /// <summary>
        /// Method to be executed when 'Ninjatrader.LastBar' is removed.
        /// </summary>
        void OnLastBarRemoved();

        /// <summary>
        /// Method to be executed when <see cref="IBarsService"/> current bar price changed.
        /// </summary>
        void OnPriceChanged();

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
