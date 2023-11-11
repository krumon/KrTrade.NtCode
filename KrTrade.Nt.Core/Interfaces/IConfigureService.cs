namespace KrTrade.Nt.Core.Interfaces
{
    /// <summary>
    /// Defines methods that are necesary to be executed to configure the service.
    /// </summary>
    public interface IConfigureService
    {
        /// <summary>
        /// Method to configure the service.
        /// </summary>
        /// <param name="ninjascriptObjects">The necesary objects to configure the service.</param>
        void Configure();
    }
}
