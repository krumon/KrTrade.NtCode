using KrTrade.Nt.Core.Collections;
using System;
using System.Collections.Generic;

namespace KrTrade.Nt.Services
{

    public abstract class BaseNinjascriptServiceCollection<TService> : BaseKeyCollection<TService>, IConfigure, IDataLoaded, ITerminated
    where TService : INinjascriptService
    {
        public bool IsConfigure { get;protected set; }
        public bool IsDataLoaded { get; protected set; }

        protected BaseNinjascriptServiceCollection() { }
        protected BaseNinjascriptServiceCollection(IEnumerable<TService> elements) : base(elements) { }
        protected BaseNinjascriptServiceCollection(int capacity) : base(capacity) { }

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

        #endregion

    }

    //public abstract class BaseNinjascriptServiceCollection<TService> : BaseNinjascriptService, IEnumerable, IEnumerable<TService>
    //where TService : INinjascriptService
    //{
    //    protected IList<TService> _services;
    //    private IDictionary<string, int> _keys;
    //    protected new NinjascriptServiceCollectionOptions _options;

    //    public new NinjascriptServiceCollectionOptions Options { get => _options ?? new NinjascriptServiceCollectionOptions(); protected set { _options = value; } }

    //    public TService this[string key]
    //    {
    //        get
    //        {
    //            try
    //            {
    //                if (_services == null)
    //                    throw new ArgumentNullException(nameof(_services));
    //                int index = -1;
    //                _keys?.TryGetValue(key, out index);

    //                if (index == -1)
    //                    throw new KeyNotFoundException($"The {key} key DOESN`T EXISTIS.");

    //                return _services[index].Options.IsEnable ? _services[index] : default;
    //            }
    //            catch (Exception ex)
    //            {
    //                PrintService.LogError(ex);
    //                return default;
    //            }
    //        }
    //    }
    //    public TService this[int index]
    //    {
    //        get
    //        {
    //            try
    //            {
    //                return _services[index].Options.IsEnable ? _services[index] : default;
    //            }
    //            catch (Exception ex)
    //            {
    //                PrintService.LogError(ex);
    //                return default;
    //            }
    //        }
    //    }

    //    public BaseNinjascriptServiceCollection(NinjaScriptBase ninjascript, IPrintService printService, Action<NinjascriptServiceOptions> configureOptions, NinjascriptServiceOptions options) : base(ninjascript, printService, configureOptions, options) { }

    //    #region Implementation

    //    public override string ToLogString()
    //    {

    //        if (_services == null || _services.Count == 0)
    //            return string.Empty;

    //        string logText = string.Empty;
    //        foreach (var service in _services)
    //            logText += service.ToLogString() + "NewLine";

    //        logText.Remove(logText.Length - 7);
    //        logText.Replace("NewLine", Environment.NewLine);

    //        return logText;
    //    }
    //    internal override void Configure(out bool isConfigured)
    //    {
    //        if (_services == null || _services.Count == 0)
    //            isConfigured = false;
    //        else
    //        {
    //            string logText = string.Empty;
    //            isConfigured = true;
    //            foreach (var service in _services)
    //            {
    //                service.Configure();
    //                if (!service.IsConfigure)
    //                    isConfigured = false;
    //            }
    //        }
    //    }
    //    internal override void DataLoaded(out bool isDataLoaded)
    //    {
    //        if (_services == null || _services.Count == 0)
    //            isDataLoaded = false;
    //        else
    //        {
    //            isDataLoaded = true;
    //            foreach (var service in _services)
    //            {
    //                service.Configure();
    //                if (!service.IsConfigure)
    //                    isDataLoaded = false;
    //            }
    //        }
    //    }
    //    public override void Terminated()
    //    {
    //        if (_services == null || _services.Count == 0)
    //            return;
    //        foreach (var service in _services)
    //            service.Terminated();

    //        _services.Clear();
    //        _services = null;
    //    }

