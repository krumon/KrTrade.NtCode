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
        void Render();

        /// <summary>
        /// Method to be executed to render the ninjascript when a <see cref="IBarsService"/> is rendering.
        /// </summary>
        /// <param name="updatedBarsSeries"><see cref="IBarsService"/> updated.</param>
        void BarsSeriesRender(IBarsService updatedBarsSeries);
    }
}
