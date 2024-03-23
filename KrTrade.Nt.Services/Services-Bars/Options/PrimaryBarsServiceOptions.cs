using KrTrade.Nt.Core.Caches;
using KrTrade.Nt.Core.Data;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Define the primary <see cref="IBarsService"/> options. 
    /// </summary>
    public class PrimaryBarsServiceOptions : BarUpdateServiceOptions
    {

        /// <summary>
        /// Indicates the data series is primary. If the data series is primary cannot configure <see cref="InstrumentCode"/>, 
        /// <see cref="TradingHoursCode"/>, <see cref="TimeFrame"/> and <see cref="MarketDataType"/>.
        /// </summary>
        public bool IsPrimaryDataSeries { get; protected set; }

        /// <summary>
        /// Gets or sets the bars cache capacity.
        /// </summary>
        public int CacheCapacity { get; set; } = Cache.DEFAULT_CAPACITY;

        /// <summary>
        /// Gets or sets the bars removed cache capacity.
        /// </summary>
        public int RemovedCacheCapacity { get; set; } = Cache.DEFAULT_OLD_VALUES_CAPACITY;

        /// <summary>
        /// Create <see cref="PrimaryBarsServiceOptions"/> default instance.
        /// </summary>
        public PrimaryBarsServiceOptions()
        {
                IsPrimaryDataSeries = true;
        }
    }
}
