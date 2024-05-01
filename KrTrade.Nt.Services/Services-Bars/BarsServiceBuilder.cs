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
        private readonly Dictionary<string,BaseSeriesInfo> _seriesConfiguration = new Dictionary<string,BaseSeriesInfo>();

        public IBarsServiceBuilder ConfigureOptions(Action<BarsServiceInfo,BarsServiceOptions> configureBarsServiceOptions)
        {
            _optionsDelegateActions.Add(configureBarsServiceOptions ?? throw new ArgumentNullException(nameof(configureBarsServiceOptions)));
            return this;
        }

        public IBarsServiceBuilder AddSeries<TInfo>(Action<TInfo> configureSeries)
            where TInfo : BaseSeriesInfo, new()
        {
            if (configureSeries == null)
                throw new ArgumentNullException(nameof(configureSeries));

            TInfo seriesInfo = new TInfo();
            configureSeries(seriesInfo);

            if (!_seriesConfiguration.ContainsKey(seriesInfo.Key))
                _seriesConfiguration.Add(seriesInfo.Key, seriesInfo);

            return this;
        }
        public IBarsServiceBuilder AddSeries(Action<PeriodSeriesInfo> configureSeries)
        {
            PeriodSeriesInfo seriesInfo = new PeriodSeriesInfo();
            //SeriesServiceOptions seriesOptions = new SeriesServiceOptions();
            configureSeries(seriesInfo);

            if (!_seriesConfiguration.ContainsKey(seriesInfo.Key))
                _seriesConfiguration.Add(seriesInfo.Key, seriesInfo);

            return this;
        }
        
        //public IBarsServiceBuilder AddSeries<TInfo,TOptions>(Action<TInfo, TOptions> configureSeries)
        //    where TInfo : BaseSeriesInfo, new()
        //    where TOptions : SeriesOptions, new()
        //{
        //    if (configureSeries == null)
        //        throw new ArgumentNullException(nameof(configureSeries));

        //    TInfo seriesInfo = new TInfo();
        //    TOptions seriesOptions = new TOptions();
        //    configureSeries(seriesInfo, seriesOptions);

        //    if (!_seriesConfiguration.ContainsKey(seriesInfo))
        //        _seriesConfiguration.Add(seriesInfo, seriesOptions);

        //    return this;
        //}
        //public IBarsServiceBuilder AddSeries<TInfo>(Action<TInfo, SeriesOptions> configureSeries)
        //    where TInfo : BaseSeriesInfo, new()
        //{
        //    TInfo seriesInfo = new TInfo();
        //    SeriesOptions seriesOptions = new SeriesOptions();
        //    configureSeries(seriesInfo, seriesOptions);

        //    if (!_seriesConfiguration.ContainsKey(seriesInfo))
        //        _seriesConfiguration.Add(seriesInfo, seriesOptions);

        //    return this;
        //}
        //public IBarsServiceBuilder AddSeries(Action<SeriesInfo,SeriesOptions> configureSeries)
        //{
        //    SeriesInfo seriesInfo = new SeriesInfo();
        //    SeriesOptions seriesOptions = new SeriesOptions();
        //    configureSeries(seriesInfo, seriesOptions);

        //    if (!_seriesConfiguration.ContainsKey(seriesInfo))
        //        _seriesConfiguration.Add(seriesInfo, seriesOptions);

        //    return this;
        //}

        public IBarsService Build(IBarsManager barsManager)
        {

            string logText = string.Empty;

            // Create service options
            BarsServiceOptions options = new BarsServiceOptions();
            BarsServiceInfo info = new BarsServiceInfo();

            foreach (var action in _optionsDelegateActions)
                action(info,options);

            // Create the service with specified info
            IBarsService barsService = new BarsService(barsManager, info, options);

            // Log trace
            if (barsService != null)
                barsManager.PrintService.LogTrace($"{barsService.Name} has been created succesfully.");
            else
                barsManager.PrintService.LogTrace("BarsService has NOT been created. The value is NULL.");

            // Add SERIES
            foreach (var series in _seriesConfiguration)
                barsService.AddSeries(series.Value);

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
