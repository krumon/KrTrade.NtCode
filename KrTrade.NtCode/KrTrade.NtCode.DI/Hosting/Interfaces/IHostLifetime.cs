using System.Threading;
using System.Threading.Tasks;

namespace KrTrade.NtCode.Hosting
{
    public interface IHostLifetime
    {
        /// <summary>
        /// Called at the start of Microsoft.Extensions.Hosting.IHost.StartAsync(System.Threading.CancellationToken)
        /// which will wait until it's complete before continuing. This can be used to delay
        /// startup until signaled by an external event.
        /// </summary>
        /// <param name="cancellationToken">Used to abort program start.</param>
        /// <returns>A <see cref="Task"/>.</returns>
        Task WaitForStartAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Called from IHost.StopAsync(System.Threading.CancellationToken)
        /// to indicate that the host is stopping and it's time to shut down.
        /// </summary>
        /// <param name="cancellationToken">Used to indicate when stop should no longer be graceful.</param>
        /// <returns>A <see cref="Task"/>.</returns>
        Task StopAsync(CancellationToken cancellationToken);

    }
}
