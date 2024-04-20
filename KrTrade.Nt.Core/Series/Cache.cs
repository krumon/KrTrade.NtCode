using System;
using System.Collections;
using System.Collections.Generic;

namespace KrTrade.Nt.Core.Series
{
    /// <summary>
    /// Base class for all caches.
    /// </summary>
    /// <typeparam name="T">The type of cache element.</typeparam>
    public abstract class Cache : ICache
    {

        public const int DEFAULT_CAPACITY = 20;
        public const int DEFAULT_OLD_VALUES_CAPACITY = 1;

        // ICache implementation
        public int Capacity { get; protected set; }
        public int OldValuesCapacity { get; protected set; }
        protected int MaxCapacity => int.MaxValue - OldValuesCapacity;
        protected int MaxLength => Capacity + OldValuesCapacity;


        /// <summary>
        /// Create <see cref="ICache{T}"/> instance with specified <paramref name="capacity"/> and <paramref name="oldValuesCapacity"/>.
        /// When pass <paramref name="capacity"/> minor or equal than 0, the <paramref name="capacity"/> will be DEFAULT (20),
        /// and when pass <paramref name="capacity"/> grater than 0, the <paramref name="capacity"/> will be the specified.
        /// </summary>
        /// <param name="capacity">The <see cref="ICache{T}"/> capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
        /// <param name="oldValuesCapacity">The length of the removed values cache. This values are at the end of cache.</param>
        protected Cache(int capacity,int oldValuesCapacity = DEFAULT_OLD_VALUES_CAPACITY)
        {
            OldValuesCapacity = OldValuesCapacity < 1 ? DEFAULT_OLD_VALUES_CAPACITY : oldValuesCapacity;
            Capacity =  capacity <= 0 ? DEFAULT_CAPACITY : capacity > MaxCapacity ? MaxCapacity : capacity;
        }

        public object this[int index] => null;
        public abstract int Count {  get; }
        public abstract bool IsFull { get; }
        public abstract void RemoveLastElement();
        public abstract void Reset();
        public abstract void Dispose();
        public object CurrentValue => null;
        public object GetValue(int valuesAgo) { return null; }
        public object[] ToArray(int fromValuesAgo, int numOfValues) => null;
    }

    /// <summary>
    /// Base class for all caches.
    /// </summary>
    /// <typeparam name="T">The type of cache element.</typeparam>
    public abstract class Cache<T> : Cache, ICache<T>,
        IEnumerable<T>,
        IEnumerable
    {

        private IList<T> _cache = new List<T>();
        private bool _isOldValuesCacheRunning;
        protected int OldValuesLength => Count < Capacity || Count > MaxLength ? 0 : MaxLength - Count;

        // ICache<T> implementation
        public override bool IsFull => Count > MaxLength;
        public new T CurrentValue { get => _cache == null || Count == 0 ? default : _cache[0]; protected set => _cache[0] = value; }
        public override void RemoveLastElement()
        {
            if (_isOldValuesCacheRunning && Count > Capacity)
                RemoveAt(0);
            else
                throw new Exception("The cache cannot restore the last element removed because the old values cache is empty.");
        }
        public override void Reset()
        {
            _cache?.Clear();
        }
        public override void Dispose()
        {
            Reset();
            _cache = null;
        }
        public new T GetValue(int valuesAgo) => IsValidIndex(valuesAgo) ? _cache[valuesAgo] : default;
        public new T[] ToArray(int fromValuesAgo, int numOfValues)
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
        public override int Count => _cache.Count;
        public new T this[int index] 
        { 
            get => IsValidIndex(index) ? _cache[index] : throw new ArgumentOutOfRangeException(nameof(index));
            protected set
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
        
        // Private and protected methods
        protected abstract bool IsValidValue(T value);
        protected void Add(T item)
        {
            Insert(0, item);
        }
        private void Release()
        {
            if (Count > MaxLength)
                RemoveAt(Count - 1);
        }
        private void Insert(int index, T item)
        {
            _cache.Insert(index, item);
            if(!_isOldValuesCacheRunning && Count > Capacity)
                _isOldValuesCacheRunning = true;
            OnElementAdded(CurrentValue);
            Release();
        }
        private void RemoveAt(int index)
        {
            T removedItem = this[index];
            _cache.RemoveAt(index);
            if (index == 0)
                OnLastElementRemoved(removedItem);
            else
                OnElementRemoved(removedItem);
        }
        protected bool IsValidIndex(int barsAgo) => _cache != null && barsAgo >= 0 && barsAgo < Count;
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

        /// <summary>
        /// Create <see cref="ICache{T}"/> instance with specified <paramref name="capacity"/> and <paramref name="oldValuesCapacity"/>.
        /// When pass <paramref name="capacity"/> minor or equal than 0, the <paramref name="capacity"/> will be DEFAULT (20),
        /// and when pass <paramref name="capacity"/> grater than 0, the <paramref name="capacity"/> will be the specified.
        /// </summary>
        /// <param name="capacity">The <see cref="ICache{T}"/> capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
        protected Cache(int capacity = DEFAULT_CAPACITY, int oldValuesCapacity = DEFAULT_OLD_VALUES_CAPACITY) : base(capacity,oldValuesCapacity)
        {
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
