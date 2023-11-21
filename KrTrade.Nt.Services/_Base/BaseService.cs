using NinjaTrader.NinjaScript;
using System;
using System.Collections.Generic;

namespace KrTrade.Nt.Services
{
    public abstract class BaseService
    {
        /// <summary>
        /// Gets the name of the service.
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// Gets the Ninjatrader NinjaScript.
        /// </summary>
        public NinjaScriptBase Ninjascript { get; protected set; }

        /// <summary>
        /// Gets the <see cref="PrintService"/> to write in the NinjaScript output window.
        /// </summary>
        public PrintService Print { get; protected set; }

        public BaseService(NinjaScriptBase ninjascript) : this(ninjascript, null)
        {
        }
        public BaseService(NinjaScriptBase ninjascript, PrintService printService) 
        {
            Ninjascript = ninjascript ?? throw new ArgumentNullException($"Error in 'BaseService' constructor. The {nameof(ninjascript)} argument cannot be null.");

            if (!IsInConfigurationState())
                throw new Exception($"The {Name} instance must be executed when 'NinjaScript.State' is 'Configure' or 'DataLoaded'.");

            Print = printService;
        }

        #region Protected methods

        protected int GetCurrentBar(int barsInProgress, int barsAgo)
        {
            if (barsInProgress < 0 || barsInProgress != Ninjascript.BarsInProgress)
                return -1;
            if (barsAgo < 0 || barsAgo >= Ninjascript.BarsArray[barsInProgress].Count)
                return -1;

            return Ninjascript.CurrentBars[barsInProgress];
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

        protected void ExecuteMethods(List<Action> methods)
        {
            if (methods == null || methods.Count == 0)
                return;

            for (int i = 0; i < methods.Count; i++)
                methods[i]?.Invoke();
        }
        protected bool IsOutOfRange(int barsInProgress)
        {
            if (barsInProgress < 0 || barsInProgress >= Ninjascript.BarsArray.Length)
                throw new ArgumentOutOfRangeException(nameof(barsInProgress));
            return false;
        }
        protected bool IsInActiveState()
        {
            if (Ninjascript.State == State.SetDefaults || Ninjascript.State == State.Configure || Ninjascript.State == State.Terminated)
                return false;
            else 
                return true;
        }
        protected bool IsInConfigurationState()
        {
            if (Ninjascript.State == State.Configure || Ninjascript.State == State.DataLoaded)
                return true;
            else 
                return false;
        }
        protected bool IsInRunningState()
        {
            if (Ninjascript.State == State.Historical || Ninjascript.State == State.Realtime)
                return true;
            else 
                return false;
        }

        #endregion
    }

    public abstract class BaseService<T> : BaseService
    where T : BaseServiceOptions, new()
    {
        /// <summary>
        /// Gets the service options.
        /// </summary>
        protected internal T Options { get; protected set; }

        /// <summary>
        /// Create <see cref="NinjaScriptBase"/> instance and configure it.
        /// This instance must be created in the 'Ninjascript.State == Configure'.
        /// </summary>
        /// <param name="ninjascript">The ninjatrader ninjascript.</param>
        /// <exception cref="Exception"><paramref name="ninjascript"/> cannot be null.</exception>
        public BaseService(NinjaScriptBase ninjascript) : this(ninjascript, new T()) { }

        /// <summary>
        /// Create <see cref="NinjaScriptBase"/> instance and configure it.
        /// This instance must be created in the 'Ninjascript.State == Configure'.
        /// </summary>
        /// <param name="ninjascript">The ninjatrader ninjascript.</param>
        /// <param name="options">The ninjascript service options.</param>
        /// <exception cref="Exception"><paramref name="ninjascript"/> cannot be null.</exception>
        public BaseService(NinjaScriptBase ninjascript, T options) : base(ninjascript)
        {
            Options = options ?? new T();
        }

        /// <summary>
        /// Create <see cref="NinjaScriptBase"/> instance and configure it.
        /// This instance must be created in the 'Ninjascript.State == Configure'.
        /// </summary>
        /// <param name="ninjascript">The ninjatrader ninjascript.</param>
        /// <param name="printService">The service to print in the Ninjatrader output window.</param>
        /// <exception cref="Exception"><paramref name="ninjascript"/> cannot be null.</exception>
        public BaseService(NinjaScriptBase ninjascript, PrintService printService) : this(ninjascript, printService, new T()) { }

        /// <summary>
        /// Create <see cref="NinjaScriptBase"/> instance and configure it.
        /// This instance must be created in the 'Ninjascript.State == Configure'.
        /// </summary>
        /// <param name="ninjascript">The ninjatrader ninjascript.</param>
        /// <param name="printService">The service to print in the Ninjatrader output window.</param>
        /// <param name="options">The ninjascript service options.</param>
        /// <exception cref="Exception"><paramref name="ninjascript"/> cannot be null.</exception>
        public BaseService(NinjaScriptBase ninjascript, PrintService printService, T options) : base(ninjascript, printService)
        {
            Options = options ?? new T();
        }

        /// <summary>
        /// Create <see cref="NinjaScriptBase"/> instance and configure it.
        /// This method must be executed in the 'Ninjascript.State == Configure'.
        /// </summary>
        /// <param name="ninjascript">The ninjatrader injascript.</param>
        /// <param name="delegateOptions">The delegate options to configure the service options.</param>
        /// <exception cref="Exception"><paramref name="ninjascript"/> cannot be null.</exception>
        public BaseService(NinjaScriptBase ninjascript, Action<T> delegateOptions) : base(ninjascript)
        {
            T options = new T();
            delegateOptions?.Invoke(options);
            Options = options;
        }

        /// <summary>
        /// Create <see cref="NinjaScriptBase"/> instance and configure it.
        /// This method must be executed in the 'Ninjascript.State == Configure'.
        /// </summary>
        /// <param name="ninjascript">The ninjatrader injascript.</param>
        /// <param name="printService">The service to print in the Ninjatrader output window.</param>
        /// <param name="delegateOptions">The delegate options to configure the service options.</param>
        /// <exception cref="Exception"><paramref name="ninjascript"/> cannot be null.</exception>
        public BaseService(NinjaScriptBase ninjascript, PrintService printService, Action<T> delegateOptions) : base(ninjascript, printService)
        {
            T options = new T();
            delegateOptions?.Invoke(options);
            Options = options;
        }
    }

}
