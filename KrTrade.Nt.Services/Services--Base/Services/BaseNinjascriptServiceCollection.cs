using KrTrade.Nt.Core.Collections;
using KrTrade.Nt.Core.Services;
using NinjaTrader.NinjaScript;
using System;

namespace KrTrade.Nt.Services
{

    public abstract class BaseNinjascriptServiceCollection<TService> : BaseKeyCollection<TService>, INinjascriptServiceCollection<TService>
        where TService : INinjascriptService
    {

        private readonly NinjaScriptBase _ninjascript;
        private readonly IPrintService _printService;

        private bool _isConfigured;
        private bool _isDataLoaded;

        public NinjaScriptBase Ninjascript => _ninjascript;
        public IPrintService PrintService => _printService;

        public IServiceInfo Info { get; protected set; }
        public IServiceOptions Options { get; protected set; }

        public ServiceCollectionType Type { get => Info.Type.ToServiceCollectionType(); protected set => Info.Type = value.ToServiceType(); }
        public bool IsConfigure => _isConfigured;
        public bool IsDataLoaded => _isDataLoaded;

        public bool IsConfigureAll => IsConfigure && IsDataLoaded;

        public string Name => string.IsNullOrEmpty(Info.Name) ? Info.Type.ToString() : Info.Name;
        public bool IsEnable => Options.IsEnable;
        public bool IsLogEnable => Options.IsLogEnable;

        protected bool IsPrintServiceAvailable => _printService != null && IsLogEnable;

        protected BaseNinjascriptServiceCollection(NinjaScriptBase ninjascript, IPrintService printService, NinjascriptServiceInfo info, NinjascriptServiceOptions options) 
        {
            _ninjascript = ninjascript ?? throw new ArgumentNullException($"Error in 'BaseNinjascriptServiceCollection' constructor. The {nameof(ninjascript)} argument cannot be null.");
            _printService = printService;
            Info = info ?? new NinjascriptServiceInfo();
            Options = options ?? new NinjascriptServiceOptions();

            Type = GetServiceType();

            if (string.IsNullOrEmpty(Info.Name))
                Info.Name = Info.Type.ToString();
        }
        protected BaseNinjascriptServiceCollection(NinjaScriptBase ninjascript, IPrintService printService, NinjascriptServiceInfo info, NinjascriptServiceOptions options, int capacity) : base(capacity) 
        {
            _ninjascript = ninjascript ?? throw new ArgumentNullException($"Error in 'BaseNinjascriptServiceCollection' constructor. The {nameof(ninjascript)} argument cannot be null.");
            _printService = printService;
            Info = info ?? new NinjascriptServiceInfo();
            Options = options ?? new NinjascriptServiceOptions();

            Type = GetServiceType();

            if (string.IsNullOrEmpty(Info.Name))
                Info.Name = Info.Type.ToString();

        }

        #region Implementation

        public void Configure()
        {
            if (_collection == null)
                throw new NullReferenceException($"{Name} inner collection is null.");
            if (_collection.Count == 0)
                if (_collection is BarsServiceCollection)
                    throw new Exception($"{Name} inner collection is empty.");

            _isConfigured = true;
            foreach (var service in _collection)
            {
                service.Configure();
                if (!service.IsConfigure)
                    _isConfigured = false;
            }

            if (!_isConfigured)
                PrintService.LogInformation($"'{Name}' cannot be configured because one or more 'service' could not be configured.");

            Configure(out _isConfigured);

            if (!_isConfigured)
                PrintService.LogError($"'{Name}' cannot be configured successfully.");
        }
        public void DataLoaded()
        {
            if (_collection == null)
                throw new NullReferenceException($"{Name} inner collection is null.");
            if (_collection.Count == 0)
                if (_collection is BarsServiceCollection)
                    throw new Exception($"{Name} inner collection is empty.");

            _isDataLoaded = true;

            foreach (var service in _collection)
            {
                service.DataLoaded();
                if (!service.IsDataLoaded)
                    _isDataLoaded = false;
            }

            if (!_isDataLoaded)
                PrintService.LogInformation($"'{Name}' cannot be configured when data loaded because one 'BarsService' could not be configured.");

            DataLoaded(out _isDataLoaded);

            if (!_isDataLoaded)
                PrintService.LogError($"'{Name}' cannot be configured when data loaded successfully.");

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

        internal abstract void Configure(out bool isConfigured);
        internal abstract void DataLoaded(out bool isDataLoaded);

        protected abstract ServiceCollectionType GetServiceType();

        public virtual string ToLogString() => ToLogString(Name, 0, ", ");
        public virtual string ToLogString(int tabOrder) => ToLogString(Name, tabOrder, Environment.NewLine);
        public string ToLogString(string header, int tabOrder, string separator)
        {
            string text = string.Empty;
            string tab = string.Empty;
            separator = string.IsNullOrEmpty(separator) ? ", " : separator;

            for (int i = 0; i < tabOrder; i++)
                tab += "\t";

            if (!string.IsNullOrEmpty(header))
                text += tab + header;

            if (_collection == null)
                return $"{text}[NULL]";
            if (_collection.Count == 0)
                return $"{text}[EMPTY]";

            text += (separator == Environment.NewLine ? Environment.NewLine + tab : string.Empty) + "[" + (separator == Environment.NewLine ? Environment.NewLine + tab : string.Empty);
            for (int i = 0; i < _collection.Count; i++)
            {
                text += _collection[i].ToString(tabOrder + 1);
                if (i == _collection.Count - 1)
                    text += (separator == Environment.NewLine ? Environment.NewLine + tab : string.Empty) + "]";
                else
                    text += (separator != Environment.NewLine ? separator : string.Empty) + (separator == Environment.NewLine ? Environment.NewLine : string.Empty);
            }

            return text;
        }
        
        protected virtual string ToLogString(bool isMultiline) => ToLogString(Name, 0, isMultiline ? Environment.NewLine : ", ");

        public void Log()
        {
            if (!IsPrintServiceAvailable)
                return;
            _printService?.LogValue(ToLogString());
        }
        public void Log(int tabOrder)
        {
            if (!IsPrintServiceAvailable)
                return;
            _printService?.LogValue(ToLogString(tabOrder));
        }
        //public void Log(bool isMultiLine)
        //{
        //    if (!IsPrintServiceAvailable)
        //        return;
        //    if (isMultiLine)
        //        _printService?.LogValue(ToLogString(isMultiLine));
        //}

        public override string ToString() => ToString(Type.ToString(), 0, false);
        public override string ToLongString() => ToString(Type.ToString(),0,true);
        public override string ToLongString(int tabOrder) => ToString(Type.ToString(),tabOrder,true);

        #endregion

        /// <summary>
        /// Print in NinjaScript putput window the configuration state. If the configuration has been ok or error.
        /// </summary>
        protected void LogConfigurationState()
        {
            if (!IsPrintServiceAvailable)
                return;

            if (IsDataLoaded && Ninjascript.State == State.DataLoaded)
                _printService?.LogInformation($"'{Name}' has been configured when data loaded successfully.");
            else if (IsConfigure && Ninjascript.State == State.Configure)
                _printService?.LogInformation($"'{Name}' has been configured successfully.");
            else if (!IsConfigureAll && Ninjascript.State == State.DataLoaded)
                _printService?.LogError($"'{Name}' has NOT been configured. The service will not work.");
            else
                _printService?.LogError($"'{Name}' has NOT been configured. You are configuring the service out of configure or data loaded states.");
        }
    }
}
