using KrTrade.Nt.Core.Bars;
using KrTrade.Nt.Core.Caches;

namespace KrTrade.Nt.Services
{
    public interface IBarsCache : ICache<BarDataModel>
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
        double RangeInterQuartil {get;}

        /// <summary>
        /// Gets the range of cache values.
        /// </summary>
        double Range { get; }

    }
}
