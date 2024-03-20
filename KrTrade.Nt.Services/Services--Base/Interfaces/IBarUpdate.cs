namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Defines methods that are necesary to be executed when the bar is updated.
    /// </summary>
    public interface IBarUpdate
    {

        /// <summary>
        /// Method to be executed to update the service.
        /// </summary>
        void Update();

        /// <summary>
        /// Method to be executed to update the service when a <see cref="IBarsService"/> is updated.
        /// </summary>
        /// <param name="updatedBarsSeries"The ><see cref="IBarSeries"/> updated.<see cref="IBarsService"/> updated.</param>
        void Update(IBarsService updatedBarsSeries);

    }
}
