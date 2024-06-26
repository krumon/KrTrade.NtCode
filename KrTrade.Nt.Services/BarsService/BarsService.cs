﻿using KrTrade.Nt.Core.Bars;
using KrTrade.Nt.Core.Data;
using KrTrade.Nt.Services.Series;
using NinjaTrader.Core.FloatingPoint;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using KrTrade.Nt.Core.Helpers;
using KrTrade.Nt.Core.Services;
using KrTrade.Nt.Core.Series;
using KrTrade.Nt.Core.TradingHours;

namespace KrTrade.Nt.Services
{
    public class BarsService : BaseService<IBarsServiceInfo, IBarsServiceOptions>, IBarsService
    {

        #region Private members

        // Data series information control
        private bool _isPrimaryBars;

        // Data series control
        private int _lastBarIdx;
        private int _currentBarIdx;
        private double _lastPrice;
        private double _currentPrice;
        private bool _isRunning;
        // Events
        private Dictionary<BarEvent, bool> _barEvents;
        // Logging
        private List<string> _logLines;

        // Bars Series
        protected IBarsSeriesCollection BarsSeriesCollection;
        //// Access
        //private readonly Dictionary<int, string> _keys;

        #endregion

        #region Public properties

        public int CacheCapacity { get => Options.CacheCapacity; protected set { Options.CacheCapacity = value; } }
        public int RemovedCacheCapacity {  get => Options.RemovedCacheCapacity; protected set { Options.RemovedCacheCapacity = value; }}
        public int Index { get; internal set; } = -1;
        public bool IsWaitingFirstTick => false;

        public bool IsPrimaryBars 
        { 
            get => _isPrimaryBars; 
            internal set 
            {
                if (_isPrimaryBars == value) return;
                Info.SetNinjascriptValues(Ninjascript, 0);
                _isPrimaryBars = value;
            } 
        }
        public string InstrumentName => Info.InstrumentCode.ToName();
        public string TradingHoursName => Info.TradingHoursCode.ToName();
        public NinjaTrader.Data.BarsPeriod BarsPeriod => Info.TimeFrame.ToBarsPeriod();

        public double this[int index] => BarsSeriesCollection.Close[index];
        public ICurrentBarSeries CurrentBar => BarsSeriesCollection.CurrentBar;
        public ITimeSeries Time => BarsSeriesCollection.Time;
        public IPriceSeries Open => BarsSeriesCollection.Open;
        public IPriceSeries High => BarsSeriesCollection.High;
        public IPriceSeries Low => BarsSeriesCollection.Low;
        public IPriceSeries Close => BarsSeriesCollection.Close;
        public IVolumeSeries Volume => BarsSeriesCollection.Volume;
        public ITickSeries Tick => BarsSeriesCollection.Tick;

        public bool IsUpdated => IsConfigure && IsDataLoaded && _barEvents[BarEvent.Updated];
        public bool IsClosed => IsUpdated && _barEvents[BarEvent.Closed];
        public bool LastBarIsRemoved => IsUpdated && _barEvents[BarEvent.Removed];
        public bool IsTick => IsUpdated && _barEvents[BarEvent.Tick];
        public bool IsFirstTick => IsUpdated && _barEvents[BarEvent.FirstTick];
        public bool IsPriceChanged => IsUpdated && _barEvents[BarEvent.PriceChanged];

        public SeriesCollection Series {  get; set; }
        //public IndicatorCollection Indicators { get; private set; }
        //public StatsCollection Stats { get; private set; }
        //public FiltersCollection Filters { get; private set; }

        //public override string ToString(int tabOrder)
        //{
        //    string text = string.Empty;
        //    string tab = string.Empty;
        //    for (int i = 0; i < tabOrder; i++)
        //        tab += "\t";

        //    text += $"{tab}{Name}({ToString()})";
        //    tabOrder += 1;

        //    if (BarsSeriesCollection != null && BarsSeriesCollection.Count > 0)
        //    {
        //        text += Environment.NewLine;
        //        text += BarsSeriesCollection.ToString(tabOrder,0);
        //    }

        //    if (Series != null && Series.Count > 0)
        //    {
        //        text += Environment.NewLine;
        //        Series.ToString(tabOrder,0);
        //    }

        //    return text;
        //}

        public override string ToString() => Info.ToString();
        protected override string GetHeaderString() => "BARS";
        protected override string GetParentString() => null;
        protected override string GetDescriptionString() => ToString();
        protected override string GetLogString(string state)
            => ToLogString(
                tabOrder: 0,
                label: GetLabelString(
                    isLabelVisible: true,
                    isHeaderVisible: true,
                    isParentVisible: false,
                    isDescriptionVisible: true,
                    isDescriptionBracketsVisible: true,
                    isIndexVisible: false),
                state: state);

