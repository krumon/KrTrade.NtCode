using KrTrade.Nt.Core.Bars;
using System.Collections.Generic;

namespace KrTrade.Nt.Core.Series
{
    public interface IBarsSeriesCollection : ISeriesCollection<IBarsSeries>, IBarUpdate // INumericSeries
    {

        /// <summary>
        /// Gets the index series.
        /// </summary>
        ICurrentBarSeries CurrentBar { get; }

        /// <summary>
        /// Gets the time series.
        /// </summary>
        ITimeSeries Time { get; }

        /// <summary>
        /// Gets the open series.
        /// </summary>
        IPriceSeries Open { get; }

        /// <summary>
        /// Gets the high series.
        /// </summary>
        IPriceSeries High { get; }

        /// <summary>
        /// Gets the low series.
        /// </summary>
        IPriceSeries Low { get; }

        /// <summary>
        /// Gets the close series.
        /// </summary>
        IPriceSeries Close { get; }

        /// <summary>
        /// Gets the volume series.
        /// </summary>
        IVolumeSeries Volume { get; }

        /// <summary>
        /// Gets the tick count series.
        /// </summary>
        ITickSeries Tick { get; }

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
