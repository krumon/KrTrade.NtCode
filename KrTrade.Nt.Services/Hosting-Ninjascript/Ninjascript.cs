using NinjaTrader.NinjaScript;
using System;

namespace KrTrade.Nt.Services
{
    public class Ninjascript : INinjascript, IConfigure
    {
        /// <summary>
        /// Gets 'Ninjatrader.NinjaScript' instance.
        /// </summary>
        public NinjaScriptBase Instance {get; internal set;}

        /// <summary>
        /// Gets or sets the <see cref="Ninjascript"/> options. This options will be configured when
        /// 'Ninjatrader.NinjaScript' state is 'Configure'.
        /// </summary>
        public NinjascriptOptions Options { get; internal set;}

        /// <summary>
        /// <see cref="IPrintService"/> to print in 'Ninjatrader.OutputWindow'.
        /// </summary>
        public IPrintService PrintService {get; internal set;}

        /// <summary>
        /// <see cref="IServiceProvider"/> to gets all services necesary for the <see cref="INinjascript"/>.
        /// </summary>
        public IServiceProvider Services { get; internal set; }

        public bool IsConfigure => throw new NotImplementedException();

        public bool IsDataLoaded => throw new NotImplementedException();

        /// <summary>
        /// Create <see cref="Ninjascript"/> instance.
        /// </summary>
        /// <param name="ninjascript">The specific 'Ninjatrader.NinjaScript' necesariy to inject in all services.</param>
        /// <param name="services">The <see cref="IServiceProvider"/> necesary for inject all services in the ninjascript.</param>
        /// <param name="printService">The <see cref="IPrintService"/> for print in 'Ninjatrader.Output.Window'.</param>
        /// <param name="ninjascriptOptions">The ninjascript options.</param>
        /// <exception cref="ArgumentNullException"><paramref name="ninjascript"/> or <paramref name="services"/> cannot be null.</exception>
        internal Ninjascript(
            NinjaScriptBase ninjascript, 
            IServiceProvider services,
            NinjascriptOptions ninjascriptOptions, 
            IPrintService printService)
        {
            Instance = ninjascript ?? throw new ArgumentNullException($"Error in '{nameof(Ninjascript)}' constructor. The {nameof(ninjascript)} argument cannot be null.");
            Options = ninjascriptOptions ?? new NinjascriptOptions();
            PrintService = printService;
        }

        public void Configure()
        {
            // TODO:    Asignar las opciones a 'Ninjatrader.NinjaScript'.
            //          Crear DefaultNinjascriptOptions.
        }

        public void DataLoaded()
        {
            throw new NotImplementedException();
        }

        public void OnBarUpdate()
        {
            throw new NotImplementedException();
        }

        public void Terminated()
        {
            throw new NotImplementedException();
        }
    }
}
