using System.Threading;

namespace KrTrade.NtCode.Hosting
{
    //
    // Resumen:
    //     Allows consumers to be notified of application lifetime events. This interface
    //     is not intended to be user-replaceable.
    public interface IHostApplicationLifetime
    {
        //
        // Resumen:
        //     Triggered when the application host has fully started.
        CancellationToken ApplicationStarted { get; }

        //
        // Resumen:
        //     Triggered when the application host is starting a graceful shutdown. Shutdown
        //     will block until all callbacks registered on this token have completed.
        CancellationToken ApplicationStopping { get; }

        //
        // Resumen:
        //     Triggered when the application host has completed a graceful shutdown. The application
        //     will not exit until all callbacks registered on this token have completed.
        CancellationToken ApplicationStopped { get; }

        //
        // Resumen:
        //     Requests termination of the current application.
        void StopApplication();
    }
}
