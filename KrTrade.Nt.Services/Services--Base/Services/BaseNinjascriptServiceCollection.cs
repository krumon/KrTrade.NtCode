using KrTrade.Nt.Core.Collections;
using KrTrade.Nt.Core.Services;
using NinjaTrader.NinjaScript;
using System;
using System.Runtime.CompilerServices;

namespace KrTrade.Nt.Services
{

    public abstract class BaseNinjascriptServiceCollection<TService> : BaseKeyCollection<TService>, INinjascriptServiceCollection<TService>
        where TService : INinjascriptService
    {

        private readonly NinjaScriptBase _ninjascript;
        private readonly IPrintService _printService;

        public NinjaScriptBase Ninjascript => _ninjascript;
        public IPrintService PrintService => _printService;

        public IServiceInfo Info { get; protected set; }
        public IServiceOptions Options { get; protected set; }

        public bool IsConfigure { get;protected set; }
        public bool IsDataLoaded { get; protected set; }

        public bool IsConfigureAll => IsConfigure && IsDataLoaded;

        public string Name => Info.Name;
        public bool IsEnable => Options.IsEnable;
        public bool IsLogEnable => Options.IsLogEnable;

        protected bool IsPrintServiceAvailable => _printService != null && IsLogEnable;

        protected BaseNinjascriptServiceCollection(NinjaScriptBase ninjascript, IPrintService printService, NinjascriptServiceInfo info, NinjascriptServiceOptions options) 
        {
            _ninjascript = ninjascript ?? throw new ArgumentNullException($"Error in 'BaseNinjascriptServiceCollection' constructor. The {nameof(ninjascript)} argument cannot be null.");
            _printService = printService;
            //Name = name;
            //_options = options;
            Info = info ?? new NinjascriptServiceInfo();
            Options = options ?? new NinjascriptServiceOptions();

            Info.Type = GetServiceType();
            if (string.IsNullOrEmpty(Info.Name))
                Info.Name = Info.Type.ToString();
        }
        protected BaseNinjascriptServiceCollection(NinjaScriptBase ninjascript, IPrintService printService, NinjascriptServiceInfo info, NinjascriptServiceOptions options, int capacity) : base(capacity) 
        {
            _ninjascript = ninjascript ?? throw new ArgumentNullException($"Error in 'BaseNinjascriptServiceCollection' constructor. The {nameof(ninjascript)} argument cannot be null.");
            _printService = printService;
            //Name = name;
            //_options = options;

            Info = info ?? new NinjascriptServiceInfo();
            Options = options ?? new NinjascriptServiceOptions();

            Info.Type = GetServiceType();
            if (string.IsNullOrEmpty(Info.Name))
                Info.Name = Info.Type.ToString();

        }

        #region Implementation

        public void Configure()
        {
            if (_collection == null)
                throw new NullReferenceException($"The service collection is null.");
            if (_collection.Count == 0)
                throw new Exception($"The service collection is empty.");

            IsConfigure = true;
            foreach (var service in _collection)
            {
                service.Configure();
                if (!service.IsConfigure)
                    IsConfigure = false;
            }
        }
        public void DataLoaded()
        {
            if (_collection == null)
                throw new NullReferenceException($"The service collection is null.");
            if (_collection.Count == 0)
                throw new Exception($"The service collection is empty.");

            IsDataLoaded = true;
            foreach (var service in _collection)
            {
                service.DataLoaded();
                if (!service.IsDataLoaded)
                    IsDataLoaded = false;
            }
        }
        public void Terminated()
        {
            if (_collection == null)
                return;
            if (_collection.Count == 0)
            {
                _collection = null; 
                return;
            }

            ForEach(x => x.Terminated());
            _collection.Clear();
            _collection = null;
        }

        protected abstract ServiceType GetServiceType();
        public string ToLogString()
        {
            if (_collection == null)
                return $"{Name}[NULL]";
            if (_collection.Count == 0)
                return $"{Name}[EMPTY]";

            string log = $"{Name}:{Environment.NewLine}";
            foreach (var service in _collection)
                log += service.ToLogString() + Environment.NewLine;
            return log;
        }
        public void Log()
        {
            if (!IsPrintServiceAvailable)
                return;
            _printService?.LogValue(ToLogString());
        }

        #endregion

        /// <summary>
        /// Print in NinjaScript putput window the configuration state. If the configuration has been ok or error.
        /// </summary>
        protected void LogConfigurationState()
        {
            if (!IsPrintServiceAvailable)
                return;

            if (IsDataLoaded && Ninjascript.State == State.DataLoaded)
                _printService?.LogInformation($"The {Name} has been loaded successfully.");
            else if (IsConfigure && Ninjascript.State == State.Configure)
                _printService?.LogInformation($"The {Name} has been configured successfully.");
            else if (!IsConfigureAll && Ninjascript.State == State.DataLoaded)
                _printService?.LogError($"The '{Name}' has NOT been configured. The service will not work.");
            else
                _printService?.LogError($"The '{Name}' has NOT been configured. You are configuring the service out of configure or data loaded states.");
        }

        protected void LogInitStart([CallerMemberName] string memberName = "")
        {
            if (!IsPrintServiceAvailable)
                return;

            _printService.LogTrace($"The {Name} is being initialized in {memberName} constructor.");
        }
        protected void LogInitEnd()
        {
            if (!IsPrintServiceAvailable)
                return;

            _printService.LogTrace($"The {Name} has been initialized successfully");
        }

    }
}
