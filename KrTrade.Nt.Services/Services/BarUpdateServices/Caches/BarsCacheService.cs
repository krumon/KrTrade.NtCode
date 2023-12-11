using KrTrade.Nt.Core.Bars;
using NinjaTrader.NinjaScript;
using System;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Represents a cache with bar values.
    /// </summary>
    public class BarsCacheService : BaseCacheService<Bar,CacheOptions>
    {

        #region Constructors

        /// <summary>
        /// Create <see cref="BarsCacheService"/> instance and configure it.
        /// </summary>
        /// <param name="dataSeriesService">The <see cref="IDataSeriesService"/> necesary for the <see cref="BarsCacheService"/>.</param>
        /// <exception cref="ArgumentNullException">The <see cref="IDataSeriesService"/> cannot be null.</exception>
        protected BarsCacheService(IDataSeriesService dataSeriesService) : base(dataSeriesService)
        {
        }

        /// <summary>
        /// Create <see cref="BarsCacheService"/> instance and configure it.
        /// </summary>
        /// <param name="dataSeriesService">The <see cref="IDataSeriesService"/> necesary for the <see cref="BarsCacheService"/>.</param>
        /// <param name="capacity">The cache capacity.</param>
        /// <param name="displacement">The <see cref="BaseCacheService"/> displacement respect the bars collection.</param>
        /// <exception cref="ArgumentNullException">The <see cref="IDataSeriesService"/> cannot be null.</exception>
        protected BarsCacheService(IDataSeriesService dataSeriesService, int capacity, int displacement) : base(dataSeriesService, capacity, displacement)
        {
        }

        /// <summary>
        /// Create <see cref="BarsCacheService"/> instance and configure it.
        /// </summary>
        /// <param name="dataSeriesService">The <see cref="IDataSeriesService"/> necesary for the <see cref="BarsCacheService"/>.</param>
        /// <param name="configureOptions">The configure options of the service.</param>
        /// <exception cref="ArgumentNullException">The <see cref="IDataSeriesService"/> cannot be null.</exception>
        protected BarsCacheService(IDataSeriesService dataSeriesService, IConfigureOptions<CacheOptions> configureOptions) : base(dataSeriesService, configureOptions)
        {
        }

        #endregion

        #region Public properties

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
        public Bar LastValue => GetValue(0);

        /// <summary>
        /// Gets the range of cache values.
        /// </summary>
        public double Range => GetRange(0, Count - 1);

        #endregion

        #region Implementation

        /// <summary>
        /// Gets the name of the service.
        /// </summary>
        public override string Name => $"BarsCache({Capacity})";

        /// <summary>
        /// Gets the value of the next element we want to add to the cache.
        /// </summary>
        /// <param name="seriesDisplacement">The value of the index that corresponds to the value we want to obtain. 
        /// This index corresponds to the displacement from index 0 (the most recent value) of the series.</param>
        /// <returns>The value of the next element we want to add to the cache.</returns>
        public override Bar GetNextCandidateValue(int seriesDisplacement)
        {
            if (Ninjascript.BarsInProgress != DataSeriesService.Idx || Ninjascript.CurrentBars[DataSeriesService.Idx] < seriesDisplacement)
                return null;
            return new Bar()
            {
                Idx = Ninjascript.CurrentBars[DataSeriesService.Idx] - Displacement,
                Time = Ninjascript.Times[DataSeriesService.Idx][Displacement],
                Open = Ninjascript.Opens[DataSeriesService.Idx][Displacement],
                High = Ninjascript.Opens[DataSeriesService.Idx][Displacement],
                Low = Ninjascript.Opens[DataSeriesService.Idx][Displacement],
                Close = Ninjascript.Opens[DataSeriesService.Idx][Displacement],
                Volume = Ninjascript.Opens[DataSeriesService.Idx][Displacement]
            };
        }
        public override bool IsValidCandidateValue() => CandidateValue != null;
        public override bool IsBestCandidateValue() => !CandidateValue.IsEqualsTo(LastValue);

        #endregion

        #region Public methods

        /// <summary>
        /// Gets the value of the next element we want to add to the cache.
        /// </summary>
        /// <param name="series">The series where we are going to get the element.</param>
        /// <param name="seriesDisplacement">The value of the index that corresponds to the value we want to obtain. 
        /// This index corresponds to the displacement from index 0 (the most recent value) of the series.</param>
        /// <returns>The value of the next element we want to add to the cache.</returns>
        public double GetNextCandidateValue(ISeries<double> series, int seriesDisplacement)
        {
            if (Ninjascript.BarsInProgress != DataSeriesService.Idx || Ninjascript.CurrentBars[DataSeriesService.Idx] < seriesDisplacement)
                return double.NaN;
            return series[seriesDisplacement];
        }


        /// <summary>
        /// Returns the value of the cache element at a specified index.
        /// </summary>
        /// <param name="idx">The specified idx. 0 is the most recent value.</param>
        /// <returns>The value of the cache element.</returns>
        public Bar GetValue(int idx)
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
                value = Math.Max(value, this[i].High);

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
                value = Math.Min(value, this[i].Low);
            }
            return value;
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

        protected override string ToLogString()
        {
            return $"{Name}({Capacity}): Bar({Displacement})[Current]:{LastValue}, Bar({Displacement + 1})[Last]:{GetValue(Displacement + 1)}"; 
        }

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
