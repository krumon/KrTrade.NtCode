using System;
using System.Collections.Generic;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Build <see cref="BarsService"/> objects. 
    /// </summary>
    public class BarsServiceBuilder : IBarsServiceBuilder
    {

        private List<Action<BarsServiceOptions>> _optionsDelegateActions = new List<Action<BarsServiceOptions>>();

        public IBarsService Build(IBarsManager barsService)
        {
            BarsServiceOptions options = new BarsServiceOptions();
            foreach (var action in _optionsDelegateActions)
                action(options);

            IBarsService dataSeriesService = new BarsService(barsService,options);

            return dataSeriesService;
        }

        public IBarsServiceBuilder ConfigureOptions(Action<BarsServiceOptions> configureOptions)
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
