using KrTrade.NtCode.Data;
using KrTrade.NtCode.DependencyInjection;

namespace KrTrade.NtCode.Services
{
    /// <summary>
    /// Represents a default implementation of data series descriptor collection.
    /// </summary>
    public class DataSeriesCollection : BaseServiceCollection<DataSeriesDescriptor>, IDataSeriesCollection
    {

        #region Implementation methods

        /// <inheritdoc/>
        public override void Add(DataSeriesDescriptor item)
        {
            if (item == null)
                return;
            if (!Exist(item))
                base.Add(item);
        }

        /// <inheritdoc/>
        public IDataSeriesCollection AddDataSerie(InstrumentCode instrumentKey, PeriodType periodType, int periodValue, TradingHoursCode tradingHoursKey)
        {
            DataSeriesDescriptor descriptor = new DataSeriesDescriptor(instrumentKey, periodType, periodValue, tradingHoursKey);
            Add(descriptor);
            return this;
        }

        /// <inheritdoc/>
        public IDataSeriesCollection AddDataSerie(PeriodType periodType, int periodValue) =>
            AddDataSerie(InstrumentCode.Default, periodType, periodValue, TradingHoursCode.Default);

        /// <inheritdoc/>
        public IDataSeriesCollection AddDataSerie(InstrumentCode instrumentKey, PeriodType periodType, int periodValue) =>
            AddDataSerie(instrumentKey, periodType, periodValue, TradingHoursCode.Default);

        #endregion

    }
}
