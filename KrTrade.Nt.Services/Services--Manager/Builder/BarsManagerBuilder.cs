using KrTrade.Nt.Core.Data;
using KrTrade.Nt.Core.DataSeries;
using NinjaTrader.NinjaScript;
using System;
using System.Collections.Generic;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Represents properties and method to built <see cref="IBarsManager"/> objects. 
    /// </summary>
    public class BarsManagerBuilder : IBarsManagerBuilder
    {
        private readonly List<Action<BarsManagerOptions>> _optionsDelegateActions = new List<Action<BarsManagerOptions>>();
        private readonly List<Action<PrintOptions>> _printDelegateActions = new List<Action<PrintOptions>>();

        private Action<IBarsServiceBuilder> _primaryBarsServiceDelegateBuilder;
        private DataSeriesOptions _primaryDataSeriesOptions;

        private Dictionary<DataSeriesOptions, Action<IBarsServiceBuilder>> _barsServiceDelegateBuilder = new Dictionary<DataSeriesOptions, Action<IBarsServiceBuilder>>();
        
        public IBarsManagerBuilder ConfigureOptions(Action<BarsManagerOptions> configureBarsManagerOptions)
        {
            _optionsDelegateActions.Add(configureBarsManagerOptions ?? throw new ArgumentNullException(nameof(configureBarsManagerOptions)));
            return this;
        }
        public IBarsManagerBuilder AddPrintService(Action<PrintOptions> configurePrintServiceOptions)
        {
            _printDelegateActions.Add(configurePrintServiceOptions ?? throw new ArgumentNullException(nameof(configurePrintServiceOptions)));
            return this;
        }

        public IBarsManagerBuilder ConfigurePrimaryDataSeries(Action<IBarsServiceBuilder> configurePrimaryBarsServiceBuilder)
        {
            _primaryDataSeriesOptions = new DataSeriesOptions();
            _primaryBarsServiceDelegateBuilder = configurePrimaryBarsServiceBuilder ?? throw new ArgumentNullException(nameof(configurePrimaryBarsServiceBuilder));

            return this;
        }
        public IBarsManagerBuilder ConfigurePrimaryDataSeries(string name, Action<IBarsServiceBuilder> configurePrimaryBarsServiceBuilder)
        {
            _primaryDataSeriesOptions = new DataSeriesOptions(name);
            _primaryBarsServiceDelegateBuilder = configurePrimaryBarsServiceBuilder ?? throw new ArgumentNullException(nameof(configurePrimaryBarsServiceBuilder));

            return this;
        }

        public IBarsManagerBuilder AddDataSeries(Action<DataSeriesOptions> configureDataSeriesOptions, Action<IBarsServiceBuilder> configureBarsServiceBuilder)
        {
            DataSeriesOptions dataSeriesOptions = configureDataSeriesOptions != null ? new DataSeriesOptions() : throw new ArgumentNullException(nameof(configureDataSeriesOptions));
            configureDataSeriesOptions(dataSeriesOptions);

            if (_barsServiceDelegateBuilder.ContainsKey(dataSeriesOptions))
                _barsServiceDelegateBuilder[dataSeriesOptions] = configureBarsServiceBuilder ?? throw new ArgumentNullException(nameof(configureBarsServiceBuilder));
            else
                _barsServiceDelegateBuilder.Add(dataSeriesOptions, configureBarsServiceBuilder ?? throw new ArgumentNullException(nameof(configureBarsServiceBuilder)));

            return this;
        }
        public IBarsManagerBuilder AddDataSeries(string name, Action<DataSeriesOptions> configureDataSeriesOptions, Action<IBarsServiceBuilder> configureBarsServiceBuilder)
        {
            DataSeriesOptions dataSeriesOptions = configureDataSeriesOptions != null ? new DataSeriesOptions(name) : throw new ArgumentNullException(nameof(configureDataSeriesOptions));
            configureDataSeriesOptions(dataSeriesOptions);

            if (_barsServiceDelegateBuilder.ContainsKey(dataSeriesOptions))
                _barsServiceDelegateBuilder[dataSeriesOptions] = configureBarsServiceBuilder ?? throw new ArgumentNullException(nameof(configureBarsServiceBuilder));
            else
                _barsServiceDelegateBuilder.Add(dataSeriesOptions, configureBarsServiceBuilder ?? throw new ArgumentNullException(nameof(configureBarsServiceBuilder)));

            return this;
        }

        public IBarsManager Build(NinjaScriptBase ninjascript)
        {
            string logText = string.Empty;

            // Make sure ninjascript is NOT NULL.
            if (ninjascript == null)
                throw new ArgumentNullException(nameof(ninjascript));

            // Make sure the method is executed whe state is 'Configure'.
            if (ninjascript.State != State.Configure)
                throw new Exception("'BarsManager' must be built when 'NinjaScript.State' is 'State.Configure'.");

            // Configure PrintService
            PrintOptions printOptions = new PrintOptions();
            foreach (var action in _printDelegateActions)
                action(printOptions);
            IPrintService printService = new PrintService(ninjascript, printOptions);

            // Log initialized trace
            logText = $"{printService.Name} has been initialized succesfully.";
            printService.LogTrace(logText);

            // Configure Options
            BarsManagerOptions options = new BarsManagerOptions();
            foreach (var action in _optionsDelegateActions)
                action(options);

            // Log config trace
            logText = $"{nameof(BarsManagerOptions)} configuration has been loaded.";
            printService.LogTrace(logText);

            // Initialize 'BarsManager' with the configure parameters.
            BarsManager barsManager = new BarsManager(ninjascript, printService, options);

            // Log initialized trace.
            logText = $"{barsManager.Name} has been initialized succesfully.";
            printService.LogTrace(logText);

            // ++++++++++ CONFIGURE AND ADD DATA SERIES ++++++++++++++  //

            // Configure primary data series
            IBarsServiceBuilder primaryBarsServiceBuilder = new BarsServiceBuilder();
            _primaryBarsServiceDelegateBuilder.Invoke(primaryBarsServiceBuilder);
            BarsService primaryBarsService = (BarsService)primaryBarsServiceBuilder.Build(barsManager);
            _primaryDataSeriesOptions.SetNinjascriptValues(ninjascript);
            primaryBarsService.SetDataSeriesInfo(_primaryDataSeriesOptions);

            // Log initialized trace
            logText = $"{primaryBarsService.Name} has been initialized succesfully.";
            printService.LogTrace(logText);

            if (_primaryDataSeriesOptions.Name == null || _primaryDataSeriesOptions.Name == string.Empty)
                barsManager.Add(primaryBarsService);
            else
                barsManager.Add(_primaryDataSeriesOptions.Name, primaryBarsService);

            // Log config trace
            logText = $"{primaryBarsService.Name} has been added to {barsManager.Name} succesfully.";
            printService.LogTrace(logText);

            // Configure all data series
            foreach (var dataseriesOptions in _barsServiceDelegateBuilder)
            {
                IBarsServiceBuilder barsServiceBuilder = new BarsServiceBuilder();
                dataseriesOptions.Value.Invoke(barsServiceBuilder);
                BarsService barsService = (BarsService)barsServiceBuilder.Build(barsManager);
                DataSeriesOptions dataSeriesOptions = dataseriesOptions.Key;

                if (dataSeriesOptions.InstrumentCode == InstrumentCode.Default) 
                    dataSeriesOptions.InstrumentCode = _primaryDataSeriesOptions.InstrumentCode;
                if (dataSeriesOptions.TradingHoursCode == TradingHoursCode.Default) 
                    dataSeriesOptions.TradingHoursCode = _primaryDataSeriesOptions.TradingHoursCode;
                if (dataSeriesOptions.TimeFrame == TimeFrame.Default) 
                    dataSeriesOptions.TimeFrame = _primaryDataSeriesOptions.TimeFrame;

                barsService.SetDataSeriesInfo(dataSeriesOptions);

                // Log initialized trace
                logText = $"{barsService.Name} has been initialized succesfully.";
                printService.LogTrace(logText);

                if (dataseriesOptions.Key.Name == null || dataseriesOptions.Key.Name == string.Empty)
                    barsManager.Add(barsService);
                else
                    barsManager.Add(dataseriesOptions.Key.Name, barsService);

                // Log added trace
                logText = $"{barsService.Name} has been added to {barsManager.Name} succesfully.";
                printService.LogTrace(logText);
            }

            // Log initialized info
            logText = 
                $"++++++ {barsManager.Name} has been initialized succesfully.           ++++++{Environment.NewLine}" +
                $"++++++ {barsManager.Name} contains {barsManager.Count} 'BarsService'. ++++++";
            printService.LogInformation(logText.ToUpper());

            return barsManager;
        }
    }
}
