namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Used to create TOptions instances.
    /// </summary>
    /// <typeparam name="TOptions">The type of options being requested.</typeparam>
    public interface IOptionsFactory<TOptions> where TOptions : class
    {
        /// <summary>
        /// Returns a configured TOptions instance with the given name.
        /// </summary>
        /// <param name="name">The name of the configured TOptions.</param>
        /// <returns></returns>
        TOptions Create(string name);
    }
}
