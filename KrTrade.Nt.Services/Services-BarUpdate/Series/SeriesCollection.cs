﻿using System;

namespace KrTrade.Nt.Services
{
    public class SeriesCollection<TSeries> : BarUpdateServiceCollection<ISeriesService<TSeries>, SeriesCollectionOptions>
    {

        public SeriesCollection(IBarsService barsService) : base(barsService)
        {
        }

        public SeriesCollection(IBarsService barsService, Action<SeriesCollectionOptions> configureOptions) : base(barsService, configureOptions)
        {
        }

        public SeriesCollection(IBarsService barsService, SeriesCollectionOptions options) : base(barsService, options)
        {
        }
    }
}
