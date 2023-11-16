using NinjaTrader.NinjaScript;
using System;
using System.Collections.Generic;

namespace KrTrade.Nt.Services
{
    public abstract class BaseService
    {
        internal NinjaScriptBase _ninjascript;
        internal PrintService _printService;

        /// <summary>
        /// Gets the name of the service.
        /// </summary>
        public string Name { get; protected set; } = "Krumon_Service";

        protected BaseService(NinjaScriptBase ninjascript) : this(ninjascript, null)
        {
        }
        protected BaseService(NinjaScriptBase ninjascript, PrintService printService) 
        {
            _ninjascript = ninjascript ?? throw new ArgumentNullException($"Error in 'BaseService' constructor. The {nameof(ninjascript)} argument cannot be null.");

            if (!IsInConfigurationStates())
                throw new Exception($"The {Name} instance must be executed when 'NinjaScript.State' is 'Configure' or 'DataLoaded'.");

            _printService = printService;
        }

        #region Protected methods

        protected NinjaScriptBase Ninjascript { get { return _ninjascript; } }

        protected int GetCurrentBar(int barsInProgress, int barsAgo)
        {
            if (barsInProgress < 0 || barsInProgress != _ninjascript.BarsInProgress)
                return -1;
            if (barsAgo < 0 || barsAgo >= _ninjascript.BarsArray[barsInProgress].Count)
                return -1;

            return _ninjascript.CurrentBars[barsInProgress];
        }
        protected double GetOpen(int barsInProgress, int barsAgo)
        {
            if (barsInProgress < 0 || barsInProgress != _ninjascript.BarsInProgress)
                return 0.0;
            if (barsAgo < 0 || barsAgo >= _ninjascript.BarsArray[barsInProgress].Count)
                return 0.0;

            return _ninjascript.Opens[barsInProgress][barsAgo];
        }
        protected double GetHigh(int barsInProgress, int barsAgo)
        {
            if (barsInProgress < 0 || barsInProgress != _ninjascript.BarsInProgress)
                return double.MinValue;
            if (barsAgo < 0 || barsAgo >= _ninjascript.BarsArray[barsInProgress].Count)
                return double.MinValue;

            return _ninjascript.Highs[barsInProgress][barsAgo];
        }
        protected double GetLow(int barsInProgress, int barsAgo)
        {
            if (barsInProgress < 0 || barsInProgress != _ninjascript.BarsInProgress)
                return double.MaxValue;
            if (barsAgo < 0 || barsAgo >= _ninjascript.BarsArray[barsInProgress].Count)
                return double.MaxValue;

            return _ninjascript.Lows[barsInProgress][barsAgo];
        }
        protected double GetClose(int barsInProgress, int barsAgo)
        {
            if (barsInProgress < 0 || barsInProgress != _ninjascript.BarsInProgress)
                return 0.0;
            if (barsAgo < 0 || barsAgo >= _ninjascript.BarsArray[barsInProgress].Count)
                return 0.0;

            return _ninjascript.Closes[barsInProgress][barsAgo];
        }
        protected double GetVolume(int barsInProgress, int barsAgo)
        {
            if (barsInProgress < 0 || barsInProgress != _ninjascript.BarsInProgress)
                return -1.0;
            if (barsAgo < 0 || barsAgo >= _ninjascript.BarsArray[barsInProgress].Count)
                return -1.0;

            return _ninjascript.Volumes[barsInProgress][barsAgo];
        }
        protected DateTime GetTime(int barsInProgress, int barsAgo)
        {
            if (barsInProgress < 0 || barsInProgress != _ninjascript.BarsInProgress)
                return new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Local);
            if (barsAgo < 0 || barsAgo >= _ninjascript.BarsArray[barsInProgress].Count)
                return new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Local);

            return _ninjascript.Times[barsInProgress][barsAgo];
        }

        protected void ExecuteMethods(List<Action> methods)
        {
            if (methods == null || methods.Count == 0)
                return;

            for (int i = 0; i < methods.Count; i++)
                methods[i]?.Invoke();
        }

        protected bool IsOutOfRange(int barsInProgress)
        {
            if (barsInProgress < 0 || barsInProgress >= _ninjascript.BarsArray.Length)
                throw new ArgumentOutOfRangeException(nameof(barsInProgress));
            return false;
        }

        protected bool IsInActiveStates()
        {
            if (_ninjascript.State == State.SetDefaults || _ninjascript.State == State.Configure || _ninjascript.State == State.Terminated)
                return false;
            else 
                return true;
        }

        protected bool IsInConfigurationStates()
        {
            if (_ninjascript.State == State.Configure || _ninjascript.State == State.DataLoaded)
                return true;
            else 
                return false;
        }

        protected bool IsInRunningStates()
        {
            if (_ninjascript.State == State.Historical || _ninjascript.State == State.Realtime)
                return true;
            else 
                return false;
        }

        #endregion
    }
}
