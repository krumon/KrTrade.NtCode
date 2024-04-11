namespace KrTrade.Nt.Services.Series
{
    /// <summary>
    /// Defines methods that are necesary to be executed to configure the series.
    /// </summary>
    public interface IConfigureSeries
    {
        /// <summary>
        /// Method to configure the series when 'Ninjatrader.NinjaScript.State' is equal to 'Configure'.
        /// </summary>
        /// <param name="barsService">The service necesary to configure the series.</param>
        void Configure(IBarsService barsService);

    }
}
