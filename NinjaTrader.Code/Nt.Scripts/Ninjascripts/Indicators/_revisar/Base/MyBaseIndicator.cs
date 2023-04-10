using NinjaTrader.Cbi;
using NinjaTrader.Data;
using NinjaTrader.NinjaScript.DrawingTools;
using System.Windows.Media;

/// <summary>
/// namespace that stores any technical indicator 
/// from the ninjatrader ninjascript
/// </summary>
namespace NinjaTrader.NinjaScript.Indicators
{/*
    /// <summary>
    /// Represents a base class for any ninjascript technical indicator.
    /// </summary>
    public class MyBaseIndicator : Indicator
    {

        #region Private members declaration



        #endregion

        #region Public properties

        #endregion

        // Stores all the methods that will be fired when the OnStateChanged method is raised. 
        // OnStateChanged method represents the current progression of the object as it advances from setup, 
        // processing data, to termination.  
        // These states can be used for setting up or declaring various resources and properties.
        #region Stage Changed methods

        /// <summary>
        /// This method is used to initialize default values (pushed to UI).
        /// This method occurs when the user open new indicator and open the dialog.
        /// The properties initialize in this method, can be changed by the user while the indicator is alive.
        /// </summary>
        protected virtual void SetDefaults()
        {

            InitializeIndicatorProperties();
            InitializeCustomProperties();
            InitializeCustomSeries();

        }

        /// <summary>
        /// This method occurs when the user presses the OK or Apply button in the UI.
        /// This method is used to configure the indicator.
        /// </summary>
        protected virtual void ConfigureSessionHoursList()
        {
            // Add services to the indicator
        }

        /// <summary>
        /// This method occurs when the object is sessionHoursListIsConfigured and is ready to receive instructions.
        /// This method is used to set values when the object is sessionHoursListIsConfigured and is ready to receive instructions.
        /// </summary>
        protected virtual void Active() { }

        /// <summary>
        /// This method occurs when all the data series have been loaded.
        /// The method is used to initialize any variable or data series of the indicator.
        /// </summary>
        protected virtual void DataLoaded() { }

        /// <summary>
        /// This method occurs when begins to process historical data.
        /// </summary>
        protected virtual void Historical() { }

        /// <summary>
        /// This method occurs when finished processing historical data.
        /// </summary>
        protected virtual void Transition() { }

        /// <summary>
        /// This method occurs when begins to process realtime data.
        /// </summary>
        protected virtual void RealTime() { }

        /// <summary>
        /// This method occurs when Begins to shut down.
        /// This method is used to free memory.
        /// </summary>
        protected virtual void Terminated() { }

        #endregion

        #region Add Properties, Plots, Lines and Series

        protected virtual void AddIndicatorPlots() { }

        protected virtual void AddIndicatorLines() { }

        protected virtual void InitializeIndicatorProperties() { }

        protected virtual void InitializeCustomProperties() { }

        protected virtual void InitializeCustomSeries() { }

        #endregion

        #region Event methods

        protected override void OnConnectionStatusUpdate(ConnectionStatusEventArgs connectionStatusUpdate)
        {

        }

        protected override void OnFundamentalData(FundamentalDataEventArgs fundamentalDataUpdate)
        {

        }

        protected override void OnMarketData(MarketDataEventArgs marketDataUpdate)
        {

        }

        protected override void OnMarketDepth(MarketDepthEventArgs marketDepthUpdate)
        {

        }

        protected override void OnBarUpdate()
        {
            //Add your custom indicator logic here.
            //Draw.Line(this, "MyLine", 20, 4500, 50, 4510, Brushes.Aqua);
        }

        #endregion


    }
    */
}
