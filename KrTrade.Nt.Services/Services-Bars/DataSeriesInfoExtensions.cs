using KrTrade.Nt.Core.Data;
using KrTrade.Nt.Services;

namespace KrTrade.Nt.Core.Extensions
{

    /// <summary>
    /// Helper methods of <see cref="NinjascriptDataSeriesInfo"/> structure.
    /// </summary>
    public static class DataSeriesInfoExtensions
    {
        /// <summary>
        /// Converts tha actual object to <see cref="BarsServiceInfo"/> object.
        /// </summary>
        /// <returns>The <see cref="BarsServiceInfo"/> object with ninjascript data series values.</returns>
        public static BarsServiceInfo ToBarsServiceInfo(this NinjascriptDataSeriesInfo info)
        {
            return new BarsServiceInfo
            {
                InstrumentCode = info.InstrumentName.ToInstrumentCode(),
                TimeFrame = info.BarsPeriod.ToTimeFrame(),
                TradingHoursCode = info.TradingHoursName.ToTradingHoursCode(),
                MarketDataType = info.BarsPeriod.MarketDataType.ToKrMarketDataType(),
            };
        }
    }
}