        //protected override string ToHeader() => Type.ToString();
        //protected override string ToDescription() => Info.ToString() ;
        //public override string ToLogString() => BarsSeriesCollection.ToLogString();
        //public override string ToString(int tabOrder) => BarsSeriesCollection.ToLogString(Name, tabOrder, Environment.NewLine, 0);

        public void LogState() => Log(Core.Logging.LogLevel.Information, ToLogState());
        private string ToLogState()
        {
            if (_logLines == null || _logLines.Count == 0)
                return string.Empty;
            string stateText = Name + ": ";
            for (int i = 0; i < _logLines.Count; i++)
            {
                stateText += _logLines[i];
                if (i < _logLines.Count - 1)
                    stateText += " - ";
            }

            return stateText;
        }
        
        #endregion

        #region Constructors

        internal BarsService(IService service) : this(service, null, new BarsServiceOptions()) { }
        internal BarsService(IService service, BarsServiceOptions options) : this(service, null, options) { }
        internal BarsService(IService service, BarsServiceInfo barsServiceInfo, BarsServiceOptions barsServiceOptions) : 
            base(service?.Ninjascript, service?.PrintService, barsServiceInfo, barsServiceOptions)
        {
            BarsSeriesCollection = new BarsSeriesCollection(this);
            Series = new SeriesCollection(
                barsService: this, 
                info: new SeriesCollectionInfo());
            //
            //Indicators = new IndicatorCollection(this);
            //Filters = new FiltersCollection(this);
            //Stats = new StatsCollection(this);
        }

        #endregion

        #region Implementation

        protected override ServiceType ToElementType() => ServiceType.BARS;

        protected override void Configure(out bool isConfigured)
        {

            _barEvents = new Dictionary<BarEvent, bool>()
            {
                [BarEvent.Updated] = false,
                [BarEvent.Removed] = false,
                [BarEvent.Closed] = false,
                [BarEvent.FirstTick] = false,
                [BarEvent.PriceChanged] = false,
                [BarEvent.Tick] = false
            };

            _logLines = new List<string>();
            _lastBarIdx = int.MinValue;
            _currentBarIdx = int.MinValue;
            _lastPrice = double.MinValue;
            _currentPrice = double.MinValue;

            if (Info == null)
                PrintService.LogError(new Exception("The 'DataSeriesOptions' must be loaded before the service will be configured."));

            // Configure series
            BarsSeriesCollection.Configure();
            Series?.Configure();
            //Indicators?.Configure();
            //Filters?.Configure();
            //Stats?.Configure();

            //isConfigured = Info != null && _barSeries.IsConfigure && Indicators.IsConfigure && Filters.IsConfigure && Stats.IsConfigure;

            isConfigured = Info != null && BarsSeriesCollection.IsConfigure && Series.IsConfigure;

        }
        protected override void DataLoaded(out bool isDataLoaded)
        {
            // TODO: Delete this line.
            Debugger.Break();

            for (int i = 0; i < Ninjascript.BarsArray.Length; i++)
                if (Info.EqualsTo(Ninjascript, i))
                {
                    Index = i;
                    break;
                }

            if (Index == -1)
                PrintService.LogError(new Exception($"{Name} cannot be loaded because the 'DataSeriesOptions' don't match with any 'NinjaScript.DataSeries'."));

            // Configure series.
            BarsSeriesCollection.DataLoaded();
            Series?.DataLoaded();
            //Indicators?.DataLoaded();
            //Filters?.DataLoaded();
            //Stats?.DataLoaded();

            isDataLoaded =
                Index != -1
                && BarsSeriesCollection.IsDataLoaded 
                && Series.IsDataLoaded;
                //&& Indicators != null ? Indicators.IsDataLoaded : true
                //&& Filters != null ? Filters.IsDataLoaded : true
                //&& Stats != null ? Stats.IsDataLoaded : true;
        }

