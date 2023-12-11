using System.Collections.Generic;

namespace KrTrade.Nt.Services
{
    public class BarsService : BarUpdateService<BarsOptions>, IBarUpdateService
    {
        #region Private members

        private readonly List<BarService> _barsService = new List<BarService>();

        public BarsService(IDataSeriesService dataSeriesService) : base(dataSeriesService)
        {
        }

        public BarsService(IDataSeriesService dataSeriesService, IConfigureOptions<BarsOptions> configureOptions) : base(dataSeriesService, configureOptions)
        {
        }

        public override void LogUpdatedState()
        {
            throw new System.NotImplementedException();
        }

        public override void Update()
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

        #endregion
    }
}
