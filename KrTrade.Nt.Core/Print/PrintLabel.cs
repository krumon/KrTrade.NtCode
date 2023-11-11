using KrTrade.Nt.Core.Extensions;
using KrTrade.Nt.Core.Data;
using KrTrade.Nt.Core.DataSeries;
using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using System;
using System.Text;

namespace KrTrade.Nt.Core.Print
{
    /// <summary>
    /// Optional label to print in the ninjascript output window or other site.
    /// The label will be made up of its status, the bars that are in progress, 
    /// the data series and the bar number in which we are.
    /// </summary>
    public class PrintLabel
    {
        private readonly string _label = "{State}{[BarsInProgress,InstrumentName(TimeFrame),NumOfBar]}{LabelSeparator}";
        private readonly bool _isBeforeContent;
        private readonly BasePrint _root;

        public bool IsVisible => StateIsVisible || BarsInfoIsVisible;
        public bool BarsInfoIsVisible => (ShowBarsInProgress || ShowDataSerie || ShowNumOfBar) && (_root.State == State.Historical || _root.State == State.Transition || _root.State == State.Realtime);
        public bool StateIsVisible => _root.ShowState;
        public bool ShowBarsInProgress => _root.ShowBarsInProgress;
        public bool ShowDataSerie => _root.ShowDataSerie;
        public bool ShowNumOfBar => _root.ShowNumOfBar;
        public string LabelSeparator => !IsVisible || _isBeforeContent ? string.Empty : _root.LabelSeparator;
        public FormatType FormatType => _root.FormatType;
        public FormatLength FormatLength => _root.FormatLength;

        public PrintLabel(BasePrint root) : this(root,false) { }
        public PrintLabel(BasePrint root, bool isBeforeContent)
        {
            _root = root ?? throw new ArgumentNullException("The 'BasePrint' object cannot be null.");
            _isBeforeContent = isBeforeContent;
        }

        public string ToString(string state, int barsInProgress, string instrumentName, string timeFrame, int currentBar)
        {
            StringBuilder sb = new StringBuilder(_label);

            // Hide the label
            if (!IsVisible)
                return string.Empty;

            // Insert the state
            if (StateIsVisible) sb.Replace("{State}", state);
            else sb.Replace("{State}", string.Empty);

            //Insert the DataSeries.
            if (!BarsInfoIsVisible)
                sb.Replace("{[BarsInProgress,InstrumentName(TimeFrame),NumOfBar]}", string.Empty);
            else
            {
                string dataSeries = "[";
                // Insert bars in progress
                if (ShowBarsInProgress) // && (state == State.Historical || state == State.Transition || state == State.Realtime))
                {
                    dataSeries += barsInProgress.ToString(FormatType);
                    if ((ShowDataSerie || ShowNumOfBar)) // && (state != State.Historical || state == State.Transition || state != State.Realtime)) 
                        dataSeries += ",";
                }
                //else dataSeries += "*";

                // Insert DataSeries
                if (ShowDataSerie) // && (state == State.Historical || state == State.Transition || state == State.Realtime))
                {
                    dataSeries += instrumentName + "(" + timeFrame + ")";
                    if (ShowNumOfBar) // && (state == State.Historical || state == State.Realtime))
                        dataSeries += ",";
                }
                // Replace Num of Bar
                if (ShowNumOfBar) // && (state == State.Historical || state == State.Transition || state == State.Realtime)) 
                    dataSeries += currentBar.ToString(FormatType);

                dataSeries += "]";

                sb.Replace("{[BarsInProgress,InstrumentName(TimeFrame),NumOfBar]}", dataSeries);
            }

            sb.Replace("{LabelSeparator}", LabelSeparator);

            return sb.ToString();
        }
        public string ToString(State state, int barsInProgress, NinjaTrader.Cbi.Instrument instrument, BarsPeriod barsPeriod, int currentBar)
        {
            StringBuilder sb = new StringBuilder(_label);

            // Hide the label
            if (!IsVisible)
                return string.Empty;

            // Insert the state
            if (StateIsVisible) sb.Replace("{State}", state.ToString(FormatType, FormatLength));
            else sb.Replace("{State}", string.Empty);

            //Insert the DataSeries.
            if (!BarsInfoIsVisible)
                sb.Replace("{[BarsInProgress,InstrumentName(TimeFrame),NumOfBar]}", string.Empty);
            else
            {
                string dataSeries = "[";
                // Insert bars in progress
                if (ShowBarsInProgress)
                {
                    dataSeries += barsInProgress.ToString(FormatType);
                    if ((ShowDataSerie || ShowNumOfBar)) 
                        dataSeries += ",";
                }
                // Insert DataSeries
                if (ShowDataSerie)
                {
                    dataSeries += instrument.MasterInstrument.Name + "(" + barsPeriod.ToString(FormatType) + ")";
                    if (ShowNumOfBar) dataSeries += ",";
                }
                // Replace Num of Bar
                if (ShowNumOfBar) 
                    dataSeries += currentBar.ToString(FormatType);

                dataSeries += "]";

                sb.Replace("{[BarsInProgress,InstrumentName(TimeFrame),NumOfBar]}", dataSeries);
            }

            sb.Replace("{LabelSeparator}", LabelSeparator);

            return sb.ToString();
        }

    }

