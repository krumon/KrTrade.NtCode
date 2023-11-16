﻿using KrTrade.Nt.Core.Interfaces;
using NinjaTrader.NinjaScript;
using System;

namespace KrTrade.Nt.Services.Caches
{
    /// <summary>
    /// Represents a cache with double values.
    /// </summary>
    public abstract class SeriesCacheService : BaseCacheService<double>,
        IOnBarUpdateService,
        IOnBarClosedService,
        IOnPriceChangedService,
        IOnLastBarRemovedService
    {

        #region Constructors

        /// <summary>
        /// Create <see cref="SeriesCacheService"/> new instance with a specific capacity.
        /// </summary>
        /// <param name="ninjascript">The ninjascript object.</param>
        /// <param name="capacity">The cache capacity.</param>
        /// <param name="barsService">The bars service is necesary to updated the cache service and cannot be null.</param>
        /// <exception cref="ArgumentNullException">The bars service cannot be null.</exception>
        public SeriesCacheService(NinjaScriptBase ninjascript, BarsService barsService, int capacity) : base(ninjascript, barsService, capacity)
        {
            _barsService = barsService ?? throw new ArgumentNullException($"Error in 'LastPriceCacheService' constructor. The {nameof(barsService)} argument cannot be null."); ;
            
            if (barsService != null)
                _barsService.AddService(this);
        }

        #endregion

        #region Public properties

        /// <summary>
        /// The series where we are going to get the elements.
        /// </summary>
        public abstract ISeries<double> Series { get; }

        /// <summary>
        /// Gets the maximum value in cache.
        /// </summary>
        public double Max => GetMax(0, Count - 1);

        /// <summary>
        /// Gets the minimum value in cache.
        /// </summary>
        public double Min => GetMin(0, Count - 1);

        /// <summary>
        /// Gets the last value in cache.
        /// </summary>
        public double LastValue => GetValue(0);

        /// <summary>
        /// Gets the range of cache values.
        /// </summary>
        public double Range => GetRange(0, Count - 1);

        /// <summary>
        /// Gets the sum of cache values.
        /// </summary>
        public double Sum => GetSum(0, Count - 1);

        #endregion

        #region Implementation

        /// <summary>
        /// Gets the value of the next element we want to add to the cache.
        /// </summary>
        /// <param name="seriesDisplacement">The value of the index that corresponds to the value we want to obtain. 
        /// This index corresponds to the displacement from index 0 (the most recent value) of the series.</param>
        /// <returns>The value of the next element we want to add to the cache.</returns>
        public override double GetNextCandidateValue(int seriesDisplacement)
        {
            if (_ninjascript.BarsInProgress != _barsService.Idx || _ninjascript.CurrentBars[_barsService.Idx] < seriesDisplacement)
                return double.NaN;
            return Series[seriesDisplacement];
        }

        /// <summary>
        /// Indicates if the candidate value is valid. For any class, the no valid value are 'null'.
        /// </summary>
        /// <returns></returns>
        public override bool IsValidCandidateValue() => CandidateValue != double.NaN;

        #endregion

        #region Public methods

        /// <summary>
        /// Returns the value of the cache element at a specified index.
        /// </summary>
        /// <param name="idx">The specified idx. 0 is the most recent value.</param>
        /// <returns>The value of the cache element.</returns>
        public double GetValue(int idx)
        {
            IsValidIndex(idx);
            return this[Count - 1 - idx];
        }

        /// <summary>
        /// Returns the maximum value stored in the cache between the specified start and end indexes.
        /// </summary>
        /// <param name="initialIdx">The initial cache index from which we start calculating the maximum value. 0 is the most recent value in the cache.</param>
        /// <param name="finalIdx">The final cache index up to which we finish calculating the maximum value. 0 is the most recent value in the cache.</param>
        /// <returns>The maximum value stored in the cache between the specified start and end indexes.</returns>
        public double GetMax(int initialIdx, int finalIdx)
        {
            IsValidIndex(initialIdx, initialIdx + finalIdx);

            double value = double.MinValue;
            for (int i = Count - 1 - initialIdx; i >= Count - 1 - initialIdx - finalIdx; i--)
                value = Math.Max(value, this[i]);

            return value;
        }

        /// <summary>
        /// The minimum value stored in the cache between the specified start and end indexes.
        /// </summary>
        /// <param name="initialIdx">The initial cache index from which we start calculating the minimum value. 0 is the most recent value in the cache.</param>
        /// <param name="finalIdx">The final cache index up to which we finish calculating the minimum value. 0 is the most recent value in the cache.</param>
        /// <returns>The minimum value stored in the cache between the specified start and end indexes.</returns>
        public double GetMin(int initialIdx, int finalIdx)
        {
            IsValidIndex(initialIdx, initialIdx + finalIdx);

            double value = double.MaxValue;

            for (int i = Count - 1 - initialIdx; i >= Count - (initialIdx + finalIdx); i--)
            {
                value = Math.Min(value, this[i]);
            }
            return value;
        }

        /// <summary>
        /// Returns the sum of values stored in the cache between the specified start and end indexes.
        /// </summary>
        /// <param name="initialIdx">The initial cache index from which we start calculating the sum. 0 is the most recent value in the cache.</param>
        /// <param name="finalIdx">The final cache index up to which we finish calculating the sum. 0 is the most recent value in the cache.</param>
        /// <returns>The sum of the cache elements.</returns>
        public double GetSum(int initialIdx, int finalIdx)
        {
            IsValidIndex(initialIdx, initialIdx + finalIdx);

            double sum = 0;

            for (int i = Count - 1 - initialIdx; i >= Count - (initialIdx + finalIdx); i--)
            {
                sum += this[i];
            }
            return sum;
        }

        /// <summary>
        /// Returns the range value between the specified start and end indexes.
        /// </summary>
        /// <param name="initialIdx">The initial cache index from which we start calculating the range. 0 is the most recent value in the cache.</param>
        /// <param name="finalIdx">The final cache index up to which we finish calculating the range. 0 is the most recent value in the cache.</param>
        /// <returns>The range value (the difference between the maximum value and the minimum value).</returns>
        public double GetRange(int initialIdx, int finalIdx)
        {
            return GetMax(initialIdx, finalIdx) - GetMin(initialIdx, finalIdx);
        }

        #endregion

        #region Private methods

        private bool IsValidIndex(int idx)
        {
            if (idx < 0 || idx >= Count)
                throw new ArgumentOutOfRangeException(nameof(idx));

            return true;
        }
        private bool IsValidIndex(int startIdx, int finalIdx)
        {
            if (startIdx > finalIdx)
                throw new ArgumentException(string.Format("The {0} cannot be mayor than {1}.", nameof(startIdx), nameof(finalIdx)));

            if (IsValidIndex(startIdx) && IsValidIndex(finalIdx))
                return true;

            return false;
        }

        #endregion

    }
}
