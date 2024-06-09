using KrTrade.Nt.Core.Data;
using KrTrade.Nt.Core.Elements;
using KrTrade.Nt.Core.Logging;
using NinjaTrader.NinjaScript;
using System.Diagnostics;

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

        new public INinjascriptServiceInfo Info { get => (INinjascriptServiceInfo)base.Info;}
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

        /// <summary>
        /// Create <see cref="BaseNinjascriptService"/> instance and configure it.
        /// This instance must be created in the 'Ninjascript.State == Configure'.
        /// </summary>
        /// <param name="ninjascript">The <see cref="NinjaScriptBase"/> to gets 'Ninjatrader.NinjaScript' properties and objects.</param>
        /// <param name="printService">The <see cref="IPrintService"/> to log.</param>
        /// <param name="info">The service information.</param>
        /// <param name="options">The service options.</param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="ninjascript"/> cannot be null.</exception>
        protected BaseNinjascriptService(NinjaScriptBase ninjascript, IPrintService printService, IServiceInfo info, IServiceOptions options) : 
            base(ninjascript, info ?? new NinjascriptServiceInfo(), options ?? new NinjascriptServiceOptions()) 
        {
            _printService = printService;
        }

        #endregion

        #region Implementation methods

        public void Configure()
        {

            // TODO: Delete this line
            Debugger.Break();

            if (IsOutOfConfigurationStates())
                LoggingHelpers.ThrowIsNotConfigureException(Name);

            if (_isConfigure && _isDataLoaded)
                return;

            if (Ninjascript.State == State.Configure && !_isConfigure)
                Configure(out _isConfigure);

            else if (Ninjascript.State == State.DataLoaded && !_isConfigure)
                Configure(out _isConfigure);

            //else if (Ninjascript.State == State.DataLoaded && _isConfigure)
            //    DataLoaded(out _isDataLoaded);

            LogConfigurationState();
        }
        public void DataLoaded()
        {

            // TODO: Delete this line
            Debugger.Break();

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

        public string ToString(string header, string description, string separator, object value, int tabOrder)
        {
            string text = string.Empty;
            string tab = string.Empty;
            separator = value != null && string.IsNullOrEmpty(separator) ? ": " : separator;

            if (tabOrder > 0)
                for (int i = 0; i < tabOrder; i++)
                    tab += "\t";
            text += tab;

            if (!string.IsNullOrEmpty(header))
                text += tab + header;

            if (!string.IsNullOrEmpty(description))
            {
                if (string.IsNullOrEmpty(header))
                    text += description;
                else
                    text += "(" + description + ")";
            }

            if (value != null)
                text += separator + value.ToString();

            return text;
        }
        public override string ToString() => ToString(
            header: GetHeaderString(),
            description: GetDescriptionString(),
            separator: string.Empty,
            value: null,
            tabOrder: 0);
        public string ToString(int tabOrder, object value, bool isHeaderVisible = true, bool isDescriptionVisible = true, bool isSeparatorVisible = false, bool isValueVisible = true) => ToString(
            header: isHeaderVisible ? GetHeaderString() : string.Empty,
            description: isDescriptionVisible ? GetDescriptionString() : string.Empty,
            separator: isSeparatorVisible ? ": " : " ",
            value: isValueVisible ? value : null,
            tabOrder: tabOrder);

        public void Log() => Log(LogLevel.Information, null, 0);
        public void Log(int tabOrder) => Log(LogLevel.Information, null, tabOrder);
        public void Log(string message, int tabOrder = 0) => Log(LogLevel.Information, message, tabOrder);
        public void Log(LogLevel level, string message, int tabOrder = 0)
        {
            if (_printService == null || !Options.IsLogEnable)
                return;
            switch (level)
            {
                case LogLevel.Trace:
                    _printService?.LogTrace(ToString(tabOrder, message)); 
                    break;
                case LogLevel.Debug:
                    _printService?.LogDebug(ToString(tabOrder, message)); 
                    break;
                case LogLevel.Information:
                    _printService?.LogInformation(ToString(tabOrder, message)); 
                    break;
                case LogLevel.Warning:
                    _printService?.LogWarning(ToString(tabOrder, message)); 
                    break;
                case LogLevel.Error:
                    _printService?.LogError(ToString(tabOrder, message)); 
                    break;
                default:
                    break;
            }
        }

        public void Log(LogLevel level, string state)
        {
            if (_printService == null || !Options.IsLogEnable)
                return;
            switch (level)
            {
                case LogLevel.Trace:
                    _printService?.LogTrace(GetLogString(state));
                    break;
                case LogLevel.Debug:
                    _printService?.LogDebug(GetLogString(state));
                    break;
                case LogLevel.Information:
                    _printService?.LogInformation(GetLogString(state));
                    break;
                case LogLevel.Warning:
                    _printService?.LogWarning(GetLogString(state));
                    break;
                case LogLevel.Error:
                    _printService?.LogError(GetLogString(state));
                    break;
                default:
                    break;
            }
        }
        public virtual void LogConfigurationState()
        {
            if (!IsPrintServiceAvailable)
                return;

            if (IsDataLoaded && Ninjascript.State == State.DataLoaded)
                Log(LogLevel.Information, "has been loaded successfully.");
            else if (IsConfigure && Ninjascript.State == State.Configure)
                Log(LogLevel.Information, "has been configured successfully.");
            else if (!IsConfigureAll && Ninjascript.State == State.DataLoaded)
                Log(LogLevel.Error, "could not be configured. The service will not work.");
            else
                Log(LogLevel.Error, "could not be configured. You are configuring the service out of configure or data loaded states.");
        }

        #endregion

    }

    public abstract class BaseNinjascriptService<TInfo> : BaseNinjascriptService, INinjascriptService<TInfo>
        where TInfo : INinjascriptServiceInfo, new()
    {
        //new public TInfo Info => (TInfo)base.Info;
        new public TInfo Info { get => (TInfo)base.Info; }

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
