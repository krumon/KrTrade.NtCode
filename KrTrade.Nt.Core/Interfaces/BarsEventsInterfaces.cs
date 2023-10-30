namespace KrTrade.Nt.Core.Interfaces
{
    public interface INeedBarsService { }

    public interface IOnBarUpdateService : INeedBarsService
    {
        void OnBarUpdate();
    }
    public interface IOnLastBarRemovedService : INeedBarsService
    {
        void OnLastBarRemoved();
    }
    public interface IOnBarClosedService : INeedBarsService
    {
        void OnBarClosed();
    }
    public interface IOnEachTickService : INeedBarsService
    {
        void OnTick();
    }
    public interface IOnFirstTickService : INeedBarsService
    {
        void OnFirstTick();
    }
    public interface IOnPriceChangedService : INeedBarsService
    {
        void OnPriceChanged();
    }
}
