using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrTrade.NtCode.Configuration
{
    internal static class InternalConfigurationRootExtensions
    {
        //// <summary>
        /// In fact, it is to get all the values ​​of the Key including the given Path in the data source dictionary
        /// </summary>
        internal static IEnumerable<IConfigurationSection> GetChildrenImplementation(this IConfigurationRoot root, string path)
        {
            return root.Providers
                .Aggregate(Enumerable.Empty<string>(),
                    (seed, source) => source.GetChildKeys(seed, path))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .Select(key => root.GetSection(path == null ? key : ConfigurationPath.Combine(path, key)));
        }
    }
}
