using NinjaTrader.NinjaScript;
using System;

namespace KrTrade.Nt.Services
{
    public abstract class NinjascriptService : BaseService, INinjascriptService
    {
        #region Private members

        protected new NinjascriptServiceOptions _options;
        private readonly IPrintService _printService;
        private bool _isConfigure = false;
        private bool _isDataLoaded = false;

        #endregion

        #region Properties

        public new NinjascriptServiceOptions Options { get => _options ?? new NinjascriptServiceOptions(); protected set { _options = value; } }
        public bool IsLogEnable { get => _options.IsLogEnable; set { _options.IsLogEnable = value; } }
        public bool IsConfigured => _isConfigure && _isDataLoaded;
        public IPrintService PrintService => _printService;

        #endregion

        #region Constructors

        /// <summary>
        /// Create <see cref="NinjascriptService"/> instance and configure it.
        /// This instance must be created in the 'Ninjascript.State == Configure'.
        /// </summary>
        /// <param name="ninjascript">The <see cref="INinjascript"/> to gets 'Ninjatrader.NinjaScript' properties and objects.</param>
        /// <exception cref="ArgumentNullException">The <see cref="INinjascript"/> cannot be null.</exception>
        protected NinjascriptService(NinjaScriptBase ninjascript) : base(ninjascript)
        {
        }

        /// <summary>
        /// Create <see cref="NinjascriptService"/> instance and configure it.
        /// This instance must be created in the 'Ninjascript.State == Configure'.
        /// </summary>
        /// <param name="ninjascript">The <see cref="INinjascript"/> to gets 'Ninjatrader.NinjaScript' properties and objects.</param>
        /// <param name="printService">The <see cref="IPrintService"/> to log.</param>
        /// <exception cref="ArgumentNullException">The <see cref="INinjascript"/> cannot be null.</exception>
        protected NinjascriptService(NinjaScriptBase ninjascript, IPrintService printService) : base(ninjascript)
        {
            _printService = printService;
        }

        /// <summary>
        /// Create <see cref="NinjascriptService"/> instance and configure it.
        /// This instance must be created in the 'Ninjascript.State == Configure'.
        /// </summary>
        /// <param name="ninjascript">The <see cref="INinjascript"/> to gets 'Ninjatrader.NinjaScript' properties and objects.</param>
        /// <param name="printService">The <see cref="IPrintService"/> to log.</param>
        /// <param name="configureOptions">The configure options of the service.</param>
        /// <exception cref="ArgumentNullException">The <see cref="INinjascript"/> cannot be null.</exception>
        protected NinjascriptService(NinjaScriptBase ninjascript, IPrintService printService, IConfigureOptions<NinjascriptServiceOptions> configureOptions) : base(ninjascript)
        {
            _printService = printService;
            Options = new NinjascriptServiceOptions();
            configureOptions?.Configure(Options);
        }

        #endregion

        #region Implementation methods

        /// <summary>
        /// Method to configure the service when 'Ninjatrader.NinjaScript.State' is equal to 'Configure'.
        /// </summary>
        public void Configure()
        {

            if (IsOutOfConfigurationStates())
                LoggingHelpers.OutOfConfigurationStatesException(Name);

            if (_isConfigure && _isDataLoaded)
                return;

            if (Ninjascript.State == State.Configure && !_isConfigure)
                Configure(out _isConfigure);

            else if (Ninjascript.State == State.DataLoaded && !_isConfigure)
            {
                Configure(out _isConfigure);
                DataLoaded(out _isDataLoaded);
            }
            else if (Ninjascript.State == State.DataLoaded && _isConfigure)
                DataLoaded(out _isDataLoaded);

            LogConfigureState();
        }
        public void DataLoaded()
        {
            if (Ninjascript.State != State.DataLoaded)
                LoggingHelpers.OutOfConfigurationStatesException(Name);

            if (_isConfigure && _isDataLoaded)
                return;

            if (Ninjascript.State == State.DataLoaded && !_isConfigure)
                Configure(out _isConfigure);

            if (Ninjascript.State == State.DataLoaded && _isConfigure)
                DataLoaded(out _isDataLoaded);

            LogConfigureState();
        }

        /// <summary>
        /// Method to configure the service. This method should be used by 
        /// services that can be configured without first passing any filters.
        /// </summary>
        /// <param name="isConfigured">True, if the service has been configure, otherwise false.</param>
        internal abstract void Configure(out bool isConfigured);

        /// <summary>
        /// Method to configure the service when NinjaScript data is loaded. This method should be used by 
        /// services that can be configured without first passing any filters.
        /// </summary>
        /// <param name="isDataLoaded">True, if the service has been configure, otherwise false.</param>
        internal abstract void DataLoaded(out bool isDataLoaded);

        #endregion

        #region Public methods

        /// <summary>
        /// Print in NinjaScript putput window the configuration state. If the configuration has been ok or error.
        /// </summary>
        public void LogConfigureState()
        {
            if (_printService == null)
                return;

            //if (IsConfigured && IsInConfigurationStates())
            //    _printService?.LogInformation($"The {Name} has been configured succesfully.");
            //else if (!IsConfigured && Ninjascript.Instance.State == State.DataLoaded)
            //    _printService?.LogError($"The {Name} has NOT been configured. The service will not work.");

            if (IsConfigured)
                _printService?.LogInformation($"The {Name} has been configured succesfully.");
            else
                _printService?.LogError($"The '{Name}' has NOT been configured. The service will not work. '{Name}' must be configured when 'State = Configure and DataLoaded'.");
        }

        #endregion

        #region Protected methods

        protected bool IsPrintServiceAvailable()
        {
            return
                _printService != null &&
                IsLogEnable;
        }

        #endregion
    }

    public abstract class NinjascriptService<TOptions> : NinjascriptService, INinjascriptService<TOptions>
        where TOptions: NinjascriptServiceOptions, new()
    {

        protected new TOptions _options;
        public new TOptions Options { get => _options ?? new TOptions(); protected set { _options = value; } }

        protected NinjascriptService(NinjaScriptBase ninjascript) : base(ninjascript)
        {
        }
        protected NinjascriptService(NinjaScriptBase ninjascript, IPrintService printService) : base(ninjascript, printService)
        {
        }
        protected NinjascriptService(NinjaScriptBase ninjascript, IPrintService printService, IConfigureOptions<TOptions> configureOptions) : base(ninjascript, printService)
        {
            Options = new TOptions();
            configureOptions?.Configure(Options);
        }
    }
}
