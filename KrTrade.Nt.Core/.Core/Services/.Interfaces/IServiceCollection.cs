using KrTrade.Nt.Core.Data;

namespace KrTrade.Nt.Core
{

    /// <summary>
    /// Defines properties and methods for any ninjascript service collection.
    /// </summary>
    public interface IServiceCollection<TElement> : ICollection<TElement, ServiceType, IServiceInfo, IServiceCollectionInfo, ServiceCollectionType>, IConfigure, IDataLoaded, ITerminated
        where TElement : IService
    {
        /// <summary>
        /// Gets the options of the service.
        /// </summary>
        IServiceOptions Options { get; }

        /// <summary>
        /// Gets the type of the service.
        /// </summary>
        new ServiceCollectionType Type { get; }

        /// <summary>
        /// Indicates that the service is enabled.
        /// </summary>
        bool IsEnable { get; }

        /// <summary>
        /// Indicates that the logger service is enabled.
        /// </summary>
        bool IsLogEnable { get; }

        /// <summary>
        /// Print in NinjaScript output winw the log string.
        /// </summary>
        void Log();

        /// <summary>
        /// Print in NinjaScript output window the log string.
        /// </summary>
        /// <param name="tabOrder">The number of tabulation strings to insert in the log string.</param>
        void Log(int tabOrder);

    }
}
