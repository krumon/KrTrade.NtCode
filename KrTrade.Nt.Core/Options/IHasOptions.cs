namespace KrTrade.Nt.Core.Options
{
    public interface IHasOptions
    {
        /// <summary>
        /// Gets the options of the object.
        /// </summary>
        IOptions Options { get; }
    }
    public interface IHasOptions<TOptions>
        where TOptions : IOptions
    {
        /// <summary>
        /// Gets the options of the object.
        /// </summary>
        TOptions Options { get; }
    }
}
