using NinjaTrader.NinjaScript;
using KrTrade.Nt.Scripts.NinjatraderObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using KrTrade.Nt.DI.Services;
using KrTrade.Nt.DI.Options;
using KrTrade.Nt.DI.Logging;

namespace KrTrade.Nt.Scripts.Services
{
    /// <summary>
    /// Represents any ninjascript data series.
    /// </summary>
    public class DataSeries : IDataSeries
    {
        private readonly INinjaScriptBase _ninjascript;
        private readonly IChartBarsProperties _chartBarsProperties;
        private readonly IOptionsMonitor<DataSeriesOptions> _optionsMonitor;
        private readonly ILogger _logger;

        private DataSeriesOptions _currentOptions => GetCurrentOptions();
        private System.IDisposable _optionsReloadToken;

        //private Action<NinjaTrader.Data.BarsPeriodType, int> _addDataSeries;

        private DataSeriesDescriptor _seriesDescriptor;
        private List<DataSeriesDescriptor> _seriesDescriptors;

        //private readonly bool _instanceError;
        //private readonly InstrumentCode _instrumentKey;

        public DataSeriesDescriptor Primary 
        { 
            get 
            {
                if (_seriesDescriptors != null && _seriesDescriptors.Count > 0)
                {
                    return _seriesDescriptors[_seriesDescriptors.Count - 1];
                }

                Debug.Assert(_seriesDescriptor != null);
                return _seriesDescriptor;
            } 
        }

        public int Count
        {
            get
            {
                if (_seriesDescriptor == null)
                {
                    return _seriesDescriptors?.Count ?? 0;
                }

                return 1 + (_seriesDescriptors?.Count ?? 0);
            }
        }

        public DataSeriesDescriptor this[int index]
        {
            get
            {
                if (index >= Count)
                {
                    throw new ArgumentOutOfRangeException(nameof(index));
                }

                if (index == 0 && _seriesDescriptor != null)
                {
                    return _seriesDescriptor;
                }
                else if(index == 0 && _seriesDescriptor == null)
                    return _seriesDescriptors[index];

                return _seriesDescriptors[index - 1];
            }
        }

        /// <summary>
        /// Gets the unique key of the service.
        /// </summary>
        //public OptionalServiceType Key { get; private set; }

        ///// <summary>
        ///// The instrument unique code.
        ///// </summary>
        //public InstrumentCode InstrumentKey => _instrumentKey;

        ///// <summary>
        ///// The market exchange owner of the instrument.
        ///// </summary>
        //public MarketExchange MarketExchange => _instrumentKey.ToMarketExchange();

        ///// <summary>
        ///// The trading hours key.
        ///// </summary>
        //public TradingHoursCode TradingHoursKey {get;set;} = TradingHoursCode.Default;

        ///// <summary>
        ///// Gets the instument name.
        ///// </summary>
        //public string Name => _instanceError ? string.Empty : _instrumentKey.ToString();

        ///// <summary>
        ///// Gets the instrument description.
        ///// </summary>
        //public string Description => _instanceError ? string.Empty : _instrumentKey.ToDescription();

        ///// <summary>
        ///// Gets the default trading hours name.
        ///// </summary>
        //public string TradingHoursName 
        //{
        //    get
        //    {
        //        if (TradingHoursKey == TradingHoursCode.Default)
        //            TradingHoursKey = _instrumentKey.ToDefaultTradingHoursKey();

        //        return TradingHoursKey.ToName();
        //    }
        //    set
        //    {
        //        TradingHoursKey = _instrumentKey.ToTradingHoursKey(value);
        //    }
        //}

        ///// <summary>
        ///// Gets or sets the instrument point value.
        ///// </summary>
        //public double PointValue => _instrumentKey.ToPointValue();

        ///// <summary>
        ///// Gets or sets the instrument tick size.
        ///// </summary>
        //public double TickSize => _instrumentKey.ToTickSize();

        /// <summary>
        /// Create <see cref="InstrumentProvider"/> default instance.
        /// </summary>
        public DataSeries(INinjaScriptBase ninjascript, IChartBarsProperties chartBarsProperties, IOptionsMonitor<DataSeriesOptions> options, ILogger<DataSeries> logger, string stringKey="")
        {
            _ninjascript = ninjascript ?? throw new ArgumentNullException(nameof(ninjascript));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _optionsMonitor = options ?? throw new ArgumentNullException(nameof(options));
            _chartBarsProperties = chartBarsProperties;

            Configure();

            //if (string.IsNullOrEmpty(stringKey))
            //    throw new ArgumentException($"the parameter {nameof(stringKey)} cannot be null or empty");

            //if (!stringKey.TryGetInstrumentKey(out _instrumentKey))
            //{
            //    _instanceError = true;
            //    throw new Exception("Unknown string key passed bay parameter.");
            //}
        }

        public void Configure()
        {
            if (_ninjascript.State == State.Configure)
            {
                if (_chartBarsProperties != null)
                    //Add(new DataSeriesDescriptor()
                    //{
                    //    //InstrumentName = _chartBarsProperties.InstrumentName,
                    //    PeriodType = _chartBarsProperties.BarsPeriod.PeriodType,
                    //    PeriodValue = _chartBarsProperties.BarsPeriod.PeriodValue,
                    //    //TradingHoursName = _chartBarsProperties.TradingHoursName
                    //}, isPrimaryDataSerie: true);
                    Add(null);

                ReloadDataSeriesOptions(_currentOptions);
                _optionsReloadToken = _optionsMonitor?.OnChange(ReloadDataSeriesOptions);
            }
            else if (_ninjascript.State == State.Terminated)
                Dispose();
        }

        public void Dispose()
        {
            _optionsReloadToken?.Dispose();
        }

        public int GetSlot(DataSeriesDescriptor descriptor)
        {
            if (descriptor == _seriesDescriptor)
            {
                return Count - 1;
            }

            if (_seriesDescriptors != null)
            {
                int index = _seriesDescriptors.IndexOf(descriptor);
                if (index != -1)
                {
                    return _seriesDescriptors.Count - (index + 1);
                }
            }

            throw new InvalidOperationException("Data Series Descriptor Not Exist");
        }

        public void Add(DataSeriesDescriptor descriptor, bool isPrimaryDataSerie = false)
        {
            if (isPrimaryDataSerie)
            {
                _seriesDescriptor = descriptor;
            }
            else
            {
                if (_seriesDescriptors == null)
                    _seriesDescriptors = new List<DataSeriesDescriptor>();
                _seriesDescriptors.Add(descriptor);
            }
        }
        protected virtual void ReloadDataSeriesOptions(DataSeriesOptions options)
        {
            if (ValidateOptions(options))
            {
                //_ninjascript.Instance.SetState(State.Configure);
                return;
            }
        }

        private bool ValidateOptions(DataSeriesOptions options)
        {
            return true;
        }

        private DataSeriesOptions GetCurrentOptions() => _optionsMonitor.CurrentValue;

    }
}
