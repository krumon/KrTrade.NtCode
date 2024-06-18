using KrTrade.Nt.Core.Data;
using NinjaTrader.NinjaScript;
using System;

namespace KrTrade.Nt.Core
{
    //public abstract class BaseService : BaseElement<IServiceInfo>, IService
    //{

    //    new public ServiceType Type { get => base.Type.ToServiceType(); set => base.Type = value.ToElementType(); }
    //    public IServiceOptions Options { get; protected set; }
    //    public bool Equals(IService other) => Equals(other as IElement);

    //// Quick access to properties
    //public bool IsEnable => Options.IsEnable;
    //public bool IsLogEnable => Options.IsLogEnable;
    //public Calculate CalculateMode { get => Options.CalculateMode; internal set { Options.CalculateMode = value; } }
    //public MultiSeriesCalculateMode MultiSeriesCalculateMode { get => Options.MultiSeriesCalculateMode; internal set { Options.MultiSeriesCalculateMode = value; } }

    //    /// <summary>
    //    /// Create <see cref="BaseService"/> instance with specified information and options.
    //    /// This instance must be created in the 'Ninjascript.State == Configure'.
    //    /// </summary>
    //    /// <param name="ninjascript">The <see cref="NinjaScriptBase"/> to gets 'Ninjatrader.NinjaScript' properties and objects.</param>
    //    /// <param name="printService">The <see cref="IPrintService"/> to log messages.</param>
    //    /// <param name="info">The service informartion.</param>
    //    /// <param name="options">The service options.</param>
    //    /// <exception cref="ArgumentNullException">The <see cref="NinjaScriptBase"/> cannot be null.</exception>
    //    protected BaseService(NinjaScriptBase ninjascript, IPrintService printService, IServiceInfo info, IServiceOptions options) : 
    //        base(ninjascript,printService, info ?? new ServiceInfo())
    //    {
    //        Options = options ?? throw new ArgumentNullException($"The {nameof(options)} argument cannot be null.");
    //    }

    //}

    //public abstract class BaseService<TInfo> : BaseService, IService<TInfo>
    //    where TInfo : IServiceInfo, new()
    //{

    //    new public TInfo Info => (TInfo)base.Info;

    //    /// <summary>
    //    /// Create <see cref="BaseService"/> instance with specified information and options.
    //    /// This instance must be created in the 'Ninjascript.State == Configure'.
    //    /// </summary>
    //    /// <param name="ninjascript">The <see cref="NinjaScriptBase"/> to gets 'Ninjatrader.NinjaScript' properties and objects.</param>
    //    /// <param name="printService">The <see cref="IPrintService"/> to log messages.</param>
    //    /// <param name="info">The service informartion.</param>
    //    /// <param name="options">The service options.</param>
    //    /// <exception cref="ArgumentNullException">The <see cref="NinjaScriptBase"/> cannot be null.</exception>
    //    protected BaseService(NinjaScriptBase ninjascript, IPrintService printService, TInfo info, IServiceOptions options) : 
    //        base(ninjascript, printService, info == null ? new TInfo() : info, options ?? new ServiceOptions()) 
    //    {
    //    }

    //}

    public abstract class BaseService<TInfo,TOptions> : BaseElement<ServiceType,IServiceInfo,IServiceOptions>, IService
        where TInfo : IServiceInfo, new()
        where TOptions : IServiceOptions, new()
    {

        // Quick access to properties
        public bool IsLogEnable => Options.IsLogEnable;
        public Calculate CalculateMode { get => Options.CalculateMode; internal set { Options.CalculateMode = value; } }
        public MultiSeriesCalculateMode MultiSeriesCalculateMode { get => Options.MultiSeriesCalculateMode; internal set { Options.MultiSeriesCalculateMode = value; } }

        /// <summary>
        /// Create <see cref="BaseService"/> instance with specified information and options.
        /// This instance must be created in the 'Ninjascript.State == Configure'.
        /// </summary>
        /// <param name="ninjascript">The <see cref="NinjaScriptBase"/> to gets 'Ninjatrader.NinjaScript' properties and objects.</param>
        /// <param name="printService">The <see cref="IPrintService"/> to log messages.</param>
        /// <param name="info">The service informartion.</param>
        /// <param name="options">The service options.</param>
        /// <exception cref="ArgumentNullException">The <see cref="NinjaScriptBase"/> cannot be null.</exception>
        protected BaseService(NinjaScriptBase ninjascript, IPrintService printService, TInfo info, TOptions options) : 
            base(ninjascript, printService, info == null ? new TInfo() : info, options == null ? new TOptions() : options) 
        {
        }
    }
}
