using KrTrade.Nt.Core.Data;
using NinjaTrader.NinjaScript;

namespace KrTrade.Nt.Core.Interfaces
{
    public interface IPrintLoggerService
    {
        INinjaScriptService Ninjascript { get; }

        void Configure();
        bool IsEnabled(LogLevel logLevel);

        void Open(int barsAgo = 0);
        void High(int barsAgo = 0);
        void Low(int barsAgo = 0);
        void Close(int barsAgo = 0);
        void Input(int barsAgo = 0);
        void OHLC(int barsAgo = 0, char separator = '-');

        void Text(object o);

    }
}
