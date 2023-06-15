using KrTrade.NtCode.Options;
using System;

namespace KrTrade.NtCode.Services
{
    public class ConfigureDataSeriesOptions : ConfigureOptions<DataSeriesOptions>
    {
        public ConfigureDataSeriesOptions(Action<DataSeriesOptions> action) : base(action)
        {
        }
    }
}
