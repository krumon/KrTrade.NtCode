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
        
        private Action<IPrimaryBarsServiceBuilder> _primaryDataSeriesDelegateActions;
        private readonly List<Action<IBarsServiceBuilder>> _dataSeriesDelegateActions = new List<Action<IBarsServiceBuilder>>();
        
        public IBarsManagerBuilder ConfigureOptions(Action<BarsManagerOptions> configureDelegate)
        {
            _optionsDelegateActions.Add(configureDelegate ?? throw new ArgumentNullException(nameof(configureDelegate)));
            return this;
        }
        public IBarsManagerBuilder AddPrintService(Action<PrintOptions> configureDelegate)
        {
            _printDelegateActions.Add(configureDelegate ?? throw new ArgumentNullException(nameof(configureDelegate)));
            return this;
        }

        public IBarsManagerBuilder ConfigurePrimaryDataSeries(Action<IPrimaryBarsServiceBuilder> configureDelegate)
        {
            _primaryDataSeriesDelegateActions = configureDelegate ?? throw new ArgumentNullException(nameof(configureDelegate));
            return this;
        }

        public IBarsManagerBuilder AddDataSeries(Action<IBarsServiceBuilder> configureDelegate)
        {
            _dataSeriesDelegateActions.Add(configureDelegate ?? throw new ArgumentNullException(nameof(configureDelegate)));
            return this;
        }


        public IBarsManager Build(NinjaScriptBase ninjascript)
        {
            // Make sure ninjascript is NOT NULL.
            if (ninjascript == null)
                throw new ArgumentNullException(nameof(ninjascript));

            // Configure PrintService
            PrintOptions printOptions = new PrintOptions();
            foreach (var action in _printDelegateActions)
                action(printOptions);
            IPrintService printService = new PrintService(ninjascript, printOptions);

            // Configure Options
            BarsManagerOptions options = new BarsManagerOptions();
            foreach (var action in _optionsDelegateActions)
                action(options);

            // Initialize 'BarsManager' with the configure parameters.
            BarsManager barsManager = new BarsManager(ninjascript, printService, options);

            // ++++++++++ CONFIGURE AND ADD DATA SERIES ++++++++++++++  //

            // Configure primary data series
            PrimaryBarsServiceBuilder primaryServiceBuilder = new PrimaryBarsServiceBuilder();
            _primaryDataSeriesDelegateActions?.Invoke(primaryServiceBuilder);
            //barsManager.AddDataSeries(primaryServiceBuilder.Build(barsManager));

            // Configure all data series
            foreach (var action in _dataSeriesDelegateActions)
            {
                BarsServiceBuilder barsServiceBuilder = new BarsServiceBuilder();
                action?.Invoke(barsServiceBuilder);
                //barsManager.AddDataSeries(barsServiceBuilder.Build(barsManager));
            }

            return barsManager;
        }
    }
}
