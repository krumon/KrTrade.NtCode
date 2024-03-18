using KrTrade.Nt.Core.DataSeries;
using KrTrade.Nt.Core.Extensions;
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
        private readonly List<Action<BarsServiceOptions>> _primaryDataSeriesDelegateActions = new List<Action<BarsServiceOptions>>();

        public IBarsManagerBuilder ConfigureOptions(Action<BarsManagerOptions> configureDelegate)
        {
            _optionsDelegateActions.Add(configureDelegate ?? throw new ArgumentNullException(nameof(configureDelegate)));
            return this;
        }
        public IBarsManagerBuilder UsePrintService(Action<PrintOptions> configureDelegate)
        {
            _printDelegateActions.Add(configureDelegate ?? throw new ArgumentNullException(nameof(configureDelegate)));
            return this;
        }
        public IBarsManagerBuilder ConfigureDataSeries(Action<BarsServiceOptions> configureDelegate)
        {
            _primaryDataSeriesDelegateActions.Add(configureDelegate ?? throw new ArgumentNullException(nameof(configureDelegate)));
            return this;
        }
        public IBarsManagerBuilder AddDataSeries(Action<DataSeriesInfo, IBarsServiceBuilder> configureDelegate)
        {
            throw new NotImplementedException();
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

            // Configure primary data series
            DataSeriesInfo dataSeriesInfo = new DataSeriesInfo();
            dataSeriesInfo.TimeFrame = ninjascript.BarsPeriods[0].ToTimeFrame();
            // TODO: Añadir el resto de información
            BarsServiceOptions dataSeriesOptions = new BarsServiceOptions();
            foreach (var action in _primaryDataSeriesDelegateActions)
                action(dataSeriesOptions);

            //BarsSeriesService primaryDataSeries = new BarsSeriesService();

            // Create Bars Services
            IBarsManager barsServices = new BarsManager(ninjascript, printService, options);

            
            return barsServices;
        }

    }
}
