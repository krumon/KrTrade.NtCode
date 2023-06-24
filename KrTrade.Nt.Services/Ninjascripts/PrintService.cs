using KrTrade.Nt.Core.Interfaces;
using NinjaTrader.NinjaScript;
using System;

namespace KrTrade.Nt.Services
{
    public class PrintService : IPrintService
    {
        private const int maxHistoricalBarsToPrint = 500;
        private const int minHistoricalBarsToPrint = 1;

        private readonly NinjaScriptBase _ninjascript;
        private readonly int _historicalBarsToPrint = 10;
        private readonly bool _historicalIsEnabled = true;
        private readonly bool _realTimeIsEnable = true;
        private int _count;

        private PrintService(NinjaScriptBase ninjascript)
        {
            _ninjascript = ninjascript ?? throw new Exception("The ninjascript argument cannot be null. The argument is necesary to configure the service.");
        }
        private PrintService(NinjaScriptBase ninjascript, int maxHistoricalBarsToPrint, bool historicalIsEnabled, bool realTimeIsEnabled)
        {
            _ninjascript = ninjascript ?? throw new Exception("The ninjascript argument cannot be null. The argument is necesary to configure the service.");
            _historicalBarsToPrint = maxHistoricalBarsToPrint;
            _historicalIsEnabled= historicalIsEnabled;
            _realTimeIsEnable= realTimeIsEnabled;
        }
        public static IPrintService Configure(NinjaScriptBase ninjascript)
        {
            IPrintService service = new PrintService(ninjascript);
            service.Configure();
            return service;
        }
        public static IPrintService Configure(NinjaScriptBase ninjascript, int historicalBarsToPrint, bool historicalIsEnabled, bool realTimeIsEnabled)
        {
            historicalBarsToPrint = 
                historicalBarsToPrint < minHistoricalBarsToPrint ? minHistoricalBarsToPrint : 
                historicalBarsToPrint > maxHistoricalBarsToPrint ? maxHistoricalBarsToPrint : 
                historicalBarsToPrint;
            IPrintService service = new PrintService(ninjascript, historicalBarsToPrint, historicalIsEnabled, realTimeIsEnabled);
            service.Configure();
            return service;
        }

        public NinjaScriptBase Ninjascript => _ninjascript;
        public bool IsEnabled 
        { 
            get 
            {
                bool isEnable = true;
                if (_ninjascript.State == State.Historical && (!_historicalIsEnabled || _count > _historicalBarsToPrint)) isEnable = false;
                if (_ninjascript.State == State.Realtime && !_realTimeIsEnable) isEnable = false;

                return isEnable;
            } 
        }

        public void Configure()
        {
            _count = 0;
        }

        public void Open(int barsAgo = 0) => Print(OpenText(barsAgo));
        public void High(int barsAgo = 0) => Print(HighText(barsAgo));
        public void Low(int barsAgo = 0) => Print(LowText(barsAgo));
        public void Close(int barsAgo = 0) => Print(CloseText(barsAgo));
        public void Input(int barsAgo = 0) => Print(InputText(barsAgo));
        public void Text(object o) => Print(o);
        public void OHLC(int barsAgo = 0, char separator = '-') => Print(OhlcText(separator, barsAgo));

        private void Print(object o) 
        {
            if (!IsEnabled)
                return;
            _ninjascript.Print(o);
            _count++;
        }
        private string OpenText(int barsAgo) => "Open: " + _ninjascript.Open[barsAgo];
        private string HighText(int barsAgo) => "High: " + _ninjascript.High[barsAgo];
        private string LowText(int barsAgo) => "Low: " + _ninjascript.Low[barsAgo];
        private string CloseText(int barsAgo) => "Close: " + _ninjascript.Close[barsAgo];
        private string InputText(int barsAgo) => "Input: " + _ninjascript.Input[barsAgo];
        private string OhlcText(char separator = '-', int barsAgo = 0)
        {
            return
                OpenText(barsAgo) + separator +
                HighText(barsAgo) + separator +
                LowText(barsAgo) + separator +
                CloseText(barsAgo);
        }
    }
}
