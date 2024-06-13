//using KrTrade.Nt.Core;

//namespace KrTrade.Nt.Core
//{
//    public interface INinjascriptSeries : ISeries, IConfigure, IDataLoaded, ITerminated
//    {

//        ///// <summary>
//        ///// Returns string thats represents the instance and the value specified in bars ago index.
//        ///// </summary>
//        ///// <param name="tabOrder"></param>
//        ///// <param name="barsAgo"></param>
//        ///// <returns></returns>
//        //string ToString(int tabOrder, int barsAgo, bool displayIndex = true);

//        /// <summary>
//        /// Returns string thats represents the actual instance of the object.
//        /// </summary>
//        /// <param name="name">The name of the actual instance of the object.</param>
//        /// <param name="description">The description of the actual instace of the object.</param>
//        /// <param name="displayIndex">Indicates if the string represents the value index.</param>
//        /// <param name="separator">String between description and value.</param>
//        /// <param name="tabOrder">Number of tabulations below the string.</param>
//        /// <param name="barsAgo">The index of the value to represents.</param>
//        /// <param name="displayValue">Indicates if the string represents the value.</param>
//        /// <returns>String thats represents the actual instance of the object.</returns>
//        string ToString(string name, string description, bool displayIndex, string separator, int tabOrder, int barsAgo, bool displayValue = true);

//        /// <summary>
//        /// Returns string thats represents the actual instance of the object.
//        /// </summary>
//        /// <param name="tabOrder">Number of tabulations below the string.</param>
//        /// <param name="barsAgo">The index of the value to represents.</param>
//        /// <param name="separator">String between description and value.</param>
//        /// <param name="displayIndex">Indicates if the string represents the value index.</param>
//        /// <param name="displayValue">Indicates if the string represents the value.</param>
//        /// <param name="displayName">Indicates if the string represents the name.</param>
//        /// <param name="displayDescription">Indicates if the string represents the description.</param>
//        /// <returns>String thats represents the actual instance of the object.</returns>
//        string ToString(int tabOrder, int barsAgo, string separator = ": ", bool displayIndex = true, bool displayValue = true, bool displayName = true, bool displayDescription = false);

//        /// <summary>
//        /// Logs the actual instance of the object.
//        /// </summary>
//        void Log();

//        /// <summary>
//        /// Logs the actual instance of the object and the value in specified bars ago.
//        /// </summary>
//        /// <param name="barsAgo">The index of element to logs.</param>
//        void Log(int barsAgo);

//        ///// <summary>
//        ///// Gets series log string.
//        ///// </summary>
//        ///// <returns>The string thats represents the series in the logger.</returns>
//        //string ToLogString();

//        ///// <summary>
//        ///// Gets series log string.
//        ///// </summary>
//        ///// <param name="barsAgo">The element to display in the logger.</param>
//        ///// <returns>The string thats represents the series in the logger.</returns>
//        //string ToLogString(int barsAgo);

//        ///// <summary>
//        ///// Gets series log string.
//        ///// </summary>
//        ///// <param name="barsAgo">The element to display in the logger.</param>
//        ///// <param name="tabOrder">The number of tabulation to insert in the log string.</param>
//        ///// <returns>The string thats represents the series in the logger.</returns>
//        //string ToLogString(int barsAgo, int tabOrder);

//        ///// <summary>
//        ///// Dispose the series.
//        ///// </summary>
//        //void Dispose();

//        ///// <summary>
//        ///// Reset the <see cref="ISeries"/>. Clear the cache and initialize all properties.
//        ///// </summary>
//        //void Reset();

//        ///// <summary>
//        ///// Add new element to the series.
//        ///// </summary>
//        //void Add();

//        ///// <summary>
//        ///// Update the last element of the series.
//        ///// </summary>
//        //void Update();

//        ///// <summary>
//        ///// Remove the last element of the series.
//        ///// </summary>
//        //void RemoveLastElement();

//    }

//    public interface INinjascriptSeries<T> : INinjascriptSeries, ISeries<T>
//    {
//    }

//}
