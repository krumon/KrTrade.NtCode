namespace KrTrade.Nt.DI.Logging.Debug
{
    /// <summary>
    /// The provider for the <see cref="DebugLogger"/>.
    /// </summary>
    [ProviderAlias("Delegate")]
    public class DelegateLoggerProvider : ILoggerProvider
    {
        /// <inheritdoc />
        public ILogger CreateLogger(string name)
        {
            return new DebugLogger(name);
        }

        public void Dispose()
        {
        }
    }
}
