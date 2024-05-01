using KrTrade.Nt.Core.Data;
using KrTrade.Nt.Core.Info;
using System;
using System.Collections.Generic;

namespace KrTrade.Nt.Core.Series
{
    public interface ISeriesInfo : IKeyInfo
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

        /// <summary>
        /// Gets the inputs series.
        /// </summary>
        List<ISeriesInfo> Inputs { get; }

        /// <summary>
        /// Adds the input series to the series object.
        /// </summary>
        /// <typeparam name="TInfo">The type of the series info.</typeparam>
        /// <param name="configureSeriesInfo">Delegate to configure the series info.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="configureSeriesInfo"/> cannot be null.</exception>
        void AddInputSeries<TInfo>(Action<TInfo> configureSeriesInfo)
            where TInfo : ISeriesInfo, new();

        /// <summary>
        /// Adds the input series to the series object.
        /// </summary>
        /// <param name="configureSeriesInfo">Delegate to configure the series info.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="configureSeriesInfo"/> cannot be null.</exception>
        void AddInputSeries(Action<SeriesInfo> configureSeriesInfo);

    }

    public interface ISeriesInfo<T> : ISeriesInfo
        where T : Enum
    {
        /// <summary>
        /// Gets or sets the type of the series.
        /// </summary>
        new T Type { get; set; }
    }
}
