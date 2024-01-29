﻿using KrTrade.Nt.Core.Bars;
using KrTrade.Nt.Core.Data;
using NinjaTrader.NinjaScript;

namespace KrTrade.Nt.Services
{
    public interface IBarsCache : ISeriesCache<double,NinjaScriptBase> // ICache<Bar>, 
    {

        IndexCache Index { get; }
        TimeCache Time { get; }
        DoubleCache<NinjaScriptBase> Open { get; }
        HighCache High { get; }
        DoubleCache<NinjaScriptBase> Low { get; }
        DoubleCache<NinjaScriptBase> Close { get; }
        VolumeCache Volume { get; }
        TicksCache Ticks { get; }

        ///// <summary>
        ///// Gets the open price in cache.
        ///// </summary>
        //double Open { get; }

        ///// <summary>
        ///// Gets the high price in cache.
        ///// </summary>
        //double High { get; }

        ///// <summary>
        ///// Gets the low price in cache.
        ///// </summary>
        //double Low { get; }

        ///// <summary>
        ///// Gets the last price in cache.
        ///// </summary>
        //double Close { get; }

        ///// <summary>
        ///// Gets the total volume in cache.
        ///// </summary>
        //double Volume { get; }

        ///// <summary>
        ///// Gets the range of prices in cache.
        ///// </summary>
        //double Range { get; }

        /// <summary>
        /// Returns the <see cref="Bar"/> result from the <paramref name="numberOfBars"/> specified.
        /// </summary>
        /// <param name="initialIdx">The initial cache index from which we start calculating the <see cref="Bar"/> value. 0 is the most recent value in the cache.</param>
        /// <param name="numberOfBars">The number of bars to calculate the <see cref="Bar"/> value.</param>
        /// <returns>The <see cref="Bar"/> value result from bars stored in the cache between the specified start and the specified number of bars.</returns>
        Bar GetBar(int initialIdx, int numberOfBars);

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
