using KrTrade.Nt.DI.Data;

namespace KrTrade.Nt.DI.Services
{
    /// <summary>
    /// Represents default implementation of <see cref="DataSeriesCollection"/>.
    /// </summary>
    public interface IDataSeriesCollection //: IServiceCollection<IDataSeriesDescriptor>
    {

        /// <summary>
        /// Adds <see cref="DataSeriesDescriptor"/> to <see cref="DataSeriesCollection"/>.
        /// </summary>
        /// <param name="instrumentKey">The data serie instrument key.</param>
        /// <param name="periodType">The data serie period type.</param>
        /// <param name="periodValue">The data serie period value.</param>
        /// <param name="tradingHoursKey">The data serie trading hours template.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        IDataSeriesCollection AddDataSerie(InstrumentCode instrumentKey, PeriodType periodType, int periodValue, TradingHoursCode tradingHoursKey);

        /// <summary>
        /// Adds <see cref="DataSeriesDescriptor"/> to <see cref="DataSeriesCollection"/>.
        /// </summary>
        /// <param name="periodType">The data serie period type.</param>
        /// <param name="periodValue">The data serie period value.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        IDataSeriesCollection AddDataSerie(PeriodType periodType, int periodValue);

        /// <summary>
        /// Adds <see cref="DataSeriesDescriptor"/> to <see cref="DataSeriesCollection"/>.
        /// </summary>
        /// <param name="instrumentKey">The data serie instrument key.</param>
        /// <param name="periodType">The data serie period type.</param>
        /// <param name="periodValue">The data serie period value.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        IDataSeriesCollection AddDataSerie(InstrumentCode instrumentKey, PeriodType periodType, int periodValue);

    }
}
