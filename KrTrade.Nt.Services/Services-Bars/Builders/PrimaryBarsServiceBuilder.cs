using System;
using System.Collections.Generic;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Build <see cref="BarsService"/> objects. 
    /// </summary>
    public class PrimaryBarsServiceBuilder : IPrimaryBarsServiceBuilder
    {

        private List<Action<BarsServiceOptions>> _optionsDelegateActions = new List<Action<BarsServiceOptions>>();

        public IBarsService Build(IBarsManager barsManager)
        {
            // Create service options
            BarsServiceOptions options = new BarsServiceOptions();
            foreach (var action in _optionsDelegateActions)
                action(options);

            // Create the service with specified options
            IBarsService dataSeriesService = new BarsService(barsManager,options);

            // Add diferent service to the 'BARSSERVICE'
            // .
            // .
            // .

            return dataSeriesService;
        }

        public IPrimaryBarsServiceBuilder ConfigureOptions(Action<PrimaryBarsServiceOptions> configureOptions)
        {
            _optionsDelegateActions.Add(configureOptions ?? throw new ArgumentNullException(nameof(configureOptions)));
            return this;
        }

        //public IDataSeriesBuilder AddIndicators(Action<IIndicatorsBuilder> configureIndicatorsDelegate)
        //{
        //    throw new NotImplementedException();
        //}

        //public IDataSeriesBuilder AddFilters()
        //{
        //    throw new NotImplementedException();
        //}

        //public IDataSeriesBuilder UseStats()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
