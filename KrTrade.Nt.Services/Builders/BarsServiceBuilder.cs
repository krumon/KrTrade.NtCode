using KrTrade.Nt.Core.Data;
using KrTrade.Nt.Core.Infos;
using KrTrade.Nt.Core.Logging;
using KrTrade.Nt.Core.Services;
using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Build <see cref="IBarsService"/> services. 
    /// </summary>
    public class BarsServiceBuilder : IBarsServiceBuilder
    {
        private bool _isBuilt;
        private readonly NinjaScriptBase _ninjascript;
        private readonly IPrintService _printService;
        private readonly Action<string, BarsPeriod, string> _addDataSeriesDelegate;

        private readonly List<Action<IBarsServiceInfo,BarsServiceOptions>> _optionsDelegateActions = new List<Action<IBarsServiceInfo, BarsServiceOptions>>();
        private readonly Dictionary<string,IInputSeriesInfo> _seriesConfiguration = new Dictionary<string,IInputSeriesInfo>();

        public BarsServiceBuilder(NinjaScriptBase ninjascript, IPrintService printService, Action<string, BarsPeriod, string> addDataSeriesDelegate = null)
        {
            _ninjascript = ninjascript ?? throw new ArgumentNullException(nameof(ninjascript));
            _printService = printService ?? throw new ArgumentNullException(nameof(printService));
            _addDataSeriesDelegate = addDataSeriesDelegate;
        }

        public IBarsServiceBuilder Configure(Action<IBarsServiceInfo,BarsServiceOptions> configureBarsServiceOptions)
        {
            _optionsDelegateActions.Add(configureBarsServiceOptions ?? throw new ArgumentNullException(nameof(configureBarsServiceOptions)));
            return this;
        }

        public IBarsServiceBuilder AddSeries<TInfo>(Action<TInfo> configureSeries)
            where TInfo : IInputSeriesInfo, new()
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

        public IBarsService Build()
        {
            if (_isBuilt)
                throw new InvalidOperationException("The service is already construct. Only once time the service can be created.");

            if (_ninjascript.State != State.Configure)
                throw new InvalidOperationException("The service must be construct in 'NinjaScript.OnStateChange()' method when 'NinjaScript.State == State.Configure'.");

            // Create service information and options
            BarsServiceOptions options = new BarsServiceOptions();
            BarsServiceInfo info = new BarsServiceInfo();
            BarsServiceInfo primaryDataSeriesInfo = new BarsServiceInfo(_ninjascript);

            foreach (var action in _optionsDelegateActions)
                action(info,options);

            if (info.InstrumentCode == InstrumentCode.Default)
                info.InstrumentCode = primaryDataSeriesInfo.InstrumentCode;
            if (info.TradingHoursCode == TradingHoursCode.Default)
                info.TradingHoursCode = primaryDataSeriesInfo.TradingHoursCode;
            if (info.TimeFrame == TimeFrame.Default)
                info.TimeFrame = primaryDataSeriesInfo.TimeFrame;

            IBarsService barsService = new BarsService(_ninjascript, _printService, info, options);

            // TODO: Delete this breakpoint.
            Debugger.Break();

            if (!info.Equals(primaryDataSeriesInfo) && _addDataSeriesDelegate != null)
            {
                _addDataSeriesDelegate.Invoke(barsService.InstrumentName, barsService.BarsPeriod, barsService.TradingHoursName);
                // Log trace
                _printService.LogTrace($"{info} has been added to the 'NinjaScript'.");
            }

            //// Add SERIES
            //foreach (var series in _seriesConfiguration)
            //    (barsService as BarsService).AddSeries(series.Value);

            barsService.Configure();
            _isBuilt = true;

            return barsService;
        }
    }
}
