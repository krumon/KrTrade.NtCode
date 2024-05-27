using System;
using System.Collections;
using System.Collections.Generic;

namespace KrTrade.Nt.Core.Collections
{
    public abstract class BaseKeyCollection<T> : IKeyCollection<T>, IEnumerable, IEnumerable<T>
        where T : IKeyItem
    {
        protected IList<T> _collection;
        protected IDictionary<string, int> _keys;

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
        public BaseKeyCollection(IEnumerable<T> items) 
        { 
            _collection = new List<T>(items);
        }
        public BaseKeyCollection(int capacity) 
        { 
            _collection = new List<T>(capacity);
        }

        #region Implementation

        public override string ToString() => ToString(string.Empty,0,false);
        public string ToString(string header) => ToString(header,0,false);
        protected string ToString(string header, int tabOrder, bool isMultiLine) => ToString(header, tabOrder, isMultiLine ? Environment.NewLine : ", ");

        public virtual string ToLongString() => ToString(string.Empty, 0, true);
        public virtual string ToLongString(int tabOrder) => ToLongString(string.Empty, tabOrder);
        public string ToLongString(string header, int tabOrder) => ToString(header, tabOrder, true);

        protected string ToString(string header, int tabOrder, string separator)
        {
            string text = string.Empty;
            string tab = string.Empty;
            separator = string.IsNullOrEmpty(separator) ? ", " : separator;

            for (int i = 0; i < tabOrder; i++)
                tab += "\t";

            if (!string.IsNullOrEmpty(header))
                text += tab + header;
            
            if (_collection != null && _collection.Count > 0)
            {
                text += (separator == Environment.NewLine ? Environment.NewLine + tab : string.Empty) + "[" + (separator == Environment.NewLine ? Environment.NewLine + tab : string.Empty);
                for (int i = 0; i < _collection.Count; i++)
                {
                    text += _collection[i].ToString(tabOrder + 1);
                    if (i == _collection.Count - 1)
                        text += (separator == Environment.NewLine ? Environment.NewLine + tab : string.Empty) + "]";
                    else
                        text += (separator != Environment.NewLine ? separator : string.Empty) + (separator == Environment.NewLine ? Environment.NewLine : string.Empty);
                }
            }
            else
                text += "[EMPTY]";

            return text;
        }

        public void Add(T item)
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
                    else if (key != name)
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
        public void TryAdd(T item)
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
