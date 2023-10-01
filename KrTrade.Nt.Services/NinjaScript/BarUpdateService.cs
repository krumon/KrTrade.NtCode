using KrTrade.Nt.Core.Data;
using KrTrade.Nt.Core.Interfaces;
using NinjaTrader.Core.FloatingPoint;
using NinjaTrader.NinjaScript;
using System;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Represents the properties and methods when a bar is updated. 
    /// </summary>
    public class BarUpdateService : BaseNinjaScriptService<BarUpdateServiceOptions>, IBarUpdateService
    {

        private int[] _saveCurrentBars;
        private double[] _lastPrices;
        private double[] _currentPrices;

        private bool[] _isBarClosed;
        private bool[] _isLastBarRemoved;
        private bool[] _isTick;
        private bool[] _isPriceChanged;
        private bool[] _isFirstTick;
        private double[] _gap;

        private string _logText;

        public BarUpdateService(NinjaScriptBase ninjascript) : base(ninjascript) { }
        public BarUpdateService(NinjaScriptBase ninjascript, BarUpdateServiceOptions options) : base(ninjascript, options) { }
        public BarUpdateService(NinjaScriptBase ninjascript, Action<BarUpdateServiceOptions> delegateOptions) : base(ninjascript, delegateOptions) { }

        public bool BarClosed => IsBarClosed();
        public bool LastBarRemoved => IsLastBarRemoved();
        public bool Tick => IsFirstTick();
        public bool PriceChanged => IsPriceChanged();
        public double Gap => GapValue();
        public bool FirstTick => IsFirstTick();

        public override void Configure()
        {
        }
        public override void DataLoaded()
        {
            _saveCurrentBars = new int[NinjaScript.BarsArray.Length];
            _lastPrices = new double[NinjaScript.BarsArray.Length];
            _currentPrices = new double[NinjaScript.BarsArray.Length];
            _isBarClosed = new bool[NinjaScript.BarsArray.Length];
            _isLastBarRemoved = new bool[NinjaScript.BarsArray.Length];
            _isPriceChanged = new bool[NinjaScript.BarsArray.Length];
            _isTick = new bool[NinjaScript.BarsArray.Length];
            _isFirstTick = new bool[NinjaScript.BarsArray.Length];
            _gap = new double[NinjaScript.BarsArray.Length];
            
            InitializeArray(_saveCurrentBars, -1);
            InitializeArray(_lastPrices, 0);
            InitializeArray(_currentPrices, 0);
            InitializeArray(_isBarClosed, false);
            InitializeArray(_isLastBarRemoved, false);
            InitializeArray(_isPriceChanged, false);
            InitializeArray(_isTick, false);
            InitializeArray(_isFirstTick, false);
            InitializeArray(_gap, 0);
        }

        public override void OnBarUpdate()
        {
            //SetState(PriceState.Bar);
            double currentPrice = NinjaScript.Inputs[NinjaScript.BarsInProgress][0];
            int idx = NinjaScript.BarsInProgress;

            if (NinjaScript.CurrentBars[idx] < _saveCurrentBars[idx])
            {
                // Set properties
                SetArrayProperties(idx, true, false, false, false);
                // Call the methods
                LastBarRemovedHandler();
                return;
            }
            else if (NinjaScript.CurrentBars[idx] != _saveCurrentBars[idx])
            {
                SetArrayValue(idx,_isLastBarRemoved, false);
                SetArrayValue(idx, _isBarClosed, true);
                BarClosedHandler();
                if (_currentPrices[idx] != currentPrice)
                {
                    //SetState(PriceState.Price);
                    SetArrayValue(idx,_isPriceChanged, true);
                    SetArrayValue(idx, _gap, _currentPrices[idx] - currentPrice);
                    PriceChangedHandler(new PriceChangedEventArgs(_currentPrices[idx], currentPrice));
                }
                else
                {
                    SetArrayValue(idx, _isPriceChanged, false);
                    SetArrayValue(idx, _gap, 0);
                }
                if (NinjaScript.Calculate == Calculate.OnEachTick)
                {
                    //SetState(PriceState.Tick);
                    SetArrayValue(idx, _isTick, true);
                    SetArrayValue(idx, _isFirstTick, true);
                    TickHandler(new TickEventArgs(true));
                }
                else
                {
                    SetArrayValue(idx, _isTick, false);
                    SetArrayValue(idx, _isFirstTick, false);
                }
            }
            else
            {
                SetArrayValue(idx, _isLastBarRemoved, false);
                SetArrayValue(idx, _isBarClosed, false);
                if (_currentPrices[idx] != currentPrice)
                {
                    //SetState(PriceState.Price);
                    SetArrayValue(idx, _isPriceChanged, true);
                    SetArrayValue(idx, _gap, _currentPrices[idx] - currentPrice);
                    PriceChangedHandler(new PriceChangedEventArgs(_currentPrices[idx], currentPrice));
                }
                else
                {
                    SetArrayValue(idx, _isPriceChanged, false);
                    SetArrayValue(idx, _gap, 0);
                }
                if (NinjaScript.Calculate == Calculate.OnEachTick)
                {
                    //SetState(PriceState.Tick);
                    SetArrayValue(idx, _isTick, true);
                    SetArrayValue(idx, _isFirstTick, true);
                    TickHandler(new TickEventArgs(false));
                }
                else
                {
                    SetArrayValue(idx, _isTick, false);
                    SetArrayValue(idx, _isFirstTick, false);
                }
            }
            _saveCurrentBars[idx] = NinjaScript.CurrentBar;
            _currentPrices[idx] = currentPrice;
            NinjaScript.ClearOutputWindow();
            NinjaScript.Print(_logText);
            //_logText += Environment.NewLine;
            //SetState(PriceState.None);
        }

        public bool IsBarClosed(int barsInProgress = 0)
        {
            return !IsOutOfRange(barsInProgress) && _isBarClosed[barsInProgress];
        }
        public bool IsBarTick(int barsInProgress = 0)
        {
            //if (NinjaScript.Calculate != Calculate.OnEachTick)
            //    throw new InvalidOperationException("Invalid Operation: The calculate mode must be 'OnEachTick'.");
            return !IsOutOfRange(barsInProgress) && NinjaScript.Calculate == Calculate.OnEachTick && _isTick[barsInProgress];
        }
        public bool IsLastBarRemoved(int barsInProgress = 0)
        {
            return !IsOutOfRange(barsInProgress) && _isLastBarRemoved[barsInProgress];
        }
        public bool IsPriceChanged(int barsInProgress = 0)
        {
            //if (NinjaScript.Calculate == Calculate.OnBarClose)
            //    throw new InvalidOperationException("Invalid Operation: The calculate mode must be 'PriceChanged' or 'OnEachTick'.");
            return NinjaScript.Calculate != Calculate.OnBarClose && _isPriceChanged[barsInProgress];
        }
        public bool IsFirstTick(int barsInProgress = 0)
        {
            //if (NinjaScript.Calculate != Calculate.OnEachTick)
            //    throw new InvalidOperationException("Invalid Operation: The calculate mode must be 'OnEachTick'.");
            return NinjaScript.Calculate == Calculate.OnEachTick && _isFirstTick[barsInProgress];
        }
        public bool IsGap(int barsInProgress = 0, int ticks = 2)
        {
            return GapValue(barsInProgress).ApproxCompare(NinjaScript.TickSize*ticks) >= 0; 
        }
        public double GapValue(int barsInProgress = 0)
        {
            return Math.Abs(_gap[barsInProgress]); 
        }

        public virtual void OnLastBarRemoved() { }
        public virtual void OnBarClosed() { }
        public virtual void OnPriceChanged(PriceChangedEventArgs args) { }
        public virtual void OnEachTick() { }
        public virtual void OnFirstTick() { }
        public override string ToString()
        {
            return _logText;
        }

        private void LastBarRemovedHandler()
        {
            // Call to parent
            OnLastBarRemoved();
            //Set the log text
            _logText += " LastBarRemoved";
        }
        private void BarClosedHandler()
        {
            // Call to parent
            OnBarClosed();
            //Set the log text
            _logText = string.Empty;
            _logText += "[" + NinjaScript.BarsInProgress + "]: Price: " + NinjaScript.Inputs[NinjaScript.BarsInProgress][0] + "Events:";
            _logText += " BarClose";
        }
        private void PriceChangedHandler(PriceChangedEventArgs args)
        {
            // Make sure the arguments is not null.
            if (args == null)
                throw new ArgumentNullException("args");
            // Call to parent
            OnPriceChanged(args);
            //Set the log text
            _logText += " PriceChanged";
        }
        private void TickHandler(TickEventArgs args)
        {
            // Make sure the arguments is not null.
            if (args == null)
                throw new ArgumentNullException("args");
            // Call to parents
            if (args.IsFirstTick)
                OnFirstTick();
            // Call to parent
            OnEachTick();
            //Set the log text
            if (!args.IsFirstTick)
                _logText += " Tick";
            else
                _logText += " FirstTick";
        }
        private void InitializeArray(int[] array, int value)
        {
            for (int i = 0; i < array.Length; i++)
                array[i] = value;
        }
        private void InitializeArray(double[] array, double value)
        {
            for (int i = 0; i < array.Length; i++)
                array[i] = value;
        }
        private void InitializeArray(bool[] array, bool value)
        {
            for (int i = 0; i < array.Length; i++)
                array[i] = value;
        }
        private void InitializeArray(NinjaScriptEvent[] array, NinjaScriptEvent value)
        {
            for (int i = 0; i < array.Length; i++)
                array[i] = value;
        }
        private bool IsOutOfRange(int idx)
        {
            if (idx < 0 || idx >= NinjaScript.BarsArray.Length)
                throw new ArgumentOutOfRangeException(nameof(idx));
            return true;
        }
        private void SetArrayProperties(int idx, bool lastBarRemoved, bool barClosed, bool priceChanged, bool tick)
        {
            _isLastBarRemoved[idx] = lastBarRemoved;
            _isBarClosed[idx] = barClosed;
            _isPriceChanged[idx] = priceChanged;
            _isTick[idx] = tick;
        }
        private void SetArrayValue(int idx, bool[] array, bool value)
        {
            array[idx] = value;
        }
        private void SetArrayValue(int idx, double[] array, double value)
        {
            array[idx] = value;
        }

    }
}
