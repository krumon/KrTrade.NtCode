using KrTrade.Nt.Core.Data;
using KrTrade.Nt.Core.Extensions;
using KrTrade.Nt.Core.Logging;
using NinjaTrader.NinjaScript;
using System;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Ninjascript service that print any object in the output window.
    /// </summary>
    public class PrintService : BaseLoggerService<PrintOptions, PrintFormatter>, IPrintService
    {

        #region Private members

        private const string LabelSeparator = ": ";
        private const string ValueSeparator = "  ";
        private const string MainLabelSeparator = " => ";
        private const int LineLength = 300;

        #endregion

        #region implementation

        public override string Name => Type.ToElementType().ToString(); //.ToShortString();
        protected override ServiceType ToElementType() => ServiceType.PRINT;

        #endregion

        #region Public properties

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
        public FormatLength StringFormatLength { get => Options.FormatLength; set { Options.FormatLength = value; } }

        #endregion

        #region Constructors

        /// <summary>
        /// Create <see cref="PrintService"/> instance and configure it.
        /// This instance must be created in the 'Ninjascript.State == Configure'.
        /// </summary>
        /// <param name="ninjascript">The <see cref="INinjascript"/> to gets 'Ninjatrader.NinjaScript' properties and objects.</param>
        public PrintService(NinjaScriptBase ninjascript) : this(ninjascript, new PrintOptions())
        {
        }

        /// <summary>
        /// Create <see cref="PrintService"/> instance and configure it.
        /// This instance must be created in the 'Ninjascript.State == Configure'.
        /// </summary>
        /// <param name="ninjascript">The <see cref="INinjascript"/> to gets 'Ninjatrader.NinjaScript' properties and objects.</param>
        /// <param name="options">The configure options of the service.</param>
        public PrintService(NinjaScriptBase ninjascript, PrintOptions options) : base(ninjascript, options)
        {
        }

        #endregion

        #region Implementation

        protected override void WriteTo(object value) => Ninjascript.Print(value);
        protected override void ClearLoggerMessages() => Ninjascript.ClearOutputWindow();

        /// <summary>
        /// Prints in the ninjascript output window the OPEN price of the specific <paramref name="barsAgo"/> bar and
        /// the specific <paramref name="barsInProgress"/> data series. The default values for <paramref name="barsAgo"/> and
        /// <paramref name="barsInProgress"/> parameters is 0.
        /// </summary>
        /// <param name="barsAgo">The specific bar.</param>
        /// <param name="barsInProgress">The specific data series.</param>
        public void LogOpen(int barsAgo = 0, int barsInProgress = 0)
        {
            if (!IsEnable) return;

            string label = StringFormatLength == FormatLength.Long ? "Open" : "O";
            Log(label, Ninjascript.Opens[barsInProgress][barsAgo]);
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
            if (!IsEnable) return;

            string label = Options.Formatter.StringFormatLength == FormatLength.Long ? "High" : "H";
            Log(label, Ninjascript.Highs[barsInProgress][barsAgo]);
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
            if (!IsEnable) return;

            string label = Options.Formatter.StringFormatLength == FormatLength.Long ? "Low" : "L";
            Log(label, Ninjascript.Lows[barsInProgress][barsAgo]);
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
            if (!IsEnable) return;

            string label = Options.Formatter.StringFormatLength == FormatLength.Long ? "Close" : "C";
            Log(label, Ninjascript.Closes[barsInProgress][barsAgo]);
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
            if (!IsEnable) return;

            string label = Options.Formatter.StringFormatLength == FormatLength.Long ? "Input" : "I";
            Log(label, Ninjascript.Inputs[barsInProgress][barsAgo]);
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
            if (!IsEnable) return;

            string label = Options.Formatter.StringFormatLength == FormatLength.Long ? "Volume" : "V";
            Log(label, Ninjascript.Volumes[barsInProgress][barsAgo].ToString(DoubleFormat.Volume));
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
            if (!IsEnable) return;

            string labels = Options.Formatter.StringFormatLength == FormatLength.Long ? "Open, High, Low, Close" : "O, H, L, C";
            Log(labels,
                Ninjascript.Opens[barsInProgress][barsAgo],
                Ninjascript.Highs[barsInProgress][barsAgo],
                Ninjascript.Lows[barsInProgress][barsAgo],
                Ninjascript.Closes[barsInProgress][barsAgo]
                );
        }

        /// <summary>
        /// Prints in the ninjascript output window the OPEN, HIGH, LOW AND CLOSE prices of the specific <paramref name="barsAgo"/> bar and
        /// the specific <paramref name="barsInProgress"/> data series. The default values for <paramref name="barsAgo"/> and
        /// <paramref name="barsInProgress"/> parameters is 0.
        /// </summary>
        /// <param name="label">The label of the values.</param>
        /// <param name="barsAgo">The specific bar.</param>
        /// <param name="barsInProgress">The specific data series.</param>
        public void LogOHLC(string label, int barsAgo = 0, int barsInProgress = 0)
        {
            if (!IsEnable) return;

            string labels = string.IsNullOrEmpty(label) || string.IsNullOrWhiteSpace(label) ? string.Empty : label + ", ";
            labels += Options.Formatter.StringFormatLength == FormatLength.Long ? "Open, High, Low, Close" : "O, H, L, C";
            Log(labels,
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
            if (!IsEnable) return;

            string labels = Options.Formatter.StringFormatLength == FormatLength.Long ? "Open, High, Low, Close, Volume" : "O, H, L, C, V";
            Log(labels,
                Ninjascript.Opens[barsInProgress][barsAgo],
                Ninjascript.Highs[barsInProgress][barsAgo],
                Ninjascript.Lows[barsInProgress][barsAgo],
                Ninjascript.Closes[barsInProgress][barsAgo],
                Ninjascript.Volumes[barsInProgress][barsAgo].ToString(DoubleFormat.Volume)
                );
        }

        /// <summary>
        /// Prints in the ninjascript output window the OPEN, HIGH, LOW, CLOSE AND VOLUME of the specific <paramref name="barsAgo"/> bar and
        /// the specific <paramref name="barsInProgress"/> data series. The default values for <paramref name="barsAgo"/> and
        /// <paramref name="barsInProgress"/> parameters is 0.
        /// </summary>
        /// <param name="barsAgo">The specific bar.</param>
        /// <param name="barsInProgress">The specific data series.</param>
        public void LogOHLCV(string label, int barsAgo = 0, int barsInProgress = 0)
        {
            if (!IsEnable) return;

            string labels = string.IsNullOrEmpty(label) || string.IsNullOrWhiteSpace(label) ? string.Empty : label + ", ";
            labels += Options.Formatter.StringFormatLength == FormatLength.Long ? "Open, High, Low, Close, Volume" : "O, H, L, C, V";
            Log(labels,
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
            if (!IsEnable) return;

            Write(title, true);
        }

        /// <summary>
        /// Print a line in NinjaScript output window.
        /// </summary>
        /// <param name="lineChar">The character that forms the line.</param>
        public void WriteLine(char lineChar = '-')
        {
            if (!IsEnable) return;

            string text = string.Empty;
            //int count = LastLength == 0 ? 100 : LastLength*2;
            for (int i = 0; i < LineLength; i++)
                text += lineChar;

            Write(text);
        }

        /// <summary>
        /// Print a blank line in NinjaScript output window.
        /// </summary>
        public void WriteBlankLine()
        {
            if (!IsEnable) return;

            Write(" ");
        }

        /// <summary>
        /// Log a value in NinjaScript output window.
        /// </summary>
        /// <param name="message">The message to log.</param>
        public void Log(string message)
        {
            if (!IsEnable) return;

            LogInformation(message);
        }

        /// <summary>
        /// Log a value in NinjaScript output window.
        /// </summary>
        /// <param name="value">The value to log.</param>
        public void Log(object value)
        {
            if (!IsEnable) return;

            LogInformation(ToString(value));
        }

        /// <summary>
        /// Log a value with label in NinjaScript output window.
        /// </summary>
        /// <param name="label">The label of the value.</param>
        /// <param name="value">The value to log.</param>
        public void Log(string label, object value)
        {
            if (!IsEnable) return;

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
            if (!IsEnable) return;

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
        public void Log(string labels, params object[] values)
        {
            if (!IsEnable) return;

            if (labels.IsNullOrEmpty())
                return;

            if (values == null || values.Length == 0)
                return;

            string[] lb = labels.Split(new char[] { ',', ';', ' ', '-', '_' }, StringSplitOptions.RemoveEmptyEntries);
            if (lb.Length != values.Length && lb.Length != values.Length + 1)
                return;

            string logText = string.Empty;
            int initialIdx = 0;
            if (lb.Length == values.Length + 1)
            {
                logText += lb[0];
                logText += MainLabelSeparator;
                initialIdx = 1;
            }

            for (int i = initialIdx; i < lb.Length; i++)
            {
                if (i != initialIdx)
                    logText += ValueSeparator;
                logText += lb[i];
                logText += LabelSeparator;
                logText += ToString(values[i - initialIdx]);
            }
            LogInformation(logText);
        }

        #endregion

        #region Private methods

        protected string ToString(object value)
        {
            if (value == null) return string.Empty;

            else if (value is string s) return s;
            else if (value is double d) return d.ToString(FormatLength.Long);
            else if (value is float f) return f.ToString(Options.Formatter.StringFormatLength);
            else if (value is decimal dec) return dec.ToString(Options.Formatter.StringFormatLength);
            else if (value is int i) return i.ToString(Options.Formatter.StringFormatLength);
            else if (value is long l) return l.ToString(Options.Formatter.StringFormatLength);
            else if (value is DateTime time)
            {
                TimeFormat timeFormat;
                if (IsInRunningStates())
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
