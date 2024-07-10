namespace KrTrade.Nt.Core.Logging
{
    /// <summary>
    /// Ninjascript service that print any object in the output window.
    /// </summary>
    public interface IPrintService : ILogger<PrintOptions, PrintFormatter>
    {

        /// <summary>
        /// Prints in the ninjascript output window the OPEN price of the specific <paramref name="barsAgo"/> bar and
        /// the specific <paramref name="barsInProgress"/> data series. The default values for <paramref name="barsAgo"/> and
        /// <paramref name="barsInProgress"/> parameters is 0.
        /// </summary>
        /// <param name="barsAgo">The specific bar.</param>
        /// <param name="barsInProgress">The specific data series.</param>
        void LogOpen(int barsAgo = 0, int barsInProgress = 0);

        /// <summary>
        /// Prints in the ninjascript output window the HIGH price of the specific <paramref name="barsAgo"/> bar and
        /// the specific <paramref name="barsInProgress"/> data series. The default values for <paramref name="barsAgo"/> and
        /// <paramref name="barsInProgress"/> parameters is 0.
        /// </summary>
        /// <param name="barsAgo">The specific bar.</param>
        /// <param name="barsInProgress">The specific data series.</param>
        void LogHigh(int barsAgo = 0, int barsInProgress = 0);

        /// <summary>
        /// Prints in the ninjascript output window the LOW price of the specific <paramref name="barsAgo"/> bar and
        /// the specific <paramref name="barsInProgress"/> data series. The default values for <paramref name="barsAgo"/> and
        /// <paramref name="barsInProgress"/> parameters is 0.
        /// </summary>
        /// <param name="barsAgo">The specific bar.</param>
        /// <param name="barsInProgress">The specific data series.</param>
        void LogLow(int barsAgo = 0, int barsInProgress = 0);

        /// <summary>
        /// Prints in the ninjascript output window the CLOSE price of the specific <paramref name="barsAgo"/> bar and
        /// the specific <paramref name="barsInProgress"/> data series. The default values for <paramref name="barsAgo"/> and
        /// <paramref name="barsInProgress"/> parameters is 0.
        /// </summary>
        /// <param name="barsAgo">The specific bar.</param>
        /// <param name="barsInProgress">The specific data series.</param>
        void LogClose(int barsAgo = 0, int barsInProgress = 0);

        /// <summary>
        /// Prints in the ninjascript output window the INPUT price of the specific <paramref name="barsAgo"/> bar and
        /// the specific <paramref name="barsInProgress"/> data series. The default values for <paramref name="barsAgo"/> and
        /// <paramref name="barsInProgress"/> parameters is 0.
        /// </summary>
        /// <param name="barsAgo">The specific bar.</param>
        /// <param name="barsInProgress">The specific data series.</param>
        void LogInput(int barsAgo = 0, int barsInProgress = 0);

        /// <summary>
        /// Prints in the ninjascript output window the VOLUME of the specific <paramref name="barsAgo"/> bar and
        /// the specific <paramref name="barsInProgress"/> data series. The default values for <paramref name="barsAgo"/> and
        /// <paramref name="barsInProgress"/> parameters is 0.
        /// </summary>
        /// <param name="barsAgo">The specific bar.</param>
        /// <param name="barsInProgress">The specific data series.</param>
        void LogVolume(int barsAgo = 0, int barsInProgress = 0);

        /// <summary>
        /// Prints in the ninjascript output window the OPEN, HIGH, LOW AND CLOSE prices of the specific <paramref name="barsAgo"/> bar and
        /// the specific <paramref name="barsInProgress"/> data series. The default values for <paramref name="barsAgo"/> and
        /// <paramref name="barsInProgress"/> parameters is 0.
        /// </summary>
        /// <param name="barsAgo">The specific bar.</param>
        /// <param name="barsInProgress">The specific data series.</param>
        void LogOHLC(int barsAgo = 0, int barsInProgress = 0);

        /// <summary>
        /// Prints in the ninjascript output window the OPEN, HIGH, LOW AND CLOSE prices of the specific <paramref name="barsAgo"/> bar and
        /// the specific <paramref name="barsInProgress"/> data series. The default values for <paramref name="barsAgo"/> and
        /// <paramref name="barsInProgress"/> parameters is 0.
        /// </summary>
        /// <param name="label">The label of the values.</param>
        /// <param name="barsAgo">The specific bar.</param>
        /// <param name="barsInProgress">The specific data series.</param>
        void LogOHLC(string label, int barsAgo = 0, int barsInProgress = 0);

        /// <summary>
        /// Prints in the ninjascript output window the OPEN, HIGH, LOW, CLOSE AND VOLUME of the specific <paramref name="barsAgo"/> bar and
        /// the specific <paramref name="barsInProgress"/> data series. The default values for <paramref name="barsAgo"/> and
        /// <paramref name="barsInProgress"/> parameters is 0.
        /// </summary>
        /// <param name="barsAgo">The specific bar.</param>
        /// <param name="barsInProgress">The specific data series.</param>
        void LogOHLCV(int barsAgo = 0, int barsInProgress = 0);

        /// <summary>
        /// Prints in the ninjascript output window the OPEN, HIGH, LOW, CLOSE AND VOLUME of the specific <paramref name="barsAgo"/> bar and
        /// the specific <paramref name="barsInProgress"/> data series. The default values for <paramref name="barsAgo"/> and
        /// <paramref name="barsInProgress"/> parameters is 0.
        /// </summary>
        /// <param name="barsAgo">The specific bar.</param>
        /// <param name="barsInProgress">The specific data series.</param>
        void LogOHLCV(string label, int barsAgo = 0, int barsInProgress = 0);

        /// <summary>
        /// Print a title in the NinjaScript output window.
        /// </summary>
        /// <param name="title">The title to print.</param>
        void WriteTitle(string title);

        /// <summary>
        /// Print a line in NinjaScript output window.
        /// </summary>
        /// <param name="lineChar">The character that forms the line.</param>
        void WriteLine(char lineChar = '-');

        /// <summary>
        /// Print a blank line in NinjaScript output window.
        /// </summary>
        void WriteBlankLine();

        /// <summary>
        /// Log a value in NinjaScript output window.
        /// </summary>
        /// <param name="message">The message to log.</param>
        void Log(string message);

        /// <summary>
        /// Log a value in NinjaScript output window.
        /// </summary>
        /// <param name="value">The value to log.</param>
        void Log(object value);

        /// <summary>
        /// Log a value with label in NinjaScript output window.
        /// </summary>
        /// <param name="label">The label of the value.</param>
        /// <param name="value">The value to log.</param>
        void Log(string label, object value);

        /// <summary>
        /// Log a collection of values with label in NinjaScript output window.
        /// </summary>
        /// <param name="values">The values to log.</param>
        void LogValues(params object[] values);

        /// <summary>
        /// Log a collection of values with label in NinjaScript output window.
        /// </summary>
        /// <param name="labels">The labels. If exists one more label than values, 
        /// this label will be used as main label. "label0 => label1: value0 label2: value1".
        /// if only passed one label, will be used as main label. "label0 => value0 value1 value 2".
        /// The labels must be passed as a set of words separated by special characters ("," ";" " " "_" "-" ) 
        /// If exists a labels format error or number of labels don't match with values the method print only the values. </param>
        /// <param name="values">The values to log.</param>
        void Log(string labels, params object[] values);

    }
}
