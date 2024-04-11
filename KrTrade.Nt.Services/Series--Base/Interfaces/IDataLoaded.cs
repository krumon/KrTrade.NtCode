namespace KrTrade.Nt.Services.Series
{
    /// <summary>
    /// Defines methods that are necesary to be executed to configure the series when data is loaded.
    /// </summary>
    public interface IDataLoadedSeries
    {
        /// <summary>
        /// Method to configure the series when ninjatrader data is loaded.
        /// </summary>
        /// <param name="barsService">The service necesary to configure the series.</param>
        void DataLoaded(IBarsService barsService);

    }
}
