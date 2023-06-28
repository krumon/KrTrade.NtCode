using NinjaTrader.NinjaScript;

namespace KrTrade.Nt.Core.Interfaces
{
    public interface INinjaScriptService
    {

        NinjaScriptBase Ninjascript { get; }

        void Configure();
        void DataLoaded();

        bool BarClosed { get; }
        bool BarTick { get; }
        bool LastBarRemoved { get; }
        bool PriceChanged { get; }
        bool Gap { get; }
        bool FirstTick { get; }

        bool IsBarClosed(int barsInProgress = 0);
        bool IsBarTick(int barsInProgress = 0);
        bool IsLastBarRemoved(int barsInProgress = 0);
        bool IsPriceChanged(int barsInProgress = 0);
        bool IsGap(int barsInProgress = 0);
        bool IsFirstTick(int barsInProgress = 0);

        void Update();
        void Update(int currentBar, double currentPrice);

    }
}
