namespace KrTrade.NtCode.Hosting
{

    /// <summary>
    /// Defines methods that are necesary to configure the service whe data is loaded.
    /// </summary>
    public interface IDataLoadedService
    {
        /// <summary>
        /// Method to configure the service when the data is loaded.
        /// </summary>
        /// <param name="ninjascriptObjects">The necesary objects to configure the service.</param>
        void DataLoaded(object[] ninjascriptObjects);

        /// <summary>
        /// Indicates if the service has been configured successfully when the ninjascript data is loaded. 
        /// </summary>
        bool IsDataLoaded { get; }
    }
}
