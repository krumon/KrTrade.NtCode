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
        /// <param name="barsService">The <see cref="IBarsService"/> thats content <see cref="LastBarService"/></param>
        public LastBarService(IBarsService barsService) : base(barsService) // base(barsService, 0, 0)
        {
        }
    }
}
