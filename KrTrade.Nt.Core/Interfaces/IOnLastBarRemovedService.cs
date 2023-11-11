namespace KrTrade.Nt.Core.Interfaces
{
    public interface IOnLastBarRemovedService : INeedBarsService
    {
        void OnLastBarRemoved();
    }
}
