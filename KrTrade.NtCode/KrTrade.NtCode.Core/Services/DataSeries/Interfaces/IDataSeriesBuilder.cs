using Nt.Core.DependencyInjection;
using Nt.Core.Options;

namespace Nt.Core.Services
{
    /// <summary>
    /// Represents default implementation of <see cref="DataSeriesProvider"/>.
    /// </summary>
    public interface IDataSeriesBuilder : IServiceProviderBuilder<DataSeriesProvider, DataSeriesBuilder, DataSeriesCollection, DataSeriesDescriptor,DataSeriesOptions>
    {
    }
}
