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

        public const int DEFAULT_PERIOD = 20;

        #endregion

        #region Private members

        private readonly IList<T> _cache = new List<T>();
        private T _candidateValue;
        private T _currentValue;

        //private T _lastRemovedValue;
        //private bool _redo = true;

        protected bool IsOverLength => Count > Capacity;
        protected bool HasOldValues => Count > Period + Displacement;
        protected int MaxPeriod => int.MaxValue - Displacement - RemovedValuesCacheLength;

        #endregion

        #region Implementation

        public int Period { get; protected set; } = -1;
        public int Displacement { get; protected set; } = 0;
        public int RemovedValuesCacheLength { get; set; } = 1;

        public int Capacity => Period + Displacement + RemovedValuesCacheLength;
        public bool IsFull => Count >= Capacity;

        public T CurrentValue => _currentValue;
        //public T RemovedValue => _lastRemovedValue;
        //public T LastValue => GetValue(Displacement);

        public void Release()
        {
            _candidateValue = default;
            if (Count > Capacity)
            {
                T removedValue = _cache[Count - 1];
                _cache.RemoveAt(Count - 1);
                OnElementRemoved(removedValue);
            }
        }
        public void ReDo()
        {
            if (_cache == null || Count == 0)
                return;
            if (HasOldValues)
                _cache.RemoveAt(0);
            //Release();
        }
        public void Reset()
        {
            _cache?.Clear();
            Release();
            _currentValue = default;
            //_lastRemovedValue = default;
        }
        public bool Add(NinjaScriptBase ninjascript = null)
        {
            bool isAdded = false;
            _candidateValue = GetCandidateValue(ninjascript);
            if (IsValidValue(_candidateValue))
            {
                _currentValue = _candidateValue;
                Insert(0, _candidateValue);
                //_currentValue = _cache[Displacement];
                OnElementAdded(_currentValue);
                isAdded = true;
            }
            Release();
            return isAdded;
        }
        public bool Add(MarketDataEventArgs marketDataEventArgs)
        {
            bool isAdded = false;
            _candidateValue = GetCandidateValue(marketDataEventArgs);
            if (IsValidValue(_candidateValue))
            {
                _currentValue = _candidateValue;
                Insert(0, _candidateValue);
                //_currentValue = _cache[Displacement];
                OnElementAdded(_currentValue);
                isAdded = true;
            }
            Release();
            return isAdded;
        }
        public bool Update(NinjaScriptBase ninjascript = null)
        {
            if (_cache == null || Count == 0)
                return false;
            
            bool isUpdated = false;
            if (UpdateCurrentValue(ref _currentValue, ninjascript))
            {
                OnElementUpdated(_currentValue);
                isUpdated = true;
            }
            //Release();
            return isUpdated;
        }
        public bool Update(MarketDataEventArgs marketDataEventArgs)
        {
            if (_cache == null || Count <= Displacement)
                return false;

            bool isUpdated = false;
            if (UpdateCurrentValue(ref _currentValue, marketDataEventArgs))
            {
                OnElementUpdated(_currentValue);
                isUpdated = true;
            }
            //Release();
            return isUpdated;
        }
        public T GetValueAt(int index)
        {
            if (IsValidIndex(index))
                return this[index];
            throw new ArgumentOutOfRangeException(nameof(index));
        }
        public T[] GetValues(int initialIdx, int numberOfElements)
        {
            if (!IsValidIndexs(initialIdx, initialIdx + numberOfElements))
                throw new ArgumentOutOfRangeException(nameof(numberOfElements));

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
        protected abstract T GetCandidateValue(MarketDataEventArgs marketDataEventArgs);
        protected abstract bool UpdateCurrentValue(ref T currentValue, NinjaScriptBase ninjascript = null);
        protected abstract bool UpdateCurrentValue(ref T currentValue, MarketDataEventArgs marketDataEventArgs);
        protected abstract bool IsValidValue(T value);

        #endregion

        #region ISeries<T> implementation

        public int Count => _cache.Count;
        public T this[int index]
        {
            get => IsValidIndex(index) ? _cache[index] : default;
            set
            {
                if (IsValidIndex(index))
                    _cache[index] = value;
            }
        }
        public abstract bool IsValidDataPoint(int barsAgo);
        public abstract bool IsValidDataPointAt(int barIndex);

        #endregion

        #region IEnumerable & ICollection implementation

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

        #endregion

        #region Constructors

        /// <summary>
        /// Create <see cref="ICache{T}"/> instance.
        /// When pass <paramref name="period"/> minor than 0, the <paramref name="period"/> will be MAXIMUM,
        /// when pass <paramref name="period"/> equal than 0, the <paramref name="period"/> will be DEFAULT,
        /// and when pass <paramref name="period"/> grater than 0, the <paramref name="period"/> will be the specified.
        /// </summary>
        /// <param name="period">The <see cref="ICache{T}"/> calculate period. When pass a number minor than 0, the period will be the MAXIMUM,</param>
        /// <param name="displacement">The displacement of <see cref="ICache{T}"/> respect NinjaScript <see cref="ISeries"/> used to gets elements.</param>
        protected Cache(int period, int displacement)
        {
            Displacement = displacement <= 0 ? 0 : displacement;
            Period = period < 0 ? MaxPeriod : period == 0 ? DEFAULT_PERIOD : period > MaxPeriod ? MaxPeriod : period;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// An event driven method which is called whenever a element is added to cache.
        /// </summary>
        /// <param name="addedElement">The element added.</param>
        public virtual void OnElementAdded(T addedElement)
        {

        }

        /// <summary>
        /// An event driven method which is called whenever a element is removed of cache.
        /// </summary>
        /// <param name="removedElement">The removed element.</param>
        public virtual void OnElementRemoved(T removedElement)
        {

        }

        /// <summary>
        /// An event driven method which is called whenever a element changed in cache.
        /// </summary>
        /// <param name="updatedElement">The updated element.</param>
        public virtual void OnElementUpdated(T updatedElement)
        {

        }

        #endregion

        //private bool IsValidCandidateValueToUpdateCache(T candidateValue) => IsValidValue(candidateValue) && IsValidCandidateValueToUpdate(_currentValue, candidateValue);
        protected bool IsValidIndex(int idx)
        {
            if (idx < 0 || idx >= Count)
                return false;
                //throw new ArgumentOutOfRangeException(nameof(idx));

            return true;
        }
        protected bool IsValidIndexs(int startIdx, int finalIdx)
        {
            if (startIdx > finalIdx)
                return false;
                //throw new ArgumentException(string.Format("The {0} cannot be mayor than {1}.", nameof(startIdx), nameof(finalIdx)));

            if (IsValidIndex(startIdx) && IsValidIndex(finalIdx))
                return true;

            return false;
        }

    }
}
