using NinjaTrader.NinjaScript;

namespace KrTrade.Nt.Core.Interfaces
{
    public interface INinjaScriptService
    {
        NinjaScriptBase Ninjascript { get; }

        INinjaScriptService Configure(NinjaScriptBase ninjascript);
        void Configure();

        bool IsBarClosed { get; }
        bool IsBarTick { get; }
        bool IsLastBarRemoved { get; }
        bool IsPriceChanged { get; }

        void UpdateCurrentBar();
        void UpdateCurrentBar(int currentBar);
        void UpdateCurrentPrice();
        void UpdateCurrentPrice(double currentPrice);

    }
}
