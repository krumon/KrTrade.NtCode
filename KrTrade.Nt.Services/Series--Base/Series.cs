using KrTrade.Nt.Core.Series;

namespace KrTrade.Nt.Services.Series
{
    public abstract class Series<T> : BaseSeries<T>, ISeries<T>
    {
        protected int BarsIndex { get; set; }
        public int Period { get; internal set; }

        /// <summary>
        /// Create <see cref="Series{TElement}"/> default instance with specified parameters.
        /// </summary>
        /// <param name="period">The specified period to calculate values in cache.</param>
        /// <param name="capacity">The <see cref="ICache{T}"/> capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
        /// <param name="oldValuesCapacity">The length of the old values cache. This values are at the end of cache.</param>
        /// <param name="barsIndex">The index of the 'NinjaScript.Series' necesary for gets the cache elements.</param>
        
        // TODO: ELIMINAR ESTE CONSTRUCTOR!!!!!!!!!

        protected Series(int period, int capacity, int oldValuesCapacity, int barsIndex) : base(new SeriesInfo())
        {
            Period = period < 1 ? 1 : period > Capacity ? Capacity : period;
            BarsIndex = barsIndex < 0 ? 0 : barsIndex;
        }

        /// <summary>
        /// Create <see cref="Series{TElement}"/> default instance with specified parameters.
        /// </summary>
        /// <param name="info">The series information necesary to construct it.</param>
        protected Series(BaseSeriesInfo info) : base(info)
        {
        }

        public virtual void Configure(IBarsService barsService) { }
        public virtual void DataLoaded(IBarsService barsService) { }

    }
}
