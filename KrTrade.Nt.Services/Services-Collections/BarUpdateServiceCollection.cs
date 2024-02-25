using System;
using System.Collections;
using System.Collections.Generic;

namespace KrTrade.Nt.Services
{
    public class BarUpdateServiceCollection<TService,TOptions> : BarUpdateService<TOptions> , IEnumerable, IEnumerable<KeyValuePair<string,TService>>
        where TService : IBarUpdateService
        where TOptions : BarUpdateServiceCollectionOptions, new()
    {

        private IDictionary<string, TService> _services;

        public TService this[string key] 
        {
            get 
            {
                try
                {
                    if (_services == null)
                        throw new ArgumentNullException(nameof(_services));
                    if (_services.Count == 0)
                        throw new NotSupportedException("There are any service. The collection is empty.");
                    return _services[key.Trim().ToUpper()];
                }
                catch (Exception ex)
                {
                    PrintService.LogError(ex);
                    return default;
                }
            }
        }

        public BarUpdateServiceCollection(IBarsService barsService) : base(barsService)
        {
        }

        public BarUpdateServiceCollection(IBarsService barsService, Action<TOptions> configureOptions) : base(barsService, configureOptions)
        {
        }

        public BarUpdateServiceCollection(IBarsService barsService, TOptions options) : base(barsService, options)
        {
        }

        #region Implementation

        public override string ToLogString()
        {

            if (_services == null || _services.Count == 0)
                return string.Empty;

            string logText = string.Empty;
            foreach(var key  in _services.Keys) 
                logText += _services[key].ToLogString() + "NewLine";

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
                    service.Value.Configure();
                    if (!service.Value.IsConfigure)
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
                    service.Value.Configure();
                    if (!service.Value.IsConfigure)
                        isDataLoaded = false;
                }
            }
        }
        public override void Update()
        {
            if (_services == null || _services.Count == 0)
                return;
            foreach (var service in _services)
                service.Value.Update();

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
            string infoText;
            if (service == null)
                infoText = $"The '{nameof(service)}' is NULL and and could not be added to the collection.";
            else
            {
                if (_services == null)
                    _services = new Dictionary<string, TService>();

                //key = key.Trim().ToUpper();
                if (_services.ContainsKey(key))
                    infoText = $"The '{key}' key already exists. The service has been added before or the key is being used by another service.";
                else
                {
                    _services.Add(key, service);
                    infoText = $"The {service.Name} service has been added successfully";
                }
            }

            PrintService.LogInformation(infoText);
        }
        public int Count => _services.Count;
        public void Clear() => _services?.Clear();
        public void Remove(string key) => _services?.Remove(key.Trim().ToUpper());
        public bool ContainsKey(string key) => _services == null ? false : _services.ContainsKey(key);

        public IEnumerator<KeyValuePair<string, TService>> GetEnumerator() => _services.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        #endregion

    }
}
