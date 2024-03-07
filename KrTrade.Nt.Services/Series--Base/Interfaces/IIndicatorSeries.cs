//namespace KrTrade.Nt.Services
//{
//    public interface IIndicatorSeries : INinjaSeries, IHasCalculatedValues
//    {
//    }
//    public interface IIndicatorSeries<T> : INinjaSeries<T,double>, IIndicatorSeries
//    {
//        /// <summary>
//        /// Returns the maximum value stored in the cache between the specified start and end indexes.
//        /// </summary>
//        /// <param name="displacement">The displacement in cache from which we start calculating the maximum value. 0 is the most recent value in the cache.</param>
//        /// <param name="period">The number of elements to calculate the minimum value.</param>
//        /// <returns>The maximum value stored in the cache between the specified start and the specified number of bars.</returns>
//        new T Max(int displacement = 0, int period = 1);

//        /// <summary>
//        /// The minimum value stored in the cache between the specified start and end indexes.
//        /// </summary>
//        /// <param name="displacement">The displacement in cache from which we start calculating the maximum value. 0 is the most recent value in the cache.</param>
//        /// <param name="period">The number of elements to calculate the minimum value.</param>
//        /// <returns>The minimum value stored in the cache between the specified start and the specified number of bars.</returns>
//        new T Min(int displacement = 0, int period = 1);

//        /// <summary>
//        /// Returns the sum of values stored in the cache between the specified start and end indexes.
//        /// </summary>
//        /// <param name="displacement">The displacement in cache from which we start calculating the maximum value. 0 is the most recent value in the cache.</param>
//        /// <param name="period">The number of elements to calculate the minimum value.</param>
//        /// <returns>The sum of cache elements between the specified start and the specified number of bars.</returns>
//        new T Sum(int displacement = 0, int period = 1);

//        new T Range(int displacement = 0, int period = 1);
//        new T SwingHigh(int displacement = 0, int strength = 4);
//        new T SwingLow(int displacement = 0, int strength = 4);

//    }

//}
