using KrTrade.Nt.DI.Options;
using System;

namespace KrTrade.Nt.Scripts.Services
{
    public class ConfigureDataSeriesOptions : ConfigureOptions<DataSeriesOptions>
    {
        public ConfigureDataSeriesOptions(Action<DataSeriesOptions> action) : base(action)
        {
        }
    }
}
