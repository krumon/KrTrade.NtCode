using KrTrade.Nt.Core.Data;
using KrTrade.Nt.Core.Logging;
using NinjaTrader.NinjaScript;

namespace KrTrade.Nt.Core.Services
{

    public abstract class BaseServiceCollection<TElement> : BaseCollection<TElement, ServiceType,IServiceInfo,IServiceCollectionInfo,ServiceCollectionType>, IServiceCollection<TElement>
        where TElement : IService
    {

        public IServiceOptions Options { get; protected set; }

        public bool IsConfigureAll => IsConfigure && IsDataLoaded;

        public bool IsEnable => Options.IsEnable;
        public bool IsLogEnable => Options.IsLogEnable;

        protected bool IsPrintServiceAvailable => PrintService != null && IsLogEnable;

        protected BaseServiceCollection(NinjaScriptBase ninjascript, IPrintService printService, ServiceCollectionInfo info, ServiceCollectionOptions options) : base(ninjascript, printService, info, new ServiceOptions())
        {
            Options = options ?? new ServiceOptions();

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
