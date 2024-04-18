using KrTrade.Nt.Core.Elements;
using KrTrade.Nt.Core.Services;
using System;
using System.Collections;
using System.Collections.Generic;

namespace KrTrade.Nt.Core.Collections
{
    public abstract class BaseKeyCollection<TElement,TInfo> : IEnumerable, IEnumerable<TElement>
        where TElement : IKeyCollectionItem
        where TInfo : IElementInfo
    {
        protected IList<TElement> _collection;
        private IDictionary<string, int> _keys;

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

        public BaseKeyCollection() 
        { 
            _collection = new List<TElement>();
        }
        public BaseKeyCollection(IEnumerable<TElement> elements) 
        { 
            _collection = new List<TElement>(elements);
        }
        public BaseKeyCollection(int capacity) 
        { 
            _collection = new List<TElement>(capacity);
        }

        #region Implementation

        public override string ToString()
        {

            if (_collection == null || _collection.Count == 0)
                return string.Empty;

            string text = string.Empty;
            foreach (var element in _collection)
                text += element.Info.GetKey() + "NewLine";

            text.Remove(text.Length - 7);
            text.Replace("NewLine", Environment.NewLine);

            return text;
        }

        //public void Add(TElement series) => Add(null, series);
        public void Add(TElement element)
        {
            try
            {
                if (element == null)
                    throw new ArgumentNullException(nameof(element));

                if (_keys == null)
                    _keys = new Dictionary<string, int>();

                string key = element.Info.GetKey();
                string name = element.Info.Name;

                // El servicio no existe
                if (!ContainsKey(key))
                {
                    _collection.Add(element);
                    _keys.Add(key, _collection.Count - 1);
                    // El pseudónimo ya existe.
                    if (key != name && ContainsKey(name))
                        throw new Exception($"The pseudo-name: '{name}' already exists. The pseudo-name is being used by another service and the service cannot be added.");
                    else if (key != name)
                        _keys.Add(name, _collection.Count - 1);
                }
                else
                    throw new Exception("The key of element to be added already exists.");
            }
            catch (Exception e)
            {
                throw new Exception($"The element with name:{element.Info.Name} and key:{element.Info.GetKey()} cannot be added.",e);
            }
        }
        //public void TryAdd(TElement series)
        //{
        //    try
        //    {
        //        Add(series);
        //    }
        //    catch { }
        //}
        public void TryAdd(TElement element)
        {
            try
            {
                Add(element);
            }
            catch { }
        }
        public abstract void Add<TOptions>(TInfo elementInfo, TOptions elementOptions)
            where TOptions : ServiceOptions;

        public int Count => _collection.Count;
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
        public bool ContainsKey(string key) => _collection != null && _keys.ContainsKey(key);
        public bool TryGetValue(string key,out int index) => _keys.TryGetValue(key,out index);

        public IEnumerator<TElement> GetEnumerator() => _collection.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

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

    }
}
