using KrTrade.Nt.Core.Collections;
using NinjaTrader.NinjaScript;
using System;

namespace KrTrade.Nt.Services
{

    public abstract class BaseNinjascriptServiceCollection<TService> : BaseKeyCollection<TService>, INinjascriptServiceCollection<TService>
        where TService : INinjascriptService
    {

        private readonly NinjaScriptBase _ninjascript;
        private readonly IPrintService _printService;
        protected readonly NinjascriptServiceOptions _options;

        public NinjaScriptBase Ninjascript => _ninjascript;
        public IPrintService PrintService => _printService;
        public NinjascriptServiceOptions Options => _options;

        public bool IsConfigure { get;protected set; }
        public bool IsDataLoaded { get; protected set; }

        public abstract string Name { get; protected set; }
        public bool IsEnable => Options.IsEnable;
        public bool IsLogEnable => Options.IsLogEnable;

        protected BaseNinjascriptServiceCollection(NinjaScriptBase ninjascript, IPrintService printService, string name, NinjascriptServiceOptions options) 
        {
            _ninjascript = ninjascript ?? throw new ArgumentNullException($"Error in 'BaseNinjascriptServiceCollection' constructor. The {nameof(ninjascript)} argument cannot be null.");
            Name = name;
            _options = options;
            _printService = printService;
        }
        protected BaseNinjascriptServiceCollection(NinjaScriptBase ninjascript, IPrintService printService, string name, NinjascriptServiceOptions options, int capacity) : base(capacity) 
        {
            _ninjascript = ninjascript ?? throw new ArgumentNullException($"Error in 'BaseNinjascriptServiceCollection' constructor. The {nameof(ninjascript)} argument cannot be null.");
            Name = name;
            _options = options;
            _printService = printService;
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
            if (_printService == null || !Options.IsLogEnable)
                return;
            _printService?.LogValue(ToLogString());
        }

        #endregion

    }
}
