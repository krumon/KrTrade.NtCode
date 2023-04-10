using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using Nt.Core.Events;

namespace ConsoleApp
{

    /// <summary>
    /// Interface for any ninjascript
    /// </summary>
    public interface INinjascript : IElement
    {

        #region Protected members

        /// <summary>
        /// Indicates if the session is configured.
        /// </summary>
        bool IsLoaded { get; }

        /// <summary>
        /// Indicates if the session is configured.
        /// </summary>
        bool IsConfigured { get; }

        /// <summary>
        /// Inidicates if allow more than one element in the manager collection.
        /// </summary>
        bool AllowManagerMultiUse { get; }

        #endregion

        #region State changed methods

        /// <summary>
        /// Loaded the Script in OnStateChanged method.
        /// </summary>
        /// <param name="ninjascript">The parent ninjascript object.</param>
        /// <param name="bars">The chart bars object loaded.</param>
        void Load(NinjaScriptBase ninjascript, Bars bars);

        /// <summary>
        /// Free the memory of the script.
        /// </summary>
        void Terminated();

        #endregion

        #region Market Data methods

        /// <summary>
        /// Event driven method which is called whenever a bar is updated. 
        /// The frequency in which OnBarUpdate is called will be determined by the "Calculate" property. 
        /// OnBarUpdate() is the method where all of your script's core bar based calculation logic should be contained.
        /// </summary>
        void OnBarUpdate();

        /// <summary>
        /// Event driven method which is called and guaranteed to be in the correct sequence 
        /// for every change in level one market data for the underlying instrument. 
        /// OnMarketData() can include but is not limited to the bid, ask, last price and volume.
        /// </summary>
        void OnMarketData();

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the <see cref="INinjascript"/> builder object.
        /// </summary>
        /// <typeparam name="Script">The <see cref="INinjascript"/> object.</typeparam>
        /// <typeparam name="Builder">The <see cref="IBuilder"/> object.</typeparam>
        /// <returns></returns>
        Builder CreateBuilder<Script, Builder>()
            where Script : INinjascript
            where Builder : IBuilder;

        /// <summary>
        /// Gets an order for an event driven method.
        /// </summary>
        /// <param name="eventType">The event type.</param>
        /// <returns>The ninjascript order in the event driven method.</returns>
        int GetOrder(EventType eventType);

        /// <summary>
        /// Execute the ninjascript handler methods.
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="e"></param>
        void ExecuteHandlerMethod(EventType eventType, SessionUpdateArgs e = null);

        #endregion

    }

}