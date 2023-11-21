using NinjaTrader.NinjaScript;
using System;

namespace KrTrade.Nt.Services
{
    public class NinjaScriptServices 
    {
        #region private members

        //internal readonly NinjaScriptBase Ninjascript;
        //private readonly NinjaScriptServiceOptions _options;

        //internal readonly PrintService PrintService;
        //internal readonly PlotService PlotService;

        #endregion

        #region Public properties

        /// <summary>
        /// Gets the Ninjatrader NinjaScript.
        /// </summary>
        internal NinjaScriptBase Ninjascript {  get; set; }

        /// <summary>
        /// Gets the NinjaScript Print Service.
        /// </summary>
        internal PrintService PrintService {  get; set; }

        /// <summary>
        /// Gets the NinjaScript Plot Service
        /// </summary>
        internal PlotService PlotService { get; set; }

        #endregion

        #region Constructors

        public NinjaScriptServices(NinjaScriptBase ninjascript) : this(ninjascript, null) { }
        public NinjaScriptServices(NinjaScriptBase ninjascript, Action<NinjaScriptServicesConfiguration> options)
        {

            Ninjascript = ninjascript ?? throw new ArgumentNullException($"Error in 'NinjaScriptService' constructor. The 'ninjascript' argument cannot be null.");

            if (Ninjascript.State != State.Configure)
                throw new Exception($"The 'NinjaScriptService' instance must be executed when 'NinjaScript.State' is equal to 'State.Configure'");

            NinjaScriptServicesConfiguration config = new NinjaScriptServicesConfiguration(this);
            options?.Invoke(config);

        }

        #endregion

        #region Public methods

        public void PrintText()
        {
            // TODO: Imprimir con el PrintService si no es null y si está enable.
        }

        #endregion

    }
}
