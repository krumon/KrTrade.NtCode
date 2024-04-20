using KrTrade.Nt.Core.Series;
using NinjaTrader.NinjaScript;
using System;
using System.Collections;
using System.Collections.Generic;

namespace KrTrade.Nt.Services.Series
{

    public abstract class SeriesCollection<TSeries> : BaseNinjascriptService, IEnumerable, IEnumerable<TSeries>
    where TSeries : ISeries
    {
        protected IList<TSeries> _series;
        private IDictionary<string, int> _keys;
        protected new SeriesCollectionOptions _options;

        public new SeriesCollectionOptions Options { get => _options ?? new SeriesCollectionOptions(); protected set { _options = value; } }

        public TSeries this[string key]
        {
            get
            {
                try
                {
                    if (_series == null)
                        throw new ArgumentNullException(nameof(_series));
                    int index = -1;
                    _keys?.TryGetValue(key, out index);

                    if (index == -1)
                        throw new KeyNotFoundException($"The {key} key DOESN`T EXISTIS.");

                    return _series[index];
                }
                catch (Exception ex)
                {
                    PrintService.LogError(ex);
                    return default;
                }
            }
        }
        public TSeries this[int index]
        {
            get
            {
                try
                {
                    return _series[index];
                }
                catch (Exception ex)
                {
                    PrintService.LogError(ex);
                    return default;
                }
            }
        }

        public SeriesCollection(NinjaScriptBase ninjascript, IPrintService printService, NinjascriptServiceOptions options) : base(ninjascript, printService, null, options) { }

        #region Implementation

        public override string ToLogString()
        {

            if (_series == null || _series.Count == 0)
                return string.Empty;

            string logText = string.Empty;
            foreach (var series in _series)
                logText += series.Key + "NewLine";

            logText.Remove(logText.Length - 7);
            logText.Replace("NewLine", Environment.NewLine);

            return logText;
        }
        internal override void Configure(out bool isConfigured)
        {
            if (_series == null || _series.Count == 0)
                isConfigured = false;
            else
            {
                isConfigured = true;
                foreach (var series in _series)
                {
                    //series.Configure();
                    //if (!series.IsConfigure)
                    //    isConfigured = false;
                }
            }
        }
        internal override void DataLoaded(out bool isDataLoaded)
        {
            if (_series == null || _series.Count == 0)
                isDataLoaded = false;
            else
            {
                isDataLoaded = true;
                foreach (var series in _series)
                {
                    //series.DataLoaded();
                    //if (!series.IsDataLoaded)
                    //    isDataLoaded = false;
                }
            }
        }
        public override void Terminated()
        {
            if (_series == null || _series.Count == 0)
                return;
            foreach (var series in _series)
                series.Dispose();

            _series.Clear();
            _series = null;
        }

        public void Add(TSeries series) => Add(null, series);
        public void Add(string name, TSeries series)
        {
            string logText;
            try
            {
                if (series == null)
                    throw new ArgumentNullException(nameof(series));

                if (_series == null)
                    _series = new List<TSeries>();

                if (_keys == null)
                    _keys = new Dictionary<string,int>();

                if (string.IsNullOrEmpty(name))
                    name = series.Key;

                // El servicio no existe
                if (!ContainsKey(series.Key))
                {
                    _series.Add(series);
                    _keys.Add(series.Key, _series.Count - 1);
                    // El pseudónimo ya existe.
                    if (series.Key != name && ContainsKey(name))
                        PrintService.LogError(new Exception($"The pseudo-name: '{name}' already exists. The pseudo-name is being used by another service and the service cannot be added."));
                    else if (series.Key != name)
                        _keys.Add(name, _series.Count - 1);
                }
                
            }
            catch (Exception e)
            {
                logText = $"The {series.Name} service cannot be added.";
                PrintService.LogError(logText, e);
            }
        }
        public void Add(BaseSeriesInfo info)
        {

        }
        public int Count => _series.Count;
        public void Clear() => _series?.Clear();
        public void Remove(string key)
        {
            try
            {
                if (_series == null)
                    throw new ArgumentNullException(nameof(_series));

                int index = -1;
                _keys?.TryGetValue(key, out index);

                if (index < 0 || index >= _series.Count)
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
                if (_series == null)
                    throw new ArgumentNullException(nameof(_series));

                _series?.RemoveAt(index);
            }
            catch (Exception e)
            {
                PrintService.LogError("The element cannot be added.", e);
            }
        }
        public bool ContainsKey(string key) => _series != null && _keys.ContainsKey(key);

        public IEnumerator<TSeries> GetEnumerator() => _series.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        protected void Execute(Action<TSeries> action)
        {
            if (_series == null || _series.Count == 0)
                return;
            foreach (var service in _series)
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
        protected bool IsValidIndex(int index) => _series != null && index >= 0 && index < Count;

        #endregion

    }

    public abstract class SeriesCollection<TSeries,TOptions> : SeriesCollection<TSeries>
        where TSeries : ISeries
        where TOptions : SeriesServiceOptions, new()
    {
        protected new TOptions _options;

        public SeriesCollection(NinjaScriptBase ninjascript, IPrintService printService, NinjascriptServiceOptions options) : base(ninjascript, printService, options) { }

        public new TOptions Options { get => _options ?? new TOptions(); protected set { _options = value; } }

    }
}
