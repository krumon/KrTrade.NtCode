using KrTrade.Nt.Core.Collections;
using KrTrade.Nt.Core.Series;

namespace KrTrade.Nt.Services.Series
{
    public interface INinjascriptSeries : IConfigure, IDataLoaded, ITerminated, IKeyItem
    {
        /// <summary>
        /// Gets the series capacity.
        /// </summary>
        int Capacity { get; }

        /// <summary>
        /// Gets the removed values cache capacity.
        /// </summary>
        int OldValuesCapacity { get; }

        /// <summary>
        /// Gets the series information.
        /// </summary>
        IBaseSeriesInfo Info { get; }

        ///// <summary>
        ///// Add new element to the series.
        ///// </summary>
        //void Add();

        ///// <summary>
        ///// Update the last element of the series.
        ///// </summary>
        //void Update();

        /// <summary>
        /// Dispose the series.
        /// </summary>
        void Dispose();

        ///// <summary>
        ///// Remove the last element of the series.
        ///// </summary>
        //void RemoveLastElement();

        /// <summary>
        /// Reset the <see cref="ISeries"/>. Clear the cache and initialize all properties.
        /// </summary>
        void Reset();

    }
}
