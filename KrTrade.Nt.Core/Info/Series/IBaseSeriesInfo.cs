using KrTrade.Nt.Core.Data;
using KrTrade.Nt.Core.Info;
using System;

namespace KrTrade.Nt.Core.Series
{
    public interface IBaseSeriesInfo : IKeyInfo
    {

        /// <summary>
        /// Gets or sets the type of the series.
        /// </summary>
        SeriesType Type { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="ISeries"/> capacity.
        /// </summary>
        int Capacity { get; set; }

        /// <summary>
        /// Gets or sets the number of elements to store in cache, before will be removed forever.
        /// </summary>
        int OldValuesCapacity { get; set; }

    }

    public interface IBaseSeriesInfo<T> : IBaseSeriesInfo
        where T : Enum
    {
        /// <summary>
        /// Gets or sets the type of the series.
        /// </summary>
        new T Type { get; set; }

    }
}
