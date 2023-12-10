namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Defines interfaces that are necesary to log the services.
    /// </summary>
    public interface IHasPrintService
    {
        /// <summary>
        /// <see cref="IPrintService"/> to print in 'Ninjatrader.OutputWindow'.
        /// </summary>
        IPrintService PrintService { get; }

    }
}
