namespace KrTrade.Nt.Services
{
    public interface IBarsCacheService : IBarsSeriesCache, IBarUpdateService
    {

        ///// <summary>
        ///// 
        ///// </summary>
        //IndexCache Index { get; }

        ///// <summary>
        ///// Gets the time series.
        ///// </summary>
        //TimeCache Time { get; }

        ///// <summary>
        ///// Gets the open series.
        ///// </summary>
        //DoubleCache<NinjaScriptBase> Open { get; }

        ///// <summary>
        ///// Gets the high series.
        ///// </summary>
        //HighCache High { get; }

        ///// <summary>
        ///// Gets the low series.
        ///// </summary>
        //DoubleCache<NinjaScriptBase> Low { get; }

        ///// <summary>
        ///// Gets the close series.
        ///// </summary>
        //DoubleCache<NinjaScriptBase> Close { get; }

        ///// <summary>
        ///// Gets the volume series.
        ///// </summary>
        //VolumeCache Volume { get; }

        ///// <summary>
        ///// Gets the tick count series.
        ///// </summary>
        //TicksCache Ticks { get; }

        ///// <summary>
        ///// Returns the <see cref="Bar"/> of the specified <paramref name="barsAgo"/>.
        ///// </summary>
        ///// <param name="barsAgo">The index specified. 0 is the most recent value in the cache.</param>
        ///// <returns>The <see cref="Bar"/> value result from bars stored in the cache between the <paramref name="barsAgo"/> to and the <paramref name="period"/> specified.</returns>
        //Bar GetBar(int barsAgo);

        ///// <summary>
        ///// Returns the <see cref="Bar"/> result from <paramref name="barsAgo"/> to <paramref name="period"/> specified.
        ///// </summary>
        ///// <param name="barsAgo">The initial index from most recent bar. 0 is the most recent value in the cache.</param>
        ///// <param name="period">The number of bars to calculate the <see cref="Bar"/> value.</param>
        ///// <returns>The <see cref="Bar"/> value result from bars stored in the cache between the <paramref name="barsAgo"/> to and the <paramref name="period"/> specified.</returns>
        //Bar GetBar(int barsAgo, int period);

        ///// <summary>
        ///// Returns the <see cref="Bar"/> collection result from <paramref name="barsAgo"/> to <paramref name="period"/> specified.
        ///// </summary>
        ///// <param name="barsAgo">The initial index from most recent bar. 0 is the most recent value in the cache.</param>
        ///// <param name="period">The number of bars to calculate the <see cref="Bar"/> value.</param>
        ///// <returns>The <see cref="Bar"/> collection result from bars stored in the cache between the <paramref name="barsAgo"/> to and the <paramref name="period"/> specified.</returns>
        //IList<Bar> GetBars(int barsAgo, int period);


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
