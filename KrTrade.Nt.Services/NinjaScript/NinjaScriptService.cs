using KrTrade.Nt.Core.Interfaces;
using NinjaTrader.NinjaScript;
using System;

namespace KrTrade.Nt.Services
{
    public class NinjaScriptService : INinjaScriptService
    {
        private readonly NinjaScriptBase _ninjascript;
        private readonly double _minGapValue;
        private int[] _saveCurrentBars;
        private double[] _lastPrices;
        private double[] _currentPrices;

        private NinjaScriptService(NinjaScriptBase ninjascript, double minGapValue)
        {
            _ninjascript = ninjascript ?? throw new Exception("The ninjascript argument cannot be null. The argument is necesary to configure the service.");
            _minGapValue = minGapValue;
        }

        public static INinjaScriptService Configure(NinjaScriptBase ninjascript, int minGapSize = 2)
        {
            if (minGapSize < 2) minGapSize = 2;
            double minGapValue = ninjascript.TickSize * (double)minGapSize;
            INinjaScriptService service = new NinjaScriptService(ninjascript, minGapValue);
            service.Configure();
            return service;
        }
        public void Configure()
        {
        }

        public void DataLoaded()
        {
            _saveCurrentBars = new int[Ninjascript.BarsArray.Length];
            _lastPrices = new double[Ninjascript.BarsArray.Length];
            _currentPrices = new double[Ninjascript.BarsArray.Length];
            InitializeArray(_saveCurrentBars, -1);
            InitializeArray(_lastPrices, 0);
            InitializeArray(_currentPrices, 0);
        }

        public NinjaScriptBase Ninjascript => _ninjascript;
        public bool BarClosed => IsBarClosed(Ninjascript.BarsInProgress);
        public bool BarTick => !BarClosed;
        public bool LastBarRemoved => IsLastBarRemoved(Ninjascript.BarsInProgress);
        public bool PriceChanged => IsPriceChanged(Ninjascript.BarsInProgress);
        public bool Gap => IsGap(Ninjascript.BarsInProgress);
        public bool FirstTick => IsFirstTick(Ninjascript.BarsInProgress);

        public void Update()
        {
            UpdateCurrentBar();
            UpdateCurrentPrice();
        }
        public void Update(int currentBar, double currentPrice)
        {
            UpdateCurrentBar(currentBar);
            UpdateCurrentPrice(currentPrice);
        }
        public bool IsBarClosed(int barsInProgress = 0) =>  _saveCurrentBars[barsInProgress] != Ninjascript.CurrentBars[barsInProgress];
        public bool IsBarTick(int barsInProgress = 0) => !IsBarClosed(barsInProgress);
        public bool IsLastBarRemoved(int barsInProgress = 0) => Ninjascript.BarsArray[0].BarsType.IsRemoveLastBarSupported && Ninjascript.CurrentBars[barsInProgress] < _saveCurrentBars[barsInProgress];
        public bool IsPriceChanged(int barsInProgress = 0) => _lastPrices[barsInProgress] != _currentPrices[barsInProgress];
        public bool IsGap(int barsInProgress = 0) => IsPriceChanged(barsInProgress) && _currentPrices[barsInProgress] - _lastPrices[barsInProgress] >= _minGapValue;
        public bool IsFirstTick(int barsInProgress = 0) => Ninjascript.Calculate == Calculate.OnEachTick && IsBarTick(barsInProgress);

        private void UpdateCurrentBar()
        {
            if (_saveCurrentBars[Ninjascript.BarsInProgress] != Ninjascript.CurrentBars[Ninjascript.BarsInProgress])
                _saveCurrentBars[Ninjascript.BarsInProgress] = Ninjascript.CurrentBars[Ninjascript.BarsInProgress];
        }
        private void UpdateCurrentBar(int currentBar)
        {
            if (_saveCurrentBars[Ninjascript.BarsInProgress] != currentBar)
                _saveCurrentBars[Ninjascript.BarsInProgress] = currentBar;
        }
        private void UpdateCurrentPrice()
        {
            _lastPrices[Ninjascript.BarsInProgress] = _currentPrices[Ninjascript.BarsInProgress];
            _currentPrices[Ninjascript.BarsInProgress] = Ninjascript.Inputs[Ninjascript.BarsInProgress][0];
        }
        private void UpdateCurrentPrice(double currentPrice)
        {
            _lastPrices[Ninjascript.BarsInProgress] = _currentPrices[Ninjascript.BarsInProgress];
            _currentPrices[Ninjascript.BarsInProgress] = currentPrice;
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
    }
}
