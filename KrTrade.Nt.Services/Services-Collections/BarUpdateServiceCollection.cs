using NinjaTrader.NinjaScript;
using System;
using System.Collections;
using System.Collections.Generic;

namespace KrTrade.Nt.Services
{
    public class BarUpdateServiceCollection<TService,TOptions> : BaseNinjascriptService<TOptions>, IBarUpdateService, IEnumerable, IEnumerable<TService>
        where TService : IBarUpdateService
        where TOptions : BarUpdateServiceCollectionOptions, new()
    {
        private IList<TService> _services;
        private readonly IDictionary<string, int> _keys;

        //private IDictionary<string, TService> _services;

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

                    if (index < 0 || index >= _services.Count)
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

        public BarUpdateServiceCollection(NinjaScriptBase ninjascript) : base(ninjascript)
        {
        }

        public BarUpdateServiceCollection(NinjaScriptBase ninjascript, IPrintService printService) : base(ninjascript, printService)
        {
        }

        public BarUpdateServiceCollection(NinjaScriptBase ninjascript, IPrintService printService, Action<TOptions> configureOptions) : base(ninjascript, printService, configureOptions)
        {
        }
        public BarUpdateServiceCollection(NinjaScriptBase ninjascript, IPrintService printService, TOptions options) : base(ninjascript, printService, options)
        {
        }

        #region Implementation

        public override string ToLogString()
        {

            if (_services == null || _services.Count == 0)
                return string.Empty;

            string logText = string.Empty;
            foreach(var service  in _services) 
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
        public void Update()
        {
            if (_services == null || _services.Count == 0)
                return;
            foreach (var service in _services)
                service.Update();

        }
        //public void Terminated()
        //{
        //    foreach (var service in _services)
        //    {
        //        if (service.Value.GetType().IsAssignableFrom(typeof(ITerminated)))
        //            service.Value.Terminated();
        //    }
        //}

        public void Add(string key, TService service)
        {
            string logText;
            try
            {
                if (service == null)
                    throw new ArgumentNullException(nameof(service));

                if (!ContainsKey(key))
                    throw new Exception($"The '{key}' key already exists. The key is being used by another service.");

                if (_services == null)
                    _services = new List<TService>();

                _services.Add(service);
                _keys.Add(key, _services.Count - 1);

                logText = $"The {service.Name} service has been added successfully.";
                PrintService.LogInformation(logText);
            }
            catch (Exception e)
            {
                logText = $"The {service.Name} service has NOT been added.";
                PrintService.LogError(logText,e);
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
                PrintService.LogError("The element cannot be added.",ex);
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
        public bool ContainsKey(string key) => _services == null ? false : _keys.ContainsKey(key);

        public IEnumerator<TService> GetEnumerator() => _services.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        #endregion

    }
}
