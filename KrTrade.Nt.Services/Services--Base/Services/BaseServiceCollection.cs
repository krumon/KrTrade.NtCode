//using KrTrade.Nt.Core.Data;
//using KrTrade.Nt.Core;
//using KrTrade.Nt.Core.Logging;
//using NinjaTrader.NinjaScript;
//using System;

//namespace KrTrade.Nt.Services
//{

//    public abstract class BaseServiceCollection<TElement,TInfoCollection> : BaseCollection<TElement,TInfoCollection>, INinjascriptServiceCollection<TElement,TInfoCollection>
//        where TElement : INinjascriptService
//        where TInfoCollection : IServiceCollectionInfo
//    {

//        private readonly IPrintService _printService;

//        private bool _isConfigured;
//        private bool _isDataLoaded;

//        public IServiceOptions Options { get; protected set; }

//        public ServiceCollectionType Type { get => Info.Type.ToElementType().ToServiceCollectionType(); protected set => Info.Type = value; }
//        public bool IsConfigure => _isConfigured;
//        public bool IsDataLoaded => _isDataLoaded;

//        public bool IsConfigureAll => IsConfigure && IsDataLoaded;

//        public bool IsEnable => Options.IsEnable;
//        public bool IsLogEnable => Options.IsLogEnable;

//        protected bool IsPrintServiceAvailable => _printService != null && IsLogEnable;

//        new public ServiceCollectionType Type { get; protected set; }

//        protected BaseServiceCollection(NinjaScriptBase ninjascript, IPrintService printService, ServiceCollectionInfo info, NinjascriptServiceOptions options) : base(ninjascript,info)
//        {
//            _printService = printService;
//            Options = options ?? new NinjascriptServiceOptions();

//            Type = GetServiceType();

//            if (string.IsNullOrEmpty(Info.Name))
//                Info.Name = Info.Type.ToString();
//        }
        
//        #region Implementation

//        public void Configure()
//        {
//            if (_collection == null)
//                throw new NullReferenceException($"{Name} inner collection is null.");
//            if (_collection.Count == 0)
//                if (_collection is BarsServiceCollection)
//                    throw new Exception($"{Name} inner collection is empty.");

//            _isConfigured = true;
//            foreach (var service in _collection)
//            {
//                service.Configure();
//                if (!service.IsConfigure)
//                    _isConfigured = false;
//            }

//            if (!_isConfigured)
//                PrintService.LogInformation($"'{Name}' cannot be configured because one or more 'service' could not be configured.");

//            Configure(out _isConfigured);

//            if (!_isConfigured)
//                PrintService.LogError($"'{Name}' cannot be configured successfully.");
//        }
//        public void DataLoaded()
//        {
//            if (_collection == null)
//                throw new NullReferenceException($"{Name} inner collection is null.");
//            if (_collection.Count == 0)
//                if (_collection is BarsServiceCollection)
//                    throw new Exception($"{Name} inner collection is empty.");

//            _isDataLoaded = true;

//            foreach (var service in _collection)
//            {
//                service.DataLoaded();
//                if (!service.IsDataLoaded)
//                    _isDataLoaded = false;
//            }

//            if (!_isDataLoaded)
//                PrintService.LogInformation($"'{Name}' cannot be configured when data loaded because one 'BarsService' could not be configured.");

//            DataLoaded(out _isDataLoaded);

//            if (!_isDataLoaded)
//                PrintService.LogError($"'{Name}' cannot be configured when data loaded successfully.");

//        }
//        public void Terminated()
//        {
//            if (_collection == null)
//                return;
//            if (_collection.Count == 0)
//            {
//                _collection = null; 
//                return;
//            }

//            ForEach(x => x.Terminated());
//            _collection.Clear();
//            _collection = null;
//        }

//        internal abstract void Configure(out bool isConfigured);
//        internal abstract void DataLoaded(out bool isDataLoaded);

//        protected abstract ServiceCollectionType GetServiceType();
        
//        public void Log() => Log(LogLevel.Information, null, 0);
//        public void Log(int tabOrder) => Log(LogLevel.Information, null, tabOrder);
//        public void Log(string message, int tabOrder = 0) => Log(LogLevel.Information, message, tabOrder);
//        public void Log(LogLevel level, string message, int tabOrder = 0)
//        {
//            if (_printService == null || !Options.IsLogEnable)
//                return;

//            string tab = string.Empty;
//            if (tabOrder > 0)
//                for (int i = 0; i < tabOrder; i++)
//                    tab += "\t";

//            switch (level)
//            {
//                case LogLevel.Trace:
//                    _printService?.LogTrace(tab + ToString() + " " + message);
//                    break;
//                case LogLevel.Debug:
//                    _printService?.LogDebug(tab + ToString() + " " + message);
//                    break;
//                case LogLevel.Information:
//                    _printService?.LogInformation(tab + ToString() + " " + message);
//                    break;
//                case LogLevel.Warning:
//                    _printService?.LogWarning(tab + ToString() + " " + message);
//                    break;
//                case LogLevel.Error:
//                    _printService?.LogError(tab + ToString() + " " + message);
//                    break;
//                default:
//                    break;
//            }
//        }

//        #endregion

//        /// <summary>
//        /// Print in NinjaScript putput window the configuration state. If the configuration has been ok or error.
//        /// </summary>
//        protected void LogConfigurationState()
//        {
//            if (!IsPrintServiceAvailable)
//                return;

//            if (IsDataLoaded && Ninjascript.State == State.DataLoaded)
//                _printService?.LogInformation($"'{Name}' has been configured when data loaded successfully.");
//            else if (IsConfigure && Ninjascript.State == State.Configure)
//                _printService?.LogInformation($"'{Name}' has been configured successfully.");
//            else if (!IsConfigureAll && Ninjascript.State == State.DataLoaded)
//                _printService?.LogError($"'{Name}' has NOT been configured. The service will not work.");
//            else
//                _printService?.LogError($"'{Name}' has NOT been configured. You are configuring the service out of configure or data loaded states.");
//        }
//    }
//}
