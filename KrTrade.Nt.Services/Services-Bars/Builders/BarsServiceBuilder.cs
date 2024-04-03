using KrTrade.Nt.Core.Data;
using System;
using System.Collections.Generic;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Build <see cref="BarsService"/> objects. 
    /// </summary>
    public class BarsServiceBuilder : IBarsServiceBuilder
    {

        private readonly List<Action<BarsServiceOptions>> _optionsDelegateActions = new List<Action<BarsServiceOptions>>();

        public IBarsService Build(IBarsManager barsManager, DataSeriesInfo dataSeriesInfo)
        {

            string logText = string.Empty;

            // Create service options
            BarsServiceOptions options = new BarsServiceOptions();

            foreach (var action in _optionsDelegateActions)
                action(options);

            // Create the service with specified info
            IBarsService barsService = new BarsService(barsManager, dataSeriesInfo, options);

            // Log trace
            if (barsService != null)
                logText = $"{barsService.Name} has been created succesfully.";
            else 
                logText = "BarsService has NOT been created. The value is NULL.";
            barsManager.PrintService.LogTrace(logText);

            // Add diferent service to the 'BARSSERVICE'
            // .
            // .
            // .

            return barsService;
        }

        public IBarsServiceBuilder ConfigureOptions(Action<BarsServiceOptions> barsServiceOptions)
        {
            _optionsDelegateActions.Add(barsServiceOptions ?? throw new ArgumentNullException(nameof(barsServiceOptions)));
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
