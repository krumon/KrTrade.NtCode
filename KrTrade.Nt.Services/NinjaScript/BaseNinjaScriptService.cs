//using NinjaTrader.NinjaScript;
//using System;

//namespace KrTrade.Nt.Services
//{
//    public abstract class BaseNinjaScriptService : BaseService
//    {
//        /// <summary>
//        /// Create <see cref="NinjaScriptBase"/> instance and configure it.
//        /// This instance must be created in the 'Ninjascript.State == Configure'.
//        /// </summary>
//        /// <param name="ninjascript">The ninjatrader ninjascript.</param>
//        /// <exception cref="Exception"><paramref name="ninjascript"/> cannot be null.</exception>
//        public BaseNinjaScriptService(NinjaScriptBase ninjascript) : base(ninjascript)
//        {
//            if (Ninjascript.State != State.Configure)
//                throw new Exception($"The 'NinjaScriptService' instance must be executed when 'NinjaScript.State' is equal to 'State.Configure'");
//        }
//    }

//    public abstract class BaseNinjaScriptService<T> : BaseNinjaScriptService
//        where T : NinjaScriptServiceOptions, new()
//    {
//        public T Options { get; protected set; }

//        /// <summary>
//        /// Create <see cref="NinjaScriptBase"/> instance and configure it.
//        /// This instance must be created in the 'Ninjascript.State == Configure'.
//        /// </summary>
//        /// <param name="ninjascript">The ninjatrader ninjascript.</param>
//        /// <exception cref="Exception"><paramref name="ninjascript"/> cannot be null.</exception>
//        public BaseNinjaScriptService(NinjaScriptBase ninjascript) : this(ninjascript, new T()) { }

//        /// <summary>
//        /// Create <see cref="NinjaScriptBase"/> instance and configure it.
//        /// This instance must be created in the 'Ninjascript.State == Configure'.
//        /// </summary>
//        /// <param name="ninjascript">The ninjatrader ninjascript.</param>
//        /// <param name="options">The ninjascript service options.</param>
//        /// <exception cref="Exception"><paramref name="ninjascript"/> cannot be null.</exception>
//        public BaseNinjaScriptService(NinjaScriptBase ninjascript, T options) : base(ninjascript)
//        {
//            Options = options ?? new T();
//        }

//        /// <summary>
//        /// Create <see cref="NinjaScriptBase"/> instance and configure it.
//        /// This method must be executed in the 'Ninjascript.State == Configure'.
//        /// </summary>
//        /// <param name="ninjascript">The ninjatrader injascript.</param>
//        /// <param name="delegateOptions">The delegate options to configure the service options.</param>
//        /// <exception cref="Exception"><paramref name="ninjascript"/> cannot be null.</exception>
//        public BaseNinjaScriptService(NinjaScriptBase ninjascript, Action<T> delegateOptions) : base(ninjascript)
//        {
//            T options = new T();
//            delegateOptions?.Invoke(options);
//            Options = options;
//        }
//    }
//}
