using NinjaTrader.NinjaScript;
using System;
using System.Collections;
using System.Collections.Generic;

namespace KrTrade.Nt.Services
{

    public abstract class BaseNinjascriptServiceCollection<TService> : BaseNinjascriptService, IEnumerable, IEnumerable<TService>
    where TService : INinjascriptService
    {
        protected IList<TService> _services;
        private IDictionary<string, int> _keys;
        protected new NinjascriptServiceCollectionOptions _options;

        public new NinjascriptServiceCollectionOptions Options { get => _options ?? new NinjascriptServiceCollectionOptions(); protected set { _options = value; } }

        public TService this[string key]
        {
            get
            {
                try
                {
                    if (_services == null)
                        throw new ArgumentNullException(nameof(_services));
                    int index = -1;
                    _keys?.TryGetValue(key, out index);

                    if (index == -1)
                        throw new KeyNotFoundException($"The {key} key DOESN`T EXISTIS.");

                    return _services[index];
                }
                catch (Exception ex)
                {
                    PrintService.LogError(ex);
                    return default;
                }
            }
        }
        public TService this[int index]
        {
            get
            {
                try
                {
                    return _services[index];
                }
                catch (Exception ex)
                {
                    PrintService.LogError(ex);
                    return default;
                }
            }
        }

        public BaseNinjascriptServiceCollection(NinjaScriptBase ninjascript) : base(ninjascript) { }
        public BaseNinjascriptServiceCollection(NinjaScriptBase ninjascript, IPrintService printService) : base(ninjascript, printService) { }
        public BaseNinjascriptServiceCollection(NinjaScriptBase ninjascript, IPrintService printService, NinjascriptServiceOptions options) : base(ninjascript, printService, options) { }
        public BaseNinjascriptServiceCollection(NinjaScriptBase ninjascript, IPrintService printService, Action<NinjascriptServiceOptions> configureOptions, NinjascriptServiceOptions options) : base(ninjascript, printService, configureOptions, options) { }

        #region Implementation

        public override string ToLogString()
        {

            if (_services == null || _services.Count == 0)
                return string.Empty;

            string logText = string.Empty;
            foreach (var service in _services)
                logText += service.ToLogString() + "NewLine";

            logText.Remove(logText.Length - 7);
            logText.Replace("NewLine", Environment.NewLine);

            return logText;
        }
        internal override void Configure(out bool isConfigured)
        {
            if (_services == null || _services.Count == 0)
                isConfigured = false;
            else
            {
                isConfigured = true;
                foreach (var service in _services)
                {
                    service.Configure();
                    if (!service.IsConfigure)
                        isConfigured = false;
                }
            }
        }
        internal override void DataLoaded(out bool isDataLoaded)
        {
            if (_services == null || _services.Count == 0)
                isDataLoaded = false;
            else
            {
                isDataLoaded = true;
                foreach (var service in _services)
                {
                    service.Configure();
                    if (!service.IsConfigure)
                        isDataLoaded = false;
                }
            }
        }
        public override void Terminated()
        {
            if (_services == null || _services.Count == 0)
                return;
            foreach (var service in _services)
                service.Terminated();
        }

        public void Add(TService service) => Add(null, service);
        public void Add(string name, TService service)
        {
            string logText;
            try
            {
                if (service == null)
                    throw new ArgumentNullException(nameof(service));

                if (_services == null)
                    _services = new List<TService>();

                if (_keys == null)
                    _keys = new Dictionary<string,int>();

                if (string.IsNullOrEmpty(name))
                    name = service.Key;

                // Si existe el servicio lo sobrescribo
                if (ContainsKey(service.Key))
                {
                    int i = _keys[service.Key];
                    _services[i] = service;
                    logText = $"The {service.Name} service replace other service with the same key.";
                    // Añado una clave específica para el servicio.
                    if (service.Key != name)
                        _keys[name] = i;
                }
                // El servicio no existe
                else
                {
                    _services.Add(service);
                    _keys.Add(service.Key, _services.Count - 1);
                    logText = $"The {service.Name} service has been added successfully.";
                    // La clave específica ya existe.
                    if (service.Key != name && ContainsKey(name))
                        throw new Exception($"The '{name}' name already exists. The name is being used by another service.");
                    else if (service.Key != name)
                        _keys.Add(name, _services.Count - 1);

                }
                PrintService.LogInformation(logText);
            }
            catch (Exception e)
            {
                logText = $"The {service.Name} service name cannot be added.";
                PrintService.LogError(logText, e);
            }
        }
        public int Count => _services.Count;
        public void Clear() => _services?.Clear();
        public void Remove(string key)
        {
            try
            {
                if (_services == null)
                    throw new ArgumentNullException(nameof(_services));

                int index = -1;
                _keys?.TryGetValue(key, out index);

                if (index < 0 || index >= _services.Count)
                    throw new KeyNotFoundException($"The {key} key DOESN`T EXISTIS.");

                RemoveAt(index);
            }
            catch (Exception ex)
            {
                PrintService.LogError("The element cannot be added.", ex);
            }
        }
        public void RemoveAt(int index)
        {
            try
            {
                if (_services == null)
                    throw new ArgumentNullException(nameof(_services));

                _services?.RemoveAt(index);
            }
            catch (Exception e)
            {
                PrintService.LogError("The element cannot be added.", e);
            }
        }
        public bool ContainsKey(string key) => _services != null && _keys.ContainsKey(key);

        public IEnumerator<TService> GetEnumerator() => _services.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        protected void Execute(Action<TService> action)
        {
            if (_services == null || _services.Count == 0)
                return;
            foreach (var service in _services)
            {
                try
                {
                    action(service);
                }
                catch (Exception e)
                {
                    string logText = $"The {service.Name} action has NOT been executed.";
                    PrintService.LogError(logText, e);
                }
            }
        }
        protected bool IsValidIndex(int index) => _services != null && index >= 0 && index < Count;

        #endregion

    }

    public abstract class BaseNinjascriptServiceCollection<TService,TOptions> : BaseNinjascriptServiceCollection<TService>
        where TService : INinjascriptService
        where TOptions : NinjascriptServiceCollectionOptions, new()
    {
        protected new TOptions _options;

        public BaseNinjascriptServiceCollection(NinjaScriptBase ninjascript) : base(ninjascript) { } 
        public BaseNinjascriptServiceCollection(NinjaScriptBase ninjascript, IPrintService printService) : base(ninjascript, printService) { }
        public BaseNinjascriptServiceCollection(NinjaScriptBase ninjascript, IPrintService printService, NinjascriptServiceOptions options) : base(ninjascript, printService, options) { }
        public BaseNinjascriptServiceCollection(NinjaScriptBase ninjascript, IPrintService printService, Action<NinjascriptServiceOptions> configureOptions, NinjascriptServiceOptions options) : base(ninjascript, printService, configureOptions, options) { }

        public new TOptions Options { get => _options ?? new TOptions(); protected set { _options = value; } }

    }
}
