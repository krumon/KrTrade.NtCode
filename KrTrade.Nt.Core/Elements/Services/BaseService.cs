using KrTrade.Nt.Core.Data;
using NinjaTrader.NinjaScript;
using System;

namespace KrTrade.Nt.Core.Elements
{
    public abstract class BaseService : BaseElement<IServiceInfo>, IService
    {

        #region Implementation

        new public ServiceType Type { get => base.Type.ToServiceType(); set => base.Type = value.ToElementType(); }
        public IServiceOptions Options { get; protected set; }
        public bool Equals(IService other) => Equals(other as IElement);

        // Quick access to properties
        public bool IsEnable => Options.IsEnable;
        public bool IsLogEnable => Options.IsLogEnable;

        #endregion

        /// <summary>
        /// Create <see cref="BaseService"/> instance with specified information and options.
        /// This instance must be created in the 'Ninjascript.State == Configure'.
        /// </summary>
        /// <param name="ninjascript">The <see cref="NinjaScriptBase"/> to gets 'Ninjatrader.NinjaScript' properties and objects.</param>
        /// <param name="info">The service informartion.</param>
        /// <param name="options">The service options.</param>
        /// <exception cref="ArgumentNullException">The <see cref="NinjaScriptBase"/> cannot be null.</exception>
        protected BaseService(NinjaScriptBase ninjascript, IServiceInfo info, IServiceOptions options) : base(ninjascript,info)
        {
            Options = options ?? throw new ArgumentNullException($"The {nameof(options)} argument cannot be null.");
        }

    }

    public abstract class BaseService<TInfo> : BaseService, IService<TInfo>
        where TInfo : IServiceInfo, new()
    {

        new public TInfo Info => (TInfo)base.Info;

        /// <summary>
        /// Create <see cref="BaseService"/> instance with specified information and options.
        /// This instance must be created in the 'Ninjascript.State == Configure'.
        /// </summary>
        /// <param name="ninjascript">The <see cref="NinjaScriptBase"/> to gets 'Ninjatrader.NinjaScript' properties and objects.</param>
        /// <param name="info">The service informartion.</param>
        /// <param name="options">The service options.</param>
        /// <exception cref="ArgumentNullException">The <see cref="NinjaScriptBase"/> cannot be null.</exception>
        protected BaseService(NinjaScriptBase ninjascript, TInfo info, IServiceOptions options) : 
            base(ninjascript, info == null ? new TInfo() : info, options ?? new ServiceOptions()) 
        {
        }

    }

    public abstract class BaseService<TInfo,TOptions> : BaseService<TInfo>, IService<TInfo,TOptions>
        where TInfo : IServiceInfo, new()
        where TOptions : IServiceOptions, new()
    {
        new public TOptions Options => (TOptions)base.Options;

        /// <summary>
        /// Create <see cref="BaseService"/> instance with specified information and options.
        /// This instance must be created in the 'Ninjascript.State == Configure'.
        /// </summary>
        /// <param name="ninjascript">The <see cref="NinjaScriptBase"/> to gets 'Ninjatrader.NinjaScript' properties and objects.</param>
        /// <param name="info">The service informartion.</param>
        /// <param name="options">The service options.</param>
        /// <exception cref="ArgumentNullException">The <see cref="NinjaScriptBase"/> cannot be null.</exception>
        protected BaseService(NinjaScriptBase ninjascript, TInfo info, TOptions options) : 
            base(ninjascript, info == null ? new TInfo() : info, options == null ? new TOptions() : options) 
        {
        }
    }
}
