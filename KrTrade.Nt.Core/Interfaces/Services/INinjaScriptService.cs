using KrTrade.Nt.Core.Data;

namespace KrTrade.Nt.Core.Interfaces
{
    public interface INinjaScriptService : IBaseNinjaScript
    {

        //bool BarClosed { get; }
        //bool BarTick { get; }
        //bool LastBarRemoved { get; }
        //bool PriceChanged { get; }
        //bool Gap { get; }
        //bool FirstTick { get; }

        void OnBarUpdate();
        void OnLastBarRemoved();
        void OnBarClosed();
        void OnPriceChanged(PriceChangedEventArgs args);
        void OnEachTick();
        void OnFirstTick();

        //bool IsBarClosed(int barsInProgress = 0);
        //bool IsBarTick(int barsInProgress = 0);
        //bool IsLastBarRemoved(int barsInProgress = 0);
        //bool IsPriceChanged(int barsInProgress = 0);
        //bool IsGap(int barsInProgress = 0);
        //bool IsFirstTick(int barsInProgress = 0);

        //void Update();
        //void Update(int currentBar, double currentPrice);

    }
}
