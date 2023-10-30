using KrTrade.Nt.Core.Caches;
using KrTrade.Nt.Core.Interfaces;
using KrTrade.Nt.Services.Bars;
using NinjaTrader.NinjaScript;
using System;

namespace KrTrade.Nt.Services.Caches
{
    /// <summary>
    /// Base class for all caches.
    /// </summary>
    /// <typeparam name="T">The type of cache element.</typeparam>
    public abstract class CacheService<T> : BaseCache<T>, 
        IOnBarUpdateService, 
        IOnBarClosedService, 
        IOnPriceChangedService,
        IOnLastBarRemovedService
    {

        #region Private members

        protected readonly NinjaScriptBase _ninjascript;
        protected T _currentValue;

        #endregion

        #region Constructors

        /// <summary>
        /// Create <see cref="BaseCache{T}"/> new instance with a specific capacity.
        /// </summary>
        /// <param name="ninjascript">The ninjascript object.</param>
        /// <param name="capacity">The cache capacity.</param>
        /// <exception cref="ArgumentNullException">The bars service cannot be null.</exception>
        public CacheService(NinjaScriptBase ninjascript, BarsService barsService, int capacity) : base(capacity)
        {
            _ninjascript = ninjascript ?? throw new ArgumentNullException(nameof(ninjascript));

            if (barsService == null)
                throw new ArgumentNullException(nameof(barsService));

            barsService.AddService(this);
        }

        #endregion

        #region Public methods


        public void OnBarUpdate()
        {
            _currentValue = GetNextCacheValue();
        }
        public void OnLastBarRemoved()
        {
            Clear();
            for (int barsBack = Math.Min(_ninjascript.CurrentBar, Capacity) - 1; barsBack >= 0; barsBack--)
                Add(GetNextCacheValue(barsBack));
        }
        public void OnBarClosed()
        {
            if (_currentValue is double value && value == double.NaN)
                return;

            Add(_currentValue);
        }
        public void OnPriceChanged()
        {
            if (CheckReplacementConditions(_currentValue))
                Replace(_currentValue);
        }

        /// <summary>
        /// Filled the cache with last values in ninjascript.
        /// </summary>
        public void Fill()
        {
            for (int barsBack = Math.Min(_ninjascript.CurrentBar, Capacity) - 1; barsBack >= 0; barsBack--)
                Add(GetNextCacheValue(barsBack));
        }

        #endregion

    }
}
