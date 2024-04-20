using KrTrade.Nt.Core.Series;

namespace KrTrade.Nt.Services.Series
{
    public interface ISeries : IBaseSeries
    {
        void Configure(IBarsService barsService);
        void DataLoaded(IBarsService barsService);
    }

    public interface ISeries<T> : ISeries, ICache<T>
    {
    }
}
