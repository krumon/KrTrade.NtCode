namespace KrTrade.NtCode.Options
{
    /// <summary>
    /// Represents something that configures the TOptions type.
    /// </summary>
    /// <typeparam name="TOptions"></typeparam>
    public interface IConfigureOptions<in TOptions> 
        where TOptions : class
    {
        /// <summary>
        /// Invoked to configure a TOptions instance.
        /// </summary>
        /// <param name="options">The options instance to configure.</param>
        void Configure(TOptions options);
    }
}
