using KrTrade.Nt.Core.Data;
using KrTrade.Nt.Core.DataSeries;
using KrTrade.Nt.DI.DependencyInjection;

namespace KrTrade.Nt.DI.Services
{
    /// <summary>
    /// Represents default implementation of <see cref="DataSeriesDescriptor"/>.
    /// </summary>
    public interface IDataSeriesDescriptor : IServiceDescriptor
    {

        /// <summary>
        /// Gets or sets the instrument key.
        /// </summary>
        InstrumentCode InstrumentKey { get; }

        /// <summary>
        /// Gets or sets the series period type.
        /// </summary>
        PeriodType PeriodType { get; set; }

        /// <summary>
        /// Gets or sets the series period value.
        /// </summary>
        int PeriodValue { get;set; }

        /// <summary>
        /// Gets or sets the trading hours key.
        /// </summary>
        TradingHoursCode TradingHoursKey { get; set; }

    }
}
