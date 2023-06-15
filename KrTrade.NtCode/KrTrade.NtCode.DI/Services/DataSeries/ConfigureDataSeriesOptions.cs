using Nt.Core.Options;
using System;

namespace Nt.Core.Services
{
    public class ConfigureDataSeriesOptions : ConfigureOptions<DataSeriesOptions>
    {
        public ConfigureDataSeriesOptions(Action<DataSeriesOptions> action) : base(action)
        {
        }
    }
}
