using NinjaTrader.NinjaScript;

namespace KrTrade.Nt.Core.Interfaces
{
    public interface IBarService
    {
        NinjaScriptBase Ninjascript { get; }

        void Configure();

        bool Closed { get; }
        bool Tick { get; }
        bool Removed { get; }
        bool PriceChanged { get; }
        bool Gap { get; }
        bool FirstTick { get; }

        void Update();
        void Update(int currentBar, double currentPrice);

    }
}
