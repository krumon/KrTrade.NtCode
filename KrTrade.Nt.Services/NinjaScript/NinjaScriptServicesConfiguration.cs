using System;

namespace KrTrade.Nt.Services
{
    public class NinjaScriptServicesConfiguration
    {
        private readonly NinjaScriptServices _ninjascriptService;

        private PrintOptions _printServiceOptions;
        private PlotServiceOptions _plotServiceOptions;

        public NinjaScriptServicesConfiguration(NinjaScriptServices ninjascriptService)
        {
            _ninjascriptService = ninjascriptService ?? throw new ArgumentNullException(nameof(ninjascriptService));
        }

        public void AddPrintOptions(Action<PrintOptions> printOptions)
        {
            _printServiceOptions = new PrintOptions();
            printOptions?.Invoke(_printServiceOptions);

            _ninjascriptService.PrintService = new PrintService(_ninjascriptService.Ninjascript);
        }
        public void AddPlotOptions(Action<PlotServiceOptions> plotOptions)
        {
            _plotServiceOptions = new PlotServiceOptions();
            plotOptions?.Invoke(_plotServiceOptions);

            _ninjascriptService.PlotService = new PlotService(_ninjascriptService.Ninjascript);
        }
    }
}
