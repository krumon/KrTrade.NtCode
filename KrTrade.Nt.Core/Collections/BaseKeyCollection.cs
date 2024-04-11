using KrTrade.Nt.Core.Caches;
using System;
using System.Collections;
using System.Collections.Generic;

namespace KrTrade.Nt.Core.Collections
{
    public class BaseKeyCollection<T> : IEnumerable, IEnumerable<T>
        where T : IHasKey
    {
        protected IList<T> _collection;
        private IDictionary<string, int> _keys;

        public T this[string key]
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
        public T this[int index]
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
            _collection = new List<T>();
        }
        public BaseKeyCollection(IEnumerable<T> elements) 
        { 
            _collection = new List<T>(elements);
        }
        public BaseKeyCollection(int capacity) 
        { 
            _collection = new List<T>(capacity);
        }

        #region Implementation

        public override string ToString()
        {

            if (_collection == null || _collection.Count == 0)
                return string.Empty;

            string text = string.Empty;
            foreach (var element in _collection)
                text += element.Key + "NewLine";

            text.Remove(text.Length - 7);
            text.Replace("NewLine", Environment.NewLine);

            return text;
        }

        public void Add(T series) => Add(null, series);
        public void Add(string name, T element)
        {
            try
            {
                if (element == null)
                    throw new ArgumentNullException(nameof(element));

                if (_keys == null)
                    _keys = new Dictionary<string, int>();

                if (string.IsNullOrEmpty(name))
                    name = element.Key;

                // El servicio no existe
                if (!ContainsKey(element.Key))
                {
                    _collection.Add(element);
                    _keys.Add(element.Key, _collection.Count - 1);
                    // El pseudónimo ya existe.
                    if (element.Key != name && ContainsKey(name))
                        throw new Exception($"The pseudo-name: '{name}' already exists. The pseudo-name is being used by another service and the service cannot be added.");
                    else if (element.Key != name)
                        _keys.Add(name, _collection.Count - 1);
                }
                else
                    throw new Exception("The key of element to be added already exists.");
            }
            catch (Exception e)
            {
                throw new Exception($"The element with name:{element.Name} and key:{element.Key} cannot be added.",e);
            }
        }
        public void TryAdd(T series)
        {
            try
            {
                Add(series);
            }
            catch { }
        }
        public void TryAdd(string name, T element)
        {
            try
            {
                Add(name, element);
            }
            catch { }
        }

        //public void Add(BaseSeriesInfo info)
        //{

        //}
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

        public IEnumerator<T> GetEnumerator() => _collection.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        protected void ForEach(Action<T> action)
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
        protected void TryForEach(Action<T> action)
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
