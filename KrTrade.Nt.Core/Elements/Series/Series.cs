using KrTrade.Nt.Core.Data;
using NinjaTrader.NinjaScript;
using System;
using System.Collections;
using System.Collections.Generic;

namespace KrTrade.Nt.Core.Elements
{
    /// <summary>
    /// Base class for all series.
    /// </summary>
    public abstract class Series : BaseElement<ISeriesInfo>, ISeries
    {

        public const int DEFAULT_CAPACITY = 2; // The current and last values.
        public const int DEFAULT_OLD_VALUES_CAPACITY = 1; // The last element removed

        // ISeries implementation
        new public SeriesType Type { get => base.Type.ToSeriesType(); }
        public int Capacity { get => Info.Capacity; protected internal set { Info.Capacity = value; } }
        public int OldValuesCapacity { get => Info.Capacity; protected internal set { Info.Capacity = value; } }
        public bool Equals(ISeries other) => Equals(other as IElement);

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
        //protected string ToString(SeriesType seriesType)
        //{
        //    switch (seriesType)
        //    {
        //        //case ServiceType.BARS_SERVICES_COLLECTION:
        //        //    return "BARS";
        //        default:
        //            return seriesType.ToString();
        //    }
        //}

        ////protected virtual string ToString(int barsAgo) => this[barsAgo].ToString();

        //public string ToString(int tabOrder)
        //{
        //    StringBuilder sb = new StringBuilder();

        //    string tab = string.Empty;
        //    if (tabOrder > 0)
        //        for (int i = 0; i < tabOrder; i++)
        //            tab += "\t";

        //    sb.Append(tab);
        //    sb.Append(ToString());

        //    return sb.ToString();
        //}

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
        public Series(NinjaScriptBase ninjascript, ISeriesInfo info) : base(ninjascript,info)
        {
            Info.OldValuesCapacity = OldValuesCapacity < 1 ? DEFAULT_OLD_VALUES_CAPACITY : OldValuesCapacity;
            Info.Capacity = Capacity <= 0 ? DEFAULT_CAPACITY : Capacity > MaxCapacity ? MaxCapacity : Capacity;
        }

        public object this[int index] => null;
        public abstract int Count {  get; }
        public abstract bool IsFull { get; }
        public abstract void RemoveLastElement();
        public abstract void Reset();
        public abstract void Dispose();
        public object CurrentValue { get; protected set; }
        public object LastValue { get ; protected set; }

        public object GetValue(int valuesAgo) { return null; }
        public object[] ToArray(int fromValuesAgo, int numOfValues) => null;

    }

    /// <summary>
    /// Generic class for all series.
    /// </summary>
    /// <typeparam name="T">The type of series items.</typeparam>
    public class Series<T> : Series, ISeries<T>,
        IEnumerable<T>,
        IEnumerable
    {

        private IList<T> _cache = new List<T>();
        private bool _isOldValuesCacheRunning;
        protected int OldValuesLength => Count < Capacity || Count > MaxLength ? 0 : MaxLength - Count;

        // ICache<T> implementation
        public override bool IsFull => Count > MaxLength;
        new public T CurrentValue { get => (T)base.CurrentValue; protected set => base.CurrentValue = value; }
        new public T LastValue { get => (T)base.LastValue; protected set => base.LastValue = value; }

        public override void RemoveLastElement()
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
        public override void Reset()
        {
            _cache?.Clear();
            CurrentValue = default;
            LastValue = default;
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
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        
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
            if(!_isOldValuesCacheRunning && Count > Capacity)
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

        /// <summary>
        /// Create <see cref="ISeries{T}"/> instance with specified information.
        /// </summary>
        /// <param name="ninjascript">The 'NinjaTrader.NinjaScript' necesary for any element.</param>
        /// <param name="info">The specified information of the series.</param>
        public Series(NinjaScriptBase ninjascript, ISeriesInfo info) : base(ninjascript,info)
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