        public void OnBarUpdate()
        {
            if (_isRunning && Ninjascript.BarsInProgress == Index)
                BarUpdate();
            else
            {
                if (!(IsConfigure && IsDataLoaded))
                    NinjascriptThrowHelpers.ThrowIsNotConfigureException(Name);

                if (!IsInRunningStates())
                    NinjascriptThrowHelpers.ThrowOutOfRunningStatesException(Name);

                _isRunning = true;
                if (Ninjascript.BarsInProgress == Index)
                    BarUpdate();
            }
        }
        public void BarUpdate()
        {
            if (!Options.IsEnable || Ninjascript.BarsInProgress != Index)
                return;

            //// Check FILTERS
            //Filters.Update();

            ResetBarsEvents();
            _currentBarIdx = Ninjascript.CurrentBars[Index];
            _currentPrice = Ninjascript.Closes[Index][0];

            // LasBarRemoved
            if (Ninjascript.BarsArray[Index].BarsType.IsRemoveLastBarSupported && _currentBarIdx < _lastBarIdx)
            {
                SetBarsEventValue(BarEvent.Removed, true);
                OnLastBarRemoved();
            }
            else
            {
                // BarClosed Or First tick success
                if (_currentBarIdx != _lastBarIdx)
                {
                    SetBarsEventValue(BarEvent.Closed, true);
                    OnBarClosed();

                    if (Ninjascript.Calculate != NinjaTrader.NinjaScript.Calculate.OnBarClose)
                    {
                        SetBarsEventValue(BarEvent.FirstTick, true);
                        if (_lastPrice.ApproxCompare(_currentPrice) != 0)
                            SetBarsEventValue(BarEvent.PriceChanged, true);

                        if (Ninjascript.Calculate == NinjaTrader.NinjaScript.Calculate.OnEachTick)
                            SetBarsEventValue(BarEvent.Tick, true);
                    }
                }

                // Tick Success
                else
                {
                    if (_lastPrice.ApproxCompare(_currentPrice) != 0)
                    {
                        SetBarsEventValue(BarEvent.PriceChanged, true);
                        OnPriceChanged();
                    }
                    if (Ninjascript.Calculate == NinjaTrader.NinjaScript.Calculate.OnEachTick)
                    {
                        SetBarsEventValue(BarEvent.Tick, true);
                        OnEachTick();
                    }
                }
            }
            SetBarsEventValue(BarEvent.Updated, true);
            LogState();

            ////// Check FILTERS
            ////Filters.Update();
            BarsSeriesCollection.BarUpdate();
            Series.BarUpdate();
            //Stats.Update();
            //Indicators.Update();

            SetBarsEventValue(BarEvent.Updated, false);
            _lastBarIdx = _currentBarIdx;
            _lastPrice = _currentPrice;
        }
        public void BarUpdate(IBarsService updatedBarsSeries)
        {
            BarsSeriesCollection.BarUpdate(updatedBarsSeries);
            Series.BarUpdate(updatedBarsSeries);
        }
        public void MarketData(NinjaTrader.Data.MarketDataEventArgs args)
        {
            BarsSeriesCollection.MarketData(args);
            Series.MarketData(args);
        }
        public void MarketData(IBarsService updatedBarsSeries)
        {
            BarsSeriesCollection.MarketData(updatedBarsSeries);
            Series.MarketData(updatedBarsSeries);
        }
        public void MarketDepth(NinjaTrader.Data.MarketDepthEventArgs args)
        {
            BarsSeriesCollection.MarketDepth(args);
            Series.MarketDepth(args);
        }
        public void MarketDepth(IBarsService updatedBarsSeries)
        {
            BarsSeriesCollection.MarketDepth(updatedBarsSeries);
            Series.MarketDepth(updatedBarsSeries);
        }
        public void Render()
        {
            throw new NotImplementedException();
        }
        public void Render(IBarsService updatedBarsSeries)
        {
            throw new NotImplementedException();
        }

        public Bar GetBar(int barsAgo) => BarsSeriesCollection.GetBar(barsAgo);
        public Bar GetBar(int barsAgo, int period) => BarsSeriesCollection.GetBar(barsAgo, period);
        public IList<Bar> GetBars(int barsAgo, int period) => BarsSeriesCollection.GetBars(barsAgo, period);

        #endregion

        #region Internal methods

        internal void AddSeries(IInputSeriesInfo info)
        {
            try
            {
                if (info == null)
                    throw new ArgumentNullException(nameof(info));

                // El servicio no existe
                if (!Series.ContainsKey(info.Key))
                {
                    INumericSeries series = GetOrAddSeries(info) ?? throw new Exception($"The series with key:{info.Key} could not be added to the collection");
                }
            }
            catch (Exception e)
            {
                throw new Exception($"The series with key:{info.Key} could not be added to the collection.", e);
            }
        }
        internal void AddSeries<TInfo>(TInfo info)
            where TInfo : IInputSeriesInfo
        {
            try
            {
                if (info == null)
                    throw new ArgumentNullException(nameof(info));

                // El servicio no existe
                if (!Series.ContainsKey(info.Key))
                {
                    INumericSeries series = GetOrAddSeries(info) ?? throw new Exception($"The series with key:{info.Key} could not be added to the collection");
                }
            }
            catch (Exception e)
            {
                throw new Exception($"The series with key:{info.Key} could not be added to the collection.", e);
            }
        }

