using KrTrade.Nt.Core.Infos;
using KrTrade.Nt.Core.Logging;
using KrTrade.Nt.Core.Options;
using NinjaTrader.NinjaScript;
using System;
using System.Collections;
using System.Collections.Generic;

namespace KrTrade.Nt.Core
{
    public abstract class BaseCollection<TElement,TElementType, TElementInfo,TCollectionInfo,TCollectionType> : BaseElement<TCollectionType, TCollectionInfo, IElementOptions>, ICollection<TElement,TElementType,TElementInfo,TCollectionInfo,TCollectionType>
        where TElement : IElement<TElementType>
        where TElementInfo : IElementInfo<TElementType>
        where TCollectionInfo : ICollectionInfo<TElementInfo,TElementType,TCollectionType>
        where TElementType : Enum
        where TCollectionType : Enum
    {
        protected IList<TElement> _collection;
        protected IDictionary<string, int> _keys;

        public TElement this[string key]
        {
            get
            {
                try
                {
                    if (_collection == null)
                        throw new ArgumentNullException(nameof(_collection));
                    int index = -1;
                    _keys?.TryGetValue(key, out index);

                    if (index == -1)
                        throw new KeyNotFoundException($"The {key} key DOESN`T EXISTIS.");

                    return _collection[index];
                }
                catch
                {
                    return default;
                }
            }
        }
        public TElement this[int index]
        {
            get
            {
                try
                {
                    return _collection[index];
                }
                catch 
                {
                    return default;
                }
            }
        }

        public BaseCollection(NinjaScriptBase ninjascript, IPrintService printService, TCollectionInfo collectionInfo, IElementOptions collectionOptions) : base(ninjascript,printService,collectionInfo,collectionOptions)
        { 
            _collection = new List<TElement>();
        }

        #region Implementation

        public void Add(TElement item)
        {
            try
            {
                if (item == null)
                    throw new ArgumentNullException(nameof(item));

                if (_keys == null)
                    _keys = new Dictionary<string, int>();

                string key = item.Key;
                string name = item.Name;
                // El servicio no existe
                if (!ContainsKey(key))
                {
                    _collection.Add(item);
                    _keys.Add(key, _collection.Count - 1);
                    // El pseudónimo ya existe.
                    if (key != name && ContainsKey(name))
                        throw new Exception($"The pseudo-name: '{name}' already exists. The pseudo-name is being used by another service and the service cannot be added.");
                    else if (key != name && !string.IsNullOrEmpty(name))
                        _keys.Add(name, _collection.Count - 1);
                }
                else
                    throw new Exception("The key of element to be added already exists.");
            }
            catch (Exception e)
            {
                throw new Exception($"The element with name: {item.Name} and key: {item.Key} cannot be added. {e.Message}",e);
            }
        }
        public void TryAdd(TElement item)
        {
            try
            {
                Add(item);
            }
            catch { }
        }

        public int Count => _collection == null ? -1 : _collection.Count;

        public void Clear() => _collection?.Clear();
        public void Remove(string key)
        {
            try
            {
                if (_collection == null)
                    throw new ArgumentNullException(nameof(_collection));

                int index = -1;
                _keys?.TryGetValue(key, out index);

                if (!IsValidIndex(index))
                    throw new KeyNotFoundException($"The key:{key} DOESN`T EXISTIS.");

                RemoveAt(index);
            }
            catch (Exception ex)
            {
                throw new Exception($"The element cannot be removed.", ex);
            }
        }
        public void RemoveAt(int index)
        {
            try
            {
                if (!IsValidIndex(index))
                    throw new IndexOutOfRangeException();

                _collection.RemoveAt(index);
                foreach(var key in _keys)
                    if (key.Value == index)
                        _keys.Remove(key);
            }
            catch (Exception e)
            {
                throw new Exception($"The element cannot be removed.", e);
            }
        }
        public bool ContainsKey(string key) => _collection != null && _keys != null && _keys.ContainsKey(key);
        public bool TryGetValue(string key,out int index) => _keys.TryGetValue(key,out index);

        public IEnumerator<TElement> GetEnumerator() => _collection.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        protected override void Configure(out bool isConfigured)
        {
            if (_collection == null)
                throw new NullReferenceException($"{Name} inner collection is null.");
            //if (_collection.Count == 0)
            //    if (Type is SeriesCollectionType.BARS)
            //        throw new Exception($"{Name} inner collection is empty.");

            IsConfigure = true;
            foreach (var element in _collection)
            {
                element.Configure();
                if (!element.IsConfigure)
                    IsConfigure = false;
            }
            if (!IsConfigure)
                PrintService.LogError($"'{Name}' cannot be configured because one 'Service' could not be configured.");

            isConfigured = IsConfigure;
        }
        protected override void DataLoaded(out bool isDataLoaded)
        {
            if (_collection == null)
                throw new NullReferenceException($"{Name} inner collection is null.");
            //if (_collection.Count == 0)
            //    if (Type is SeriesCollectionType.BARS)
            //        throw new Exception($"{Name} inner collection is empty.");

            IsDataLoaded = true;
            foreach (var element in _collection)
            {
                element.DataLoaded();
                if (!element.IsDataLoaded)
                    IsDataLoaded = false;
            }

            if (!IsDataLoaded)
                PrintService.LogError($"'{Name}' cannot be configured when data loaded because one 'Series' could not be configured.");
            isDataLoaded = IsDataLoaded;
        }
        public override void Terminated() => Dispose();
        public void Dispose()
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

        protected void ForEach(Action<TElement> action)
        {
            if (_collection == null || _collection.Count == 0)
                return;
            foreach (var service in _collection)
            {
                try
                {
                    action(service);
                }
                catch { }
            }
        }
        protected void TryForEach(Action<TElement> action)
        {
            try
            {
                ForEach(action);
            }
            catch { }
        }
        protected bool IsValidIndex(int index) => _collection != null && index >= 0 && index < Count;

        #endregion

        #region helper methods

        protected bool IsValidIndex(int barsAgo, int period)
        {
            if (!IsValidIndex(barsAgo)) return false;
            return period >= 0 && barsAgo + period < Count;
        }
        protected bool IsValidIndexRange(int initialBarsAgo, int finalBarsAgo)
        {
            if (initialBarsAgo > finalBarsAgo) return false;
            return IsValidIndex(initialBarsAgo) && IsValidIndex(finalBarsAgo);
        }

        #endregion

        #region IConvertibleToText implementation

        public virtual string ToString(string header, string description, string valuesSeparator, string elementsSeparator, int tabOrder, int index, bool inLine, bool isIndexVisible, bool isElementsHeaderVisible, bool isElementsDescriptionVisible, bool isElementsSeparatorVisible, bool isElementsValuesVisible)
        {
            string text = string.Empty;
            string tab = string.Empty;
            //valuesSeparator = string.IsNullOrEmpty(valuesSeparator) || valuesSeparator == Environment.NewLine ? ": " : valuesSeparator;
            inLine = inLine || elementsSeparator == Environment.NewLine;
            elementsSeparator = string.IsNullOrEmpty(elementsSeparator) || elementsSeparator == Environment.NewLine ? ", " : elementsSeparator;

            if (!inLine)
                elementsSeparator += Environment.NewLine;

            for (int i = 0; i < tabOrder; i++)
                tab += "\t";
            string inlineString = inLine ? string.Empty : Environment.NewLine + tab;

            text += GetLabelString(true, true, false, true, false, false, 0);

            if (!string.IsNullOrEmpty(header))
                text += tab + header;

            if (!string.IsNullOrEmpty(description))
            {
                if (string.IsNullOrEmpty(header))
                    text += description;
                else
                    text += "(" + description + ")";
            }

            if (_collection == null)
                return $"{text}[NULL]";
            if (_collection.Count == 0)
                return $"{text}[EMPTY]";

            if (isIndexVisible)
                text += $"[{index}]";

            text += inlineString + "[" + inlineString;
            for (int i = 0; i < _collection.Count; i++)
            {
                text += _collection[i].ToString();
                //tabOrder: inLine ? 0 : tabOrder + 1,
                //value: null,
                //isHeaderVisible: isElementsHeaderVisible,
                //isDescriptionVisible: isElementsDescriptionVisible,
                //isSeparatorVisible: isElementsSeparatorVisible,
                //isValueVisible: isElementsValuesVisible);

                if (i != _collection.Count - 1)
                    text += elementsSeparator;
                else
                    text += inlineString + "]";
            }

            return text;
        }
        public override string ToString() => ToArrayString(isCollapsed: true);
        
        public string ToArrayString(bool isCollapsed, int startIndex = int.MinValue, int endIndex = int.MaxValue, int tabOrder = 0, string separator = " - ")
        {
            string text = string.Empty;
            string tab = string.Empty;

            isCollapsed = isCollapsed || separator != Environment.NewLine;
            separator = string.IsNullOrEmpty(separator) || separator == Environment.NewLine ? ", " : separator;
            if (!isCollapsed)
                separator += Environment.NewLine;

            for (int i = 0; i < tabOrder; i++)
                tab += "\t";
            string collapsedString = isCollapsed ? string.Empty : Environment.NewLine + tab;

            if (Count < 0)
                return $"{text}[NULL]";
            else if (Count == 0)
                return $"{text}[EMPTY]";
            else if (startIndex < 0)
                startIndex = 0;
            else if (startIndex > Count - 1)
                startIndex = Count - 1;

            if (endIndex < startIndex)
                endIndex = startIndex;
            else if (endIndex > Count - 1)
                endIndex = Count - 1;

            text += collapsedString + "[" + collapsedString;
            for (int i = startIndex; i <= endIndex; i++)
            {
                //text += _collection[i].ToLogString(tabOrder + 1,string.Empty);
                text += _collection[i].ToString();
                if (i != _collection.Count - 1)
                    text += separator;
                else
                    text += collapsedString + "]";
            }

            return text;
        }

        #endregion

    }
}
