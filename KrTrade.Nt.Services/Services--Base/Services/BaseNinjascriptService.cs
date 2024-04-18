using KrTrade.Nt.Core.Data;
using KrTrade.Nt.Core.Elements;
using KrTrade.Nt.Core.Services;
using NinjaTrader.NinjaScript;

namespace KrTrade.Nt.Services
{

    public abstract class BaseNinjascriptService : BaseService, INinjascriptService
    {
        #region Private members

        protected new IElementInfo _info;
        protected new NinjascriptServiceOptions _options;
        private readonly IPrintService _printService;
        private bool _isConfigure = false;
        private bool _isDataLoaded = false;

        #endregion

        #region Properties

        public new IElementInfo Info { get => _info ?? new NinjascriptServiceInfo(); protected set { _info = value; } }
        public new NinjascriptServiceOptions Options { get => _options ?? new NinjascriptServiceOptions(); protected set { _options = value; } }
        
        public Calculate CalculateMode { get => _options.CalculateMode; internal set { _options.CalculateMode = value; } }
        public MultiSeriesCalculateMode MultiSeriesCalculateMode { get => _options.MultiSeriesCalculateMode; internal set { _options.MultiSeriesCalculateMode = value; } }
        
        public bool IsLogEnable { get => _options.IsLogEnable; internal set { _options.IsLogEnable = value; } }
        public bool IsConfigure => _isConfigure;
        public bool IsDataLoaded => _isDataLoaded;

        public bool IsConfigureAll => _isConfigure && _isDataLoaded;
        public IPrintService PrintService => _printService;

        #endregion

        #region Constructors

        //protected BaseNinjascriptService(NinjaScriptBase ninjascript) : this(ninjascript,null,null,null) { }
        //protected BaseNinjascriptService(NinjaScriptBase ninjascript, IPrintService printService) : this(ninjascript, printService,null,null) { }
        //protected BaseNinjascriptService(NinjaScriptBase ninjascript, IPrintService printService, IElementInfo info) : this(ninjascript, printService, info,null) { }
        //protected BaseNinjascriptService(NinjaScriptBase ninjascript, IPrintService printService, NinjascriptServiceOptions options) : this(ninjascript, printService,null, options) { }
        protected BaseNinjascriptService(NinjaScriptBase ninjascript, IPrintService printService, IElementInfo info, NinjascriptServiceOptions options) : base(ninjascript, info, options) 
        {
            _printService = printService;
        }

        ///// <summary>
        ///// Create <see cref="BaseNinjascriptService"/> instance and configure it.
        ///// This instance must be created in the 'Ninjascript.State == Configure'.
        ///// </summary>
        ///// <param name="ninjascript">The <see cref="INinjascript"/> to gets 'Ninjatrader.NinjaScript' properties and objects.</param>
        ///// <exception cref="ArgumentNullException">The <see cref="INinjascript"/> cannot be null.</exception>
        //protected BaseNinjascriptService(NinjaScriptBase ninjascript) : base(ninjascript,null,null)
        //{
        //}

        ///// <summary>
        ///// Create <see cref="BaseNinjascriptService"/> instance and configure it.
        ///// This instance must be created in the 'Ninjascript.State == Configure'.
        ///// </summary>
        ///// <param name="ninjascript">The <see cref="INinjascript"/> to gets 'Ninjatrader.NinjaScript' properties and objects.</param>
        ///// <param name="printService">The <see cref="IPrintService"/> to log.</param>
        ///// <exception cref="ArgumentNullException">The <see cref="INinjascript"/> cannot be null.</exception>
        //protected BaseNinjascriptService(NinjaScriptBase ninjascript, IPrintService printService) : base(ninjascript, null, null)
        //{
        //    _printService = printService;
        //}

        ///// <summary>
        ///// Create <see cref="BaseNinjascriptService"/> instance and configure it.
        ///// This instance must be created in the 'Ninjascript.State == Configure'.
        ///// </summary>
        ///// <param name="ninjascript">The <see cref="INinjascript"/> to gets 'Ninjatrader.NinjaScript' properties and objects.</param>
        ///// <param name="printService">The <see cref="IPrintService"/> to log.</param>
        ///// <param name="options">The service options.</param>
        ///// <exception cref="ArgumentNullException">The <see cref="INinjascript"/> cannot be null.</exception>
        //protected BaseNinjascriptService(NinjaScriptBase ninjascript, IPrintService printService, NinjascriptServiceOptions options) : base(ninjascript, null, options)
        //{
        //    _printService = printService;
        //}

