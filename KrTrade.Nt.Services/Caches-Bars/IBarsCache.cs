using NinjaTrader.NinjaScript;

namespace KrTrade.Nt.Services
{
    public interface IBarsCache : INinjaCache<double,NinjaScriptBase>, IBarsSeriesCache 
    {

        ///// <summary>
        ///// Returns the maximum value stored in the cache between the specified start and end indexes.
        ///// </summary>
        ///// <param name="initialIdx">The initial cache index from which we start calculating the maximum value. 0 is the most recent value in the cache.</param>
        ///// <param name="numberOfBars">The number of elements to calculate the minimum value.</param>
        ///// <param name="seriesType">the type of the 'NinjaScript.ISeries' used to calculated the value returns by this method. Default value is 'SeriesType.Close'.</param>
        ///// <returns>The maximum value stored in the cache between the specified start and the specified number of bars.</returns>
        //double GetMax(int initialIdx, int numberOfBars, SeriesType seriesType = SeriesType.High);

        ///// <summary>
        ///// The minimum value stored in the cache between the specified start and end indexes.
        ///// </summary>
        ///// <param name="initialIdx">The initial cache index from which we start calculating the minimum value. 0 is the most recent value in the cache.</param>
        ///// <param name="numberOfBars">The number of elements to calculate the minimum value.</param>
        ///// <param name="seriesType">the type of the 'NinjaScript.ISeries' used to calculated the value returns by this method. Default value is 'SeriesType.Close'.</param>
        ///// <returns>The minimum value stored in the cache between the specified start and the specified number of bars.</returns>
        //double GetMin(int initialIdx, int numberOfBars, SeriesType seriesType = SeriesType.Low);

        ///// <summary>
        ///// Returns the sum of values stored in the cache between the specified start and end indexes.
        ///// </summary>
        ///// <param name="initialIdx">The initial cache index from which we start calculating the sum. 0 is the most recent value in the cache.</param>
        ///// <param name="numberOfBars">The number of elements to calculate the minimum value.</param>
        ///// <param name="seriesType">the type of the 'NinjaScript.ISeries' used to calculated the value returns by this method. Default value is 'SeriesType.Close'.</param>
        ///// <returns>The sum of cache elements between the specified start and the specified number of bars.</returns>
        //double GetSum(int initialIdx, int numberOfBars, SeriesType seriesType = SeriesType.Close);

        ///// <summary>
        ///// Returns the average of values stored in the cache between the specified start and end indexes.
        ///// </summary>
        ///// <param name="initialIdx">The initial cache index from which we start calculating the sum. 0 is the most recent value in the cache.</param>
        ///// <param name="numberOfBars">The number of elements to calculate the minimum value.</param>
        ///// <param name="seriesType">the type of the 'NinjaScript.ISeries' used to calculated the value returns by this method. Default value is 'SeriesType.Close'.</param>
        ///// <returns>The average of cache elements between the specified start and the specified number of bars.</returns>
        //double GetAvg(int initialIdx, int numberOfBars, SeriesType seriesType = SeriesType.Close);

        ///// <summary>
        ///// Returns the standard desviation of values stored in the cache between the specified start and end indexes.
        ///// </summary>
        ///// <param name="initialIdx">The initial cache index from which we start calculating the sum. 0 is the most recent value in the cache.</param>
        ///// <param name="numberOfBars">The number of elements to calculate the minimum value.</param>
        ///// <param name="seriesType">the type of the 'NinjaScript.ISeries' used to calculated the value returns by this method. Default value is 'SeriesType.Close'.</param>
        ///// <returns>The standard desviation of cache elements between the specified start and the specified number of bars.</returns>
        //double GetStdDev(int initialIdx, int numberOfBars, SeriesType seriesType = SeriesType.Close);

        ///// <summary>
        ///// Returns the first, second or third quartil of values stored in the cache between the specified start and end indexes.
        ///// </summary>
        ///// <param name="numberOfQuartil">The number of quartil to gets.</param>
        ///// <param name="initialIdx">The initial cache index from which we start calculating the sum. 0 is the most recent value in the cache.</param>
        ///// <param name="numberOfBars">The number of elements to calculate the minimum value.</param>
        ///// <param name="seriesType">the type of the 'NinjaScript.ISeries' used to calculated the value returns by this method. Default value is 'SeriesType.Close'.</param>
        ///// <returns>The first, second or third quartil of cache elements between the specified start and the specified number of bars.</returns>
        //double GetQuartil(int numberOfQuartil, int initialIdx, int numberOfBars, SeriesType seriesType = SeriesType.Close);

        ///// <summary>
        ///// Returns the quartils of values stored in the cache between the specified start and end indexes.
        ///// </summary>
        ///// <param name="initialIdx">The initial cache index from which we start calculating the sum. 0 is the most recent value in the cache.</param>
        ///// <param name="numberOfBars">The number of elements to calculate the minimum value.</param>
        ///// <param name="seriesType">the type of the 'NinjaScript.ISeries' used to calculated the value returns by this method. Default value is 'SeriesType.Close'.</param>
        ///// <returns>The quartils of cache elements between the specified start and the specified number of bars.</returns>
        //double[] GetQuartils(int initialIdx, int numberOfBars, SeriesType seriesType = SeriesType.Close);

        ///// <summary>
        ///// Returns the range value between the specified start and end indexes.
        ///// </summary>
        ///// <param name="initialIdx">The initial cache index from which we start calculating the range. 0 is the most recent value in the cache.</param>
        ///// <param name="numberOfBars">The number of elements to calculate the minimum value.</param>
        ///// <param name="seriesType">the type of the 'NinjaScript.ISeries' used to calculated the value returns by this method. Default value is 'SeriesType.Close'.</param>
        ///// <returns>The range value (the difference between the maximum value and the minimum value).</returns>
        //double GetRange(int initialIdx, int numberOfBars);

        ///// <summary>
        ///// Returns the swing high value if exists, otherwise returns -1.0.
        ///// </summary>
        ///// <param name="initialIdx">The initial cache index from which we start calculating the range. 0 is the most recent value in the cache.</param>
        ///// <param name="strength">The swing strength. Number of bars before and after the swing bar.</param>
        ///// <returns>The swing high value if exists, otherwise returns -1.0.</returns>
        //double GetSwingHigh(int initialIdx, int strength);

        ///// <summary>
        ///// Returns the swing low value if exists, otherwise returns -1.0.
        ///// </summary>
        ///// <param name="initialIdx">The initial cache index from which we start calculating the range. 0 is the most recent value in the cache.</param>
        ///// <param name="strength">The swing strength. Number of bars before and after the swing bar.</param>
        ///// <returns>The swing low value if exists, otherwise returns -1.0.</returns>
        //double GetSwingLow(int initialIdx, int strength);

    }
}
