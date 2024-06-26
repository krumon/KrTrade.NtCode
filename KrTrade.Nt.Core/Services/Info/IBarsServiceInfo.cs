using KrTrade.Nt.Core.Data;
using NinjaTrader.NinjaScript;

namespace KrTrade.Nt.Core.Services
{
    /// <summary>
    /// Defines the information of the service.
    /// </summary>
    public interface IBarsServiceInfo : IServiceInfo
    {
        /// <summary>
        /// Gets or sets the Data Series instrument code.
        /// </summary>
        InstrumentCode InstrumentCode { get; set; }

        /// <summary>
        /// Gets or sets data series trading hours code.
        /// </summary>
        TradingHoursCode TradingHoursCode { get; set; }

        /// <summary>
        /// Gets or sets data series time frame.
        /// </summary>
        TimeFrame TimeFrame { get; set; }

        /// <summary>
        /// Gets or sets data series market data type.
        /// </summary>
        MarketDataType MarketDataType { get; set; }

        /// <summary>
        /// Indicates the actual object is default instance.
        /// </summary>
        bool IsDefault { get; }

        /// <summary>
        /// Sets into <see cref="IBarsServiceInfo"/> the 'NinjaScript' values.
        /// </summary>
        /// <param name="ninjascript"></param>
        /// <param name="index"></param>
        void SetNinjascriptValues(NinjaScriptBase ninjascript, int index);

        /// <summary>
        /// Compare the actual service values with 'NinjaScript' DataSeries.
        /// </summary>
        /// <param name="ninjascript"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        bool EqualsTo(NinjaScriptBase ninjascript, int index);

    }
}
