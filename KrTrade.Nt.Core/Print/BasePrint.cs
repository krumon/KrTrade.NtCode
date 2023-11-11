using KrTrade.Nt.Core.Extensions;
using KrTrade.Nt.Core.Data;
using KrTrade.Nt.Core.DataSeries;
using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using System;
using System.Text;

namespace KrTrade.Nt.Core.Print
{
    public abstract class BasePrint
    {
        private readonly Action<object> _writeDelegate;
        private readonly Action _clearDelegate;
        private int _lastLength = 0;

        private PrintLabel _label;

        public abstract State State { get; }
        public abstract int CurrentBar { get; }
        public abstract DateTime CurrentTime { get; }
        public abstract int BarsInProgress { get; }
        public abstract NinjaTrader.Cbi.Instrument Instrument { get; }
        public abstract BarsPeriod BarsPeriod { get; }

        public PrintLevel MinLevel { get; set; }
        public PrintIdType PrintId { get; set; }

        public bool ShowState { get; set; } = true;
        public bool ShowBarsInProgress { get; set; } = true;
        public bool ShowDataSerie { get; set; } = true;
        public bool ShowNumOfBar { get; set; } = true;
        public FormatType FormatType { get; set; } = FormatType.Log;
        public FormatLength FormatLength { get; set; } = FormatLength.Long;

        public string LabelSeparator { get; set; } = ": ";
        public string ContentSeparator { get; set; } = " >> ";
        public string ValuesSeparator { get; set; } = ":";
        public string ValuesTabString { get; set; } = " ";

        public BasePrint(Action<object> writeDelegate, Action clearDelegate)
        {
            _writeDelegate = writeDelegate ?? throw new ArgumentNullException("writeDelegate cannot be null.");
            _clearDelegate = clearDelegate ?? throw new ArgumentNullException("clearDelegate cannot be null.");
        }

        public void Write(object value) => Print(value);
        public void Write(string text) => Print(text);
        public void Write(string label, object value)
        {
            string content = string.Empty;
            if (!string.IsNullOrEmpty(label))
                content = label;
            if (value != null)
            {
                content += ValuesSeparator;
                content += ToString(value);
            }
            Print(content);
        }
        public void Write(string labels, params object[] value)
        {
            if (labels.IsNullOrEmpty())
                return;
            if (value == null || value.Length == 0)
                return;
            string[] lb = labels.Split(new char[] { ',', ';', ' ', '-', '_' }, StringSplitOptions.RemoveEmptyEntries);
            if (lb.Length != value.Length)
                return;
            string content = string.Empty;
            for (int i = 0; i<lb.Length; i++)
            {
                if (i != 0) 
                    content += ValuesTabString;
                content += lb[i];
                content += ValuesSeparator;
                content += ToString(value[i]);
            }
            Print(content);
        }
        public void WriteLine(char lineChar = '-', int length = 0, int numLines = 1)
        {
            string text = string.Empty;
            int count = length == 0 ? _lastLength : length;
            for (int j=numLines; j>0; j--)
            {
                for (int i = 0; i < count; i++)
                    text += lineChar;
                if (j>1) text += Environment.NewLine;
            }
            Print(text, printLabelOrId: false);
        }
        public void WriteBlankLine(int numLines = 1) => WriteLine(' ', 1, numLines);
        public void WriteTitle(string title) => Print(title?.ToUpper(), printLabelOrId: false);
        public void Clear()
        {
            _clearDelegate?.Invoke();
        }

        protected void Print(object content, bool printLabelOrId = true)
        {
            if (MinLevel == PrintLevel.None)
                return;
            if (MinLevel == PrintLevel.Realtime && State == State.Historical)
                return;
            if (MinLevel == PrintLevel.Configuration && (State == State.Historical || State == State.Transition || State == State.Realtime))
                return;

            StringBuilder str = new StringBuilder();
            if (printLabelOrId)
            {
                if (_label == null) 
                    _label = new PrintLabel(this, isBeforeContent: PrintId == PrintIdType.None);

                // Label
                str.Append(_label.ToString(State, BarsInProgress, Instrument, BarsPeriod, CurrentBar));
                if (PrintId != PrintIdType.None)
                {
                    str.Append(PrintId.ToString(CurrentTime, BarsPeriod.ToTimeFormat() , FormatType, FormatLength));
                }
                str.Append(ContentSeparator);
            }
            if (content == null) str.Append("EMPTY");
            else str.Append(content.ToString());
            _lastLength = str.Length;
            _writeDelegate?.Invoke(str.ToString());
        }



        public string ToString(object value) => ToString(value, 0);
        public string ToString(object value, int barsAgo)
        {
            if (value == null) return string.Empty;

            else if (value is string s) return s;
            else if (value is double  d) return d.ToString(FormatType);
            else if (value is float  f) return f.ToString(FormatType);
            else if (value is decimal  dec) return dec.ToString(FormatType);
            else if (value is int i) return i.ToString(FormatType);
            else if (value is long l) return l.ToString(FormatType);
            else if (value is DateTime time) return time.ToString(BarsPeriod.ToTimeFormat(), FormatType, FormatLength);
            else if (value is PriceSeries prices) return prices[barsAgo].ToString(DoubleFormat.Price);
            else if (value is VolumeSeries volumes) return volumes[barsAgo].ToString(DoubleFormat.Volume);
            else if (value is TimeSeries times) return times[barsAgo].ToString(BarsPeriod.ToTimeFormat(), FormatType, FormatLength);

            return value.ToString();
        }

    }
}
