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
        ///// <param name="barsInProgress">The bars in progress index.</param>
        void BarUpdate();

        /// <summary>
        /// Method to be executed to update the service when a <see cref="IBarsService"/> is updated.
        /// </summary>
        /// <param name="updatedBarsService">The <see cref="IBarsService"/> updated.</param>
        void BarUpdate(IBarsService updatedBarsService);

    }
}
