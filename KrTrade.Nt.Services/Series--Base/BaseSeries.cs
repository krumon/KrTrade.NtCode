using KrTrade.Nt.Core.Caches;
using KrTrade.Nt.Core.Info;

namespace KrTrade.Nt.Services.Series
{
    public abstract class BaseSeries<TElement> : Cache<TElement>, ISeries<TElement>
    {
        protected int BarsIndex { get; set; }
        public int Period { get; internal set; }

        /// <summary>
        /// Create <see cref="BaseSeries{TElement}"/> default instance with specified parameters.
        /// </summary>
        /// <param name="period">The specified period to calculate values in cache.</param>
        /// <param name="capacity">The <see cref="ICache{T}"/> capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
        /// <param name="oldValuesCapacity">The length of the old values cache. This values are at the end of cache.</param>
        /// <param name="barsIndex">The index of the 'NinjaScript.Series' necesary for gets the cache elements.</param>
        protected BaseSeries(int period, int capacity, int oldValuesCapacity, int barsIndex) : base(capacity, oldValuesCapacity)
        {
            Period = period < 1 ? 1 : period > Capacity ? Capacity : period;
            BarsIndex = barsIndex < 0 ? 0 : barsIndex;
        }

        // TODO: ****************** ARREGLAR!!!!!!!
        public string Name { get; set; }
        public bool Equals(IHasKey other) => other is IHasKey key && Key == key.Key;

        public abstract string Key { get; }
        public IInfo Info { get; set; }

        public abstract bool Add();
        public abstract bool Update();

        public void Configure(IBarsService barsService)
        {
            throw new System.NotImplementedException();
        }

        public void DataLoaded(IBarsService barsService)
        {
            throw new System.NotImplementedException();
        }

        public string GetKey()
        {
            throw new System.NotImplementedException();
        }

    }
}
