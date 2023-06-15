namespace KrTrade.NtCode.DependencyInjection
{
    /// <summary>
    /// Default implememntation of service descriptor.
    /// </summary>
    public interface IServiceDescriptor
    {
        /// <summary>
        /// Gets the unique key of the service descriptor.
        /// </summary>
        string Key { get; }

    }
}
