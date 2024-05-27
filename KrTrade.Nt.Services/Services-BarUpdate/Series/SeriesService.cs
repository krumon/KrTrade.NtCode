using KrTrade.Nt.Core.Series;
using KrTrade.Nt.Core.Services;
using System;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Base class for all caches.
    /// </summary>
    public abstract class BaseSeriesService<TSeries> : BarUpdateService<SeriesServiceInfo,SeriesServiceOptions>, ISeriesService<TSeries>
        where TSeries : ISeries
    {
        protected TSeries Series;
        //protected BaseSeriesInfo Info;

        public object this[int index] => Series[index];
        public int Capacity => Series.Capacity;
        public int OldValuesCapacity => Series.OldValuesCapacity;
        public int Period => 20; // Series.Period;
        public int Count => Series.Count;
        public bool IsFull => Series.IsFull;

        /// <summary>
        /// Create <see cref="BaseSeriesService{TSeries}"/> instance with specified options.
        /// </summary>
        /// <param name="barsService">The <see cref="IBarsService"/> necesary to construct <see cref="BaseSeriesService{TCache,TOptions}"/>.</param>
        /// <param name="options">The specified options to configure the service.</param>
        /// <exception cref="ArgumentNullException">The <see cref="IBarsService"/> cannot be null.</exception>
        public BaseSeriesService(IBarsService barsService, SeriesServiceInfo info, SeriesServiceOptions options) : base(barsService,info,options)
        {
            //Series = (TSeries)Bars.GetSeries(seriesInfo);
            if (Series == null)
                Bars.PrintService?.LogWarning($"{Series.Name} series could NOT be created.");
            else
            {
                Bars.PrintService?.LogTrace($"{Series.Name} series has been created.");
            }

        }

        //public override string Name => Info.Type.ToString();
        //public string Key => Info.GetKey();

        internal override void Configure(out bool isConfigured)
        {
            // isConfigured = Series.Configure(Bars);
            isConfigured = true;
        }
        internal override void DataLoaded(out bool isDataLoaded)
        {
            // isDataLoaded = Series.DataLoaded(Bars,options);
            isDataLoaded = true;
        }
        public override void BarUpdate()
        {
            //if (Bars.LastBarIsRemoved)
            //    Series.RemoveLastElement();
            //else if (Bars.IsClosed)
            //    Series.Add();
            //else if (Bars.IsPriceChanged)
            //    Series.Update();
            //else if (Bars.IsTick)
            //    Series.Update();
        }
        public override void BarUpdate(IBarsService updatedSeries)
        {
            // ToDo: Revisar cuando desarrolle las multiseries.
            BarUpdate();
        }

    }

    public class SeriesService<TSeries> : BaseSeriesService<TSeries>
        where TSeries : ISeries
    {
        public SeriesService(IBarsService barsService, SeriesServiceInfo info, SeriesServiceOptions options) : base(barsService, info, options)
        {
        }

        public override string ToString(int tabOrder)
        {
            throw new NotImplementedException();
        }

        protected override string GetKey() => Series.Key;

        protected override ServiceType GetServiceType()
        {
            throw new NotImplementedException();
        }
    }


    ///// <summary>
    ///// Base class for all caches.
    ///// </summary>
    //public class SeriesService<TSeries> : BarUpdateService<SeriesOptions>, ISeriesService<TSeries>
    //    where TSeries : ISeries
    //{
    //    #region Private members

    //    protected TSeries Series;
    //    private readonly object _input1;
    //    private readonly object _input2;

    //    #endregion

    //    #region Public properties

    //    public object this[int index] => Series[index];
    //    public int Capacity => Series.Capacity;
    //    public int OldValuesCapacity => Series.OldValuesCapacity;
    //    public int Period => Series.Period;
    //    public int Count => Series.Count;
    //    public bool IsFull => Series.IsFull;

    //    #endregion

    //    #region Constructors

    //    /// <summary>
    //    /// Create <see cref="SeriesService{TCache}"/> instance with <see cref="IBarsManager"/> options.
    //    /// </summary>
    //    /// <param name="barsService">The <see cref="IBarsService"/> necesary for updated the series service.</param>
    //    /// <exception cref="ArgumentNullException">The <see cref="IBarSeries"/> cannot be null.</exception>
    //    public SeriesService(IBarsService barsService) : this(barsService, null, null, barsService.CacheCapacity, barsService.RemovedCacheCapacity, barsService.Index)
    //    {
    //    }

    //    /// <summary>
    //    /// Create <see cref="SeriesService{TCache}"/> instance with <see cref="IBarsService"/> options.
    //    /// </summary>
    //    /// <param name="barsService">The <see cref="IBarsManager"/> necesary for updated <see cref="BaseCacheService"/>.</param>
    //    /// <param name="input">The input series necesary for calculate the new elements of the <see cref="SeriesService{TCache}"/></param>
    //    /// <exception cref="ArgumentNullException">The <see cref="IBarsService"/> cannot be null.</exception>
    //    public SeriesService(IBarsService barsService, object input) : this(barsService, input, null, barsService.CacheCapacity, barsService.RemovedCacheCapacity, barsService.Index)
    //    {
    //    }

    //    /// <summary>
    //    /// Create <see cref="SeriesService{TCache}"/> instance with <see cref="IBarsManager"/> options.
    //    /// </summary>
    //    /// <param name="barsService">The <see cref="IBarsManager"/> necesary for updated <see cref="BaseCacheService"/>.</param>
    //    /// <param name="input">The input series necesary for calculate the new elements of the <see cref="SeriesService{TCache}"/></param>
    //    /// <param name="configureOptions">The configure options of the service.</param>
    //    /// <exception cref="ArgumentNullException">The <see cref="IBarsManager"/> cannot be null.</exception>
    //    public SeriesService(IBarsService barsService, object input, Action<SeriesOptions> configureOptions) : base(barsService, configureOptions)
    //    {
    //        _input1 = input ?? barsService.Ninjascript;
    //        Series = (TSeries)GetSeries(_input1, _input2, Period, BarsIndex);
    //    }

    //    /// <summary>
    //    /// Create <see cref="SeriesService{TCache}"/> instance with <see cref="IBarsManager"/> options.
    //    /// </summary>
    //    /// <param name="barsService">The <see cref="IBarsManager"/> necesary for updated <see cref="SeriesService{TCache}"/>.</param>
    //    /// <param name="input">The input series necesary for calculate the new elements of the <see cref="SeriesService{TCache}"/></param>
    //    /// <param name="options">The specified options to configure the service.</param>
    //    /// <exception cref="ArgumentNullException">The <see cref="IBarsManager"/> cannot be null.</exception>
    //    public SeriesService(IBarsService barsService, object input, SeriesOptions options) : base(barsService, options)
    //    {
    //        _input1 = input ?? barsService.Ninjascript;
    //        //Series = (TSeries)GetSeries(_input1, _input2, Period, BarsIndex);
    //        //BarsIndex = barsService.Index;
    //    }

    //    /// <summary>
    //    /// Create <see cref="SeriesService{TCache}"/> instance with specified options.
    //    /// </summary>
    //    /// <param name="barsService">The <see cref="IBarsManager"/> necesary for updated <see cref="SeriesService{TCache}"/>.</param>
    //    /// <param name="input1">The input series necesary for calculate the new elements of the <see cref="SeriesService{TCache}"/></param>
    //    /// <param name="input2">Second input series necesary for calculate the new elements of the <see cref="SeriesService{TCache}"/></param>
    //    /// <param name="oldValuesCache">The length of the removed values cache.</param>
    //    /// <param name="barsIndex">The index of the 'NijaScript.Bars' used to get the cache elements.</param>
    //    /// <exception cref="ArgumentNullException">The <see cref="IBarsManager"/> cannot be null.</exception>
    //    public SeriesService(IBarsService barsService, object input1, object input2 = null, int capacity = Cache.DEFAULT_CAPACITY, int oldValuesCache = Cache.DEFAULT_OLD_VALUES_CAPACITY, int barsIndex = 0) : base(barsService, new SeriesOptions()
    //    {
    //        //Capacity = capacity,
    //        //OldValuesCapacity = oldValuesCache,
    //        BarsIndex = barsIndex
    //    })
    //    {
    //        _input1 = input1 ?? (input2 ?? barsService.Ninjascript);
    //        _input2 = input2;
    //        Series = (TSeries)GetSeries(_input1, _input2, Period, BarsIndex);
    //    }

    //    #endregion

    //    #region Implementation

    //    public override string Name => Series.Name;
    //    public override string Key => Series.Key;

    //    internal override void Configure(out bool isConfigured)
    //    {
    //        isConfigured = true;
    //    }
    //    internal override void DataLoaded(out bool isDataLoaded)
    //    {
    //        //Series = (TSeries)GetSeries(_input1,_input2,Period,BarsIndex);
    //        isDataLoaded = true;
    //    }
    //    public override void Update()
    //    {
    //        if (Bars.LastBarIsRemoved)
    //            Series.RemoveLastElement();
    //        else if (Bars.IsClosed)
    //            Series.Add();
    //        else if (Bars.IsPriceChanged)
    //            Series.Update();
    //        else if (Bars.IsTick)
    //            Series.Update();
    //    }
    //    public override void Update(IBarsService updatedSeries)
    //    {
    //        // ToDo: Revisar cuando desarrolle las multiseries.
    //        Update();
    //    }
    //    public override string ToLogString() => $"{Name}[0]: {this[0]}";

    //    #endregion

    //    #region Private methods

    //    private object GetSeries(object input, object input2, int period, int barsIndex)
    //    {
    //        if (input == null)
    //            if (input2 == null)
    //                input = Bars.Ninjascript;
    //            else
    //                input = input2;
    //        Type type = typeof(TSeries);
    //        if (type == typeof(BarSeries))
    //        {
    //            if (input is NinjaTrader.NinjaScript.NinjaScriptBase ninjascript)
    //                return new BarSeries(ninjascript, Capacity, OldValuesCapacity, barsIndex);
    //            return null;
    //        }
    //        if (type == typeof(AvgSeries))
    //        {
    //            //if (input is NinjaScriptBase ninjascript)
    //            //    return new AvgSeries(Bars,period);
    //            if (input is NinjaTrader.NinjaScript.ISeries<double> series)
    //                return new AvgSeries(Bars, period);
    //            return null;
    //        }
    //        if (type == typeof(HighSeries))
    //        {
    //            if (input is NinjaTrader.NinjaScript.NinjaScriptBase ninjascript)
    //                return new HighSeries(ninjascript, Capacity, OldValuesCapacity, barsIndex);
    //            //if (input is NinjaTrader.NinjaScript.ISeries<double> series)
    //            //    return new HighSeries(series, period, 1, barsIndex));
    //            return null;
    //        }
    //        if (type == typeof(CurrentBarSeries))
    //        {
    //            if (input is NinjaTrader.NinjaScript.NinjaScriptBase ninjascript)
    //                return new CurrentBarSeries(ninjascript, Capacity, OldValuesCapacity, barsIndex);
    //            return null;
    //        }
    //        if (type == typeof(MaxSeries))
    //        {
    //            //if (input is NinjaScriptBase ninjascript)
    //            //    return new MaxSeries(ninjascript, period:1, period, oldValuesCapacity, barsIndex);
    //            if (input is NinjaTrader.NinjaScript.ISeries<double> series)
    //                return new MaxSeries(Bars, period);
    //            return null;
    //        }
    //        if (type == typeof(MinSeries))
    //        {
    //            //if (input is NinjaScriptBase ninjascript)
    //            //    return new MinSeries(ninjascript, period, oldValuesCapacity, barsIndex);
    //            if (input is NinjaTrader.NinjaScript.ISeries<double> series)
    //                return new MinSeries(Bars, period);
    //            return null;
    //        }
    //        if (type == typeof(RangeSeries))
    //        {
    //            //if (input is NinjaScriptBase ninjascript)
    //            //    return new RangeSeries(ninjascript, period, oldValuesCapacity, barsIndex);
    //            return null;
    //        }
    //        if (type == typeof(SumSeries))
    //        {
    //            //if (input is NinjaScriptBase ninjascript)
    //            //    return new SumSeries(ninjascript, period, oldValuesCapacity, barsIndex);
    //            if (input is NinjaTrader.NinjaScript.ISeries<double> series)
    //                return new SumSeries(Bars, period);
    //            return null;
    //        }
    //        if (type == typeof(TickSeries))
    //        {
    //            return input is NinjaTrader.NinjaScript.NinjaScriptBase ninjascript
    //                ? new TickSeries(ninjascript, Capacity, OldValuesCapacity, barsIndex)
    //                : (object)null;
    //        }
    //        if (type == typeof(TimeSeries))
    //        {
    //            if (input is NinjaTrader.NinjaScript.NinjaScriptBase ninjascript)
    //                return new TimeSeries(ninjascript, Capacity, OldValuesCapacity, barsIndex);
    //            if (input is NinjaTrader.NinjaScript.TimeSeries series)
    //                return new TimeSeries(series, Capacity, OldValuesCapacity, barsIndex);
    //            return null;
    //        }
    //        if (type == typeof(VolumeSeries))
    //        {
    //            if (input is NinjaTrader.NinjaScript.NinjaScriptBase ninjascript)
    //                return new VolumeSeries(ninjascript, Capacity, OldValuesCapacity, barsIndex);
    //            if (input is NinjaTrader.NinjaScript.VolumeSeries series)
    //                return new VolumeSeries(series, Capacity, OldValuesCapacity, barsIndex);
    //            return null;
    //        }
    //        return null;
    //    }

    //    #endregion
    //}

}
