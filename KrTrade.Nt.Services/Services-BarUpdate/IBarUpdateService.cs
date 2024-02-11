namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Defines methods that are necesary to be executed when the bar is updated.
    /// </summary>
    public interface IBarUpdateService : INinjascriptService
    {

        /// <summary>
        /// Gets <see cref="IBarUpdateService"/> period.
        /// </summary>
        int Period { get; }

        /// <summary>
        /// Gets the displacement of <see cref="IBarUpdateService"/> respect 'NinjaScript.Series'./>.
        /// </summary>
        int Displacement { get; }

        /// <summary>
        /// The <see cref="IBarsService"/> necesary to execute the <see cref="IBarUpdateService"/>.
        /// </summary>
        IBarsService Bars { get; }

        /// <summary>
        /// Method to be executed to update the service.
        /// </summary>
        void Update();

    }
}
