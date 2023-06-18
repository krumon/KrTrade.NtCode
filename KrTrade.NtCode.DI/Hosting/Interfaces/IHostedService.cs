using System.Threading;
using System.Threading.Tasks;

namespace KrTrade.Nt.DI.Hosting
{

    /// <summary>
    /// Defines methods for objects that are managed by the host.
    /// </summary>
    public interface IHostedService : IConfigureService, IDataLoadedService
    {
        #region Original properties and methods

        //
        // Resumen:
        //     Triggered when the application host is ready to start the service.
        //
        // Parámetros:
        //   cancellationToken:
        //     Indicates that the start process has been aborted.
        //Task StartAsync(CancellationToken cancellationToken);

        //
        // Resumen:
        //     Triggered when the application host is performing a graceful shutdown.
        //
        // Parámetros:
        //   cancellationToken:
        //     Indicates that the shutdown process should no longer be graceful.
        //Task StopAsync(CancellationToken cancellationToken);

        #endregion

        /// <summary>
        /// Triggered when the application host is performing a graceful shutdown.
        /// </summary>
        void Dispose();

    }
}
