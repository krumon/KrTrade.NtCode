﻿using KrTrade.Nt.Core.Caches;
using System;

namespace KrTrade.Nt.Services
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
        /// <exception cref="ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        protected BaseSeries(int period, int capacity, int oldValuesCapacity, int barsIndex) : base(capacity, oldValuesCapacity)
        {
            Period = period < 1 ? 1 : period > Capacity ? Capacity : period;
            BarsIndex = barsIndex < 0 ? 0 : barsIndex;
        }

        public abstract string Name { get; }
        public abstract string Key { get; }
        public abstract bool Add();
        public abstract bool Update();

    }
}
