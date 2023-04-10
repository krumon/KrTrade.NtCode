namespace Nt.Core.Hosting
{
    /// <summary>
    /// Specifies a behavior that the <see cref="IHost"/> will honor if
    /// an unhandled exception occurs in one of its BackgroundService instances.
    /// </summary>
    public enum BackgroundServiceExceptionBehavior
    {
        /// <summary>
        /// Stops the <see cref="IHost"/> instance.
        /// </summary>
        /// <remarks>
        /// If a Microsoft.Extensions.Hosting.BackgroundService throws an exception, the
        /// <see cref="IHost"/> instance stops, and the process continues.
        /// </remarks>
        StopHost,
        /// <summary>
        /// Ignore exceptions thrown in BackgroundService.
        /// </summary>
        /// <remarks>
        /// If a Hosting.BackgroundService throws an exception, the <see cref="IHost"/>
        /// will log the error, but otherwise ignore it. The BackgroundService is not restarted.
        /// </remarks>
        Ignore
    }
}
