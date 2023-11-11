namespace KrTrade.Nt.Core.Interfaces
{
    public interface IOnPriceChangedService : INeedBarsService
    {
        void OnPriceChanged();
    }
}
