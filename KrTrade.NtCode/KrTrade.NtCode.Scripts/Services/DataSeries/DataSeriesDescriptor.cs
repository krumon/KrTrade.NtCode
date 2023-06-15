using Nt.Core.Data;

namespace KrTrade.NtCode.Services
{
    /// <summary>
    /// Represents a ninjascript instrument.
    /// </summary>
    public class DataSeriesDescriptor
    {
        /// <summary>
        /// The <see cref="DataSeries"/> instrument name.
        /// </summary>
        public string InstrumentName { get; set; }

        /// <summary>
        /// The <see cref="DataSeries"/> <see cref="PeriodType"/>.
        /// </summary>
        public PeriodType PeriodType { get; set; }

        /// <summary>
        /// The <see cref="DataSeries"/> period value.
        /// </summary>
        public int PeriodValue { get; set; }

        /// <summary>
        /// The <see cref="DataSeries"/> trading hours template name.
        /// </summary>
        public string TradingHoursName { get; set; }

        /// <summary>
        /// The unique key of the data serie.
        /// </summary>
        public string Key => $"{InstrumentName}-{PeriodType}{PeriodValue}-{TradingHoursName}";

        public override string ToString()
        {
            return $"DataSerie[{InstrumentName}] : {PeriodValue}({PeriodType}) - {TradingHoursName}.";
        }
    }
}
