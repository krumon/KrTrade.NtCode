namespace KrTrade.Nt.Core.Interfaces
{
    public interface IKrBaseIndicator
    {
        INinjaScriptService Ninjascript { get; }
        void SetDefaults();
        void Configure();
        void DataLoaded();
        void AddPlots();
        void AddDataSeries();
        void Dispose();
        void OnBarClosed();
        void OnEachTick();
        void OnPriceChanged();
    }
}
