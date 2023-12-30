namespace KrTrade.Nt.Services
{
    public interface IStats : IBarUpdateService
    {
        BaseSeriesCache Value { get; }
        BaseSeriesCache Max { get; }
        BaseSeriesCache Min { get; }
        BaseSeriesCache Sum { get; }
        BaseSeriesCache Avg { get; }
        BaseSeriesCache StdDev { get; }

    }
}
