using KrTrade.Nt.Core.Caches;
using System;

namespace KrTrade.Nt.Services
{
    public abstract class NinjaSeries<TElement,TInput> : Cache<TElement>, INinjaSeries<TElement,TInput>
    {
        protected int _barsIndex;

        public int Period { get; internal set; }

        protected NinjaSeries(object input, int period, int capacity, int oldValuesCapacity, int barsIndex) : base(capacity, oldValuesCapacity)
        {
            Period = period < 1 ? 1 : period > Capacity ? Capacity : period;
            _barsIndex = barsIndex < 0 ? 0 : barsIndex;
            Input = input != null ? GetInput(input) : throw new ArgumentNullException(nameof(input));
        }

        public abstract string Name { get; }
        public abstract bool Add();
        public abstract bool Replace();
        public TInput Input { get; protected set; }
        public abstract TInput GetInput(object input);

        ///// <summary>
        ///// Create <see cref="NinjaSeries{TElement,TInput}"/> default instance with specified properties.
        ///// </summary>
        ///// <param name="input">The object instance used to gets elements for <see cref="INinjaSeries{TElement,TInput}"/>.</param>
        ///// <param name="period">The specified period to calculate values in cache.</param>
        ///// <param name="capacity">The <see cref="ICache{T}"/> capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
        ///// <param name="barsIndex">The index of the 'NinjaScript.Series' necesary for gets the cache elements.</param>
        ///// <param name="oldValuesCapacity">The length of the old values cache. This values are at the end of cache.</param>
        ///// <exception cref="ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        //protected NinjaSeries(TInput input, int period = 1, int capacity = DEFAULT_CAPACITY, int oldValuesCapacity = DEFAULT_OLD_VALUES_CAPACITY, int barsIndex = 0) : base(capacity,oldValuesCapacity)
        //{
        //    Input = input != null ? GetInput(input) : throw new ArgumentNullException(nameof(input));
        //    BarsIndex = barsIndex < 0 ? 0 : barsIndex;
        //    Period = period < 1 ? 1 : period > Capacity ? Capacity : period;
        //}


        //public abstract TElement Max(int displacement = 0, int period = 1);
        //public abstract TElement Min(int displacement = 0, int period = 1);
        //public abstract TElement Sum(int displacement = 0, int period = 1);
        //public abstract double Avg(int displacement = 0, int period = 1);
        //public abstract double StdDev(int displacement = 0, int period = 1);
        //public abstract double[] Quartils(int displacement = 0, int period = 1);
        //public abstract double Quartil(int numberOfQuartil, int displacement, int period);
        //public abstract double InterquartilRange(int displacement = 0, int period = 1);
        //public abstract TElement Range(int displacement = 0, int period = 1);
        //public abstract TElement SwingHigh(int displacement = 0, int strength = 4);
        //public abstract TElement SwingLow(int displacement = 0, int strength = 4);
    }
}
