namespace KrTrade.Nt.Core.Interfaces
{
    public interface IConfigureOrDataLoadedService 
    {
        bool IsServiceConfigured { get; }
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
