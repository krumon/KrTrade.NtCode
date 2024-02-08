namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Defines properties and methods for any ninjascript service.
    /// </summary>
    public interface INinjascriptService : IService, IConfigure, IDataLoaded
    {
        /// <summary>
        /// Gets the options of the service.
        /// </summary>
        new NinjascriptServiceOptions Options {  get; }

        /// <summary>
        /// Get the <see cref="IPrintService"/> for print in 'Ninjatrader.Output.Window'.
        /// </summary>
        IPrintService PrintService { get; }

        /// <summary>
        /// Indicates that the service is enabled for printing on NinjaScript output window.
        /// </summary>
        bool IsLogEnable { get; set; }

        ///// <summary>
        ///// Print in NinjaScript putput window the configuration state. If the configuration has been ok or error.
        ///// </summary>
        //void LogConfigurationState();

    }
    /// <summary>
    /// Defines properties and methods for any ninjascript service.
    /// </summary>
    public interface INinjascriptService<TOptions> : INinjascriptService
        where TOptions : NinjascriptServiceOptions, new()
    {
        /// <summary>
        /// Gets the options of the service.
        /// </summary>
        new TOptions Options { get; }
    }
}
