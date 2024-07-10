using KrTrade.Nt.Core.Data;
using System;
using System.Collections;
using System.Collections.Generic;

namespace KrTrade.Nt.Core.Caches
{
    /// <summary>
    /// Base class for all caches.
    /// </summary>
    public abstract class Cache<T> : ICache<T>
    {

        private IList<T> _cache = new List<T>();
        protected int OldValuesLength => Count < Capacity || Count > MaxLength ? 0 : MaxLength - Count;
        protected int MaxCapacity => int.MaxValue - OldValuesCapacity;
        protected int MaxLength => Capacity + OldValuesCapacity;

        // ICache<T> implementation
        public int Capacity { get; protected set; }
        public int OldValuesCapacity { get; protected set; }
        public int Count => Math.Min(_cache.Count,Capacity);
        public bool IsFull => _cache.Count > MaxLength;
        public T CurrentValue { get; protected set; }
        public T LastValue { get; protected set; }
        public void RemoveLastElement()
        {
            if (IsValidIndex(0))
                RemoveAt(0);
            else
                throw new Exception("The cache is empty.");
        }
        public void Reset()
        {
            _cache?.Clear();
            CurrentValue = default;
            LastValue = default;
        }
        public void Dispose()
        {
            Reset();
            _cache = null;
        }
        public bool IsValidIndex(int index) => _cache != null && index >= 0 && index < Count;
        public bool IsValidRange(int startIndex, int count)
        {
            if (!IsValidIndex(startIndex)) return false;
            return count >= 0 && startIndex + count < Count;
        }
        public bool IsValidRange(int startIndex, int finalIndex, bool includeIndexValues)
        {
            startIndex = includeIndexValues ? startIndex : startIndex + 1;
            finalIndex = includeIndexValues ? finalIndex : finalIndex - 1;
            if (startIndex > finalIndex) return false;
            return IsValidIndex(startIndex) && IsValidIndex(finalIndex);
        }
        public T[] GetRange(int startIndex, int count)
        {
            if (!IsValidRange(startIndex, count)) return null;

            T[] range = new T[count];
            for (int i = startIndex; i <= startIndex + count; i++)
                range[i] = _cache[i];
            return range;
        }

        // IEnumerable<T> implementation
        public T this[int index]
        {
            get => IsValidIndex(index) ? _cache[index] : throw new ArgumentOutOfRangeException(nameof(index));
            protected set => _cache[index] = value;
        }

        // IEnumerable implementation
        public IEnumerator<T> GetEnumerator() => _cache.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => _cache.GetEnumerator();

        // Private and protected methods
        protected void Add(T item)
        {
            Insert(0, item);
        }
        private void Release()
        {
            if (_cache.Count > MaxLength)
                RemoveAt(Count - 1);
        }
        private void Insert(int index, T item)
        {
            _cache.Insert(index, item);
            LastValue = CurrentValue;
            CurrentValue = item;
            OnElementAdded(CurrentValue);
            Release();
        }
        private void RemoveAt(int index)
        {
            T removedItem = this[index];
            _cache.RemoveAt(index);
            if (index == 0)
            {
                CurrentValue = LastValue;
                OnLastElementRemoved(removedItem);
            }
            else
                OnElementRemoved(removedItem);
        }

        //// ISeries implementation
        //public abstract void Add(int barsAgo);
        //public abstract void Update(int barsAgo);
        //public abstract void Update(NinjaTrader.Data.MarketDataEventArgs args, int barsAgo);

        /// <summary>
        /// Create <see cref="ICache{T}"/> instance with specified capacity.
        /// <param name="capacity">The cache values capacity.</param>
        /// <param name="oldValuesCapacity">The old cache values capacity.</param>
        public Cache(int capacity, int oldValuesCapacity)
        {
            OldValuesCapacity = oldValuesCapacity < 1 ? Globals.SERIES_DEFAULT_OLD_VALUES_CAPACITY : oldValuesCapacity;
            Capacity = capacity <= 0 ? Globals.SERIES_DEFAULT_CAPACITY : capacity > MaxCapacity ? MaxCapacity : capacity;
        }

        /// <summary>
        /// An event driven method which is called whenever a element is added to cache.
        /// </summary>
        /// <param name="addedElement">The element added.</param>
        protected virtual void OnElementAdded(T addedElement)
        {
        }

        /// <summary>
        /// An event driven method which is called whenever a element is removed of cache.
        /// </summary>
        /// <param name="removedElement">The removed element.</param>
        protected virtual void OnElementRemoved(T removedElement)
        {
        }

        /// <summary>
        /// An event driven method which is called whenever a element changed in cache.
        /// </summary>
        /// <param name="oldValue">The old value of the cache element.</param>
        /// <param name="newValue">The new value of the cache element.</param>
        protected virtual void OnElementUpdated(T oldValue, T newValue)
        {
        }

        /// <summary>
        /// An event driven method which is called whenever a last element of cache is removed.
        /// </summary>
        /// <param name="removedElement">The removed element.</param>
        protected virtual void OnLastElementRemoved(T removedElement)
        {
        }

    }

}
