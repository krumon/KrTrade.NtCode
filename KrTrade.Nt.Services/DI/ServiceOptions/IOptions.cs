namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Used to retrieve configured TOptions instances.
    /// </summary>
    /// <typeparam name="TOptions">The type of options being requested.</typeparam>
    public interface IOptions<out TOptions> where TOptions : class
    {
        /// <summary>
        /// The default configured TOptions instance
        /// </summary>
        TOptions Value { get; }
    }
}
