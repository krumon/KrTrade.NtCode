using KrTrade.Nt.DI.Services;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System;

namespace KrTrade.Nt.DI.Data
{
    public class Instrument
    {

        #region Private members

        private readonly InstrumentCode _instrumentKey;

        #endregion

        #region Public properties

        /// <summary>
        /// The instrument unique code.
        /// </summary>
        public InstrumentCode Key => _instrumentKey;

        /// <summary>
        /// The market exchange owner of the instrument.
        /// </summary>
        public MarketExchange MarketExchange => _instrumentKey.ToMarketExchange();

        /// <summary>
        /// The trading hours key.
        /// </summary>
        public TradingHoursCode TradingHoursKey { get; set; } = TradingHoursCode.Default;

        /// <summary>
        /// Gets the instument name.
        /// </summary>
        public string Name => _instrumentKey.ToString();

        /// <summary>
        /// Gets the instrument description.
        /// </summary>
        public string Description => _instrumentKey.ToDescription();

        /// <summary>
        /// Gets the default trading hours name.
        /// </summary>
        public string TradingHoursName
        {
            get
            {
                if (TradingHoursKey == TradingHoursCode.Default)
                    TradingHoursKey = _instrumentKey.ToDefaultTradingHoursKey();

                return TradingHoursKey.ToName();
            }
            set
            {
                TradingHoursKey = _instrumentKey.ToTradingHoursKey(value);
            }
        }

        /// <summary>
        /// Gets or sets the instrument point value.
        /// </summary>
        public double PointValue => _instrumentKey.ToPointValue();

        /// <summary>
        /// Gets or sets the instrument tick size.
        /// </summary>
        public double TickSize => _instrumentKey.ToTickSize();

        #endregion

        #region Constructors

        /// <summary>
        /// Create <see cref="Instrument"/> default instance.
        /// </summary>
        public Instrument(string stringKey)
        {
            if (string.IsNullOrEmpty(stringKey))
                throw new ArgumentException($"the parameter {nameof(stringKey)} cannot be null or empty");

            if (!stringKey.TryGetInstrumentKey(out _instrumentKey))
                throw new Exception("Invalid string key passed bay parameter.");

        }

        #endregion
    }
}
