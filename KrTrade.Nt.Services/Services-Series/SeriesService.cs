using KrTrade.Nt.Core.Caches;
using NinjaTrader.NinjaScript;
using System;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Base class for all caches.
    /// </summary>
    public class SeriesService<TSeries> : BarUpdateService<SeriesOptions>
        where TSeries : ISeries
    {
        #region Private members

        protected TSeries _series;
        private readonly object _input1;
        private readonly object _input2;
        //protected IBarUpdateCache _cache;

        #endregion

        #region Public properties

        //public TElement _series { get; private set; }

        /// <summary>
        /// Gets the element of a sepecific index.
        /// </summary>
        /// <param name="index">The specific index.</param>
        /// <returns>Series element located at specified index.</returns>
        public object this[int index] => _series[index];

        /// <summary>
        /// Represents the cache capacity.
        /// </summary>
        public int Capacity => _series.Capacity; 

        /// <summary>
        /// The number of elements that exists in cache.
        /// </summary>
        public int Count => _series.Count;

        /// <summary>
        /// The number of elements that exists in cache.
        /// </summary>
        public bool IsFull => _series.IsFull;

        #endregion

        #region Constructors

        /// <summary>
        /// Create <see cref="SeriesService{TCache}"/> instance with <see cref="IBarsManager"/> options.
        /// </summary>
        /// <param name="barsService">The <see cref="IBarsService"/> necesary for updated the series service.</param>
        /// <exception cref="ArgumentNullException">The <see cref="IBarsSeries"/> cannot be null.</exception>
        public SeriesService(IBarsService barsService) : this(barsService,null,null,barsService.CacheCapacity, barsService.RemovedCacheCapacity, barsService.Index)
        {
        }

        /// <summary>
        /// Create <see cref="SeriesService{TCache}"/> instance with <see cref="IBarsService"/> options.
        /// </summary>
        /// <param name="barsService">The <see cref="IBarsManager"/> necesary for updated <see cref="BaseCacheService"/>.</param>
        /// <param name="input">The input series necesary for calculate the new elements of the <see cref="SeriesService{TCache}"/></param>
        /// <exception cref="ArgumentNullException">The <see cref="IBarsService"/> cannot be null.</exception>
        public SeriesService(IBarsService barsService, object input) : this(barsService,null,null,barsService.CacheCapacity, barsService.RemovedCacheCapacity, barsService.Index)
        {
            _input1 = input ?? barsService.Ninjascript;
        }

        /// <summary>
        /// Create <see cref="SeriesService{TCache}"/> instance with <see cref="IBarsManager"/> options.
        /// </summary>
        /// <param name="barsService">The <see cref="IBarsManager"/> necesary for updated <see cref="BaseCacheService"/>.</param>
        /// <param name="input">The input series necesary for calculate the new elements of the <see cref="SeriesService{TCache}"/></param>
        /// <param name="configureOptions">The configure options of the service.</param>
        /// <exception cref="ArgumentNullException">The <see cref="IBarsManager"/> cannot be null.</exception>
        public SeriesService(IBarsService barsService, object input, Action<SeriesOptions> configureOptions) : base(barsService, configureOptions)
        {
            _input1 = input ?? barsService.Ninjascript;
        }

        /// <summary>
        /// Create <see cref="SeriesService{TCache}"/> instance with <see cref="IBarsManager"/> options.
        /// </summary>
        /// <param name="barsService">The <see cref="IBarsManager"/> necesary for updated <see cref="SeriesService{TCache}"/>.</param>
        /// <param name="input">The input series necesary for calculate the new elements of the <see cref="SeriesService{TCache}"/></param>
        /// <param name="options">The specified options to configure the service.</param>
        /// <exception cref="ArgumentNullException">The <see cref="IBarsManager"/> cannot be null.</exception>
        public SeriesService(IBarsService barsService, object input, SeriesOptions options) : base(barsService,options)
        {
            _input1 = input ?? barsService.Ninjascript;
        }

        /// <summary>
        /// Create <see cref="SeriesService{TCache}"/> instance with specified options.
        /// </summary>
        /// <param name="barsService">The <see cref="IBarsManager"/> necesary for updated <see cref="SeriesService{TCache}"/>.</param>
        /// <param name="input1">The input series necesary for calculate the new elements of the <see cref="SeriesService{TCache}"/></param>
        /// <param name="input2">Second input series necesary for calculate the new elements of the <see cref="SeriesService{TCache}"/></param>
        /// <param name="period">The specified period.</param>
        /// <param name="displacement">The displacement respect the input series.</param>
        /// <param name="oldValuesCache">The length of the removed values cache.</param>
        /// <param name="barsIndex">The index of the 'NijaScript.Bars' used to get the cache elements.</param>
        /// <exception cref="ArgumentNullException">The <see cref="IBarsManager"/> cannot be null.</exception>
        public SeriesService(IBarsService barsService, object input1, object input2 = null, int capacity = Cache.DEFAULT_CAPACITY, int oldValuesCache = Cache.DEFAULT_OLD_VALUES_CAPACITY, int barsIndex = 0) : base(barsService, new SeriesOptions()
        {
            Capacity = capacity,
            OldValuesCapacity = oldValuesCache,
            BarsIndex = barsIndex
        })
        {
            _input1 = input1 ?? (input2 ?? barsService.Ninjascript);
            _input2 = input2;
        }

        #endregion

        #region Implementation

        public override string Name => $"{_series}";
        internal override void Configure(out bool isConfigured)
        {
            isConfigured = true;
        }
        internal override void DataLoaded(out bool isDataLoaded)
        {
            _series = (TSeries)GetSeries(_input1,_input2, Options.Capacity,Options.OldValuesCapacity, Options.BarsIndex);
            isDataLoaded = true;
        }
        public override void Update()
        {
            if (Bars.LastBarIsRemoved)
                _series.RemoveLastElement();
            else if (Bars.IsClosed)
                _series.Add();
            else if (Bars.IsPriceChanged)
                _series.Update();
            else if (Bars.IsTick)
                _series.Update();
        }
        public override void BarsSeriesUpdate(IBarsService updatedSeries)
        {
            throw new NotImplementedException();
        }
        public override string ToLogString() => $"{Name}[0]: {this[0]}";

        #endregion

        #region Private methods

        private object GetSeries(object input, object input2, int capacity, int oldValuesCapacity, int barsIndex)
        {
            if (input == null)
                if (input2 == null)
                    input = Bars.Ninjascript;
                else
                    input = input2;
            Type type = typeof(TSeries);
            if (type == typeof(BarsSeries))
            {
                if (input is NinjaScriptBase ninjascript)
                    return new BarsSeries(ninjascript, capacity, oldValuesCapacity, barsIndex);
                return null;
            }
            if (type == typeof(AvgSeries))
            {
                if (input is NinjaScriptBase ninjascript)
                    return new AvgSeries(ninjascript, capacity, oldValuesCapacity, barsIndex);
                if (input is NinjaTrader.NinjaScript.ISeries<double> series)
                    return new AvgSeries(series, capacity, oldValuesCapacity, barsIndex);
                return null;
            }
            if (type == typeof(HighSeries))
            {
                if (input is NinjaScriptBase ninjascript)
                    return new HighSeries(ninjascript, capacity, oldValuesCapacity, barsIndex);
                if (input is NinjaTrader.NinjaScript.ISeries<double> series)
                    return new HighSeries(series, capacity, oldValuesCapacity, barsIndex);
                return null;
            }
            if (type == typeof(CurrentBarSeries))
            {
                if (input is NinjaScriptBase ninjascript)
                    return new CurrentBarSeries(ninjascript, capacity, oldValuesCapacity, barsIndex);
                return null;
            }
            if (type == typeof(MaxSeries))
            {
                if (input is NinjaScriptBase ninjascript)
                    return new MaxSeries(ninjascript, period:1, capacity, oldValuesCapacity, barsIndex);
                if (input is NinjaTrader.NinjaScript.ISeries<double> series)
                    return new MaxSeries(series, period: 1, capacity, oldValuesCapacity, barsIndex);
                return null;
            }
            if (type == typeof(MinSeries))
            {
                if (input is NinjaScriptBase ninjascript)
                    return new MinSeries(ninjascript, capacity, oldValuesCapacity, barsIndex);
                if (input is NinjaTrader.NinjaScript.ISeries<double> series)
                    return new MinSeries(series, capacity, oldValuesCapacity, barsIndex);
                return null;
            }
            if (type == typeof(RangeSeries))
            {
                if (input is NinjaScriptBase ninjascript)
                    return new RangeSeries(ninjascript, capacity, oldValuesCapacity, barsIndex);
                return null;
            }
            if (type == typeof(SumSeries))
            {
                if (input is NinjaScriptBase ninjascript)
                    return new SumSeries(ninjascript, capacity, oldValuesCapacity, barsIndex);
                if (input is NinjaTrader.NinjaScript.ISeries<double> series)
                    return new SumSeries(series, capacity, oldValuesCapacity, barsIndex);
                return null;
            }
            if (type == typeof(TickSeries))
            {
                if (input is NinjaScriptBase ninjascript)
                    return new TickSeries(ninjascript, capacity, oldValuesCapacity, barsIndex);
                return null;
            }
            if (type == typeof(TimeSeries))
            {
                if (input is NinjaScriptBase ninjascript)
                    return new TimeSeries(ninjascript, capacity, oldValuesCapacity, barsIndex);
                if (input is NinjaTrader.NinjaScript.TimeSeries series)
                    return new TimeSeries(series, capacity, oldValuesCapacity, barsIndex);
                return null;
            }
            if (type == typeof(VolumeSeries))
            {
                if (input is NinjaScriptBase ninjascript)
                    return new VolumeSeries(ninjascript, capacity, oldValuesCapacity, barsIndex);
                if (input is NinjaTrader.NinjaScript.VolumeSeries series)
                    return new VolumeSeries(series, capacity, oldValuesCapacity, barsIndex);
                return null;
            }
            return null;
        }

        #endregion
    }
}