        ///// <summary>
        ///// Create <see cref="BaseNinjascriptService"/> instance and configure it.
        ///// This instance must be created in the 'Ninjascript.State == Configure'.
        ///// </summary>
        ///// <param name="ninjascript">The <see cref="INinjascript"/> to gets 'Ninjatrader.NinjaScript' properties and objects.</param>
        ///// <param name="printService">The <see cref="IPrintService"/> to log.</param>
        ///// <param name="configureOptions">The configure options of the service.</param>
        ///// <param name="options">The service options.</param>
        ///// <exception cref="ArgumentNullException">The <see cref="INinjascript"/> cannot be null.</exception>
        //protected BaseNinjascriptService(NinjaScriptBase ninjascript, IPrintService printService, Action<NinjascriptServiceOptions> configureOptions, NinjascriptServiceOptions options) : base(ninjascript, null, options)
        //{
        //    _printService = printService;
        //    Options = options ?? new NinjascriptServiceOptions();
        //    configureOptions?.Invoke(Options);
        //}

        ///// <summary>
        ///// Create <see cref="BaseNinjascriptService"/> instance and configure it.
        ///// This instance must be created in the 'Ninjascript.State == Configure'.
        ///// </summary>
        ///// <param name="ninjascript">The <see cref="INinjascript"/> to gets 'Ninjatrader.NinjaScript' properties and objects.</param>
        ///// <param name="printService">The <see cref="IPrintService"/> to log.</param>
        ///// <param name="info">The service information.</param>
        ///// <param name="options">The service options.</param>
        ///// <exception cref="ArgumentNullException">The <see cref="INinjascript"/> cannot be null.</exception>
        //protected BaseNinjascriptService(NinjaScriptBase ninjascript, IPrintService printService, NinjascriptServiceInfo info, NinjascriptServiceOptions options) : base(ninjascript, info, options)
        //{
        //    _printService = printService;
        //}

        #endregion

        #region Implementation methods

        public void Configure()
        {
            if (IsOutOfConfigurationStates())
                LoggingHelpers.ThrowIsNotConfigureException(Name);
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

            LogConfigurationState();
        }
        public void DataLoaded()
        {
            if (Ninjascript.State != State.DataLoaded)
                LoggingHelpers.ThrowIsNotConfigureException(Name);

            if (_isConfigure && _isDataLoaded)
                return;

            if (Ninjascript.State == State.DataLoaded && !_isConfigure)
                Configure(out _isConfigure);

            if (Ninjascript.State == State.DataLoaded && _isConfigure)
                DataLoaded(out _isDataLoaded);

            LogConfigurationState();
        }
        public virtual void Terminated() { }

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

        public abstract string ToLogString();

        /// <summary>
        /// Logs information with values thats returns 'ToLogString()' method.
        /// </summary>
        public void Log()
        {
            if (_printService == null || !Options.IsLogEnable) 
                return;
            _printService?.LogValue(ToLogString());
        }

        #endregion

        #region Protected methods

        /// <summary>
        /// Print in NinjaScript putput window the configuration state. If the configuration has been ok or error.
        /// </summary>
        protected void LogConfigurationState()
        {
            if (_printService == null || !Options.IsLogEnable)
                return;

            if (IsDataLoaded && Ninjascript.State == State.DataLoaded)
                _printService?.LogInformation($"The {Name} has been loaded succesfully.");
            else if (IsConfigure && Ninjascript.State == State.Configure)
                _printService?.LogInformation($"The {Name} has been configured succesfully.");
            else if (!IsConfigureAll && Ninjascript.State == State.DataLoaded)
                _printService?.LogError($"The '{Name}' has NOT been configured. The service will not work.");
            else
                _printService?.LogError($"The '{Name}' has NOT been configured. You are configuring the service out of configure or data loaded states.");
        }

        protected bool IsPrintServiceAvailable()
        {
            return
                _printService != null &&
                IsLogEnable;
        }

        #endregion
    }

