﻿using KrTrade.Nt.Core.Data;
using System;

namespace KrTrade.Nt.Core.Infos
{
    public interface IBaseSeriesInfo
    {

        /// <summary>
        /// Gets or sets the <see cref="ISeries"/> capacity.
        /// </summary>
        int Capacity { get; set; }

        /// <summary>
        /// Gets or sets the number of elements to store in cache, before will be removed forever.
        /// </summary>
        int OldValuesCapacity { get; set; }

        /// <summary>
        /// Represents the instance including the owner.
        /// </summary>
        /// <param name="owner">The owner string.</param>
        /// <returns>String thats represents the instance.</returns>
        string ToString(string owner);
    }

    public interface ISeriesInfo : IBaseSeriesInfo, IElementInfo<SeriesType>
    {
    }

    public interface ISeriesInfo<TType> : IBaseSeriesInfo, IElementInfo<TType>
        where TType : Enum
    {
    }
}
