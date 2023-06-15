namespace KrTrade.NtCode.Logging
{
    /// <summary>
    /// Represents a <see cref="ILoggerProvider"/> that is able to consume external scope information.
    /// </summary>
    public interface ISupportExternalScope
    {
        /// <summary>
        /// Sets external scope information source for logger provider.
        /// </summary>
        /// <param name="scopeProvider">The provider of scope data.</param>
        void SetScopeProvider(IExternalScopeProvider scopeProvider);
    }
}