        internal void TryAdd(IInputSeriesInfo info)
        {
            try
            {
                AddSeries(info);
            }
            catch
            {
                PrintService.LogWarning($"The series with key:{info.Key} could not be added to the collection");
            }
        }
        internal void TryAdd<TInfo>(TInfo info)
            where TInfo : IInputSeriesInfo
        {
            try
            {
                AddSeries(info);
            }
            catch
            {
                PrintService.LogWarning($"The series with key:{info.Key} could not be added to the collection");
            }
        }

        internal INumericSeries GetOrAddSeries(ISeriesInfo info)
        {
            if (info == null)
                return default;

            if (Series.ContainsKey(info.Key))
                return Series[info.Key];
            else if (BarsSeriesCollection.ContainsKey(info.Key))
            {
                switch(info.Type)
                {
                    //case SeriesNames.INPUT:
                    //    return Input;
                    case SeriesType.OPEN:
                        return Open;
                    case SeriesType.HIGH:
                        return High;
                    case SeriesType.LOW:
                        return Low;
                    case SeriesType.CLOSE:
                        return Close;
                    case SeriesType.VOLUME:
                        return Volume;
                    case SeriesType.TICK:
                        return Tick;
                    case SeriesType.CURRENT_BAR:
                    case SeriesType.TIME:
                        return default;
                }
            }
            INumericSeries series = null;

            if (info is IInputSeriesInfo seriesInfo)
                series = CreateSeries(info.Type, seriesInfo.Inputs);

            if (series == null)
                return null;
            Series.Add(series);
            return Series[info.Key];
        }

        internal INumericSeries GetOrAddSeries<TInfo>(TInfo info)
            where TInfo : IInputSeriesInfo
        {
            if (info == null)
                return default;

            if (Series.ContainsKey(info.Key))
                return Series[info.Key];
            else if (BarsSeriesCollection.ContainsKey(info.Key))
            {
                switch(info.Type)
                {
                    //case SeriesNames.INPUT:
                    //    return Input;
                    case SeriesType.OPEN:
                        return Open;
                    case SeriesType.HIGH:
                        return High;
                    case SeriesType.LOW:
                        return Low;
                    case SeriesType.CLOSE:
                        return Close;
                    case SeriesType.VOLUME:
                        return Volume;
                    case SeriesType.TICK:
                        return Tick;
                    case SeriesType.CURRENT_BAR:
                    case SeriesType.TIME:
                        return default;
                }
            }
            INumericSeries series = CreateSeries(info.Type, info.Inputs);
            if (series == null)
                return null;
            Series.Add(series);
            return Series[info.Key];
        }