    #region Constructores anulados

    //public PrintLabel(bool isVisible) : this(isVisible, isVisible, isVisible, isVisible, FormatLength.Long, LABEL_SEPARATOR, false) { }
    //public PrintLabel(string labelSeparator) : this(true, true, true, true, FormatLength.Long, labelSeparator, false) { }
    //public PrintLabel(string labelSeparator, FormatLength formatLength) : this(true, true, true, true, formatLength, labelSeparator, false) { }
    //public PrintLabel(bool barsInfoIsVisible, bool isBeforeContent) : this(true, barsInfoIsVisible, barsInfoIsVisible, barsInfoIsVisible, FormatLength.Long, LABEL_SEPARATOR, isBeforeContent) { }
    //public PrintLabel(bool barsInfoIsVisible, FormatLength formatLength, bool isBeforeContent) : this(true, barsInfoIsVisible, barsInfoIsVisible, barsInfoIsVisible, formatLength, LABEL_SEPARATOR, isBeforeContent) { }
    //public PrintLabel(bool stateIsVisible, string labelSeparator) : this(stateIsVisible, true, true, true, FormatLength.Long, labelSeparator, false) { }
    //public PrintLabel(bool stateIsVisible, string labelSeparator, FormatLength formatLength, bool isBeforeContent) : this(stateIsVisible, true, true, true, formatLength, labelSeparator, isBeforeContent) { }
    //public PrintLabel(bool showDataSerie, bool showNumOfBar, string labelSeparator) : this(true, true, showDataSerie, showNumOfBar, FormatLength.Long, labelSeparator, false) { }
    //public PrintLabel(bool showDataSerie, bool showNumOfBar, string labelSeparator, bool isBeforeContent) : this(true, true, showDataSerie, showNumOfBar, FormatLength.Long, labelSeparator, isBeforeContent) { }
    //public PrintLabel(bool showDataSerie, bool showNumOfBar, string labelSeparator, FormatLength formatLength, bool isBeforeContent) : this(true, true, showDataSerie, showNumOfBar, formatLength, labelSeparator, isBeforeContent) { }

    //public PrintLabel(bool stateIsVisible, bool showBarsInProgress, bool showDataSerie, bool showNumOfBar, FormatLength formatLength = FormatLength.Long, string labelSeparator = LABEL_SEPARATOR, bool isBeforeContent = false)
    //{
    //    labelSeparator = labelSeparator ?? string.Empty;
    //    StateIsVisible = stateIsVisible;
    //    ShowBarsInProgress = showBarsInProgress;
    //    ShowDataSerie = showDataSerie;
    //    ShowNumOfBar = showNumOfBar;
    //    FormatLength = formatLength;
    //    LabelSeparator = !IsVisible || isBeforeContent ? string.Empty : labelSeparator;
    //}