    public abstract class BaseNinjascriptService<TInfo> : BaseNinjascriptService, INinjascriptService<TInfo>
        where TInfo : IElementInfo, new()
    {
        protected new TInfo _info;
        public new TInfo Info { get => _info == null ? new TInfo() : _info; protected set { _info = value; } }

        //protected BaseNinjascriptService(NinjaScriptBase ninjascript) : base(ninjascript) { }
        //protected BaseNinjascriptService(NinjaScriptBase ninjascript, IPrintService printService) : base(ninjascript,printService) { }
        //protected BaseNinjascriptService(NinjaScriptBase ninjascript, IPrintService printService, TInfo info) : base(ninjascript,printService,info) { }
        //protected BaseNinjascriptService(NinjaScriptBase ninjascript, IPrintService printService, NinjascriptServiceOptions options) : base(ninjascript,printService,options) { }
        protected BaseNinjascriptService(NinjaScriptBase ninjascript, IPrintService printService, TInfo info, NinjascriptServiceOptions options) : base(ninjascript, printService, info, options) { }

        ///// <summary>
        ///// Create <see cref="BaseNinjascriptService"/> instance and configure it.
        ///// This instance must be created in the 'Ninjascript.State == Configure'.
        ///// </summary>
        ///// <param name="ninjascript">The <see cref="INinjascript"/> to gets 'Ninjatrader.NinjaScript' properties and objects.</param>
        ///// <param name="printService">The <see cref="IPrintService"/> to log.</param>
        ///// <param name="configureOptions">The configure options of the service.</param>
        ///// <param name="options">The service options.</param>
        ///// <exception cref="ArgumentNullException">The <see cref="INinjascript"/> cannot be null.</exception>
        //protected BaseNinjascriptService(NinjaScriptBase ninjascript, IPrintService printService, Action<NinjascriptServiceOptions> configureOptions, NinjascriptServiceOptions options) : base(ninjascript)
        //{
        //    _printService = printService;
        //    Options = options ?? new NinjascriptServiceOptions();
        //    configureOptions?.Invoke(Options);
        //}

        ///// <summary>
        ///// Create <see cref="BaseNinjascriptService"/> instance and configure it.
        ///// This instance must be created in the 'Ninjascript.State == Configure'.
        ///// </summary>
        ///// <param name="ninjascript">The <see cref="INinjascript"/> to gets 'Ninjatrader.NinjaScript' properties and objects.</param>
        ///// <param name="printService">The <see cref="IPrintService"/> to log.</param>
        ///// <param name="info">The service information.</param>
        ///// <param name="options">The service options.</param>
        ///// <exception cref="ArgumentNullException">The <see cref="INinjascript"/> cannot be null.</exception>
        //protected BaseNinjascriptService(NinjaScriptBase ninjascript, IPrintService printService, TInfo info, NinjascriptServiceOptions options) : base(ninjascript, printService, info, options) { }

    }

    public abstract class BaseNinjascriptService<TInfo,TOptions> : BaseNinjascriptService<TInfo>, INinjascriptService<TInfo,TOptions>
        where TInfo : IElementInfo, new()
        where TOptions : NinjascriptServiceOptions, new()
    {

        protected new TOptions _options;
        public new TOptions Options { get => _options ?? new TOptions(); protected set { _options = value; } }

        //protected BaseNinjascriptService(NinjaScriptBase ninjascript) : base(ninjascript) { }
        //protected BaseNinjascriptService(NinjaScriptBase ninjascript, IPrintService printService) : base(ninjascript, printService) { }
        //protected BaseNinjascriptService(NinjaScriptBase ninjascript, IPrintService printService, TInfo info) : base(ninjascript, printService, info) { }
        //protected BaseNinjascriptService(NinjaScriptBase ninjascript, IPrintService printService, TOptions options) : base(ninjascript, printService, options) { }
        protected BaseNinjascriptService(NinjaScriptBase ninjascript, IPrintService printService, TInfo info, TOptions options) : base(ninjascript, printService, info, options) { }

        //protected BaseNinjascriptService(NinjaScriptBase ninjascript) : base(ninjascript) { }
        //protected BaseNinjascriptService(NinjaScriptBase ninjascript, IPrintService printService) : base(ninjascript, printService) { }
        //protected BaseNinjascriptService(NinjaScriptBase ninjascript, IPrintService printService, Action<TOptions> configureOptions) : this(ninjascript, printService,configureOptions,null) { }
        //protected BaseNinjascriptService(NinjaScriptBase ninjascript, IPrintService printService, TOptions options) : base(ninjascript, printService,null,options) { }
        //protected BaseNinjascriptService(NinjaScriptBase ninjascript, IPrintService printService, Action<TOptions> configureOptions, TOptions options) : base(ninjascript,printService,configureOptions,options)
        //{
        //    Options = options ?? new TOptions();
        //    configureOptions?.Invoke(Options);
        //}

    }

}
