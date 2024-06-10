using KrTrade.Nt.Core.Data;
using KrTrade.Nt.Core;

namespace KrTrade.Nt.Services
{
    public class IndicatorCollection : BarUpdateServiceCollection<IIndicatorService, IServiceCollectionInfo>
    {
        public IndicatorCollection(IBarsService barsService, ServiceCollectionInfo info, BarUpdateServiceCollectionOptions options) : base(barsService, info, options)
        {
        }

        protected override string GetDescriptionString()
        {
            throw new System.NotImplementedException();
        }

        protected override string GetHeaderString()
        {
            throw new System.NotImplementedException();
        }

        protected override string GetLogString(string state)
        {
            throw new System.NotImplementedException();
        }

        protected override string GetParentString()
        {
            throw new System.NotImplementedException();
        }

        protected override ServiceCollectionType GetServiceType()
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
    }
}
