namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Defines methods that are necesary to render the ninjascript.
    /// </summary>
    public interface IRender
    {
        /// <summary>
        /// Render the ninjascript when 'NinjaScript.OnRended' is called.
        /// </summary>
        /// <param name="barsInProgress">The bars in progress index.</param>
        void Render(int barsInProgress = 0);

        /// <summary>
        /// Method to be executed to render the ninjascript when a <see cref="IBarsService"/> is rendering.
        /// </summary>
        /// <param name="updatedBarsSeries"><see cref="IBarsService"/> updated.</param>
        /// <param name="barsInProgress">The bars in progress index.</param>
        void Render(IBarsService updatedBarsSeries, int barsInProgress = 0);
    }
}
