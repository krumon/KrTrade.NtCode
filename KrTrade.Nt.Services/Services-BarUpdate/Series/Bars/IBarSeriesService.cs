using KrTrade.Nt.Core.Bars;
using KrTrade.Nt.Services.Series;
using System.Collections.Generic;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Defines properties and methods that are necesary to create a series service.
    /// </summary>
    public interface IBarSeriesService : ISeriesService<BarsSeries>
    {

        /// <summary>
        /// Gets the element of a sepecific index.
        /// </summary>
        /// <param name="index">The specific index.</param>
        /// <returns>Series element located at specified index.</returns>
        new double this[int index] { get; }

        /// <summary>
        /// Gets the index series.
        /// </summary>
        CurrentBarSeries CurrentBar { get; }

        /// <summary>
        /// Gets the time series.
        /// </summary>
        TimeSeries Time { get; }

        /// <summary>
        /// Gets the open series.
        /// </summary>
        PriceSeries Open { get; }

        /// <summary>
        /// Gets the high series.
        /// </summary>
        PriceSeries High { get; }

        /// <summary>
        /// Gets the low series.
        /// </summary>
        PriceSeries Low { get; }

        /// <summary>
        /// Gets the close series.
        /// </summary>
        PriceSeries Close { get; }

        /// <summary>
        /// Gets the volume series.
        /// </summary>
        VolumeSeries Volume { get; }

        /// <summary>
        /// Gets the tick count series.
        /// </summary>
        TickSeries Tick { get; }

        /// <summary>
        /// Returns the <see cref="Bar"/> of the specified <paramref name="barsAgo"/>.
        /// </summary>
        /// <param name="barsAgo">The index specified. 0 is the most recent value in the cache.</param>
        /// <returns>The <see cref="Bar"/> value result from bars stored in the cache between the <paramref name="barsAgo"/> to and the <paramref name="period"/> specified.</returns>
        Bar GetBar(int barsAgo);

        /// <summary>
        /// Returns the <see cref="Bar"/> result from <paramref name="barsAgo"/> to <paramref name="period"/> specified.
        /// </summary>
        /// <param name="barsAgo">The initial index from most recent bar. 0 is the most recent value in the cache.</param>
        /// <param name="period">The number of bars to calculate the <see cref="Bar"/> value.</param>
        /// <returns>The <see cref="Bar"/> value result from bars stored in the cache between the <paramref name="barsAgo"/> to and the <paramref name="period"/> specified.</returns>
        Bar GetBar(int barsAgo, int period);

        /// <summary>
        /// Returns the <see cref="Bar"/> collection result from <paramref name="barsAgo"/> to <paramref name="period"/> specified.
        /// </summary>
        /// <param name="barsAgo">The initial index from most recent bar. 0 is the most recent value in the cache.</param>
        /// <param name="period">The number of bars to calculate the <see cref="Bar"/> value.</param>
        /// <returns>The <see cref="Bar"/> collection result from bars stored in the cache between the <paramref name="barsAgo"/> to and the <paramref name="period"/> specified.</returns>
        IList<Bar> GetBars(int barsAgo, int period);

    }
}
