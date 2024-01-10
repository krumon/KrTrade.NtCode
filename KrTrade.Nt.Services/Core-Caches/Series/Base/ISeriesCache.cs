using KrTrade.Nt.Core.Caches;
using NinjaTrader.Core.FloatingPoint;

namespace KrTrade.Nt.Services
{
    public interface ISeriesCache : ICache<double>
    {

        /// <summary>
        /// Gets the maximum value in cache.
        /// </summary>
        double Max { get; }

        /// <summary>
        /// Gets the minimum value in cache.
        /// </summary>
        double Min { get; }

        /// <summary>
        /// Gets the sum of cache values.
        /// </summary>
        double Sum { get; }

        /// <summary>
        /// Gets the average of cache values.
        /// </summary>
        double Avg {get;}

        /// <summary>
        /// Gets the standard desviation of cache values.
        /// </summary>
        double StdDev {get;}

        /// <summary>
        /// Gets the quartils of cache values.
        /// </summary>
        double[] Quartils {get;}

        /// <summary>
        /// Gets the range interquartil of cache values.
        /// </summary>
        double InterquartilRange {get;}

        /// <summary>
        /// Gets the range of cache values.
        /// </summary>
        double Range { get; }

        /// <summary>
        /// Returns the maximum value stored in the cache between the specified start and end indexes.
        /// </summary>
        /// <param name="initialIdx">The initial cache index from which we start calculating the maximum value. 0 is the most recent value in the cache.</param>
        /// <param name="numberOfElements">The number of elements to to calculate the minimum value.</param>
        /// <returns>The maximum value stored in the cache between the specified start and the specified number of bars.</returns>
        double GetMax(int initialIdx, int numberOfElements);

        /// <summary>
        /// The minimum value stored in the cache between the specified start and end indexes.
        /// </summary>
        /// <param name="initialIdx">The initial cache index from which we start calculating the minimum value. 0 is the most recent value in the cache.</param>
        /// <param name="numberOfElements">The number of elements to to calculate the minimum value.</param>
        /// <returns>The minimum value stored in the cache between the specified start and the specified number of bars.</returns>
        double GetMin(int initialIdx, int numberOfElements);

        /// <summary>
        /// Returns the sum of values stored in the cache between the specified start and end indexes.
        /// </summary>
        /// <param name="initialIdx">The initial cache index from which we start calculating the sum. 0 is the most recent value in the cache.</param>
        /// <param name="numberOfElements">The number of elements to to calculate the minimum value.</param>
        /// <returns>The sum of cache elements between the specified start and the specified number of bars.</returns>
        double GetSum(int initialIdx, int numberOfElements);

        /// <summary>
        /// Returns the average of values stored in the cache between the specified start and end indexes.
        /// </summary>
        /// <param name="initialIdx">The initial cache index from which we start calculating the sum. 0 is the most recent value in the cache.</param>
        /// <param name="numberOfElements">The number of elements to to calculate the minimum value.</param>
        /// <returns>The average of cache elements between the specified start and the specified number of bars.</returns>
        double GetAvg(int initialIdx, int numberOfElements);

        /// <summary>
        /// Returns the standard desviation of values stored in the cache between the specified start and end indexes.
        /// </summary>
        /// <param name="initialIdx">The initial cache index from which we start calculating the sum. 0 is the most recent value in the cache.</param>
        /// <param name="numberOfElements">The number of elements to to calculate the minimum value.</param>
        /// <returns>The standard desviation of cache elements between the specified start and the specified number of bars.</returns>
        double GetStdDev(int initialIdx, int numberOfElements);

        /// <summary>
        /// Returns the first, second or third quartil of values stored in the cache between the specified start and end indexes.
        /// </summary>
        /// <param name="numberOfQuartil">The number of quartil to gets.</param>
        /// <param name="initialIdx">The initial cache index from which we start calculating the sum. 0 is the most recent value in the cache.</param>
        /// <param name="numberOfElements">The number of elements to to calculate the minimum value.</param>
        /// <returns>The first, second or third quartil of cache elements between the specified start and the specified number of bars.</returns>
        double GetQuartil(int numberOfQuartil, int initialIdx, int numberOfElements);

        /// <summary>
        /// Returns the quartils of values stored in the cache between the specified start and end indexes.
        /// </summary>
        /// <param name="initialIdx">The initial cache index from which we start calculating the sum. 0 is the most recent value in the cache.</param>
        /// <param name="numberOfElements">The number of elements to to calculate the minimum value.</param>
        /// <returns>The quartils of cache elements between the specified start and the specified number of bars.</returns>
        double[] GetQuartils(int initialIdx, int numberOfElements);

        /// <summary>
        /// Returns the range value between the specified start and end indexes.
        /// </summary>
        /// <param name="initialIdx">The initial cache index from which we start calculating the range. 0 is the most recent value in the cache.</param>
        /// <param name="numberOfElements">The number of elements to to calculate the minimum value.</param>
        /// <returns>The range value (the difference between the maximum value and the minimum value).</returns>
        double GetRange(int initialIdx, int numberOfElements);

        /// <summary>
        /// Returns the swing high value if exists, otherwise returns -1.0.
        /// </summary>
        /// <param name="initialIdx">The initial cache index from which we start calculating the range. 0 is the most recent value in the cache.</param>
        /// <param name="strength">The swing strength. Number of bars before and after the swing bar.</param>
        /// <returns>The swing high value if exists, otherwise returns -1.0.</returns>
        double GetSwingHigh(int initialIdx, int strength);

        /// <summary>
        /// Returns the swing low value if exists, otherwise returns -1.0.
        /// </summary>
        /// <param name="initialIdx">The initial cache index from which we start calculating the range. 0 is the most recent value in the cache.</param>
        /// <param name="strength">The swing strength. Number of bars before and after the swing bar.</param>
        /// <returns>The swing low value if exists, otherwise returns -1.0.</returns>
        double GetSwingLow(int initialIdx, int strength);

    }
}
