namespace KrTrade.Nt.Core
{
    //public interface IHasNumericCalculateValues
    //{

    //    /// <summary>
    //    /// Returns the maximum value stored in the cache between the specified start and end indexes.
    //    /// </summary>
    //    /// <param name="displacement">The displacement in cache from which we start calculating the maximum value. 0 is the most recent value in the cache.</param>
    //    /// <param name="period">The number of elements to calculate the minimum value.</param>
    //    /// <returns>The maximum value stored in the cache between the specified start and the specified number of bars.</returns>
    //    object Max(int displacement = 0, int period = 1);

    //    /// <summary>
    //    /// The minimum value stored in the cache between the specified start and end indexes.
    //    /// </summary>
    //    /// <param name="displacement">The displacement in cache from which we start calculating the maximum value. 0 is the most recent value in the cache.</param>
    //    /// <param name="period">The number of elements to calculate the minimum value.</param>
    //    /// <returns>The minimum value stored in the cache between the specified start and the specified number of bars.</returns>
    //    object Min(int displacement = 0, int period = 1);

    //    /// <summary>
    //    /// Returns the sum of values stored in the cache between the specified start and end indexes.
    //    /// </summary>
    //    /// <param name="displacement">The displacement in cache from which we start calculating the maximum value. 0 is the most recent value in the cache.</param>
    //    /// <param name="period">The number of elements to calculate the minimum value.</param>
    //    /// <returns>The sum of cache elements between the specified start and the specified number of bars.</returns>
    //    object Sum(int displacement = 0, int period = 1);

    //    /// <summary>
    //    /// Returns the average of values stored in the cache between the specified start and end indexes.
    //    /// </summary>
    //    /// <param name="displacement">The displacement in cache from which we start calculating the maximum value. 0 is the most recent value in the cache.</param>
    //    /// <param name="period">The number of elements to calculate the minimum value.</param>
    //    /// <returns>The average of cache elements between the specified start and the specified number of bars.</returns>
    //    double Avg(int displacement = 0, int period = 1);

    //    /// <summary>
    //    /// Returns the standard desviation of values stored in the cache between the specified start and end indexes.
    //    /// </summary>
    //    /// <param name="displacement">The displacement in cache from which we start calculating the maximum value. 0 is the most recent value in the cache.</param>
    //    /// <param name="period">The number of elements to calculate the minimum value.</param>
    //    /// <returns>The standard desviation of cache elements between the specified start and the specified number of bars.</returns>
    //    double StdDev(int displacement = 0, int period = 1);

    //    /// <summary>
    //    /// Returns the quartils of values stored in the cache between the specified start and end indexes.
    //    /// </summary>
    //    /// <param name="displacement">The displacement in cache from which we start calculating the maximum value. 0 is the most recent value in the cache.</param>
    //    /// <param name="period">The number of elements to calculate the minimum value.</param>
    //    /// <returns>The quartils of cache elements between the specified start and the specified number of bars.</returns>
    //    double[] Quartils(int displacement = 0, int period = 1);
    //    double Quartil(int numberOfQuartil, int displacement, int period);
    //    double InterquartilRange(int displacement = 0, int period = 1);

    //    object Range(int displacement = 0, int period = 1);
    //    object SwingHigh(int displacement = 0, int strength = 4);
    //    object SwingLow(int displacement = 0, int strength = 4);

    //}

    public interface IHasNumericCalculateValues<T> //: IHasCalculatedValues
    {
        /// <summary>
        /// Returns the maximum value stored in the cache between the specified start and end indexes.
        /// </summary>
        /// <param name="displacement">The displacement in cache from which we start calculating the maximum value. 0 is the most recent value in the cache.</param>
        /// <param name="period">The number of elements to calculate the minimum value.</param>
        /// <returns>The maximum value stored in the cache between the specified start and the specified number of bars.</returns>
        T Max(int displacement = 0, int period = 1);

        /// <summary>
        /// The minimum value stored in the cache between the specified start and end indexes.
        /// </summary>
        /// <param name="displacement">The displacement in cache from which we start calculating the maximum value. 0 is the most recent value in the cache.</param>
        /// <param name="period">The number of elements to calculate the minimum value.</param>
        /// <returns>The minimum value stored in the cache between the specified start and the specified number of bars.</returns>
        T Min(int displacement = 0, int period = 1);

        /// <summary>
        /// Returns the sum of values stored in the cache between the specified start and end indexes.
        /// </summary>
        /// <param name="displacement">The displacement in cache from which we start calculating the maximum value. 0 is the most recent value in the cache.</param>
        /// <param name="period">The number of elements to calculate the minimum value.</param>
        /// <returns>The sum of cache elements between the specified start and the specified number of bars.</returns>
        T Sum(int displacement = 0, int period = 1);


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

        T Range(int displacement = 0, int period = 1);
        T SwingHigh(int displacement = 0, int strength = 4);
        T SwingLow(int displacement = 0, int strength = 4);

    }
}
