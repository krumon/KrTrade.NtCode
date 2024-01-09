using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using System;
using System.Collections;
using System.Collections.Generic;

namespace KrTrade.Nt.Core.Caches
{
    /// <summary>
    /// Base class for all caches.
    /// </summary>
    /// <typeparam name="T">The type of cache element.</typeparam>
    public abstract class Cache<T> : ICache<T>,
        IList<T>,
        ICollection<T>,
        IEnumerable<T>,
        IEnumerable
    {

        #region Consts

        public const int MAX_CAPACITY = 256;
        public const int DEFAULT_CAPACITY = 20;

        #endregion

        #region Private members

        private readonly IList<T> _cache = new List<T>();
        private T _candidateValue;
        private T _lastRemovedValue;
        private T _currentValue;
        private bool _released = true;
        private bool _redo = true;

        #endregion

        #region Implementation

        public int Capacity { get; private set; }
        public int Displacement { get; private set; }
        public bool IsFull => Count == Capacity;
        public T CurrentValue => _currentValue;
        public void Release()
        {
            _candidateValue = default;
            if (Count > Capacity)
            {
                _lastRemovedValue = _cache[Count-1];
                _cache.RemoveAt(Count-1);
                _redo = false;
                OnElementRemoved();
            }
            _released = true;
        }
        public void ReDo()
        {
            if (_redo)
                return;
            if (_cache == null || Count == 0 || !IsFull)
                return;
            _cache.RemoveAt(0);
            _cache.Insert(Count - 1, _lastRemovedValue);
            _lastRemovedValue = default;
            _redo = true;
            Release();
        }
        public void Reset()
        {
            _cache?.Clear();
            Release();
            _lastRemovedValue = default;
        }
        public void SetCandidateValue(NinjaScriptBase ninjascript = null)
        {
            _candidateValue = GetCandidateValue(ninjascript);
            _released = false;
        }
        public void UpdateCandidateValue(ref T candidateValue, NinjaScriptBase ninjascript = null)
        {
            UpdateCandidateValue(ref candidateValue, ninjascript, null);
        }
        public void UpdateCandidateValue(ref T candidateValue, MarketDataEventArgs marketDataEventArgs = null)
        {
            UpdateCandidateValue(ref candidateValue, null, marketDataEventArgs);
        }
        public void Add()
        {
            if (_released)
                return;
            if (IsValidCandidateValueToAdd(_candidateValue))
            {
                Insert(0, _candidateValue);
                _currentValue = _cache[0];
                OnElementAdded();
            }
            Release();
        }
        public void Update()
        {
            if (_cache == null || Count == 0)
                return;
            if (_released)
                return;
            if (IsValidCandidateValueToUpdateCache(_candidateValue))
            {
                UpdateCurrentValue(ref _currentValue, _candidateValue);
                OnElementChanged();
            }
            Release();
        }
        public T GetElement(int index)
        {
            IsValidIndex(index);
            return this[index];
        }
        public T[] GetElements(int initialIdx, int numberOfElements)
        {
            IsValidIndex(initialIdx, initialIdx + numberOfElements);

            T[] elements = new T[numberOfElements];
            int count = 0;
            for (int i = initialIdx; i < initialIdx + numberOfElements; i++)
            {
                elements[count] = this[i];
                count++;
            }

            return elements;
        }

        protected abstract T GetCandidateValue(NinjaScriptBase ninjascript = null);
        protected abstract void UpdateCandidateValue(ref T candidateValue, NinjaScriptBase ninjascript = null, MarketDataEventArgs marketDataEventArgs = null);
        protected abstract void UpdateCurrentValue(ref T currentValue, T candidateValue);
        protected abstract bool IsValidCandidateValueToAdd(T candidateValue);
        protected abstract bool IsValidCandidateValueToUpdate(T currentValue, T candidateValue);

        #endregion

        #region IEnumerable & ICollection implementation

        public int Count => _cache.Count;
        public void Add(T item)
        {
            _cache.Add(item);
        }
        public void Clear()
        {
            _cache.Clear();
        }
        public int IndexOf(T item)
        {
            return _cache.IndexOf(item);
        }
        public void Insert(int index, T item)
        {
            _cache.Insert(index, item);
        }
        public void RemoveAt(int index)
        {
            _cache.RemoveAt(index);
        }
        public bool Contains(T item)
        {
            return _cache.Contains(item);
        }
        public void CopyTo(T[] array, int arrayIndex)
        {
            _cache.CopyTo(array, arrayIndex);
        }
        public bool Remove(T item)
        {
            return _cache.Remove(item);
        }
        public IEnumerator<T> GetEnumerator()
        {
            return _cache.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        public bool IsReadOnly => false;
        public T this[int index]
        {
            get => _cache[index];
            set => _cache[index] = value;
        }

        #endregion

        #region Constructors

        ///// <summary>
        ///// Create <see cref="ICache{T}"/> instance with the default capacity.
        ///// </summary>
        //public Cache() : this(DEFAULT_CAPACITY, 0)
        //{
        //}

        ///// <summary>
        ///// Create <see cref="ICache{T}"/> instance with specific capacity.
        ///// </summary>
        ///// <param name="capacity">The <see cref="ICache{T}"/> capacity.</param>
        //public Cache(int capacity) : this(capacity, 0)
        //{
        //}

        ///// <summary>
        ///// Create <see cref="ICache{T}"/> instance with infinity capacity.
        ///// </summary>
        ///// <param name="infiniteCapacity">Indicates infinite <see cref="ICache{T}"/> capacity.</param>
        //public Cache(bool infiniteCapacity) : this(DEFAULT_CAPACITY, 0, infiniteCapacity)
        //{
        //}

        ///// <summary>
        ///// Create <see cref="ICache{T}"/> instance.
        ///// </summary>
        ///// <param name="capacity">The <see cref="ICache{T}"/> capacity.</param>
        ///// <param name="displacement">The displacement of <see cref="ICache{T}"/> respect NinjaScript <see cref="ISeries"/> used to gets elements.</param>
        //public Cache(int capacity, int displacement) : this(capacity,displacement,false)
        //{
        //}

        /// <summary>
        /// Create <see cref="ICache{T}"/> instance.
        /// </summary>
        /// <param name="capacity">The <see cref="ICache{T}"/> capacity.</param>
        /// <param name="displacement">The displacement of <see cref="ICache{T}"/> respect NinjaScript <see cref="ISeries"/> used to gets elements.</param>
        /// <param name="infiniteCapacity">Indicates infinite <see cref="ICache{T}"/> capacity.</param>
        protected Cache(int capacity, int displacement, bool infiniteCapacity)
        {
            Capacity = infiniteCapacity ? int.MaxValue : capacity <= 0 ? DEFAULT_CAPACITY : capacity > MAX_CAPACITY ? DEFAULT_CAPACITY : capacity;
            Displacement = displacement < 0 ? 0 : displacement > capacity ? capacity - 1 : displacement;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// An event driven method which is called whenever a element is added to cache.
        /// </summary>
        public virtual void OnElementAdded()
        {

        }

        /// <summary>
        /// An event driven method which is called whenever a element is removed of cache.
        /// </summary>
        public virtual void OnElementRemoved()
        {

        }

        /// <summary>
        /// An event driven method which is called whenever a element changed in cache.
        /// </summary>
        public virtual void OnElementChanged()
        {

        }

        #endregion

        private bool IsValidCandidateValueToUpdateCache(T candidateValue) => IsValidCandidateValueToAdd(candidateValue) && IsValidCandidateValueToUpdate(_currentValue, candidateValue);
        protected bool IsValidIndex(int idx)
        {
            if (idx < 0 || idx >= Count)
                throw new ArgumentOutOfRangeException(nameof(idx));

            return true;
        }
        protected bool IsValidIndex(int startIdx, int finalIdx)
        {
            if (startIdx > finalIdx)
                throw new ArgumentException(string.Format("The {0} cannot be mayor than {1}.", nameof(startIdx), nameof(finalIdx)));

            if (IsValidIndex(startIdx) && IsValidIndex(finalIdx))
                return true;

            return false;
        }

    }
}
