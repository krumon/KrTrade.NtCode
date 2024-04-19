using KrTrade.Nt.Core.Info;
using KrTrade.Nt.Core.Options;
using NinjaTrader.NinjaScript;
using System;
using System.Collections.Generic;

namespace KrTrade.Nt.Core.Services
{
    public abstract class BaseService : IService
    {

        #region Private members

        private readonly NinjaScriptBase _ninjascript;
        protected IOptions _options;
        protected IInfo _info;

        #endregion

        #region Implementation

        public NinjaScriptBase Ninjascript => _ninjascript;
        public IInfo Info { get => _info ?? new ServiceInfo(); protected set { _info = value; } }
        public IOptions Options { get => _options ?? new ServiceOptions(); protected set { _options = value; } }

        public string Key => GetKey();
        public string Name { get => Info.Name; set { Info.Name = value; } }
        public bool IsEnable { get => Options.IsEnable; set { Options.IsEnable = value; } }
        public bool IsLogEnable { get => Options.IsLogEnable; set { Options.IsLogEnable = value; } }

        public bool Equals(IHasKey other) => Info.Equals(other);

        protected abstract string GetKey();

        #endregion

        #region Constructors

        /// <summary>
        /// Create <see cref="BaseService"/> instance with specified information and options.
        /// This instance must be created in the 'Ninjascript.State == Configure'.
        /// </summary>
        /// <param name="ninjascript">The <see cref="NinjaScriptBase"/> to gets 'Ninjatrader.NinjaScript' properties and objects.</param>
        /// <param name="info">The service informartion.</param>
        /// <param name="options">The service options.</param>
        /// <exception cref="ArgumentNullException">The <see cref="NinjaScriptBase"/> cannot be null.</exception>
        protected BaseService(NinjaScriptBase ninjascript, IInfo info, IOptions options)
        {
            _ninjascript = ninjascript ?? throw new ArgumentNullException($"Error in 'BaseService' constructor. The {nameof(ninjascript)} argument cannot be null.");
            _info = info;
            _options = options;
        }

        #endregion

        #region Protected methods

        protected int GetBarIdx(int barsInProgress, int barsAgo)
        {
            if (barsInProgress < 0 || barsInProgress != Ninjascript.BarsInProgress)
                return -1;
            if (barsAgo < 0 || barsAgo >= Ninjascript.BarsArray[barsInProgress].Count)
                return -1;

            return Ninjascript.CurrentBars[barsInProgress]-barsAgo;
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

    public abstract class BaseService<TInfo> : BaseService, IService<TInfo>
        where TInfo : ServiceInfo, new()
    {

        protected new TInfo _info;
        public new TInfo Info { get => _info ?? new TInfo(); protected set { _info = value; } }

        /// <summary>
        /// Create <see cref="BaseService"/> instance with specified information and options.
        /// This instance must be created in the 'Ninjascript.State == Configure'.
        /// </summary>
        /// <param name="ninjascript">The <see cref="NinjaScriptBase"/> to gets 'Ninjatrader.NinjaScript' properties and objects.</param>
        /// <param name="info">The service informartion.</param>
        /// <param name="options">The service options.</param>
        /// <exception cref="ArgumentNullException">The <see cref="NinjaScriptBase"/> cannot be null.</exception>
        protected BaseService(NinjaScriptBase ninjascript, TInfo info, IOptions options) : base(ninjascript, info, options) { }

    }

    public abstract class BaseService<TInfo,TOptions> : BaseService<TInfo>, IService<TInfo,TOptions>
        where TInfo : ServiceInfo, new()
        where TOptions : ServiceOptions, new()
    {
        protected new TOptions _options;
        public new TOptions Options { get => _options ?? new TOptions(); protected set { _options = value; } }

        //protected BaseService(NinjaScriptBase ninjascript) : this(ninjascript, null,null) { }
        ////protected BaseService(NinjaScriptBase ninjascript, Action<TOptions> configureOptions) : this(ninjascript, configureOptions, null) { }
        //protected BaseService(NinjaScriptBase ninjascript, TOptions options) : this(ninjascript, null, options) { }
        //protected BaseService(NinjaScriptBase ninjascript, TInfo info) : this(ninjascript, info, null) { }
        //protected BaseService(NinjaScriptBase ninjascript, Action<TOptions> configureOptions, TOptions options) : base(ninjascript)
        //{
        //    Options = options ?? new TOptions();
        //    configureOptions?.Invoke(Options);
        //}

        /// <summary>
        /// Create <see cref="BaseService"/> instance with specified information and options.
        /// This instance must be created in the 'Ninjascript.State == Configure'.
        /// </summary>
        /// <param name="ninjascript">The <see cref="NinjaScriptBase"/> to gets 'Ninjatrader.NinjaScript' properties and objects.</param>
        /// <param name="info">The service informartion.</param>
        /// <param name="options">The service options.</param>
        /// <exception cref="ArgumentNullException">The <see cref="NinjaScriptBase"/> cannot be null.</exception>
        protected BaseService(NinjaScriptBase ninjascript, TInfo info, TOptions options) : base(ninjascript, info, options) { }

    }
}
