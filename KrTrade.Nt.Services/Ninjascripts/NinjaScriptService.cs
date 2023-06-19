using KrTrade.Nt.Core.Interfaces;
using NinjaTrader.Core.FloatingPoint;
using NinjaTrader.NinjaScript;
using System;

namespace KrTrade.Nt.Services
{
    public class NinjaScriptService : INinjaScriptService
    {
        private readonly NinjaScriptBase _ninjascript;
        private int _saveCurrentBar;
        private double _lastPrice;
        private double _currentPrice;

        private NinjaScriptService(NinjaScriptBase ninjascript)
        {
            _ninjascript = ninjascript ?? throw new Exception("The ninjascript argument cannot be null. The argument is necesary to configure the service.");
        }

        public INinjaScriptService Configure(NinjaScriptBase ninjascript)
        {
            INinjaScriptService service = new NinjaScriptService(ninjascript);
            service.Configure();
            return new NinjaScriptService(ninjascript);
        }
        public void Configure()
        {
            _saveCurrentBar = -1;
            _lastPrice = _currentPrice = 0;
        }

        public NinjaScriptBase Ninjascript => _ninjascript;
        public bool IsBarClosed => _saveCurrentBar != Ninjascript.CurrentBar;
        public bool IsBarTick => !IsBarClosed;
        public bool IsLastBarRemoved => Ninjascript.BarsArray[0].BarsType.IsRemoveLastBarSupported && Ninjascript.CurrentBar < _saveCurrentBar;
        public bool IsPriceChanged => _lastPrice.ApproxCompare(_currentPrice) != 0;

        public void UpdateCurrentBar()
        {
            _saveCurrentBar = Ninjascript.CurrentBar;
        }
        public void UpdateCurrentBar(int currentBar)
        {
            _saveCurrentBar = currentBar;
        }
        public void UpdateCurrentPrice()
        {
            _lastPrice = _currentPrice;
            _currentPrice = Ninjascript.Close[0];
        }
        public void UpdateCurrentPrice(double currentPrice)
        {
            _lastPrice = _currentPrice;
            _currentPrice = currentPrice;
        }

    }
}
