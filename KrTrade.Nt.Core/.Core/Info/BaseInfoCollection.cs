using System;
using System.Collections;
using System.Collections.Generic;

namespace KrTrade.Nt.Core
{
    public abstract class BaseInfoCollection<TElement> : BaseInfo, IInfoCollection<TElement>, IEnumerable, IEnumerable<TElement>
        where TElement : IInfo
    {
        protected IList<TElement> _collection;

        public TElement this[string key]
        {
            get
            {
                try
                {
                    TryGetValue(key, out int index);
                    if (IsValidIndex(index))
                        return _collection[index];
                    else
                        return default;
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

        public BaseInfoCollection() 
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

                if (!_collection.Contains(item))
                    _collection.Add(item);
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
        public void Remove(TElement item)
        {
            try
            {
                if (_collection == null)
                    throw new ArgumentNullException(nameof(_collection));

                _collection.Remove(item);
            }
            catch (Exception ex)
            {
                throw new Exception($"The element cannot be removed.", ex);
            }
        }
        public void Remove(string key)
        {
            try
            {
                TryGetValue(key, out int index);
                if (IsValidIndex(index))
                    Remove(_collection[index]);
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
            }
            catch (Exception e)
            {
                throw new Exception($"The element cannot be removed.", e);
            }
        }
        public bool Contains(TElement item) => _collection != null && _collection.Contains(item);
        public bool Contains(string key)
        {
            if (_collection == null || string.IsNullOrEmpty(key) || Count < 1)
                return false;

            bool contains = false;
            for (int i = 0; i < _collection.Count; i++)
                if (_collection[i].Key.Equals(key))
                {
                    contains = true; 
                    break;
                }
            return contains;
        } 
        public bool TryGetValue(TElement item,out int index)
        {
            index = -1;
            if (_collection == null || item == null || Count < 1)
                return false;

            bool contains = false;
            for (int i = 0; i < _collection.Count; i++)
                if (_collection[i].Equals(item))
                {
                    contains = true;
                    index = i;
                    break;
                }
            return contains;
        }
        public bool TryGetValue(string key,out int index)
        {
            index = -1;
            if (_collection == null || string.IsNullOrEmpty(key) || Count < 1)
                return false;

            bool contains = false;
            for (int i = 0; i < _collection.Count; i++)
                if (_collection[i].Key.Equals(key))
                {
                    contains = true;
                    index = i;
                    break;
                }
            return contains;
        }

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
