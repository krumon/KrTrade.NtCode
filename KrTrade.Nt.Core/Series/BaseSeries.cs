using KrTrade.Nt.Core.Data;
using KrTrade.Nt.Core.Infos;
using KrTrade.Nt.Core.Options;
using KrTrade.Nt.Core.Services;
using NinjaTrader.Data;
using System;
using System.Collections;
using System.Collections.Generic;

namespace KrTrade.Nt.Core.Series
{
    /// <summary>
    /// Base class for all series.
    /// </summary>
    public abstract class BaseSeries<T> : BaseElement<SeriesType, ISeriesInfo, IElementOptions>, ISeries<T>
    {

        private IList<T> _cache = new List<T>();
        private bool _isOldValuesCacheRunning;
        protected int OldValuesLength => Count < Capacity || Count > MaxLength ? 0 : MaxLength - Count;
        protected IBarsService Bars { get; set; }

        // ICache<T> implementation
        public bool IsFull => Count > MaxLength;
        public T CurrentValue { get; protected set; }
        public T LastValue { get; protected set; }

        public void RemoveLastElement()
        {
            if (IsValidIndex(0))
            {
                RemoveAt(0);
                CurrentValue = LastValue;
            }
            else
                throw new Exception("The cache is empty.");

            //if (_isOldValuesCacheRunning && Count > Capacity)
            //    RemoveAt(0);
            //else
            //    throw new Exception("The cache cannot restore the last element removed because the old values cache is empty.");
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

        // ISeries<T> implementation
        public int Count => _cache.Count;
        public T this[int index]
        {
            get => IsValidIndex(index) ? _cache[index] : throw new ArgumentOutOfRangeException(nameof(index));
            protected set
            {
                if (!IsValidIndex(index)) throw new ArgumentOutOfRangeException(nameof(index));
                if (index != 0) throw new ArgumentOutOfRangeException("The unique element thats can be updated is the element of the index 0.");
                _cache[index] = value;
                LastValue = CurrentValue;
                CurrentValue = value;
                OnElementUpdated(LastValue, CurrentValue);
            }
        }
        public T GetValueAt(int valueIndex) => IsValidIndex(Count - valueIndex) ? _cache[Count - valueIndex] : default;
        public bool IsValidDataPoint(int valuesAgo) => IsValidIndex(valuesAgo) && _cache[valuesAgo].Equals(default(T));
        public bool IsValidDataPointAt(int valueIndex) => IsValidDataPoint(Count - valueIndex);

        // IEnumerable implementation
        public IEnumerator<T> GetEnumerator() => _cache.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => _cache.GetEnumerator();

        // Private and protected methods
        //protected abstract bool IsValidValue(T value);
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
            if (!_isOldValuesCacheRunning && Count > Capacity)
                _isOldValuesCacheRunning = true;
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
        public bool IsValidIndex(int barsAgo) => _cache != null && barsAgo >= 0 && barsAgo < Count;
        public bool IsValidIndex(int barsAgo, int period)
        {
            if (!IsValidIndex(barsAgo)) return false;
            return period >= 0 && barsAgo + period < Count;
        }
        public bool IsValidIndexRange(int initialBarsAgo, int finalBarsAgo)
        {
            if (initialBarsAgo > finalBarsAgo) return false;
            return IsValidIndex(initialBarsAgo) && IsValidIndex(finalBarsAgo);
        }

        // ISeries implementation
        public int Capacity { get => Info.Capacity; protected internal set { Info.Capacity = value; } }
        public int OldValuesCapacity { get => Info.Capacity; protected internal set { Info.Capacity = value; } }
        public abstract void Add();
        public abstract void Update();

        public virtual void BarUpdate()
        {
            if (Bars.LastBarIsRemoved)
                RemoveLastElement();
            else if (Bars.IsClosed)
                Add();
            else if (Bars.IsPriceChanged || Bars.IsTick)
                Update();
        }
        public virtual void BarUpdate(IBarsService updatedBarsService) { }
        public virtual void MarketData(MarketDataEventArgs args) { }
        public virtual void MarketData(IBarsService updatedBarsService) { }
        public virtual void MarketDepth(MarketDepthEventArgs args) { }
        public virtual void MarketDepth(IBarsService updatedBarsService) { }

        protected virtual string ToTitle() => "SERIES";
        protected virtual string ToSubTitle() => null;
        protected virtual string ToDescription() => Key;

        protected override string GetHeaderString()
        {
            throw new NotImplementedException();
        }
        protected override string GetDescriptionString()
        {
            throw new NotImplementedException();
        }
        protected override string GetParentString()
        {
            throw new NotImplementedException();
        }
        protected override string GetLogString(string state)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Gets the difference between int.MaxValue and OldValuesCapacity.
        /// </summary>
        protected int MaxCapacity => int.MaxValue - OldValuesCapacity;
        /// <summary>
        /// Gets the maximum length of the series. Adds Capacity and OldValuesCapacity. 
        /// </summary>
        protected int MaxLength => Capacity + OldValuesCapacity;

        /// <summary>
        /// Create <see cref="ISeries"/> instance with specified information.
        /// <param name="info">The specified information of the series.</param>
        public BaseSeries(IBarsService barsService, ISeriesInfo info, IElementOptions options) : base(barsService?.Ninjascript, barsService?.PrintService, info, options)
        {
            Bars = barsService ?? throw new ArgumentNullException(nameof(barsService));
            Info.OldValuesCapacity = OldValuesCapacity < 1 ? Globals.SERIES_DEFAULT_OLD_VALUES_CAPACITY : OldValuesCapacity;
            Info.Capacity = Capacity <= 0 ? Globals.SERIES_DEFAULT_CAPACITY : Capacity > MaxCapacity ? MaxCapacity : Capacity;
        }

        public T GetValue(int valuesAgo) { return default; }
        public T[] ToArray(int fromValuesAgo, int numOfValues) => null;

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

    public class Series<T> : BaseSeries<T>
    {
        public Series(IBarsService barsService, ISeriesInfo info, IElementOptions options) : base(barsService, info, options)
        {
        }

        protected override SeriesType ToElementType() => SeriesType.CUSTOM;

        protected override void Configure(out bool isConfigured) => isConfigured = true;
        protected override void DataLoaded(out bool isDataLoaded) => isDataLoaded = true;

        public override void Add()
        {
            throw new NotImplementedException();
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }
    }
}
