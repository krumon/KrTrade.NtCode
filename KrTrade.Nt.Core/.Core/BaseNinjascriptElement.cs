using NinjaTrader.NinjaScript;
using System.Diagnostics;

namespace KrTrade.Nt.Core
{
    public abstract class BaseNinjascriptElement : BaseElement
    {

        private bool _isConfigure = false;
        private bool _isDataLoaded = false;

        private readonly IPrintService _printService;

        public IPrintService PrintService => _printService;

        public bool IsConfigure => _isConfigure;
        public bool IsDataLoaded => _isDataLoaded;

        public void Configure()
        {

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

        protected BaseNinjascriptElement(NinjaScriptBase ninjascript, IPrintService printService, IInfo info) : base(ninjascript,info)
        {
            _printService = printService;
        }

        public void Log(Logging.LogLevel level, string state)
        {
            if (_printService == null || !_printService.IsEnable)
                return;
            switch (level)
            {
                case Logging.LogLevel.Trace:
                    _printService?.LogTrace(GetLogString(state));
                    break;
                case Logging.LogLevel.Debug:
                    _printService?.LogDebug(GetLogString(state));
                    break;
                case Logging.LogLevel.Information:
                    _printService?.LogInformation(GetLogString(state));
                    break;
                case Logging.LogLevel.Warning:
                    _printService?.LogWarning(GetLogString(state));
                    break;
                case Logging.LogLevel.Error:
                    _printService?.LogError(GetLogString(state));
                    break;
                default:
                    break;
            }
        }
        public virtual void LogConfigurationState()
        {
            if (_printService != null && _printService.IsEnable)
                return;

            if (IsDataLoaded && Ninjascript.State == State.DataLoaded)
                Log(Logging.LogLevel.Information, "has been loaded successfully.");
            else if (IsConfigure && Ninjascript.State == State.Configure)
                Log(Logging.LogLevel.Information, "has been configured successfully.");
            else if (!(IsConfigure && IsDataLoaded) && Ninjascript.State == State.DataLoaded)
                Log(Logging.LogLevel.Error, "could not be configured. The service will not work.");
            else
                Log(Logging.LogLevel.Error, "could not be configured. You are configuring the service out of configure or data loaded states.");
        }

    }

    public abstract class BaseNinjascriptElement<TInfo> : BaseNinjascriptElement
        where TInfo : IInfo
    {
        new public TInfo Info => (TInfo)base.Info;

        protected BaseNinjascriptElement(NinjaScriptBase ninjascript, IPrintService printService, IInfo info) : base(ninjascript, printService, info)
        {
        }
    }

    public abstract class BaseNinjascriptElement<TInfo,TOptions> : BaseNinjascriptElement
        where TInfo : IInfo
        where TOptions : IOptions
    {
        private readonly TOptions _options;

        new public TInfo Info => (TInfo)base.Info;
        public TOptions Options => _options;

        protected BaseNinjascriptElement(NinjaScriptBase ninjascript, IPrintService printService, IInfo info) : base(ninjascript, printService, info)
        {
        }
    }

}
