namespace KrTrade.Nt.Core.Interfaces
{
    public interface IConfigureOrDataLoadedService 
    {
        bool IsConfigured { get; }
    }

    public interface IConfigureService : IConfigureOrDataLoadedService
    {
        void Configure();
    }
    public interface IDataLoadedService : IConfigureOrDataLoadedService
    {
        void DataLoaded();
    }
}
