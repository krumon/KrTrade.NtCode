using KrTrade.Nt.Core.Data;

namespace KrTrade.Nt.Core.Elements
{
    public interface ISeries : IElement<ISeriesInfo>
    {

        /// <summary>
        /// Gets the type of the series.
        /// </summary>
        new SeriesType Type { get; }

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

        /// <summary>
        /// Gets the cache element located at the specified index. 
        /// </summary>
        /// <param name="index">The specified index.</param>
        /// <returns>The cache element at the specified index.</returns>
        object this[int index] { get; }

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


    }
    public interface ISeries<T> : ISeries, NinjaTrader.NinjaScript.ISeries<T>
    {
        /// <summary>
        /// Gets the number of cache elements.
        /// </summary>
        new int Count { get; }

        /// <summary>
        /// Gets the cache element located at the specified index. 
        /// </summary>
        /// <param name="index">The specified index.</param>
        /// <returns>The cache element at the specified index.</returns>
        new T this[int index] { get; }

        /// <summary>
        /// Gets the current cache value 'cache[0]'.
        /// </summary>
        T CurrentValue { get; }

        /// <summary>
        /// Gets the last series value before the series was updated.
        /// </summary>
        T LastValue { get; }

        /// <summary>
        /// Gets The element that is at the specified index.
        /// </summary>
        /// <param name="valuesAgo">The index of the element in the <see cref="ICacheElement{T}"/>.</param>
        /// <returns>The element that is at the specified index.</returns>
        T GetValue(int valuesAgo);

        /// <summary>
        /// Returns <paramref name="numOfValues"/> of <see cref="{T}"/> from specified initial index.
        /// </summary>
        /// <param name="fromValuesAgo">The values ago where started to construct the array.</param>
        /// <param name="numOfValues">The number of <see cref="{T}"/> to returns.</param>
        /// <returns><see cref="{T}"/> collection.</returns>
        T[] ToArray(int fromValuesAgo, int numOfValues);

    }
}
