using KrTrade.Nt.Core.Data;
using KrTrade.Nt.Core;

namespace KrTrade.Nt.Services
{
    public class StatsService : BarUpdateService<StatsInfo, StatsOptions>, IStatsService
    {
        public StatsService(IBarsService barsService, StatsInfo info, StatsOptions options) : base(barsService, info, options)
        {
        }

        //public StatsService(IBarsService barsSeries) : base(barsSeries)
        //{
        //}

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

        internal override void Configure(out bool isConfigured)
        {
            throw new System.NotImplementedException();
        }

        internal override void DataLoaded(out bool isDataLoaded)
        {
            throw new System.NotImplementedException();
        }

        public override void BarUpdate()
        {
            throw new System.NotImplementedException();
        }

        public override void BarUpdate(IBarsService updatedBarsSeries)
        {
            throw new System.NotImplementedException();
        }

        public string ToString(int tabOrder)
        {
            throw new System.NotImplementedException();
        }

        protected override string GetHeaderString()
        {
            throw new System.NotImplementedException();
        }

        protected override string GetParentString()
        {
            throw new System.NotImplementedException();
        }

        protected override string GetDescriptionString()
        {
            throw new System.NotImplementedException();
        }

        protected override string GetLogString(string state)
        {
            throw new System.NotImplementedException();
        }
    }
}
