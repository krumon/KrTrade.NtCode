namespace KrTrade.Nt.Services
{
    public interface INumericCache<TElement,TInput> : IValueCache<TElement,TInput>
        where TElement : struct
    {
        /// <summary>
        /// Returns the maximum value stored in the cache between the specified start and end indexes.
        /// </summary>
        /// <param name="displacement">The displacement in cache from which we start calculating the maximum value. 0 is the most recent value in the cache.</param>
        /// <param name="period">The number of elements to calculate the minimum value.</param>
        /// <returns>The maximum value stored in the cache between the specified start and the specified number of bars.</returns>
        TElement Max(int displacement = 0, int period = 1);

        /// <summary>
        /// The minimum value stored in the cache between the specified start and end indexes.
        /// </summary>
        /// <param name="displacement">The displacement in cache from which we start calculating the maximum value. 0 is the most recent value in the cache.</param>
        /// <param name="period">The number of elements to calculate the minimum value.</param>
        /// <returns>The minimum value stored in the cache between the specified start and the specified number of bars.</returns>
        TElement Min(int displacement = 0, int period = 1);

        /// <summary>
        /// Returns the sum of values stored in the cache between the specified start and end indexes.
        /// </summary>
        /// <param name="displacement">The displacement in cache from which we start calculating the maximum value. 0 is the most recent value in the cache.</param>
        /// <param name="period">The number of elements to calculate the minimum value.</param>
        /// <returns>The sum of cache elements between the specified start and the specified number of bars.</returns>
        TElement Sum(int displacement = 0, int period = 1);

        /// <summary>
        /// Returns the average of values stored in the cache between the specified start and end indexes.
        /// </summary>
        /// <param name="displacement">The displacement in cache from which we start calculating the maximum value. 0 is the most recent value in the cache.</param>
        /// <param name="period">The number of elements to calculate the minimum value.</param>
        /// <returns>The average of cache elements between the specified start and the specified number of bars.</returns>
        double Avg(int displacement = 0, int period = 1);

        /// <summary>
        /// Returns the standard desviation of values stored in the cache between the specified start and end indexes.
        /// </summary>
        /// <param name="displacement">The displacement in cache from which we start calculating the maximum value. 0 is the most recent value in the cache.</param>
        /// <param name="period">The number of elements to calculate the minimum value.</param>
        /// <returns>The standard desviation of cache elements between the specified start and the specified number of bars.</returns>
        double StdDev(int displacement = 0, int period = 1);

        /// <summary>
        /// Returns the quartils of values stored in the cache between the specified start and end indexes.
        /// </summary>
        /// <param name="displacement">The displacement in cache from which we start calculating the maximum value. 0 is the most recent value in the cache.</param>
        /// <param name="period">The number of elements to calculate the minimum value.</param>
        /// <returns>The quartils of cache elements between the specified start and the specified number of bars.</returns>
        double[] Quartils(int displacement = 0, int period = 1);
        double Quartil(int numberOfQuartil, int displacement, int period);
        double InterquartilRange(int displacement = 0, int period = 1);

        TElement Range(int displacement = 0, int period = 1);
        TElement SwingHigh(int displacement = 0, int strength = 4);
        TElement SwingLow(int displacement = 0, int strength = 4);
    }
}