        private INumericSeries CreateSeries(SeriesType type, List<ISeriesInfo> inputs)
        {
            switch (type)
            {
                case SeriesType.MAX:
                    if (inputs == null || inputs.Count == 0)
                    {
                        PrintService.LogWarning(
                            $"The {type} series needs one Input series to calculate the values. " +
                            $"The {type} series could not be created.");
                        return default;
                    }
                    else if (inputs.Count > 1)
                    {
                        PrintService.LogWarning(
                            $"The {type} series only needs one Input series to calculate the values. " +
                            $"The rest of the series will be eliminated and will not be taken into consideration.");
                        inputs.RemoveRange(1, inputs.Count - 2);
                    }
                    if (inputs[0] is PeriodSeriesInfo maxInfo)
                        return new MaxSeries(this, maxInfo);
                    else
                    {
                        PrintService.LogWarning(
                            $"The {type} series needs 'PeriodSeriesInfo' information to be created." +
                            $"The {type} series could not be created.");
                        return default;
                    }
                case SeriesType.MIN:
                    if (inputs == null || inputs.Count == 0)
                    {
                        PrintService.LogWarning(
                            $"The {type} series needs one Input series to calculate the values. " +
                            $"The {type} series could not be created.");
                        return null;
                    }
                    else if (inputs.Count > 1)
                    {
                        PrintService.LogWarning(
                            $"The {type} series only needs one Input series to calculate the values. " +
                            $"The rest of the series will be eliminated and will not be taken into consideration.");
                        inputs.RemoveRange(1, inputs.Count - 2);
                    }
                    if (inputs[0] is PeriodSeriesInfo minInfo)
                        return new MinSeries(this, minInfo);
                    else
                    {
                        PrintService.LogWarning(
                            $"The {type} series needs 'PeriodSeriesInfo' information to be created." +
                            $"The {type} series could not be created.");
                        return null;
                    }
                case SeriesType.SUM:
                    if (inputs == null || inputs.Count == 0)
                    {
                        PrintService.LogWarning(
                            $"The {type} series needs one Input series to calculate the values. " +
                            $"The {type} series could not be created.");
                        return null;
                    }
                    else if (inputs.Count > 1)
                    {
                        PrintService.LogWarning(
                            $"The {type} series only needs one Input series to calculate the values. " +
                            $"The rest of the series will be eliminated and will not be taken into consideration.");
                        inputs.RemoveRange(1, inputs.Count - 2);
                    }
                    if (inputs[0] is PeriodSeriesInfo sumInfo)
                        return new SumSeries(this, sumInfo);
                    else
                    {
                        PrintService.LogWarning(
                            $"The {type} series needs 'PeriodSeriesInfo' information to be created." +
                            $"The {type} series could not be created.");
                        return null;
                    }
                case SeriesType.AVG:
                    if (inputs == null || inputs.Count == 0)
                    {
                        PrintService.LogWarning(
                            $"The {type} series nedds one Input series to calculate the values. " +
                            $"The {type} series could not be created.");
                        return null;
                    }
                    else if (inputs.Count > 1)
                    {
                        PrintService.LogWarning(
                            $"The {type} series only nedds one Input series to calculate the values. " +
                            $"The rest of the series will be eliminated and will not be taken into consideration.");
                        inputs.RemoveRange(1, inputs.Count - 2);
                    }
                    if (inputs[0] is PeriodSeriesInfo avgInfo)
                        return new AvgSeries(this, avgInfo);
                    else
                    {
                        PrintService.LogWarning(
                            $"The {type} series needs 'PeriodSeriesInfo' information to be created." +
                            $"The {type} series could not be created.");
                        return null;
                    }
                case SeriesType.RANGE:
                    if (inputs == null || inputs.Count < 2)
                    {
                        PrintService.LogWarning(
                            $"The {type} series needs two Input series to calculate the values. " +
                            $"The {type} series could not be created.");
                        return null;
                    }
                    else if (inputs.Count > 2)
                    {
                        PrintService.LogWarning(
                            $"The {type} series only needs two Input series to calculate the values. " +
                            $"The rest of the series will be eliminated and will not be taken into consideration.");
                        inputs.RemoveRange(1, inputs.Count - 2);
                    }
                    if (inputs[0] is PeriodSeriesInfo rangeInfo)
                        return new RangeSeries(this, rangeInfo);
                    else
                    {
                        PrintService.LogWarning(
                            $"The {type} series needs 'PeriodSeriesInfo' information to be created." +
                            $"The {type} series could not be created.");
                        return null;
                    }
                default:
                    throw new NotImplementedException();
            }
        }

        #endregion

        #region Virtual methods

        protected virtual void OnLastBarRemoved() { }
        protected virtual void OnBarClosed() { }
        protected virtual void OnPriceChanged() { }
        protected virtual void OnEachTick() { }

        #endregion

        #region Private methods

        private void SetBarsEvents(bool updated, bool isLastBarRemoved, bool isBarClosed, bool isFirstTick, bool isPriceChanged, bool isNewTick)
        {
            _barEvents[BarEvent.Updated] = updated;
            _barEvents[BarEvent.Removed] = isLastBarRemoved;
            _barEvents[BarEvent.Closed] = isBarClosed;
            _barEvents[BarEvent.FirstTick] = isFirstTick;
            _barEvents[BarEvent.PriceChanged] = isPriceChanged;
            _barEvents[BarEvent.Tick] = isNewTick;
        }
        private void ResetBarsEvents()
        {
            SetBarsEvents(
                updated: false,
                isLastBarRemoved: false,
                isBarClosed: false,
                isFirstTick: false,
                isPriceChanged: false,
                isNewTick: false
                );
            _logLines.Clear();
        }
        private void SetBarsEventValue(BarEvent barsEvent, bool value)
        {
            _barEvents[barsEvent] = value;

            if (PrintService == null)
                return;

            if (PrintService.IsLogLevelsEnable(Core.Logging.LogLevel.Information) && Options.IsLogEnable)
                _logLines.Add(barsEvent.ToString());
        }

        #endregion

    }
}
