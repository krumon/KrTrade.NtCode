//namespace KrTrade.Nt.Services
//{
//    /// <summary>
//    /// Defines properties and methods that are necesary for ninjascript <see cref="IBarUpdate"/> services.
//    /// </summary>
//    public interface IBarUpdateService : INinjascriptService, IBarUpdate
//    {

//        /// <summary>
//        /// Gets the index of the bars thats raised the updated signal.
//        /// </summary>
//        int BarsIndex { get; }

//        /// <summary>
//        /// The <see cref="IBarsService"/> necesary to execute the <see cref="IBarUpdateService{TOptions}"/>.
//        /// </summary>
//        IBarsService Bars { get; }

//    }

//    /// <summary>
//    /// Defines properties and methods that are necesary for ninjascript <see cref="IBarUpdate"/> services.
//    /// </summary>
//    public interface IBarUpdateService<TInfo> : IBarUpdateService, INinjascriptService<TInfo>
//        where TInfo : BarUpdateServiceInfo, new()
//    {
//    }

//    /// <summary>
//    /// Defines properties and methods that are necesary for ninjascript <see cref="IBarUpdate"/> services.
//    /// </summary>
//    public interface IBarUpdateService<TInfo,TOptions> : IBarUpdateService<TInfo>, INinjascriptService<TInfo,TOptions>
//        where TInfo : BarUpdateServiceInfo, new()
//        where TOptions : BarUpdateServiceOptions, new()
//    {
//    }
//}
