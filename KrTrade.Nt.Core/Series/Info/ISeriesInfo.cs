﻿using System;
using System.Collections.Generic;

namespace KrTrade.Nt.Core.Series
{
    public interface ISeriesInfo : IBaseSeriesInfo<SeriesType>
    {

        /// <summary>
        /// Gets or sets the inputs series.
        /// </summary>
        List<ISeriesInfo> Inputs { get; set; }

        /// <summary>
        /// Adds the input series to the series object.
        /// </summary>
        /// <typeparam name="TInfo">The type of the series info.</typeparam>
        /// <param name="configureSeriesInfo">Delegate to configure the series info.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="configureSeriesInfo"/> cannot be null.</exception>
        void AddInputSeries<TInfo>(Action<TInfo> configureSeriesInfo)
            where TInfo : ISeriesInfo, new();

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