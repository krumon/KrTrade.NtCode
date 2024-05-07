using System;
using System.Collections.Generic;

namespace KrTrade.Nt.Core.Series
{
    public interface ISeriesInfo : IBaseSeriesInfo
    {

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

        ///// <summary>
        ///// Adds the input series to the series object.
        ///// </summary>
        ///// <param name="configureSeriesInfo">Delegate to configure the series info.</param>
        ///// <exception cref="ArgumentNullException">The <paramref name="configureSeriesInfo"/> cannot be null.</exception>
        //void AddInputSeries(Action<BarsSeriesInfo> configureSeriesInfo);

    }
    public interface ISeriesInfo<T> : ISeriesInfo, IBaseSeriesInfo<T>
        where T : Enum
    {
    }
}
