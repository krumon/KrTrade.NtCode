using System;
using System.Collections;
using System.Collections.Generic;

namespace KrTrade.Nt.Core.Caches
{
    /// <summary>
    /// Base class for all caches.
    /// </summary>
    /// <typeparam name="T">The type of cache element.</typeparam>
    public abstract class Cache : ICache
    {

        public const int DEFAULT_PERIOD = 20;

        // ICache implementation
        public int Period { get; protected set; } = -1;
        public int Displacement { get; protected set; } = 0;
        public int LengthOfRemovedValuesCache { get; set; } = 1;
        public int Capacity => Period + Displacement + LengthOfRemovedValuesCache;
        protected int MaxPeriod => int.MaxValue - Displacement - LengthOfRemovedValuesCache;

        /// <summary>
        /// Create <see cref="ICache{T}"/> instance.
        /// When pass <paramref name="period"/> minor than 0, the <paramref name="period"/> will be MAXIMUM,
        /// when pass <paramref name="period"/> equal than 0, the <paramref name="period"/> will be DEFAULT,
        /// and when pass <paramref name="period"/> grater than 0, the <paramref name="period"/> will be the specified.
        /// </summary>
        /// <param name="period">The <see cref="ICache{T}"/> calculate period. When pass a number minor than 0, the period will be the MAXIMUM,</param>
        /// <param name="displacement">The displacement of <see cref="ICache{T}"/> respect the last element.</param>
        protected Cache(int period, int displacement)
        {
            Displacement = displacement <= 0 ? 0 : displacement;
            Period = period < 0 ? MaxPeriod : period == 0 ? DEFAULT_PERIOD : period > MaxPeriod ? MaxPeriod : period;
            OnInit();
        }

        /// <summary>
        /// An event driven method which is called whenever the <see cref="ICache{T}"/> initialize.
        /// </summary>
        protected virtual void OnInit()
        {
        }

    }

    /// <summary>
    /// Base class for all caches.
    /// </summary>
    /// <typeparam name="T">The type of cache element.</typeparam>
    public abstract class Cache<T> : ICache<T>,
        IEnumerable<T>,
        IEnumerable
    {

        public const int DEFAULT_PERIOD = 20;
        private IList<T> _cache = new List<T>();

        // ICache implementation
        public int Period { get; protected set; } = -1;
        public int Displacement { get; protected set; } = 0;
        public int LengthOfRemovedValuesCache { get; set; } = 1;
        public int Capacity => Period + Displacement + LengthOfRemovedValuesCache;
        public bool IsFull => Count >= Capacity;
        public T CurrentValue { get => _cache == null || Count == 0 ? default : _cache[0]; protected set => _cache[0] = value; }
        public void RemoveLastElement()
        {
            if (Count > 0 && Count != Period + Displacement)
                RemoveAt(0);
            else 
                throw new Exception("The cache cannot restore the element because the removed values cache is empty.");
        }
        public void Reset()
        {
            _cache?.Clear();
        }
        public void Dispose()
        {
            Reset();
            _cache = null;
        }
        public T GetValue(int valuesAgo) => IsValidIndex(valuesAgo) ? _cache[valuesAgo] : default;
        public T[] ToArray(int fromValuesAgo, int numOfValues)
        {
            if (!IsValidIndex(fromValuesAgo, numOfValues))
                throw new ArgumentOutOfRangeException(nameof(numOfValues));

            T[] elements = new T[numOfValues];
            int count = 0;
            for (int i = fromValuesAgo; i < fromValuesAgo + numOfValues; i++)
            {
                elements[count] = this[i];
                count++;
            }

            return elements;
        }

        // ISeries<T> implementation
        public int Count => _cache.Count;
        public T this[int index] 
        { 
            get => IsValidIndex(index) ? _cache[index] : throw new ArgumentOutOfRangeException(nameof(index));
            private set
            {
                if (!IsValidIndex(index)) throw new ArgumentOutOfRangeException(nameof(index));
                T oldValue = _cache[index];
                _cache[index] = value;
                OnElementUpdated(oldValue, _cache[index]);
            }
        } 
        public T GetValueAt(int valueIndex) => IsValidIndex(Count - valueIndex) ? _cache[Count - valueIndex] : default;
        public bool IsValidDataPoint(int valuesAgo) => IsValidIndex(valuesAgo) && IsValidValue(_cache[valuesAgo]);
        public bool IsValidDataPointAt(int valueIndex) => IsValidDataPoint(Count - valueIndex);

        // IEnumerable implementation
        public IEnumerator<T> GetEnumerator()
        {
            return _cache.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        
        // Private and protected members
        protected abstract bool IsValidValue(T value);
        protected void Add(T item)
        {
            Insert(0, item);
        }
        private void Release()
        {
            if (Count > Capacity)
                RemoveAt(Count - 1);
        }
        private void Insert(int index, T item)
        {
            _cache.Insert(index, item);
            OnElementAdded(CurrentValue);
            Release();
        }
        private void RemoveAt(int index)
        {
            T removedItem = this[index];
            _cache.RemoveAt(index);
            if (index == 0)
                OnLastElementRemoved();
            else
                OnElementRemoved(removedItem);
        }
        protected int MaxPeriod => int.MaxValue - Displacement - LengthOfRemovedValuesCache;
        protected bool IsValidIndex(int barsAgo) => _cache != null && barsAgo >= 0 && barsAgo < Count;
        protected bool IsValidIndex(int barsAgo, int period)
        {
            if (!IsValidIndex(barsAgo)) return false;
            return period >= 0 && barsAgo + period < Count;
        }
        protected bool IsValidIndexes(int initialBarsAgo, int finalBarsAgo)
        {
            if (initialBarsAgo > finalBarsAgo) return false;
            return IsValidIndex(initialBarsAgo) && IsValidIndex(finalBarsAgo);
        }

        /// <summary>
        /// Create <see cref="ICache{T}"/> instance.
        /// When pass <paramref name="period"/> minor than 0, the <paramref name="period"/> will be MAXIMUM,
        /// when pass <paramref name="period"/> equal than 0, the <paramref name="period"/> will be DEFAULT,
        /// and when pass <paramref name="period"/> grater than 0, the <paramref name="period"/> will be the specified.
        /// </summary>
        /// <param name="period">The <see cref="ICache{T}"/> calculate period. When pass a number minor than 0, the period will be the MAXIMUM,</param>
        /// <param name="displacement">The displacement of <see cref="ICache{T}"/> respect the last element.</param>
        protected Cache(int period, int displacement)
        {
            Displacement = displacement <= 0 ? 0 : displacement;
            Period = period < 0 ? MaxPeriod : period == 0 ? DEFAULT_PERIOD : period > MaxPeriod ? MaxPeriod : period;
            OnInit();
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
        /// An event driven method which is called whenever the <see cref="ICache{T}"/> initialize.
        /// </summary>
        protected virtual void OnInit()
        {
        }

        /// <summary>
        /// An event driven method which is called whenever a last element of cache is removed.
        /// </summary>
        protected virtual void OnLastElementRemoved()
        {
        }
    }
}
