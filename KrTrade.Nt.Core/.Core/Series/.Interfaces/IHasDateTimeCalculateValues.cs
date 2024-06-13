using System;

namespace KrTrade.Nt.Core
{
    public interface IHasDateTimeCalculateValues
    {

        /// <summary>
        /// Returns the maximum time stored in the cache between the specified start and end indexes.
        /// </summary>
        /// <param name="displacement">The displacement in cache from which we start calculating the maximum value. 0 is the most recent value in the cache.</param>
        /// <param name="period">The number of elements to calculate the minimum value.</param>
        /// <returns>The maximum time stored in the cache between the specified start and the specified number of bars.</returns>
        DateTime Max(int displacement = 0, int period = 1);

        /// <summary>
        /// The minimum time stored in the cache between the specified start and end indexes.
        /// </summary>
        /// <param name="displacement">The displacement in cache from which we start calculating the maximum value. 0 is the most recent value in the cache.</param>
        /// <param name="period">The number of elements to calculate the minimum value.</param>
        /// <returns>The minimum time stored in the cache between the specified start and the specified number of bars.</returns>
        DateTime Min(int displacement = 0, int period = 1);

        /// <summary>
        /// Returns the total time stored in the cache between the specified start and end indexes.
        /// </summary>
        /// <param name="displacement">The displacement in cache from which we start calculating the maximum value. 0 is the most recent value in the cache.</param>
        /// <param name="period">The number of elements to calculate the minimum value.</param>
        /// <returns>The time between the specified start and the specified number of bars.</returns>
        TimeSpan Sum(int displacement = 0, int period = 1);

        /// <summary>
        /// Returns the interval of values stored in the cache between the specified start and end indexes.
        /// </summary>
        /// <param name="displacement">The displacement in cache from which we start calculating the maximum value. 0 is the most recent value in the cache.</param>
        /// <param name="period">The number of elements to calculate the minimum value.</param>
        /// <returns>The interval of cache elements between the specified start and the specified number of bars.</returns>
        TimeSpan Interval(int displacement = 0, int period = 1);

    }

}
