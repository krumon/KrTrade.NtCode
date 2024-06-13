using NinjaTrader.NinjaScript;
using System;
using System.Collections.Generic;

namespace KrTrade.Nt.Core
{
    public abstract class BaseScript : IScript
    {
        private readonly NinjaScriptBase _ninjascript;

        public NinjaScriptBase Ninjascript => _ninjascript;
        public abstract ElementType Type { get; protected set; }
        public abstract string Name { get; }

        protected BaseScript(NinjaScriptBase ninjascript)
        {
            _ninjascript = ninjascript ?? throw new ArgumentNullException($"The {nameof(ninjascript)} argument cannot be null.");
        }

        #region Helpers methods

        protected int GetBarIdx(int barsInProgress, int barsAgo)
        {
            if (barsInProgress < 0 || barsInProgress != Ninjascript.BarsInProgress)
                return -1;
            if (barsAgo < 0 || barsAgo >= Ninjascript.BarsArray[barsInProgress].Count)
                return -1;

            return Ninjascript.CurrentBars[barsInProgress] - barsAgo;
        }
        protected double GetOpen(int barsInProgress, int barsAgo)
        {
            if (barsInProgress < 0 || barsInProgress != Ninjascript.BarsInProgress)
                return 0.0;
            if (barsAgo < 0 || barsAgo >= Ninjascript.BarsArray[barsInProgress].Count)
                return 0.0;

            return Ninjascript.Opens[barsInProgress][barsAgo];
        }
        protected double GetHigh(int barsInProgress, int barsAgo)
        {
            if (barsInProgress < 0 || barsInProgress != Ninjascript.BarsInProgress)
                return double.MinValue;
            if (barsAgo < 0 || barsAgo >= Ninjascript.BarsArray[barsInProgress].Count)
                return double.MinValue;

            return Ninjascript.Highs[barsInProgress][barsAgo];
        }
        protected double GetLow(int barsInProgress, int barsAgo)
        {
            if (barsInProgress < 0 || barsInProgress != Ninjascript.BarsInProgress)
                return double.MaxValue;
            if (barsAgo < 0 || barsAgo >= Ninjascript.BarsArray[barsInProgress].Count)
                return double.MaxValue;

            return Ninjascript.Lows[barsInProgress][barsAgo];
        }
        protected double GetClose(int barsInProgress, int barsAgo)
        {
            if (barsInProgress < 0 || barsInProgress != Ninjascript.BarsInProgress)
                return 0.0;
            if (barsAgo < 0 || barsAgo >= Ninjascript.BarsArray[barsInProgress].Count)
                return 0.0;

            return Ninjascript.Closes[barsInProgress][barsAgo];
        }
        protected double GetVolume(int barsInProgress, int barsAgo)
        {
            if (barsInProgress < 0 || barsInProgress != Ninjascript.BarsInProgress)
                return -1.0;
            if (barsAgo < 0 || barsAgo >= Ninjascript.BarsArray[barsInProgress].Count)
                return -1.0;

            return Ninjascript.Volumes[barsInProgress][barsAgo];
        }
        protected DateTime GetTime(int barsInProgress, int barsAgo)
        {
            if (barsInProgress < 0 || barsInProgress != Ninjascript.BarsInProgress)
                return new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Local);
            if (barsAgo < 0 || barsAgo >= Ninjascript.BarsArray[barsInProgress].Count)
                return new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Local);

