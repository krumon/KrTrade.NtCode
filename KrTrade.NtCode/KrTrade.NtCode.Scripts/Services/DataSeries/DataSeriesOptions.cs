using System;
using System.Collections.Generic;

namespace KrTrade.NtCode.Services
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
