using KrTrade.Nt.Console;
using KrTrade.Nt.Core.Data;
using KrTrade.Nt.DI.Data;
using KrTrade.Nt.DI.Services;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace KrTrade.Nt.Console
{
    /// <summary>
    /// Represents any financial instrument.
    /// </summary>
    public class InstrumentProvider
    {

        #region Private members

        private readonly bool _instanceError;
        private readonly InstrumentCode _instrumentKey;
        private DataSeriesDescriptor[] _descriptors;
        private ConcurrentDictionary<string, DataSeriesService> _createdServices;

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
        public TradingHoursCode TradingHoursKey {get;set;} = TradingHoursCode.Default;

        /// <summary>
        /// Gets the instument name.
        /// </summary>
        public string Name => _instanceError ? string.Empty : _instrumentKey.ToString();

        /// <summary>
        /// Gets the instrument description.
        /// </summary>
        public string Description => _instanceError ? string.Empty : _instrumentKey.ToDescription();

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

        public InstrumentProvider(ICollection<DataSeriesDescriptor> descriptors, InstrumentProviderOptions options)
        {
            if (descriptors == null)
                throw new ArgumentNullException(nameof(descriptors));
            if (descriptors.Count == 0)
                throw new ArgumentException("Descriptors count cannot be 0");

            _descriptors = new KrTrade.Nt.DI.Services.DataSeriesDescriptor[descriptors.Count];
            descriptors.CopyTo(_descriptors,0);
        }

        /// <summary>
        /// Create <see cref="InstrumentProvider"/> default instance.
        /// </summary>
        public InstrumentProvider(string stringKey)
        {
            if (string.IsNullOrEmpty(stringKey))
                throw new Exception($"the parameter {nameof(stringKey)} cannot be null or empty");

            if (!stringKey.TryGetInstrumentKey(out _instrumentKey))
            {
                _instanceError = true;
                throw new Exception("Unknown string key passed bay parameter.");
            }
        }

        #endregion


    }
}
