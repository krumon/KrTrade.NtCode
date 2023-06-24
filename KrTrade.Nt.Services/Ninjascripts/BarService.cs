using KrTrade.Nt.Core.Interfaces;
using NinjaTrader.Core.FloatingPoint;
using NinjaTrader.NinjaScript;
using System;

namespace KrTrade.Nt.Services
{
    public class BarService : IBarService
    {
        private readonly NinjaScriptBase _ninjascript;
        private int _saveCurrentBar;
        private double _lastPrice;
        private double _currentPrice;
        private double _minGapValue;

        private BarService(NinjaScriptBase ninjascript, double minGapValue)
        {
            _ninjascript = ninjascript ?? throw new Exception("The ninjascript argument cannot be null. The argument is necesary to configure the service.");
            _minGapValue = minGapValue;
        }

        public static IBarService Configure(NinjaScriptBase ninjascript, int minGapSize = 2)
        {
            if (minGapSize < 2) minGapSize = 2;
            double minGapValue = ninjascript.TickSize * (double)minGapSize;
            IBarService service = new BarService(ninjascript, minGapValue);
            service.Configure();
            return service;
        }
        public void Configure()
        {
            _saveCurrentBar = -1;
            _lastPrice = _currentPrice = 0;
        }

        public NinjaScriptBase Ninjascript => _ninjascript;
        public bool Closed => _saveCurrentBar != Ninjascript.CurrentBar;
        public bool Tick => !Closed;
        public bool Removed => Ninjascript.BarsArray[0].BarsType.IsRemoveLastBarSupported && Ninjascript.CurrentBar < _saveCurrentBar;
        public bool PriceChanged => _lastPrice != _currentPrice;
        public bool Gap => PriceChanged && _currentPrice - _lastPrice >= _minGapValue;
        public bool FirstTick => Ninjascript.Calculate == Calculate.OnEachTick && Tick;

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

        private void UpdateCurrentBar()
        {
            if(_saveCurrentBar != Ninjascript.CurrentBar)
                _saveCurrentBar = Ninjascript.CurrentBar;
        }
        private void UpdateCurrentBar(int currentBar)
        {
            if (_saveCurrentBar != currentBar)
                _saveCurrentBar = currentBar;
        }
        private void UpdateCurrentPrice()
        {
            _lastPrice = _currentPrice;
            _currentPrice = Ninjascript.Input[0];
        }
        private void UpdateCurrentPrice(double currentPrice)
        {
            _lastPrice = _currentPrice;
            _currentPrice = currentPrice;
        }

    }
}
