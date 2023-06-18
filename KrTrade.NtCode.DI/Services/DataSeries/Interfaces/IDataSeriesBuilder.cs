using KrTrade.NtCode.DependencyInjection;
using KrTrade.NtCode.Options;

namespace KrTrade.NtCode.Services
{
    /// <summary>
    /// Represents default implementation of <see cref="DataSeriesProvider"/>.
    /// </summary>
    public interface IDataSeriesBuilder : IServiceProviderBuilder<DataSeriesProvider, DataSeriesBuilder, DataSeriesCollection, DataSeriesDescriptor,DataSeriesOptions>
    {
    }
}
