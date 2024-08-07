﻿using KrTrade.Nt.Core.Infos;
using KrTrade.Nt.Core.Logging;
using KrTrade.Nt.Core.Options;
using NinjaTrader.NinjaScript;
using System;
using System.Collections.Generic;

namespace KrTrade.Nt.Core
{
    public abstract class BaseScript : IBaseScript
    {
        private readonly NinjaScriptBase _ninjascript;

        public NinjaScriptBase Ninjascript => _ninjascript;

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

    public abstract class BaseScript<TType> : BaseScript, IScript<TType>
    where TType : Enum
    {
        public TType Type { get; protected set; }
        public abstract string Name { get; }

        protected abstract TType ToElementType();

        protected BaseScript(NinjaScriptBase ninjascript) : base(ninjascript) 
        {
            Type = ToElementType();
        }

        protected void Log(IPrintService printService, LogLevel level, string state, string name = "")
        {
            if (printService == null || !printService.IsEnable)
                return;

            string message = name + " " + state;
            switch (level)
            {
                case LogLevel.Trace:
                    printService?.Log(LogLevel.Trace, message, null);
                    break;
                case LogLevel.Debug:
                    printService?.Log(LogLevel.Trace, message, null);
                    break;
                case LogLevel.Information:
                    printService?.Log(LogLevel.Trace, message, null);
                    break;
                case LogLevel.Warning:
                    printService?.Log(LogLevel.Trace, message, null);
                    break;
                case LogLevel.Error:
                    printService?.Log(LogLevel.Trace, message, null);
                    break;
                default:
                    break;
            }
        }
        protected void Log(IPrintService printService, LogLevel level, BarsLogLevel barsLogLevel, string state, string name = "")
        {
            if (printService == null || !printService.IsEnable)
                return;

            string message = name + " " + state;
            switch (level)
            {
                case LogLevel.Trace:
                    printService?.Log(LogLevel.Trace, barsLogLevel, message, null);
                    break;
                case LogLevel.Debug:
                    printService?.Log(LogLevel.Debug, barsLogLevel, message, null);
                    break;
                case LogLevel.Information:
                    printService?.Log(LogLevel.Information, barsLogLevel, message, null);
                    break;
                case LogLevel.Warning:
                    printService?.Log(LogLevel.Warning, barsLogLevel, message, null);
                    break;
                case LogLevel.Error:
                    printService?.Log(LogLevel.Error, barsLogLevel, message, null);
                    break;
                default:
                    break;
            }
        }
        protected void LogConfigurationState(IPrintService printService, bool isConfigured, bool isDataLoaded)
        {
            if (printService == null || !printService.IsEnable)
                return;

            if (isDataLoaded && Ninjascript.State == State.DataLoaded)
                Log(printService, LogLevel.Information, "has been loaded successfully.", Name);
            else if (isConfigured && Ninjascript.State == State.Configure)
                Log(printService, LogLevel.Information, "has been configured successfully.", Name);
            else if (!(isConfigured && isDataLoaded) && Ninjascript.State == State.DataLoaded)
                Log(printService, LogLevel.Error, "could not be configured. The service will not work.", Name);
            else
                Log(printService, LogLevel.Error, "could not be configured. You are configuring the service out of configure or data loaded states.", Name);
        }
    }

    public abstract class BaseOptionsScript<TType,TOptions> : BaseScript<TType>, IOptionsScript<TType,TOptions>
        where TOptions : IOptions
        where TType : Enum
    {
        private readonly TOptions _options;
        public TOptions Options => _options;

        protected BaseOptionsScript(NinjaScriptBase ninjascript, TOptions options) : base(ninjascript)
        {
            _options = options;
        }

    }

    public abstract class BaseInfoScript<TType,TInfo> : BaseScript<TType>, IInfoScript<TType,TInfo>
        where TInfo : IInfo
        where TType : Enum
    {
        private readonly TInfo _info;
        public TInfo Info => _info;

        protected BaseInfoScript(NinjaScriptBase ninjascript, TInfo info) : base(ninjascript)
        {
            if (info == null)
                throw new ArgumentNullException($"The {nameof(info)} argument cannot be null.");
            _info = info;
        }
    }

    public abstract class BaseScript<TType, TInfo, TOptions> : BaseScript<TType>, IScript<TType,TInfo, TOptions>
        where TInfo : IInfo<TType>
        where TOptions : IOptions
        where TType : Enum
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
}
