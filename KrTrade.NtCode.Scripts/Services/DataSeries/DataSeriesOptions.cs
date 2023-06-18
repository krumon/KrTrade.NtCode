using KrTrade.Nt.DI.Services;
using System;
using System.Collections.Generic;

namespace KrTrade.Nt.Scripts.Services
{
    public class DataSeriesOptions
    {
        /// <summary>
        /// The key to the json configure file.
        /// </summary>
        public const string Key = "IndicatorsInfo:DataSeries";

        /// <summary>
        /// The <see cref="DataSeriesDescriptor"/> collection
        /// </summary>
        public Dictionary<string, DataSeriesDescriptor> DataSeriesDescriptors { get; set; } = new Dictionary<string, DataSeriesDescriptor>(StringComparer.OrdinalIgnoreCase);
    }
}
