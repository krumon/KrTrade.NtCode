namespace KrTrade.Nt.Services
{
    public class StatsService : BarUpdateService<StatsOptions>, IStatsService
    {
        public override string Key => throw new System.NotImplementedException();

        public StatsService(IBarsService barsSeries) : base(barsSeries)
        {
        }

        //public StatsService(IBarsService barsSeries, IConfigureOptions<StatsOptions> configureOptions) : base(barsSeries, configureOptions)
        //{
        //}

        //public StatsService(IBarsService barsService, int period) : base(barsService, period)
        //{
        //}

        //public StatsService(IBarsService barsService, int period, int displacement) : base(barsService, period, displacement)
        //{
        //}

        //public StatsService(IBarsService barsService, int period, int displacement, int lengthOfRemovedValues) : base(barsService, period, displacement, lengthOfRemovedValues)
        //{
        //}

        //public BaseSeriesCache Value => throw new System.NotImplementedException();

        //public BaseSeriesCache Max => throw new System.NotImplementedException();

        //public BaseSeriesCache Min => throw new System.NotImplementedException();

        //public BaseSeriesCache Sum => throw new System.NotImplementedException();

        //public BaseSeriesCache Avg => throw new System.NotImplementedException();

        //public BaseSeriesCache StdDev => throw new System.NotImplementedException();

        public override string ToLogString()
        {
            throw new System.NotImplementedException();
        }

        internal override void Configure(out bool isConfigured)
        {
            throw new System.NotImplementedException();
        }

        internal override void DataLoaded(out bool isDataLoaded)
        {
            throw new System.NotImplementedException();
        }

        public override void Update()
        {
            throw new System.NotImplementedException();
        }

        public override void Update(IBarsService updatedSeries)
        {
            throw new System.NotImplementedException();
        }
    }
}
