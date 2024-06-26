using KrTrade.Nt.Core.Data;
using KrTrade.Nt.Core.Logging;
using NinjaTrader.NinjaScript;
using System;

namespace KrTrade.Nt.Core.Services
{
    public abstract class BaseService<TInfo,TOptions> : BaseElement<ServiceType,TInfo,TOptions>, IService<TInfo,TOptions>
        where TInfo : IServiceInfo
        where TOptions : IServiceOptions
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
            base(ninjascript, printService, info, options) 
        {
        }
    }
}
