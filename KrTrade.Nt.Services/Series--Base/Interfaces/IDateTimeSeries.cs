﻿using System;

namespace KrTrade.Nt.Services
{
    public interface IDateTimeSeries<TInput> : IValueSeries<DateTime>, IHasDateTimeCalculateValues
    {
        /// <summary>
        /// The <typeparamref name="TInput"/> object necesary to get or calculate the cache values.
        /// </summary>
        TInput Input { get; }

        /// <summary>
        /// Gets the <typeparamref name="TInput"/> object, necesary to get or calculate the cache values.
        /// </summary>
        /// <returns>The instance of the input series.</returns>
        TInput GetInput(object input);
    }

    //public interface IDateTimeSeries<TInput> : IValueSeries<DateTime,TInput>
    //{
    //}
}