    #endregion
    #region Methods anulados

    //private string ToLongString(State state, int barsInProgress, string instrumentName, string timeFrame, int currentBar)
    //{
    //    StringBuilder sb = new StringBuilder(_label);

    //    // Hide the label
    //    if (!IsVisible)
    //        return string.Empty;

    //    // Insert the state
    //    if (StateIsVisible) sb.Replace("{State}", state.ToLogString(FormatLength));
    //    else sb.Replace("{State}", string.Empty);

    //    //Insert the DataSeries.
    //    if (!BarsInfoIsVisible)
    //        sb.Replace("{[BarsInProgress,InstrumentName(TimeFrame),NumOfBar]}", string.Empty);
    //    else
    //    {
    //        string dataSeries = "[";
    //        // Insert bars in progress
    //        if (ShowBarsInProgress && (state == State.Historical || state == State.Transition || state == State.Realtime))
    //        {
    //            dataSeries += barsInProgress.ToLogString();
    //            if (ShowDataSerie || ShowNumOfBar) 
    //                dataSeries += ",";
    //        }
    //        else dataSeries += "*";

    //        // Insert DataSeries
    //        if (ShowDataSerie && (state == State.Historical || state == State.Transition || state == State.Realtime))
    //        {
    //            dataSeries += instrumentName + "(" + timeFrame + ")";
    //            if (ShowNumOfBar) dataSeries += ",";
    //        }
    //        // Replace Num of Bar
    //        if (ShowNumOfBar && (state == State.Historical || state == State.Transition || state == State.Realtime))
    //            dataSeries += currentBar.ToLogString();

    //        dataSeries += "]";

    //        sb.Replace("{[BarsInProgress,InstrumentName(TimeFrame),NumOfBar]}", dataSeries);
    //    }

    //    sb.Replace("{LabelSeparator}", LabelSeparator);

    //    return sb.ToString();
    //}
    //private string ToLongString(State state, int barsInProgress, Instrument instrument, BarsPeriod barsPeriod, int currentBar)
    //{
    //    StringBuilder sb = new StringBuilder(_label);

    //    // Hide the label
    //    if (!IsVisible)
    //        return string.Empty;

    //    // Insert the state
    //    if (StateIsVisible) sb.Replace("{State}", state.ToLogString(FormatLength));
    //    else sb.Replace("{State}", string.Empty);

    //    //Insert the DataSeries.
    //    if (!BarsInfoIsVisible)
    //        sb.Replace("{[BarsInProgress,InstrumentName(TimeFrame),NumOfBar]}", string.Empty);
    //    else
    //    {
    //        string dataSeries = "[";
    //        // Insert bars in progress
    //        if (ShowBarsInProgress && (state == State.Historical || state == State.Transition || state == State.Realtime))
    //        {
    //            dataSeries += barsInProgress.ToLogString();
    //            if (ShowDataSerie || ShowNumOfBar) dataSeries += ",";
    //        }
    //        else dataSeries += "*";

    //        // Insert DataSeries
    //        if (ShowDataSerie && (state == State.Historical || state == State.Transition || state == State.Realtime))
    //        {
    //            dataSeries += instrument.MasterInstrument.Name + "(" + barsPeriod.ToString(FormatType) + ")";
    //            if (ShowNumOfBar) dataSeries += ",";
    //        }
    //        // Replace Num of Bar
    //        if (ShowNumOfBar && (state == State.Historical || state == State.Transition || state == State.Realtime))
    //            dataSeries += currentBar.ToLogString();

    //        dataSeries += "]";

    //        sb.Replace("{[BarsInProgress,InstrumentName(TimeFrame),NumOfBar]}", dataSeries);
    //    }

    //    sb.Replace("{LabelSeparator}", LabelSeparator);

    //    return sb.ToString();
    //}

    #endregion
}
