using KrTrade.Nt.DI.Attributes;
using KrTrade.Nt.DI.Logging;
using KrTrade.Nt.DI.Logging.Abstractions;
using KrTrade.Nt.DI.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KrTrade.Nt.DI.Hosting.Internal
{
    /// <summary>
    /// Listens for Ctrl+C or SIGTERM and initiates shutdown.
    /// </summary>
    [UnsupportedOSPlatform("android")]
    [UnsupportedOSPlatform("browser")]
    [UnsupportedOSPlatform("ios")]
    [UnsupportedOSPlatform("tvos")]
    public partial class ConsoleLifetime : IHostLifetime, IDisposable
    {
        private CancellationTokenRegistration _applicationStartedRegistration;
        private CancellationTokenRegistration _applicationStoppingRegistration;
        private readonly ManualResetEvent _shutdownBlock = new ManualResetEvent(false);

        public ConsoleLifetime(IOptions<ConsoleLifetimeOptions> options, IHostEnvironment environment, IHostApplicationLifetime applicationLifetime, IOptions<HostOptions> hostOptions)
            : this(options, environment, applicationLifetime, hostOptions, NullLoggerFactory.Instance) { }

        public ConsoleLifetime(IOptions<ConsoleLifetimeOptions> options, IHostEnvironment environment, IHostApplicationLifetime applicationLifetime, IOptions<HostOptions> hostOptions, ILoggerFactory loggerFactory)
        {
            Options = options?.Value ?? throw new ArgumentNullException(nameof(options));
            Environment = environment ?? throw new ArgumentNullException(nameof(environment));
            ApplicationLifetime = applicationLifetime ?? throw new ArgumentNullException(nameof(applicationLifetime));
            HostOptions = hostOptions?.Value ?? throw new ArgumentNullException(nameof(hostOptions));
            Logger = loggerFactory.CreateLogger("Microsoft.Host.Lifetime");
        }

        private ConsoleLifetimeOptions Options { get; }

        private IHostEnvironment Environment { get; }

        private IHostApplicationLifetime ApplicationLifetime { get; }

        private HostOptions HostOptions { get; }

        private ILogger Logger { get; }

        public Task WaitForStartAsync(CancellationToken cancellationToken)
        {
            if (!Options.SuppressStatusMessages)
            {
                _applicationStartedRegistration = ApplicationLifetime.ApplicationStarted.Register(state =>
                {
                    ((ConsoleLifetime)state).OnApplicationStarted();
                },
                this);
                _applicationStoppingRegistration = ApplicationLifetime.ApplicationStopping.Register(state =>
                {
                    ((ConsoleLifetime)state).OnApplicationStopping();
                },
                this);
            }

            RegisterShutdownHandlers();

            // Console applications start immediately.
            return Task.CompletedTask;
        }

        private void RegisterShutdownHandlers()
        {
            AppDomain.CurrentDomain.ProcessExit += OnProcessExit;
            Console.CancelKeyPress += OnCancelKeyPress;
        }


        private void OnApplicationStarted()
        {
            Logger.LogInformation("Application started. Press Ctrl+C to shut down.");
            Logger.LogInformation("Host environment: {envName}", Environment.EnvironmentName);
            Logger.LogInformation("Content root path: {contentRoot}", Environment.ContentRootPath);
        }

        private void OnApplicationStopping()
        {
            Logger.LogInformation("Application is shutting down...");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            // There's nothing to do here
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            UnregisterShutdownHandlers();

            _applicationStartedRegistration.Dispose();
            _applicationStoppingRegistration.Dispose();
        }

        private void UnregisterShutdownHandlers()
        {
            _shutdownBlock.Set();

            AppDomain.CurrentDomain.ProcessExit -= OnProcessExit;
            Console.CancelKeyPress -= OnCancelKeyPress;
        }

        private void OnCancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            e.Cancel = true;
            ApplicationLifetime.StopApplication();

            // Don't block in process shutdown for CTRL+C/SIGINT since we can set e.Cancel to true
            // we assume that application code will unwind once StopApplication signals the token
            _shutdownBlock.Set();
        }

        private void OnProcessExit(object sender, EventArgs e)
        {
            ApplicationLifetime.StopApplication();
            if (!_shutdownBlock.WaitOne(HostOptions.ShutdownTimeout))
            {
                Logger.LogInformation("Waiting for the host to be disposed. Ensure all 'IHost' instances are wrapped in 'using' blocks.");
            }

            // wait one more time after the above log message, but only for ShutdownTimeout, so it doesn't hang forever
            _shutdownBlock.WaitOne(HostOptions.ShutdownTimeout);

            // On Linux if the shutdown is triggered by SIGTERM then that's signaled with the 143 exit code.
            // Suppress that since we shut down gracefully. https://github.com/dotnet/aspnetcore/issues/6526
            System.Environment.ExitCode = 0;
        }
    }
}
