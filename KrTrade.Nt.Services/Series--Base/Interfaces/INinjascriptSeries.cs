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

        /// <summary>
        /// Gets the type of the series.
        /// </summary>
        SeriesType Type { get; }

        /// <summary>
        /// Returns string thats represents the instance and the value specified in bars ago index.
        /// </summary>
        /// <param name="tabOrder"></param>
        /// <param name="barsAgo"></param>
        /// <returns></returns>
        string ToString(int tabOrder, int barsAgo);

        /// <summary>
        /// Logs the actual instance of the object.
        /// </summary>
        void Log();

        /// <summary>
        /// Logs the actual instance of the object and the value in specified bars ago.
        /// </summary>
        /// <param name="barsAgo">The index of element to logs.</param>
        void Log(int barsAgo);

        ///// <summary>
        ///// Gets series log string.
        ///// </summary>
        ///// <returns>The string thats represents the series in the logger.</returns>
        //string ToLogString();

        ///// <summary>
        ///// Gets series log string.
        ///// </summary>
        ///// <param name="barsAgo">The element to display in the logger.</param>
        ///// <returns>The string thats represents the series in the logger.</returns>
        //string ToLogString(int barsAgo);

        ///// <summary>
        ///// Gets series log string.
        ///// </summary>
        ///// <param name="barsAgo">The element to display in the logger.</param>
        ///// <param name="tabOrder">The number of tabulation to insert in the log string.</param>
        ///// <returns>The string thats represents the series in the logger.</returns>
        //string ToLogString(int barsAgo, int tabOrder);

        /// <summary>
        /// Dispose the series.
        /// </summary>
        void Dispose();

        /// <summary>
        /// Reset the <see cref="ISeries"/>. Clear the cache and initialize all properties.
        /// </summary>
        void Reset();

        ///// <summary>
        ///// Add new element to the series.
        ///// </summary>
        //void Add();

        ///// <summary>
        ///// Update the last element of the series.
        ///// </summary>
        //void Update();

        ///// <summary>
        ///// Remove the last element of the series.
        ///// </summary>
        //void RemoveLastElement();

    }
}
