using System;

namespace KrTrade.Nt.Services
{
    public class CacheServiceOptions : BarUpdateServiceOptions
    {

        /// <summary>
        /// Gets the maximum values to store in cache.
        /// </summary>
        public int Capacity { get; set; }

        /// <summary>
        /// Gets the maximum old values to store in cache.
        /// </summary>
        public int OldValuesCapacity { get; set; }

        ///// <summary>
        ///// Gets <see cref="ICache{T}"/> period.
        ///// </summary>
        //int Period { get; }

        ///// <summary>
        ///// Gets the displacement of <see cref="ICache{T}"/> respect NinjaScript <see cref="ISeries{double}"/>.
        ///// </summary>
        //int Displacement { get; }


    }
}
