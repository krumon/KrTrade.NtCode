using KrTrade.Nt.Core.Data;
using KrTrade.Nt.Core.Infos;
using KrTrade.Nt.Core.Options;
using System.Collections;
using System.Collections.Generic;

namespace KrTrade.Nt.Core.Series
{
    public interface ISeries : IElement<SeriesType, ISeriesInfo, IElementOptions>
    {
        /// <summary>
        /// Gets <see cref="ISeries"/> capacity.
        /// </summary>
        int Capacity { get; }

        /// <summary>
        /// Gets the number of elements to store in cache, before will be removed forever.
        /// </summary>
        int OldValuesCapacity { get; }

        /// <summary>
        /// Gets the number of cache elements.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Indicates if <see cref="ISeries"/> is full.
        /// </summary>
        bool IsFull { get; }

        ///// <summary>
        ///// Remove the current element of the series.
        ///// </summary>
        //void RemoveLastElement();

        /// <summary>
        /// Reset the <see cref="ISeries"/>. Clear the cache and initialize all properties.
        /// </summary>
        void Reset();

        /// <summary>
        /// Dispose the <see cref="ISeries"/>. 
        /// </summary>
        void Dispose();

        ///// <summary>
        ///// <summary>
        ///// Gets the cache element located at the specified index. 
        ///// </summary>
        ///// <param name="index">The specified index.</param>
        ///// <returns>The cache element at the specified index.</returns>
        //object this[int index] { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tabOrder"></param>
        /// <param name="title"></param>
        /// <param name="subtitle"></param>
        /// <param name="description"></param>
        /// <param name="index"></param>
        /// <param name="isDescriptionBracketsVisible"></param>
        /// <param name="isIndexVisible"></param>
        /// <param name="separator"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        string ToString(int tabOrder, string title, string subtitle, string description, int index, bool isDescriptionBracketsVisible, bool isIndexVisible, string separator, string state);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        string ToString(string state);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tabOrder"></param>
        /// <param name="state"></param>
        /// <param name="index"></param>
        /// <param name="separator"></param>
        /// <param name="isTitleVisible"></param>
        /// <param name="isSubtitleVisible"></param>
        /// <param name="isDescriptionVisible"></param>
        /// <param name="isDescriptionBracketsVisible"></param>
        /// <param name="isIndexVisible"></param>
        /// <returns></returns>
        string ToString(int tabOrder, string state, int index = 0, string separator = ": ", bool isTitleVisible = true, bool isSubtitleVisible = false, bool isDescriptionVisible = true, bool isDescriptionBracketsVisible = true, bool isIndexVisible = false);

        bool IsValidIndex(int index, int period);
    }

    public interface ISeries<TElement> : ISeries, NinjaTrader.NinjaScript.ISeries<TElement>, IEnumerable<TElement>, IEnumerable
    {

        /// <summary>
        /// Gets the number of cache elements.
        /// </summary>
        new int Count { get; }

        /// <summary>
        /// <summary>
        /// Gets the cache element located at the specified index. 
        /// </summary>
        /// <param name="index">The specified index.</param>
        /// <returns>The cache element at the specified index.</returns>
        new TElement this[int index] { get; }

        /// <summary>
        /// Gets the current cache value 'cache[0]'.
        /// </summary>
        TElement CurrentValue { get; }

        /// <summary>
        /// Gets the last series value before the series was updated.
        /// </summary>
        TElement LastValue { get; }

        /// <summary>
        /// Gets The element that is at the specified index.
        /// </summary>
        /// <param name="valuesAgo">The index of the element in the <see cref="ICacheElement{T}"/>.</param>
        /// <returns>The element that is at the specified index.</returns>
        TElement GetValue(int valuesAgo);

        /// <summary>
        /// Returns <paramref name="numOfValues"/> of <see cref="{T}"/> from specified initial index.
        /// </summary>
        /// <param name="fromValuesAgo">The values ago where started to construct the array.</param>
        /// <param name="numOfValues">The number of <see cref="{T}"/> to returns.</param>
        /// <returns><see cref="{T}"/> collection.</returns>
        TElement[] ToArray(int fromValuesAgo, int numOfValues);


    }

}
