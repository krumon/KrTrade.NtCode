﻿namespace KrTrade.Nt.Core.Data
{
    public enum ElementType
    {
        // UNKNOWN TYPE
        UNKNOWN,
        // SERVICES TYPE
        BARS_MANAGER_SERVICE,
        BARS_CACHE_SERVICE,
        BARS_SERVICE,
        PRINT_SERVICE,
        PLOT_SERVICE,
        // SERIES TYPE
        MEDIAN_SERIES,
        TYPICAL_SERIES,
        WEIGHTED_SERIES,
        // BARS SERIES TYPE
        CURRENT_BAR_SERIES,
        TIME_SERIES,
        VOLUME_SERIES,
        TICK_SERIES,
        INPUT_SERIES,
        OPEN_SERIES,
        HIGH_SERIES,
        LOW_SERIES,
        CLOSE_SERIES,
        // PERIOD SERIES TYPE
        MAX_SERIES,
        MIN_SERIES,
        SUM_SERIES,
        AVG_SERIES,
        DEVSTD_SERIES,
        RANGE_SERIES,
        // STRENGTH SERIES TYPE
        SWING_HIGH_SERIES,
        SWING_LOW_SERIES,
        // COLLECTION SERVICE TYPE
        BARS_SERVICE_COLLECTION,
        // COLLECTION SERIES TYPE
        BARS_SERIES_COLLECTION,
        SERIES_COLLECTION,
        INDICATORS_SERIES_COLLECTION,
        STATS_SERIES_COLLECTION
    }
}
