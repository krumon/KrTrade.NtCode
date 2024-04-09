namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Defines properties and methods that are necesary for ninjascript <see cref="IBarUpdate"/> services.
    /// </summary>
    public interface IBarUpdateService : INinjascriptService, IBarUpdate
    {

        /// <summary>
        /// Gets the index of the bars thats raised the updated signal.
        /// </summary>
        int BarsIndex { get; }

        /// <summary>
        /// The <see cref="IBarsService"/> necesary to execute the <see cref="IBarUpdateService{TOptions}"/>.
        /// </summary>
        IBarsService Bars { get; }

    }

    /// <summary>
    /// Defines properties and methods that are necesary for ninjascript <see cref="IBarUpdate"/> services.
    /// </summary>
    public interface IBarUpdateService<TOptions> : INinjascriptService<TOptions>, IBarUpdateService, IBarUpdate
        where TOptions : BarUpdateServiceOptions, new()
    {
    }
}
