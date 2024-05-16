using KrTrade.Nt.Core.Data;
using KrTrade.Nt.Core.Services;
using NinjaTrader.NinjaScript;
using System.Runtime.CompilerServices;

namespace KrTrade.Nt.Services
{

    public abstract class BaseNinjascriptService : BaseService, INinjascriptService
    {
        #region Private members

        private readonly IPrintService _printService;
        private bool _isConfigure = false;
        private bool _isDataLoaded = false;

        #endregion

        #region Properties

        //new public INinjascriptServiceInfo Info => (INinjascriptServiceInfo)base.Info;
        new public INinjascriptServiceInfo Info { get => (INinjascriptServiceInfo)base.Info; protected set => base.Info = value; }
        new public INinjascriptServiceOptions Options => (INinjascriptServiceOptions)base.Options;
        
        public Calculate CalculateMode { get => Options.CalculateMode; internal set { Options.CalculateMode = value; } }
        public MultiSeriesCalculateMode MultiSeriesCalculateMode { get => Options.MultiSeriesCalculateMode; internal set { Options.MultiSeriesCalculateMode = value; } }
        
        public bool IsConfigure => _isConfigure;
        public bool IsDataLoaded => _isDataLoaded;

        public bool IsConfigureAll => _isConfigure && _isDataLoaded;
        public IPrintService PrintService => _printService;

        protected bool IsPrintServiceAvailable => _printService != null && IsLogEnable;
        
        #endregion

        #region Constructors

        //protected BaseNinjascriptService(NinjaScriptBase ninjascript) : this(ninjascript,null,null,null) { }
        //protected BaseNinjascriptService(NinjaScriptBase ninjascript, IPrintService printService) : this(ninjascript, printService,null,null) { }
        //protected BaseNinjascriptService(NinjaScriptBase ninjascript, IPrintService printService, IElementInfo info) : this(ninjascript, printService, info,null) { }
        //protected BaseNinjascriptService(NinjaScriptBase ninjascript, IPrintService printService, NinjascriptServiceOptions options) : this(ninjascript, printService,null, options) { }
        protected BaseNinjascriptService(NinjaScriptBase ninjascript, IPrintService printService, IServiceInfo info, IServiceOptions options) : 
            base(ninjascript, info ?? new NinjascriptServiceInfo(), options ?? new NinjascriptServiceOptions()) 
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
            if (!IsPrintServiceAvailable)
                return;

            if (IsDataLoaded && Ninjascript.State == State.DataLoaded)
                _printService?.LogInformation($"The {Name} has been loaded successfully.");
            else if (IsConfigure && Ninjascript.State == State.Configure)
                _printService?.LogInformation($"The {Name} has been configured successfully.");
            else if (!IsConfigureAll && Ninjascript.State == State.DataLoaded)
                _printService?.LogError($"The '{Name}' has NOT been configured. The service will not work.");
            else
                _printService?.LogError($"The '{Name}' has NOT been configured. You are configuring the service out of configure or data loaded states.");
        }

        protected void LogInitStart([CallerMemberName] string memberName = "")
        {
            if (!IsPrintServiceAvailable)
                return;

            _printService.LogTrace($"Service with name: {Name} is being initialized in {memberName}.");
            _printService.LogTrace($"Service with key: {Key} is being initialized in {memberName}.");
        }
        protected void LogInitEnd()
        {
            if (!IsPrintServiceAvailable)
                return;

             _printService.LogTrace($"Service with name: {Name} has been initialized successfully");
             _printService.LogTrace($"Service with key: {Key} has been initialized successfully");
        }

        #endregion
    }

    public abstract class BaseNinjascriptService<TInfo> : BaseNinjascriptService, INinjascriptService<TInfo>
        where TInfo : INinjascriptServiceInfo, new()
    {
        //new public TInfo Info => (TInfo)base.Info;
        new public TInfo Info { get => (TInfo)base.Info; protected set => base.Info = value; }

        //protected BaseNinjascriptService(NinjaScriptBase ninjascript) : base(ninjascript) { }
        //protected BaseNinjascriptService(NinjaScriptBase ninjascript, IPrintService printService) : base(ninjascript,printService) { }
        //protected BaseNinjascriptService(NinjaScriptBase ninjascript, IPrintService printService, TInfo info) : base(ninjascript,printService,info) { }
        //protected BaseNinjascriptService(NinjaScriptBase ninjascript, IPrintService printService, NinjascriptServiceOptions options) : base(ninjascript,printService,options) { }
        protected BaseNinjascriptService(NinjaScriptBase ninjascript, IPrintService printService, TInfo info, IServiceOptions options) : 
            base(ninjascript, printService, info == null ? new TInfo() : info, options == null ? new NinjascriptServiceOptions() : options) { }

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
        where TInfo : INinjascriptServiceInfo, new()
        where TOptions : INinjascriptServiceOptions, new()
    {

        new public TOptions Options => (TOptions)base.Options;

        //protected BaseNinjascriptService(NinjaScriptBase ninjascript) : base(ninjascript) { }
        //protected BaseNinjascriptService(NinjaScriptBase ninjascript, IPrintService printService) : base(ninjascript, printService) { }
        //protected BaseNinjascriptService(NinjaScriptBase ninjascript, IPrintService printService, TInfo info) : base(ninjascript, printService, info) { }
        //protected BaseNinjascriptService(NinjaScriptBase ninjascript, IPrintService printService, TOptions options) : base(ninjascript, printService, options) { }
        protected BaseNinjascriptService(NinjaScriptBase ninjascript, IPrintService printService, TInfo info, TOptions options) : 
            base(ninjascript, printService, info == null ? new TInfo() : info, options == null ? new TOptions() : options) { }

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
