namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Defines methods that are necesary to be executed when the bar is updated.
    /// </summary>
    public interface IBarUpdateService
    {
        /// <summary>
        /// The <see cref="IBarsService"/> necesary to execute the <see cref="IBarUpdateService"/> services.
        /// </summary>
        IBarsService BarsService { get; }

        /// <summary>
        /// Method to be executed when <see cref="IBarsService"/> current bar is closed.
        /// </summary>
        void Update();

        /// <summary>
        /// Print in NinjaScript putput window the configuration state. If the configuration has been ok or error.
        /// </summary>
        void LogUpdatedState();


    }
}