    //    public void Add(TService service) => Add(null, service);
    //    public void Add(string name, TService service)
    //    {
    //        string logText;
    //        try
    //        {
    //            if (service == null)
    //                throw new ArgumentNullException(nameof(service));

    //            if (_services == null)
    //                _services = new List<TService>();

    //            if (_keys == null)
    //                _keys = new Dictionary<string,int>();

    //            if (string.IsNullOrEmpty(name))
    //                name = service.Key;

    //            // Si existe el servicio lo sobrescribo
    //            if (ContainsKey(service.Key))
    //            {
    //                int i = _keys[service.Key];
    //                _services[i] = service;
    //                // Log warning
    //                logText = $"The key: {service.Key} already exists. The new '{service.Name}' service replace the old service added with the same key. The old configuration has been replaced for the new.";
    //                PrintService.LogWarning(logText);
    //                // Añado una clave específica para el servicio.
    //                if (service.Key != name)
    //                    _keys[name] = i;
    //            }
    //            // El servicio no existe
    //            else
    //            {
    //                _services.Add(service);
    //                _keys.Add(service.Key, _services.Count - 1);
    //                // La clave específica ya existe.
    //                if (service.Key != name && ContainsKey(name))
    //                    PrintService.LogError(new Exception($"The pseudo-name: '{name}' already exists. The pseudo-name is being used by another service and the service cannot be added."));
    //                else if (service.Key != name)
    //                    _keys.Add(name, _services.Count - 1);
    //            }
                
    //        }
    //        catch (Exception e)
    //        {
    //            logText = $"The {service.Name} service cannot be added.";
    //            PrintService.LogError(logText, e);
    //        }
    //    }
    //    public int Count => _services.Count;
    //    public void Clear() => _services?.Clear();
    //    public void Remove(string key)
    //    {
    //        try
    //        {
    //            if (_services == null)
    //                throw new ArgumentNullException(nameof(_services));

    //            int index = -1;
    //            _keys?.TryGetValue(key, out index);

    //            if (index < 0 || index >= _services.Count)
    //                throw new KeyNotFoundException($"The {key} key DOESN`T EXISTIS.");

    //            RemoveAt(index);
    //        }
    //        catch (Exception ex)
    //        {
    //            PrintService.LogError("The element cannot be added.", ex);
    //        }
    //    }
    //    public void RemoveAt(int index)
    //    {
    //        try
    //        {
    //            if (_services == null)
    //                throw new ArgumentNullException(nameof(_services));

    //            _services?.RemoveAt(index);
    //        }
    //        catch (Exception e)
    //        {
    //            PrintService.LogError("The element cannot be added.", e);
    //        }
    //    }
    //    public bool ContainsKey(string key) => _services != null && _keys.ContainsKey(key);

    //    public IEnumerator<TService> GetEnumerator() => _services.GetEnumerator();
    //    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    //    protected void Execute(Action<TService> action)
    //    {
    //        if (_services == null || _services.Count == 0)
    //            return;
    //        foreach (var service in _services)
    //        {
    //            try
    //            {
    //                action(service);
    //            }
    //            catch (Exception e)
    //            {
    //                string logText = $"The {service.Name} action has NOT been executed.";
    //                PrintService.LogError(logText, e);
    //            }
    //        }
    //    }
    //    protected bool IsValidIndex(int index) => _services != null && index >= 0 && index < Count;

    //    #endregion

    //}

    //public abstract class BaseNinjascriptServiceCollection<TService,TOptions> : BaseNinjascriptServiceCollection<TService>
    //    where TService : INinjascriptService
    //    where TOptions : NinjascriptServiceCollectionOptions, new()
    //{
    //    protected new TOptions _options;

    //    public BaseNinjascriptServiceCollection(NinjaScriptBase ninjascript, IPrintService printService, Action<NinjascriptServiceOptions> configureOptions, NinjascriptServiceOptions options) : base(ninjascript, printService, configureOptions, options) { }

    //    public new TOptions Options { get => _options ?? new TOptions(); protected set { _options = value; } }

    //}
}
