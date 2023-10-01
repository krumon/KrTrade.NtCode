namespace KrTrade.Nt.Core.Interfaces
{
    public interface IKrBaseIndicator : IBaseNinjaScript
    {
        INinjaScriptService Bar { get; }
        void AddPlots();
        void AddDataSeries();
        void Dispose();
        void OnBarClosed();
        void OnEachTick();
        void OnPriceChanged();
        void OnGap();
        void OnLastBarRemoved();

    }
}
