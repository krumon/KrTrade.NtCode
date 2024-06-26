namespace KrTrade.Nt.Core.Services
{
    /// <summary>
    /// Defines properties and methods that are necesary for ninjascript <see cref="IBarUpdate"/> services.
    /// </summary>
    public interface IBarUpdateService<TInfo,TOptions> : IService<TInfo,TOptions>, IBarUpdate
        where TInfo : IServiceInfo
        where TOptions : IServiceOptions
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

}
