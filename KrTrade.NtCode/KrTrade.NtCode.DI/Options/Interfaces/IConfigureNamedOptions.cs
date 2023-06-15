namespace Nt.Core.Options
{
    /// <summary>
    /// Represents something that configures the TOptions type.
    /// </summary>
    /// <typeparam name="TOptions"></typeparam>
    public interface IConfigureNamedOptions<in TOptions> : IConfigureOptions<TOptions> where TOptions : class
    {
        /// <summary>
        /// Invoked to configure a TOptions instance.
        /// </summary>
        /// <param name="name">The name of the options instance being configured.</param>
        /// <param name="options">The options instance to configure.</param>
        void Configure(string name, TOptions options);
    }
}
