using KrTrade.Nt.Core.Data;
using KrTrade.Nt.Core.Services;
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
        private List<Action<IBarsServiceBuilder>> _barsServiceDelegateBuilders = new List<Action<IBarsServiceBuilder>>();
        
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

        public IBarsManagerBuilder ConfigurePrimaryBars(Action<IBarsServiceBuilder> barsServiceBuilder)
        {
            _primaryBarsServiceDelegateBuilder = barsServiceBuilder ?? throw new ArgumentNullException(nameof(barsServiceBuilder));
            return this;
        }
        public IBarsManagerBuilder AddDataSeries(Action<IBarsServiceBuilder> configureBarsServiceBuilder)
        {
            _barsServiceDelegateBuilders.Add(configureBarsServiceBuilder ?? throw new ArgumentNullException(nameof(configureBarsServiceBuilder)));
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

            // Log trace
            ninjascript.Print("'PrintOptions' is going to be created...");
            // Configure PrintService
            PrintOptions printOptions = new PrintOptions();
            foreach (var action in _printDelegateActions)
                action(printOptions);
            // Log trace
            ninjascript.Print("'PrintOptions' has been created succesfully.");

            ninjascript.Print("'PrintService' is going to be created...");
            IPrintService printService = new PrintService(ninjascript, printOptions);
            ninjascript.Print("'PrintService' has been created succesfully.");
            // Log trace
            //printService.LogTrace("'PrintService'has been created succesfully (2).");
            //printService.LogTrace($"{printService.Info.Type.ToLongString()} has been created succesfully.");
            //printService.LogTrace($"{printService.Name} has been created succesfully.");

            ninjascript.Print("'BarsManagerOptions' is going to be created...");
            // Configure Options
            BarsManagerOptions options = new BarsManagerOptions();
            foreach (var action in _optionsDelegateActions)
                action(options);
            ninjascript.Print("'BarsManagerOptions' has been created succesfully.");

            ninjascript.Print("'BarsManager' is going to be created...");
            // Initialize 'BarsManager' with the configure parameters.
            BarsManager barsManager = new BarsManager(ninjascript, printService, options);
            ninjascript.Print("'BarsManager' has been created succesfully.");

            // Log initialized trace.
            printService.LogTrace("'BarsManager' has been created succesfully (2).");
            //printService.LogTrace($"{barsManager.Name} has been created succesfully.");

            // ++++++++++ CONFIGURE AND ADD DATA SERIES ++++++++++++++  //

            // Configure primary data series
            IBarsServiceBuilder primaryBarsServiceBuilder = new BarsServiceBuilder();
            //_primaryBarsInfo = new BarsServiceInfo(ninjascript, _primaryBarsName);
            _primaryBarsServiceDelegateBuilder?.Invoke(primaryBarsServiceBuilder);

            BarsService primaryBarsService = (BarsService)primaryBarsServiceBuilder.Build(barsManager);
            primaryBarsService.IsPrimaryBars = true;
            barsManager.Add(primaryBarsService);

            // Log trace
            printService.LogTrace($"{primaryBarsService.Name} has been initialized and added to {barsManager.Name} succesfully.");

            // Configure all data series
            foreach (var barsServiceDelegateBuilder in _barsServiceDelegateBuilders)
            {
                IBarsServiceBuilder barsServiceBuilder = new BarsServiceBuilder();
                barsServiceDelegateBuilder(barsServiceBuilder);
                
                BarsService barsService = (BarsService)barsServiceBuilder.Build(barsManager);

                if (barsService.Info.InstrumentCode == InstrumentCode.Default)
                    barsService.Info.InstrumentCode = barsManager.Info[0].InstrumentCode;
                if (barsService.Info.TradingHoursCode == TradingHoursCode.Default)
                    barsService.Info.TradingHoursCode = barsManager.Info[0].TradingHoursCode;
                if (barsService.Info.TimeFrame == TimeFrame.Default)
                    barsService.Info.TimeFrame = barsManager.Info[0].TimeFrame;

                barsManager.Add(barsService);

                // Log trace
                printService.LogTrace($"{barsService.Name} has been initialized and added to {barsManager.Name} succesfully.");
            }

            if (barsManager.Count > 0)
                for (int i = 1; i < barsManager.Count; i++)
                {
                    addDataSeriesMethod?.Invoke(barsManager[i].InstrumentName, barsManager[i].BarsPeriod, barsManager[i].TradingHoursName);
                    // Log trace
                    printService.LogTrace($"{barsManager[i].Name}[{i}] has been added to the 'NinjaScript'.");
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
