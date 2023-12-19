namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Represents the service of data series last bar.
    /// </summary>
    public class LastBarService : BarService
    {
        /// <summary>
        /// Create <see cref="LastBarService"/> instance.
        /// </summary>
        /// <param name="barsService">The <see cref="IDataSeriesService"/> thats content <see cref="LastBarService"/></param>
        public LastBarService(IDataSeriesService barsService) : base(barsService, 0, 0)
        {
        }
    }
}
