using KrTrade.Nt.Core.Data;
using KrTrade.Nt.Core.Interfaces;
using NinjaTrader.NinjaScript;
using System;

namespace KrTrade.Nt.Services
{
    public class BaseNinjaScriptService : IBaseNinjaScriptService
    {
        public NinjaScriptBase NinjaScript {get; protected set;}

        /// <summary>
        /// Create <see cref="NinjaScriptBase"/> instance and configure it.
        /// This method must be executed in the 'Ninjascript.State == Configure'.
        /// </summary>
        /// <param name="ninjascript">The ninjatrader injascript.</param>
        /// <exception cref="Exception"><paramref name="ninjascript"/> cannot be null.</exception>
        public BaseNinjaScriptService(NinjaScriptBase ninjascript) : this(ninjascript,true)
        {
        }

        /// <summary>
        /// Create <see cref="NinjaScriptBase"/> instance and configure it.
        /// This method must be executed in the 'Ninjascript.State == Configure'.
        /// </summary>
        /// <param name="ninjascript">The ninjatrader injascript.</param>
        /// <param name="configure">Indicates if the service must be configured.</param>
        /// <exception cref="Exception"><paramref name="ninjascript"/> cannot be null.</exception>
        public BaseNinjaScriptService(NinjaScriptBase ninjascript, bool configure)
        {
            NinjaScript = ninjascript ?? throw new Exception("The ninjascript argument cannot be null. The argument is necesary to configure the service.");
            if (configure) Configure();
        }

        public virtual void SetDefaults() { }
        public virtual void Configure() { }
        public virtual void Active() { }
        public virtual void DataLoaded() { }
        public virtual void Historical() { }
        public virtual void Transition() { }
        public virtual void Realtime() { }
        public virtual void Terminated() { }

        public virtual void OnBarUpdate() { }

    }

    public class BaseNinjaScriptService<T> : BaseNinjaScriptService, IBaseNinjaScriptService<T>
        where T : class, new()
    {
        public T Options {get;protected set;}

        /// <summary>
        /// Create <see cref="NinjaScriptBase"/> instance and configure it.
        /// This method must be executed in the 'Ninjascript.State == Configure'.
        /// </summary>
        /// <param name="ninjascript">The ninjatrader injascript.</param>
        /// <exception cref="Exception"><paramref name="ninjascript"/> cannot be null.</exception>
        public BaseNinjaScriptService(NinjaScriptBase ninjascript) : this(ninjascript, new T()) { }

        /// <summary>
        /// Create <see cref="NinjaScriptBase"/> instance and configure it.
        /// This method must be executed in the 'Ninjascript.State == Configure'.
        /// </summary>
        /// <param name="ninjascript">The ninjatrader injascript.</param>
        /// <param name="options">The ninjascript options.</param>
        /// <exception cref="Exception"><paramref name="ninjascript"/> cannot be null.</exception>
        public BaseNinjaScriptService(NinjaScriptBase ninjascript, T options) : base(ninjascript, false)
        {
            Options = options ?? new T();
            Configure();
        }

        /// <summary>
        /// Create <see cref="NinjaScriptBase"/> instance and configure it.
        /// This method must be executed in the 'Ninjascript.State == Configure'.
        /// </summary>
        /// <param name="ninjascript">The ninjatrader injascript.</param>
        /// <param name="delegateOptions">The delegate options to configure the service options.</param>
        /// <exception cref="Exception"><paramref name="ninjascript"/> cannot be null.</exception>
        public BaseNinjaScriptService(NinjaScriptBase ninjascript, Action<T> delegateOptions) : base(ninjascript, false)
        {
            T options = new T();
            delegateOptions?.Invoke(options);
            Options = options;
            Configure();
        }
    }
}
