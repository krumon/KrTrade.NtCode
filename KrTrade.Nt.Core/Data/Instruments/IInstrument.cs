namespace KrTrade.Nt.Core.Data
{
    public interface IInstrument
    {

        /// <summary>
        /// The instrument unique code.
        /// </summary>
        InstrumentCode Code { get; }

        /// <summary>
        /// The market exchange owner of the instrument.
        /// </summary>
        MarketExchange MarketExchange { get; }

        /// <summary>
        /// Gets the instument name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the instrument description.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Gets or sets the instrument point value.
        /// </summary>
        double PointValue { get; }

        /// <summary>
        /// Gets or sets the instrument tick size.
        /// </summary>
        double TickSize { get; }

    }
}
