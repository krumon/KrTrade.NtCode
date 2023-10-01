using NinjaTrader.NinjaScript;
using System;
using System.Collections.Generic;

namespace KrTrade.Nt.Console
{

    public abstract class Cache<T>
        where T : BaseElement
    {

        #region Private members

        // TODO: REMOVE
        public string debugText;

        private List<T> cache = new List<T>();
        private readonly int capacity;
        private readonly int period;
        private readonly int displacement;
        protected NinjaScriptBase ninjascript;

        #endregion

        #region Events

        public event Action<T> ElementAdded = delegate { };
        public event Action<T> ElementRemoved = delegate { };
        public event Action<int,T> ElementExist = delegate { };
        public event Action<int,T,T> ElementReplaced = delegate { };

        #endregion

        #region Public properties

        public T this[int index] => cache[index];

        public int CurrentBar => ninjascript.CurrentBar;

        public int Capacity => capacity;
        
        public int Period => period;
        
        public int Displacement => displacement;

        public int Idx => CurrentBar - Displacement;

        public int Count => cache.Count;

        #endregion

        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        public Cache(NinjaScriptBase ninjascript, int capacity, int period)
        {
            this.ninjascript = ninjascript;
            this.displacement = ninjascript.Calculate == Calculate.OnBarClose ? 0 : 1;
            this.capacity = capacity <= 0 ? int.MaxValue : capacity;
            this.period = period <= 0 ? int.MaxValue : period;
        }

        #endregion

        #region Abstract methods

        public abstract T CreateCacheElement();

        public abstract void OnBarUpdated();

        #endregion

        #region Public methods

        public void Add(T element)
        {
            if (period < CurrentBar - element.Idx)
                throw new Exception("Element NOT created.");

            int idx = GetCacheIdx(element.Idx);
            if (idx == -1)
            {
                cache.Add(element);
                OnNtElementAdded(element);
                if (cache.Count > capacity)
                    cache.RemoveAt(0);
            }
            else
            {
                OnNtElementExist(idx,element);
            }
        }

        public void Replace(T newElement)
        {
            int i = GetCacheIdx(newElement.Idx);
            if (i != -1)
            {
                T tempElement = cache[i];
                T oldElement = tempElement;
                cache[i] = newElement;
                OnNtElementReplaced(newElement.Idx,newElement, oldElement);
                oldElement.Dispose();
            }
        }

        public void Clean(int currentBar)
        {
            if (cache != null && Count > 0)
            {
                while (Count > 0)
                {
                    if (cache[0].Idx < currentBar - period || cache.Count > capacity)
                        RemoveAt(0);
                    else
                        return;
                }
            }
        }

        public void Clear()
        {
            this.RemoveAll();
            cache.Clear();
        }

        public void RemoveAt(int idx)
        {
            T oldElement = cache[idx];
            cache.RemoveAt(idx);
            OnNtElementRemoved(oldElement);
            oldElement.Dispose();
        }

        public void Remove(T item)
        {
            int idx = this.GetCacheIdx(item.Idx);
            if (idx == -1)
                return;
            RemoveAt(idx);
        }

        public void RemoveAll()
        {
            if (cache != null && Count > 0)
                while (Count > 0)
                    RemoveAt(0);
        }

        public virtual void Dispose()
        {
            cache.Clear();
            cache = null;
        }

        public bool Exist(int idx)
        {
            if (cache != null && Count > 0)
                for (int i = Count - 1; i >= 0; i--)
                    if (cache[i].Idx == idx)
                        return true;
            return false;
        }

        public bool Exist(T element)
        {
            if (cache != null && Count > 0)
                for (int i = Count - 1; i >= 0; i--)
                    if (cache[i].Idx == element.Idx)
                        return true;
            return false;
        }

        public T GetElement(int elementIdx)
        {
            if (cache != null && Count > 0)
                for (int i = Count - 1; i >= 0; i--)
                    if (cache[i].Idx == elementIdx)
                        return (T)cache[i];
            return null;
        }

        public T GetElement(T element)
        {
            if (cache != null && Count > 0)
                for (int i = Count - 1; i >= 0; i--)
                    if (cache[i].Idx == element.Idx)
                        return (T)cache[i];
            return null;
        }

        public int GetCacheIdx(int elementIdx)
        {
            if (cache == null)
                throw new Exception("Cache memory is null. Object reference not set to an instance of an object");

            if (Count == 0)
                return -1;

            if (Count > 0)
                for (int i = Count - 1; i >= 0; i--)
                    if (cache[i].Idx == elementIdx)
                        return i;

            return -1;

        }

        public int GetCacheIdx(T element)
        {
            return GetCacheIdx(element.Idx);
        }

        public List<T> GetRange(int startIdx, int endIdx)
        {
            if (cache != null && Count > 0)
            {
                if (startIdx < 0 || startIdx > Count - 1 || endIdx < startIdx || endIdx >= Count)
                    return null;

                List<T> cacheRange = new List<T>();
                for (int i = startIdx; i <= endIdx; i++)
                    cacheRange.Add(this[i]);
                return cacheRange;
            }
            return null;
        }

        public List<T> GetRange(int startIdx)
        {
            if (cache != null && Count > 0)
            {
                if (startIdx < 0 || startIdx > Count - 1)
                    return null;

                List<T> cacheRange = new List<T>();
                for (int i = startIdx; i < Count; i++)
                    cacheRange.Add(this[i]);
                return cacheRange;
            }
            return null;
        }

        #endregion

        #region Virtual methods

        protected virtual void OnElementAdded(T element) { }

        protected virtual void OnElementRemoved(T element) { }
        
        protected virtual void OnElementExist(int idx,T element) { }
       
        protected virtual void OnElementReplaced(int idx,T newElement, T oldElement) { }

        #endregion

        #region Private methods

        protected void OnNtElementAdded(T element)
        {
            // Call to listeners
            OnElementAdded(element);

            // Raise Event
            ElementAdded?.Invoke(element);

        }

        protected void OnNtElementRemoved(T element)
        {
            // Call to listeners
            OnElementRemoved(element);

            // Raise Event
            ElementRemoved?.Invoke(element);

        }

        protected void OnNtElementExist(int idx, T element)
        {
            // Call to listeners
            OnElementExist(idx,element);

            // Raise Event
            ElementExist?.Invoke(idx,element);

        }

        protected void OnNtElementReplaced(int idx, T newElement, T oldElement)
        {
            // Call to listeners
            OnElementReplaced(idx, newElement, oldElement);

            // Raise Event
            ElementReplaced?.Invoke(idx, newElement, oldElement);

        }

        #endregion

    }

}
