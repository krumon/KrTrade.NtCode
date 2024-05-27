using KrTrade.Nt.Core.Series;
using KrTrade.Nt.Services.Series;
using System;
using System.Collections.Generic;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Build <see cref="BarsService"/> objects. 
    /// </summary>
    public class BarsServiceBuilder : IBarsServiceBuilder
    {

        private readonly List<Action<BarsServiceInfo,BarsServiceOptions>> _optionsDelegateActions = new List<Action<BarsServiceInfo, BarsServiceOptions>>();
        private readonly Dictionary<string,ISeriesInfo> _seriesConfiguration = new Dictionary<string,ISeriesInfo>();

        public IBarsServiceBuilder ConfigureOptions(Action<BarsServiceInfo,BarsServiceOptions> configureBarsServiceOptions)
        {
            _optionsDelegateActions.Add(configureBarsServiceOptions ?? throw new ArgumentNullException(nameof(configureBarsServiceOptions)));
            return this;
        }

        public IBarsServiceBuilder AddSeries<TInfo>(Action<TInfo> configureSeries)
            where TInfo : ISeriesInfo, new()
        {
            if (configureSeries == null)
                throw new ArgumentNullException(nameof(configureSeries));

            TInfo seriesInfo = new TInfo();
            configureSeries(seriesInfo);

            if (!_seriesConfiguration.ContainsKey(seriesInfo.Key))
                _seriesConfiguration.Add(seriesInfo.Key, seriesInfo);

            return this;
        }
        public IBarsServiceBuilder AddSeries_Period(Action<PeriodSeriesInfo> configureSeries)
        {
            PeriodSeriesInfo seriesInfo = new PeriodSeriesInfo();
            //SeriesServiceOptions seriesOptions = new SeriesServiceOptions();
            configureSeries(seriesInfo);

            if (!_seriesConfiguration.ContainsKey(seriesInfo.Key))
                _seriesConfiguration.Add(seriesInfo.Key, seriesInfo);

            return this;
        }
        
        public IBarsService Build(IBarsManager barsManager, bool isPrimaryDataSeries = false)
        {

            // Create service options
            BarsServiceOptions options = new BarsServiceOptions();
            BarsServiceInfo info = new BarsServiceInfo();
            BarsServiceInfo primaryDataSeriesInfo = null;

            foreach (var action in _optionsDelegateActions)
                action(info,options);

            if (isPrimaryDataSeries)
                primaryDataSeriesInfo = new BarsServiceInfo(barsManager.Ninjascript) { Name = info.Name };

            IBarsService barsService = new BarsService(barsManager, isPrimaryDataSeries ? primaryDataSeriesInfo : info, options);

            // Add SERIES
            foreach (var series in _seriesConfiguration)
                (barsService as BarsService).AddSeries(series.Value);

            return barsService;
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
