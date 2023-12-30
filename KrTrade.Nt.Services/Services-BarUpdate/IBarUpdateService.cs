namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Defines methods that are necesary to be executed when the bar is updated.
    /// </summary>
    public interface IBarUpdateService : INinjascriptService
    {

        /// <summary>
        /// Gets the number of bars of the service. 
        /// </summary>
        int Period { get; }

        /// <summary>
        /// Gets the bars ago of the service respect <see cref="IBarsService"/>. 
        /// The most recent bar is 0.
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
