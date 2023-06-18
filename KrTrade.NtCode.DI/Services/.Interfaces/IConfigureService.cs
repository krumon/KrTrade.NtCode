namespace KrTrade.Nt.DI.Hosting
{

    /// <summary>
    /// Defines methods that are necesary to configure the service.
    /// </summary>
    public interface IConfigureService
    {
        /// <summary>
        /// Method to configure the service.
        /// </summary>
        /// <param name="ninjascriptObjects">The necesary objects to configure the service.</param>
        void Configure(object[] ninjascriptObjects);

        /// <summary>
        /// Indicates if the service has been configured successfully. 
        /// </summary>
        bool IsConfigured { get; }

    }
}
