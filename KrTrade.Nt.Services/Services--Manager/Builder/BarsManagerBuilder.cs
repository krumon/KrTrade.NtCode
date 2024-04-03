using KrTrade.Nt.Core.Data;
using NinjaTrader.Data;
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
        private DataSeriesInfo _primaryDataSeriesOptions;
        private string _primaryDataSeriesName;

        private Dictionary<DataSeriesInfo, Action<IBarsServiceBuilder>> _barsServiceDelegateBuilder = new Dictionary<DataSeriesInfo, Action<IBarsServiceBuilder>>();
        
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
            _primaryBarsServiceDelegateBuilder = configurePrimaryBarsServiceBuilder ?? throw new ArgumentNullException(nameof(configurePrimaryBarsServiceBuilder));

            return this;
        }
        public IBarsManagerBuilder ConfigurePrimaryDataSeries(string name, Action<IBarsServiceBuilder> configurePrimaryBarsServiceBuilder)
        {
            _primaryDataSeriesName = name;
            _primaryBarsServiceDelegateBuilder = configurePrimaryBarsServiceBuilder ?? throw new ArgumentNullException(nameof(configurePrimaryBarsServiceBuilder));

            return this;
        }

        public IBarsManagerBuilder AddDataSeries(Action<DataSeriesInfo> configureDataSeriesOptions, Action<IBarsServiceBuilder> configureBarsServiceBuilder)
        {
            DataSeriesInfo dataSeriesOptions = configureDataSeriesOptions != null ? new DataSeriesInfo() : throw new ArgumentNullException(nameof(configureDataSeriesOptions));
            configureDataSeriesOptions(dataSeriesOptions);

            if (_barsServiceDelegateBuilder.ContainsKey(dataSeriesOptions))
                _barsServiceDelegateBuilder[dataSeriesOptions] = configureBarsServiceBuilder ?? throw new ArgumentNullException(nameof(configureBarsServiceBuilder));
            else
                _barsServiceDelegateBuilder.Add(dataSeriesOptions, configureBarsServiceBuilder ?? throw new ArgumentNullException(nameof(configureBarsServiceBuilder)));

            return this;
        }
        public IBarsManagerBuilder AddDataSeries(string name, Action<DataSeriesInfo> configureDataSeriesOptions, Action<IBarsServiceBuilder> configureBarsServiceBuilder)
        {
            DataSeriesInfo dataSeriesOptions = configureDataSeriesOptions != null ? new DataSeriesInfo() : throw new ArgumentNullException(nameof(configureDataSeriesOptions));
            configureDataSeriesOptions(dataSeriesOptions);
            dataSeriesOptions.Name = name;

            if (_barsServiceDelegateBuilder.ContainsKey(dataSeriesOptions))
                _barsServiceDelegateBuilder[dataSeriesOptions] = configureBarsServiceBuilder ?? throw new ArgumentNullException(nameof(configureBarsServiceBuilder));
            else
                _barsServiceDelegateBuilder.Add(dataSeriesOptions, configureBarsServiceBuilder ?? throw new ArgumentNullException(nameof(configureBarsServiceBuilder)));

            return this;
        }

        public IBarsManager Configure(NinjaScriptBase ninjascript, Action<string, BarsPeriod, string> addDataSeriesMethod = null)
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

            // Log trace
            logText = $"{printService.Name} has been created succesfully.";
            printService.LogTrace(logText);

            // Configure Options
            BarsManagerOptions options = new BarsManagerOptions();
            foreach (var action in _optionsDelegateActions)
                action(options);

            // Initialize 'BarsManager' with the configure parameters.
            BarsManager barsManager = new BarsManager(ninjascript, printService, options);

            // Log initialized trace.
            logText = $"{barsManager.Name} has been created succesfully.";
            printService.LogTrace(logText);

            // ++++++++++ CONFIGURE AND ADD DATA SERIES ++++++++++++++  //

            // Configure primary data series
            IBarsServiceBuilder primaryBarsServiceBuilder = new BarsServiceBuilder();
            _primaryDataSeriesOptions = new DataSeriesInfo(ninjascript, _primaryDataSeriesName);
            _primaryBarsServiceDelegateBuilder?.Invoke(primaryBarsServiceBuilder);

            IBarsService primaryBarsService = (BarsService)primaryBarsServiceBuilder.Build(barsManager, _primaryDataSeriesOptions);
            barsManager.Add(_primaryDataSeriesOptions.Name, primaryBarsService);
            barsManager.Info.Add(primaryBarsService.Info);

            // Log trace
            logText = $"{primaryBarsService.Name} has been initialized and added to {barsManager.Name} succesfully.";
            printService.LogTrace(logText);

            // Configure all data series
            foreach (var barsServiceDelegateConfiguration in _barsServiceDelegateBuilder)
            {
                IBarsServiceBuilder barsServiceBuilder = new BarsServiceBuilder();
                barsServiceDelegateConfiguration.Value.Invoke(barsServiceBuilder);
                DataSeriesInfo dataSeriesOptions = barsServiceDelegateConfiguration.Key;

                if (dataSeriesOptions.InstrumentCode == InstrumentCode.Default)
                    dataSeriesOptions.InstrumentCode = _primaryDataSeriesOptions.InstrumentCode;
                if (dataSeriesOptions.TradingHoursCode == TradingHoursCode.Default)
                    dataSeriesOptions.TradingHoursCode = _primaryDataSeriesOptions.TradingHoursCode;
                if (dataSeriesOptions.TimeFrame == TimeFrame.Default)
                    dataSeriesOptions.TimeFrame = _primaryDataSeriesOptions.TimeFrame;

                BarsService barsService = (BarsService)barsServiceBuilder.Build(barsManager, dataSeriesOptions);
                barsManager.Add(barsServiceDelegateConfiguration.Key.Name, barsService);
                barsManager.Info.Add(primaryBarsService.Info);

                // Log trace
                logText = $"{barsService.Name} has been initialized and added to {barsManager.Name} succesfully.";
                printService.LogTrace(logText);
            }

            if (barsManager.Count > 0)
                for (int i = 1; i < barsManager.Count; i++)
                {
                    addDataSeriesMethod?.Invoke(barsManager[i].InstrumentName, barsManager[i].BarsPeriod, barsManager[i].TradingHoursName);
                    
                    // Log trace
                    logText = $"{barsManager[i].Name}[{i}] has been added to the 'NinjaScript'.";
                    printService.LogTrace(logText);
                }

            barsManager.Configure();

            // Log info
            logText = 
                Environment.NewLine +
                $"++++++ {barsManager.Name} has been initialized succesfully.\t++++++" + 
                Environment.NewLine +
                $"++++++ {barsManager.Name} contains ({barsManager.Count}) 'BarsService'.\t\t\t\t++++++";
            for (int i = 0; i < barsManager.Count; i++)
                logText += Environment.NewLine + 
                    $"       * DataSeries {i}. {barsManager[i].Name}.";

            printService.LogInformation(logText.ToUpper());

            return barsManager;
        }
    }
}
