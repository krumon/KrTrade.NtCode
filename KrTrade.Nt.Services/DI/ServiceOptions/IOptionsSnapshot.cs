namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Used to access the value of TOptions for the lifetime of a request.
    /// </summary>
    /// <typeparam name="TOptions">Options type.</typeparam>
    public interface IOptionsSnapshot<out TOptions> : IOptions<TOptions> 
        where TOptions : class
    {
        /// <summary>
        /// Returns a configured TOptions instance with the given name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        TOptions Get(string name);
    }
}
