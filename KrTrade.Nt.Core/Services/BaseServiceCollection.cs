using KrTrade.Nt.Core.Data;
using KrTrade.Nt.Core.Infos;
using KrTrade.Nt.Core.Logging;
using KrTrade.Nt.Core.Options;
using NinjaTrader.NinjaScript;

namespace KrTrade.Nt.Core.Services
{

    public abstract class BaseServiceCollection<TElement,TElementInfo,TElementOptions> : BaseCollection<TElement, ServiceType,TElementInfo,IServiceCollectionInfo<TElementInfo,TElementOptions>,ServiceCollectionType>, IServiceCollection<TElement,TElementInfo,TElementOptions>
        where TElement : IService<TElementInfo,TElementOptions>
        where TElementInfo : IServiceInfo
        where TElementOptions : IServiceOptions
    {

        public bool IsConfigureAll => IsConfigure && IsDataLoaded;
        protected bool IsPrintServiceAvailable => PrintService != null && IsLogEnable;

        protected BaseServiceCollection(NinjaScriptBase ninjascript, IPrintService printService, IServiceCollectionInfo<TElementInfo, TElementOptions> info, IServiceOptions options) : base(ninjascript, printService, info, options ?? new ServiceCollectionOptions())
        {
            if (string.IsNullOrEmpty(Info.Name))
                Info.Name = Info.Type.ToString();
        }

        #region Implementation

        public void Log() => Log(LogLevel.Information, null, 0);
        public void Log(int tabOrder) => Log(LogLevel.Information, null, tabOrder);
        public void Log(string message, int tabOrder = 0) => Log(LogLevel.Information, message, tabOrder);
        public void Log(LogLevel level, string message, int tabOrder = 0)
        {
            if (PrintService == null || !Options.IsLogEnable)
                return;

            string tab = string.Empty;
            if (tabOrder > 0)
                for (int i = 0; i < tabOrder; i++)
                    tab += "\t";

            switch (level)
            {
                case LogLevel.Trace:
                    PrintService?.LogTrace(tab + ToString() + " " + message);
                    break;
                case LogLevel.Debug:
                    PrintService?.LogDebug(tab + ToString() + " " + message);
                    break;
                case LogLevel.Information:
                    PrintService?.LogInformation(tab + ToString() + " " + message);
                    break;
                case LogLevel.Warning:
                    PrintService?.LogWarning(tab + ToString() + " " + message);
                    break;
                case LogLevel.Error:
                    PrintService?.LogError(tab + ToString() + " " + message);
                    break;
                default:
                    break;
            }
        }

        #endregion

    }
}
