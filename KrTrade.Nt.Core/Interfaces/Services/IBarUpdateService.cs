using KrTrade.Nt.Core.Data;
using NinjaTrader.NinjaScript;

namespace KrTrade.Nt.Core.Interfaces
{
    public interface IBarUpdateService : IBaseNinjaScriptService
    {

        bool BarClosed { get; }
        bool LastBarRemoved { get; }
        bool Tick { get; }
        bool PriceChanged { get; }
        double Gap { get; }
        bool FirstTick { get; }

        void OnLastBarRemoved();
        void OnBarClosed();
        void OnPriceChanged(PriceChangedEventArgs args);
        void OnEachTick();
        void OnFirstTick();

        bool IsBarClosed(int barsInProgress = 0);
        bool IsBarTick(int barsInProgress = 0);
        bool IsLastBarRemoved(int barsInProgress = 0);
        bool IsPriceChanged(int barsInProgress = 0);
        bool IsGap(int barsInProgress = 0, int ticks=2);
        bool IsFirstTick(int barsInProgress = 0);
        double GapValue(int barsInProgress);

        //void Update();
        //void Update(int currentBar, double currentPrice);

    }
}
