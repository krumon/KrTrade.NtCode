using KrTrade.Nt.Core.Data;
using KrTrade.Nt.Core.Extensions;
using KrTrade.Nt.Core.Logging;
using KrTrade.Nt.Core.Print;
using NinjaTrader.NinjaScript;
using System;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Ninjascript service that print any object in the output window.
    /// </summary>
    public class PrintService : BaseLogService<PrintOptions, PrintFormatter>
    {

        #region Private members

        private const string LabelSeparator = ": ";
        private const string ValueSeparator = "  ";
        private const int LineLength = 250;

        #endregion

        #region Public properties

        public override string Name => nameof(PrintService);

        /// <summary>
        /// Represents the minimum log level. 0:Trace, 1:Debug, 2:Information, 3:Warning, 4:Error, 5:Critical, 6:None
        /// </summary>
        public LogLevel LogLevel { get => Options.LogLevel; set { Options.LogLevel = value; } }

        /// <summary>
        /// The minimum ninjascript log level. 0:Historical, 1:Configuration, 2:Realtime
        /// Historical level logs in all states.
        /// Configuration level not logs in Historical state.
        /// Realtime logs only in Realtime state.
        /// </summary>
        public NinjascriptLogLevel NinjascriptLogLevel { get => Options.NinjascriptLogLevel; set { Options.NinjascriptLogLevel = value; } }

        /// <summary>
        /// Indicates whether the log information will be shown.
        /// </summary>
        public bool IsLogInfoVisible { get => Options.IsLogInfoVisible; set { Options.IsLogInfoVisible = value; } }

        /// <summary>
        /// Indicates whether the log data series information will be shown.
        /// </summary>
        public bool IsDataSeriesInfoVisible { get => Options.IsDataSeriesInfoVisible; set { Options.IsDataSeriesInfoVisible = value; } }

        /// <summary>
        /// Indicates whether the log time information will be shown.
        /// </summary>
        public bool IsTimeVisible { get => Options.IsTimeVisible; set { Options.IsTimeVisible = value; } }

        /// <summary>
        /// Indicates whether the number of the will be shown in the log message.
        /// </summary>
        public bool IsNumOfBarVisible { get => Options.IsNumOfBarVisible; set { Options.IsNumOfBarVisible = value; } }

        /// <summary>
        /// Indicates the length of the strings. This property affects to the ninjascript state string, the data series string
        /// </summary>
        public FormatLength StringFormatLength { get => Options.StringFormatLength; set { Options.StringFormatLength = value; } }

        #endregion

        #region Constructors

        public PrintService(NinjaScriptBase ninjascript) : base(ninjascript)
        {
        }
        public PrintService(NinjaScriptBase ninjascript, PrintOptions options) : base(ninjascript, options)
        {
        }
        public PrintService(NinjaScriptBase ninjascript, Action<PrintOptions> delegateOptions) : base(ninjascript, delegateOptions)
        {
        }

        #endregion

        #region Implementation

        protected override Action<object> WriteMethod => Ninjascript.Print;
        protected override Action ClearMethod => Ninjascript.ClearOutputWindow;

        #endregion

        /// <summary>
        /// Prints in the ninjascript output window the OPEN price of the specific <paramref name="barsAgo"/> bar and
        /// the specific <paramref name="barsInProgress"/> data series. The default values for <paramref name="barsAgo"/> and
        /// <paramref name="barsInProgress"/> parameters is 0.
        /// </summary>
        /// <param name="barsAgo">The specific bar.</param>
        /// <param name="barsInProgress">The specific data series.</param>
        public void LogOpen(int barsAgo = 0, int barsInProgress = 0)
        {
            if (!IsEnable() || !IsInRunningState())
                return;
            string label = Options.Formatter.StringFormatLength == FormatLength.Long ? "Open" : "O";
            LogValue(label, Ninjascript.Opens[barsInProgress][barsAgo]);
        }

        /// <summary>
        /// Prints in the ninjascript output window the HIGH price of the specific <paramref name="barsAgo"/> bar and
        /// the specific <paramref name="barsInProgress"/> data series. The default values for <paramref name="barsAgo"/> and
        /// <paramref name="barsInProgress"/> parameters is 0.
        /// </summary>
        /// <param name="barsAgo">The specific bar.</param>
        /// <param name="barsInProgress">The specific data series.</param>
        public void LogHigh(int barsAgo = 0, int barsInProgress = 0)
        {
            if (!IsEnable() || !IsInRunningState())
                return;
            string label = Options.Formatter.StringFormatLength == FormatLength.Long ? "High" : "H";
            LogValue(label, Ninjascript.Highs[barsInProgress][barsAgo]);
        }

        /// <summary>
        /// Prints in the ninjascript output window the LOW price of the specific <paramref name="barsAgo"/> bar and
        /// the specific <paramref name="barsInProgress"/> data series. The default values for <paramref name="barsAgo"/> and
        /// <paramref name="barsInProgress"/> parameters is 0.
        /// </summary>
        /// <param name="barsAgo">The specific bar.</param>
        /// <param name="barsInProgress">The specific data series.</param>
        public void LogLow(int barsAgo = 0, int barsInProgress = 0)
        {
            if (!IsEnable() || !IsInRunningState())
                return;
            string label = Options.Formatter.StringFormatLength == FormatLength.Long ? "Low" : "L";
            LogValue(label, Ninjascript.Lows[barsInProgress][barsAgo]);
        }

        /// <summary>
        /// Prints in the ninjascript output window the CLOSE price of the specific <paramref name="barsAgo"/> bar and
        /// the specific <paramref name="barsInProgress"/> data series. The default values for <paramref name="barsAgo"/> and
        /// <paramref name="barsInProgress"/> parameters is 0.
        /// </summary>
        /// <param name="barsAgo">The specific bar.</param>
        /// <param name="barsInProgress">The specific data series.</param>
        public void LogClose(int barsAgo = 0, int barsInProgress = 0)
        {
            if (!IsEnable() || !IsInRunningState())
                return;
            string label = Options.Formatter.StringFormatLength == FormatLength.Long ? "Close" : "C";
            LogValue(label, Ninjascript.Closes[barsInProgress][barsAgo]);
        }

        /// <summary>
        /// Prints in the ninjascript output window the INPUT price of the specific <paramref name="barsAgo"/> bar and
        /// the specific <paramref name="barsInProgress"/> data series. The default values for <paramref name="barsAgo"/> and
        /// <paramref name="barsInProgress"/> parameters is 0.
        /// </summary>
        /// <param name="barsAgo">The specific bar.</param>
        /// <param name="barsInProgress">The specific data series.</param>
        public void LogInput(int barsAgo = 0, int barsInProgress = 0)
        {
            if (!IsEnable() || !IsInRunningState())
                return;
            string label = Options.Formatter.StringFormatLength == FormatLength.Long ? "Input" : "I";
            LogValue(label, Ninjascript.Inputs[barsInProgress][barsAgo]);
        }

        /// <summary>
        /// Prints in the ninjascript output window the VOLUME of the specific <paramref name="barsAgo"/> bar and
        /// the specific <paramref name="barsInProgress"/> data series. The default values for <paramref name="barsAgo"/> and
        /// <paramref name="barsInProgress"/> parameters is 0.
        /// </summary>
        /// <param name="barsAgo">The specific bar.</param>
        /// <param name="barsInProgress">The specific data series.</param>
        public void LogVolume(int barsAgo = 0, int barsInProgress = 0)
        {
            if (!IsEnable() || !IsInRunningState())
                return;
            string label = Options.Formatter.StringFormatLength == FormatLength.Long ? "Volume" : "V";
            LogValue(label, Ninjascript.Volumes[barsInProgress][barsAgo].ToString(DoubleFormat.Volume));
        }

        /// <summary>
        /// Prints in the ninjascript output window the OPEN, HIGH, LOW AND CLOSE prices of the specific <paramref name="barsAgo"/> bar and
        /// the specific <paramref name="barsInProgress"/> data series. The default values for <paramref name="barsAgo"/> and
        /// <paramref name="barsInProgress"/> parameters is 0.
        /// </summary>
        /// <param name="barsAgo">The specific bar.</param>
        /// <param name="barsInProgress">The specific data series.</param>
        public void LogOHLC(int barsAgo = 0, int barsInProgress = 0)
        {
            if (!IsEnable() || !IsInRunningState())
                return;
            string labels = Options.Formatter.StringFormatLength == FormatLength.Long ? "Open, High, Low, Close" : "O, H, L, C";
            LogValues(labels, 
                Ninjascript.Opens[barsInProgress][barsAgo], 
                Ninjascript.Highs[barsInProgress][barsAgo], 
                Ninjascript.Lows[barsInProgress][barsAgo],
                Ninjascript.Closes[barsInProgress][barsAgo]
                );
        }

        /// <summary>
        /// Prints in the ninjascript output window the OPEN, HIGH, LOW, CLOSE AND VOLUME of the specific <paramref name="barsAgo"/> bar and
        /// the specific <paramref name="barsInProgress"/> data series. The default values for <paramref name="barsAgo"/> and
        /// <paramref name="barsInProgress"/> parameters is 0.
        /// </summary>
        /// <param name="barsAgo">The specific bar.</param>
        /// <param name="barsInProgress">The specific data series.</param>
        public void LogOHLCV(int barsAgo = 0, int barsInProgress = 0)
        {
            if (!IsEnable() || !IsInRunningState())
                return;
            string labels = Options.Formatter.StringFormatLength == FormatLength.Long ? "Open, High, Low, Close, Volume" : "O, H, L, C, V";
            LogValues(labels,
                Ninjascript.Opens[barsInProgress][barsAgo],
                Ninjascript.Highs[barsInProgress][barsAgo],
                Ninjascript.Lows[barsInProgress][barsAgo],
                Ninjascript.Closes[barsInProgress][barsAgo],
                Ninjascript.Volumes[barsInProgress][barsAgo].ToString(DoubleFormat.Volume)
                );
        }

        /// <summary>
        /// Print a title in the NinjaScript output window.
        /// </summary>
        /// <param name="title">The title to print.</param>
        public void WriteTitle(string title) 
        { 
            WriteUpperText(title?.ToUpper());
        } 

        /// <summary>
        /// Print a line in NinjaScript output window.
        /// </summary>
        /// <param name="lineChar">The character that forms the line.</param>
        public void WriteLine(char lineChar = '-')
        {
            if (!IsEnable()) return;

            string text = string.Empty;
            //int count = LastLength == 0 ? 100 : LastLength*2;
            for (int i = 0; i < LineLength; i++)
                text += lineChar;

            WriteText(text);
        }

        /// <summary>
        /// Print a blank line in NinjaScript output window.
        /// </summary>
        public void WriteBlankLine()
        {
            WriteText(" ");
        }

        /// <summary>
        /// Log an exception in NinjaScript output window.
        /// </summary>
        /// <param name="exception">The exception to log.</param>
        public void LogException(Exception exception) => LogError(exception);

        /// <summary>
        /// Log a value in NinjaScript output window.
        /// </summary>
        /// <param name="message">The message to log.</param>
        public void LogText(string message)
        {
            LogInformation(message);
        }

        /// <summary>
        /// Log a value in NinjaScript output window.
        /// </summary>
        /// <param name="value">The value to log.</param>
        public void LogValue(object value)
        {
            LogInformation(ToString(value));
        }

        /// <summary>
        /// Log a value with label in NinjaScript output window.
        /// </summary>
        /// <param name="label">The label of the value.</param>
        /// <param name="value">The value to log.</param>
        public void LogValue(string label, object value)
        {
            if (!IsEnable()) return;

            string logText = string.Empty;
            if (!string.IsNullOrEmpty(label))
                logText = label;
            if (value != null)
            {
                logText += LabelSeparator;
                logText += ToString(value);
            }
            LogInformation(logText);
        }

        /// <summary>
        /// Log a collection of values with label in NinjaScript output window.
        /// </summary>
        /// <param name="values">The values to log.</param>
        public void LogValues(params object[] values)
        {
            if (!IsEnable()) return;

            if (values == null || values.Length == 0)
                return;

            string logText = string.Empty;
            for (int i = 0; i < values.Length; i++)
            {
                if (i != 0)
                    logText += ValueSeparator;
                logText += ToString(values[i]);
            }
            LogInformation(logText);
        }

        /// <summary>
        /// Log a collection of values with label in NinjaScript output window.
        /// </summary>
        /// <param name="labels">The value labels. 
        /// The labels must be passed as a set of words separated by special characters ("," ";" " " "_" "-" ) </param>
        /// <param name="values">The values to log.</param>
        public void LogValues(string labels, params object[] values)
        {
            if (!IsEnable()) return;

            if (labels.IsNullOrEmpty())
                return;

            if (values == null || values.Length == 0)
                return;

            string[] lb = labels.Split(new char[] { ',', ';', ' ', '-', '_' }, StringSplitOptions.RemoveEmptyEntries);
            if (lb.Length != values.Length)
                return;

            string logText = string.Empty;
            for (int i = 0; i < lb.Length; i++)
            {
                if (i != 0)
                    logText += ValueSeparator;
                logText += lb[i];
                logText += LabelSeparator;
                logText += ToString(values[i]);
            }
            LogInformation(logText);
        }

        #region Private methods

        protected bool IsEnable()
        {
            if (!Options.IsEnable) 
                return false;
            if (Options.NinjascriptLogLevel == NinjascriptLogLevel.None)
                return false;
            if (Options.NinjascriptLogLevel == NinjascriptLogLevel.Realtime && Ninjascript.State != State.Realtime)
                return false;
            if (Options.NinjascriptLogLevel == NinjascriptLogLevel.Configuration && Ninjascript.State == State.Historical)
                return false;

            return true;
        }
        protected string ToString(object value)
        {
            if (value == null) return string.Empty;

            else if (value is string s) return s;
            else if (value is double d) return d.ToString(Options.Formatter.StringFormatLength);
            else if (value is float f) return f.ToString(Options.Formatter.StringFormatLength);
            else if (value is decimal dec) return dec.ToString(Options.Formatter.StringFormatLength);
            else if (value is int i) return i.ToString(Options.Formatter.StringFormatLength);
            else if (value is long l) return l.ToString(Options.Formatter.StringFormatLength);
            else if (value is DateTime time)
            {
                TimeFormat timeFormat;
                if (IsInRunningState())
                    timeFormat = Ninjascript.BarsPeriods[Ninjascript.BarsInProgress].ToTimeFormat();
                else
                    timeFormat = TimeFormat.Minute;

                return time.ToString(timeFormat, FormatType.Log, Options.Formatter.StringFormatLength);
            }
                

            return value.ToString();
        }

        #endregion
    }
}
