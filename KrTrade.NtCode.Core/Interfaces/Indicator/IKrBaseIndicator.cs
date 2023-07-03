namespace KrTrade.Nt.Core.Interfaces
{
    public interface IKrBaseIndicator
    {
        INinjaScriptService Bar { get; }
        void SetDefaults();
        void Configure();
        void DataLoaded();
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