            return Ninjascript.Times[barsInProgress][barsAgo];
        }

        protected void Execute(List<Action> actions)
        {
            if (actions == null || actions.Count == 0)
                return;

            for (int i = 0; i < actions.Count; i++)
                actions[i]?.Invoke();
        }
        protected bool IsBarsInProgressOutOfRange(int barsInProgress)
        {
            if (barsInProgress < 0 || barsInProgress >= Ninjascript.BarsArray.Length)
                throw new ArgumentOutOfRangeException(nameof(barsInProgress));
            return false;
        }

        /// <summary>
        /// Indicates whether NinjaScript is in any of the configuration states.
        /// The configuaration states are 'Configure' and 'DataLoaded'.
        /// </summary>
        /// <returns></returns>
        protected bool IsInConfigurationStates()
        {
            if (Ninjascript.State == State.Configure || Ninjascript.State == State.DataLoaded)
                return true;
            else
                return false;
        }
        /// <summary>
        /// Indicates whether NinjaScript is in running states.
        /// The running states are 'Historical' and 'Realtime'.
        /// </summary>
        /// <returns>True, when the NijaScript State is 'Historical' or 'Realtime'.</returns>
        protected bool IsInRunningStates()
        {
            if (Ninjascript.State == State.Historical || Ninjascript.State == State.Realtime)
                return true;
            else
                return false;
        }
        /// <summary>
        /// Indicates whether NinjaScript is out of the configuration states.
        /// The configuaration states are 'Configure' and 'DataLoaded'.
        /// </summary>
        /// <returns>True, when the NijaScript State is NOT 'Configure' and 'DataLoaded'.</returns>
        protected bool IsOutOfConfigurationStates()
        {
            if (Ninjascript.State != State.Configure && Ninjascript.State != State.DataLoaded)
                return true;

            return false;
        }
        /// <summary>
        /// Indicates whether NinjaScript is out of the configure state.
        /// The configure state is 'Configure'.
        /// </summary>
        /// <returns>True, when the NijaScript State is NOT 'Configure'.</returns>
        protected bool IsOutOfConfigureState()
        {
            if (Ninjascript.State != State.Configure)
                return true;

            return false;
        }
        /// <summary>
        /// Indicates whether NinjaScript is out of the data loaded state.
        /// The data loaded state is 'DataLoaded'.
        /// </summary>
        /// <returns>True, when the NijaScript State is NOT 'DataLoaded'.</returns>
        protected bool IsOutOfDataLoadedState()
        {
            if (Ninjascript.State != State.DataLoaded)
                return true;

            return false;
        }
        /// <summary>
        /// Indicates whether NinjaScript is out of the running states.
        /// The running states are 'Historical' and 'Realtime'.
        /// </summary>
        /// <returns>True, when the NijaScript State is NOT 'Historical' and 'Realtime'.</returns>
        protected bool IsOutOfRunningStates()
        {
            if (Ninjascript.State != State.Historical && Ninjascript.State != State.Realtime)
                return true;

            return false;
        }
        /// <summary>
        /// Indicates whether NinjaScript indexes are available.
        /// The indexs are 'BarsInProgress' and 'CurrentBar'.
        /// </summary>
        /// <returns>True, when the NijaScript indexes are greater than -1.</returns>
        protected bool IsNinjaScriptIndexesAvailable()
        {
            if (IsNotAvilableBarsInProgressIdx() && IsNotAvailableFirstBarIdx())
                return true;

            return false;
        }
        /// <summary>
        /// Indicates whether NinjaScript data series is available to be updated.
        /// The index is 'CurrentBar'.
        /// </summary>
        /// <returns>True, when the NijaScript index are greater than -1.</returns>
        protected bool IsNotAvailableFirstBarIdx()
        {
            if (Ninjascript.CurrentBars[Ninjascript.BarsInProgress] < 0)
                return true;

            return false;
        }
        /// <summary>
        /// Indicates whether NinjaScript multi data series is available to be updated.
        /// The index is 'BarsInProgress'.
        /// </summary>
        /// <returns>True, when the NijaScript index are greater than -1.</returns>
        protected bool IsNotAvilableBarsInProgressIdx()
        {
            if (Ninjascript.BarsInProgress < 0)
                return true;

            return false;
        }

        #endregion

    }

    public abstract class BaseScript<TOptions> : BaseScript, IScript<TOptions>
        where TOptions : IOptions
    {
        private readonly TOptions _options;

        protected BaseScript(NinjaScriptBase ninjascript, TOptions options) : base(ninjascript)
        {
            _options = options;
        }

        public TOptions Options => _options;
    }

    public abstract class BaseScript<TInfo, TOptions> : BaseScript, IScript<TInfo, TOptions>
        where TInfo : IInfo
        where TOptions : IOptions
    {
        private readonly TInfo _info;
        private readonly TOptions _options;

        public TInfo Info => _info;
        public TOptions Options => _options;

        protected BaseScript(NinjaScriptBase ninjascript, TInfo info, TOptions options) : base(ninjascript)
        {
            if (info == null)
                throw new ArgumentNullException($"The {nameof(info)} argument cannot be null.");
            if (options == null)
                throw new ArgumentNullException($"The {nameof(options)} argument cannot be null.");
            _info = info;
            _options = options;
        }
    }

    //public abstract class BaseNinjascriptElement : BaseElement
    //{

    //    private bool _isConfigure = false;
    //    private bool _isDataLoaded = false;

    //    private readonly IPrintService _printService;

    //    public IPrintService PrintService => _printService;

    //    public bool IsConfigure => _isConfigure;
    //    public bool IsDataLoaded => _isDataLoaded;

    //    public void Configure()
    //    {

    //        if (IsOutOfConfigurationStates())
    //            LoggingHelpers.ThrowIsNotConfigureException(Name);

    //        if (_isConfigure && _isDataLoaded)
    //            return;

    //        if (Ninjascript.State == State.Configure && !_isConfigure)
    //            Configure(out _isConfigure);

    //        else if (Ninjascript.State == State.DataLoaded && !_isConfigure)
    //            Configure(out _isConfigure);

    //        //else if (Ninjascript.State == State.DataLoaded && _isConfigure)
    //        //    DataLoaded(out _isDataLoaded);

    //        LogConfigurationState();
    //    }
    //    public void DataLoaded()
    //    {

    //        // TODO: Delete this line
    //        Debugger.Break();

    //        if (Ninjascript.State != State.DataLoaded)
    //            LoggingHelpers.ThrowIsNotConfigureException(Name);

    //        if (_isConfigure && _isDataLoaded)
    //            return;

    //        if (Ninjascript.State == State.DataLoaded && !_isConfigure)
    //            Configure(out _isConfigure);

    //        if (Ninjascript.State == State.DataLoaded && _isConfigure)
    //            DataLoaded(out _isDataLoaded);

    //        LogConfigurationState();
    //    }
    //    public virtual void Terminated() { }

    //    /// <summary>
    //    /// Method to configure the service. This method should be used by 
    //    /// services that can be configured without first passing any filters.
    //    /// </summary>
    //    /// <param name="isConfigured">True, if the service has been configure, otherwise false.</param>
    //    internal abstract void Configure(out bool isConfigured);

    //    /// <summary>
    //    /// Method to configure the service when NinjaScript data is loaded. This method should be used by 
    //    /// services that can be configured without first passing any filters.
    //    /// </summary>
    //    /// <param name="isDataLoaded">True, if the service has been configure, otherwise false.</param>
    //    internal abstract void DataLoaded(out bool isDataLoaded);

    //    protected BaseNinjascriptElement(NinjaScriptBase ninjascript, IPrintService printService, IInfo info) : base(ninjascript,info)
    //    {
    //        _printService = printService;
    //    }

    //    public void Log(Logging.LogLevel level, string state)
    //    {
    //        if (_printService == null || !_printService.IsEnable)
    //            return;
    //        switch (level)
    //        {
    //            case Logging.LogLevel.Trace:
    //                _printService?.LogTrace(GetLogString(state));
    //                break;
    //            case Logging.LogLevel.Debug:
    //                _printService?.LogDebug(GetLogString(state));
    //                break;
    //            case Logging.LogLevel.Information:
    //                _printService?.LogInformation(GetLogString(state));
    //                break;
    //            case Logging.LogLevel.Warning:
    //                _printService?.LogWarning(GetLogString(state));
    //                break;
    //            case Logging.LogLevel.Error:
    //                _printService?.LogError(GetLogString(state));
    //                break;
    //            default:
    //                break;
    //        }
    //    }
    //    public virtual void LogConfigurationState()
    //    {
    //        if (_printService != null && _printService.IsEnable)
    //            return;

    //        if (IsDataLoaded && Ninjascript.State == State.DataLoaded)
    //            Log(Logging.LogLevel.Information, "has been loaded successfully.");
    //        else if (IsConfigure && Ninjascript.State == State.Configure)
    //            Log(Logging.LogLevel.Information, "has been configured successfully.");
    //        else if (!(IsConfigure && IsDataLoaded) && Ninjascript.State == State.DataLoaded)
    //            Log(Logging.LogLevel.Error, "could not be configured. The service will not work.");
    //        else
    //            Log(Logging.LogLevel.Error, "could not be configured. You are configuring the service out of configure or data loaded states.");
    //    }

    //}

    //public abstract class BaseNinjascriptElement<TInfo> : BaseNinjascriptElement
    //    where TInfo : IInfo
    //{
    //    new public TInfo Info => (TInfo)base.Info;

    //    protected BaseNinjascriptElement(NinjaScriptBase ninjascript, IPrintService printService, IInfo info) : base(ninjascript, printService, info)
    //    {
    //    }
    //}

    //public abstract class BaseNinjascriptElement<TInfo,TOptions> : BaseNinjascriptElement
    //    where TInfo : IInfo
    //    where TOptions : IOptions
    //{
    //    private readonly TOptions _options;

    //    new public TInfo Info => (TInfo)base.Info;
    //    public TOptions Options => _options;

    //    protected BaseNinjascriptElement(NinjaScriptBase ninjascript, IPrintService printService, IInfo info) : base(ninjascript, printService, info)
    //    {
    //    }
    //}

}
