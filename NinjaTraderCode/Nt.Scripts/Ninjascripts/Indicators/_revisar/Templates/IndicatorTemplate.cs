#region Attributes annotations

//
// [NinjaScriptProperty]                            The property is added to the indicator constructor as parameter
// [XmlIgnore]                                      The property does not serielize
// [Display(Name = "Name")]                         Determines the display name on the NinjaTrader UI's property grid.
// [Display(Description = "Description")]           Determines the display name on the NinjaTrader UI's property grid.
// [Display(Order = 1)]                             Determines the display name on the NinjaTrader UI's property grid.
// [Display(GroupName = "Parameters")]              Determines the display name on the NinjaTrader UI's property grid.
// [Browsable(false)]                               Determines if the following declared property displays in the NinjaTrader UI's property grid. 
// [Range(int minimum, int maximum)]                Determines if the value of the following declared property is valid within a specified range.
// [Range(double minimum, double maximum)]          Determines if the value of the following declared property is valid within a specified range.
// [Range(type type,string minimum,string maximum)] Determines if the value of the following declared property is valid within a specified range.
// [TypeConverter(string)]                          Binds an object or property to a specific TypeConverter implementation. We need implements IndicatorBaseTypeConverter.
// [TypeConverter(type)]                            Binds an object or property to a specific TypeConverter implementation. We need implements IndicatorBaseTypeConverter.
// [XmlIgnore]                                      Determines if the following declared property participates in the XML serialization routines which are used to save NinjaScript objects to a workspace or template.
// [XmlIgnore(bool)]                                Idem [XmlIgnore]. The default value is true. We use these attribute if we want serialize this object.
// [PropertyEditor(string)]                         Determines if the following declared property to a specific editor. DateTime properties is declared by key string.
// [PropertyEditor(type)]                           For example... [PropertyEditor("NinjaTrader.Gui.Tools.TimeEditorKey")]
// [CategoryOrder(string, int)]                     The CategoryOrder attribute is ONLY valid on class-level declarations.
//                                                  Categories with values less than 1,000,000 appear at the very top of the property grid(excluding the Strategy Analyzer "General" category).
//                                                  NinjaTrader UI reserves using values ending in 000, 500 and the values documented below are subject to change.
//                                                  If you wish to inject your category between a standard NinjaScript category, 
//                                                  please refer to the table below to locate the appropriate position(e.g., to set a property after "Data Series" and before the "Setup" use value of 2,000,001).

#endregion

#region Using declarations

using NinjaTrader.Cbi;
using NinjaTrader.Data;
using NinjaTrader.Gui;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Media;
using System.Xml.Serialization;

#endregion

namespace NinjaTrader.NinjaScript.Indicators
{/*

    /// <summary>
    /// Add specific description for you indicator
    /// </summary>
    public class InidicatorTemplate : MyBaseIndicator
    {

        // Region to store all private fields or series declaration.
        // For examble -->  private Series<double> Custom;
        //                  private bool IsLongPosition;
        // The custom series must be initialized in the DatoLoaded method.
        #region Private fields declaration

        // Add fields declaration

        #endregion

        #region Private series declaration

        private Series<double> CustomSerie;

        #endregion

        // Region to stores all properties that will be displayed in the user interface. 
        // These properties can be changed through the user interface if they are initialized in the "SetDefault" method and
        // should be used the "NinjaScriptPropertyAttribute" to be included in the NinjaScript object's constructor as a parameter.
        #region Indicator Custom Properties

        [NinjaScriptProperty]
        [XmlIgnore]
        [Display(Name ="BrushPropertyName", Description ="BrushPropertyDescription", Order =1, GroupName ="Plots")]
        public Brush BrushProperty
        { get; set; }

        [Browsable(false)] // Determines if the following declared property displays in the NinjaTrader UI's property grid.
        public string BrushPropertySerializable
        {
            get { return Serialize.BrushToString(BrushProperty); }
            set { BrushProperty = Serialize.StringToBrush(value); }
        }

        [NinjaScriptProperty]
        [Display(Name = "BoolPropertyName", Description = "BoolPropertyDescription", Order = 2, GroupName = "Parameters")]
        public bool BoolProperty
        { get; set; }

        [NinjaScriptProperty]
        [Range(1, double.MaxValue)]
        [Display(Name = "DoublePropertyName", Description = "DoublePropertyDescription", Order = 3, GroupName = "Parameters")]
        public double DoubleProperty
        { get; set; }

        [NinjaScriptProperty]
        [Range(1, int.MaxValue)]
        [Display(Name = "IntPropertyName", Description = "IntPropertyDescription", Order = 4, GroupName = "Parameters")]
        public int IntProperty
        { get; set; }

        [NinjaScriptProperty]
        [PropertyEditor("NinjaTrader.Gui.Tools.TimeEditorKey")]
        [Display(Name = "DateTimePropertyName", Description = "DateTimePropertyDescription", Order = 5, GroupName = "Parameters")]
        public DateTime DateTimeProperty
        { get; set; }

        [Browsable(false)]
        [XmlIgnore]
        public Series<double> PlotProperty
        {
            get { return Values[0]; }
        }

        #endregion

        // Region to declare all public properties of the indicator.
        #region Developer Custom Properties

        #endregion

        // Stores all methods that will be fired when the OnStateChanged method is raised. 
        // OnStateChanged method represents the current progression of the object as it advances from setup, 
        // processing data, to termination.  
        // These states can be used for setting up or declaring various resources and properties.
        #region Stage changed methods

        protected override void InitializeIndicatorProperties()
        {
            // Execute the base method before initialize the properties
            base.InitializeIndicatorProperties();

            // Determines the description to display of the indicator
            Description = @"Indicator Description";

            // Determines the display and unique name of the indicator.
            Name = "Indicator Unique Name";

            // Determines how often OnBarUpdate() is called for each bar. 
            // OnBarClose means once at the close of the bar.OnEachTick means on every single tick.
            // OnPriceChange means once for each price change.If there were two ticks 
            // in a row with the same price, the second tick would not trigger OnBarUpdate().
            // This can improve performance if calculations are only needed when new values are possible.
            Calculate = Calculate.OnBarClose;

            // Determines if indicator plot(s) are drawn on the chart panel over top of price. 
            // Setting this value to true will also allow an Indicator to be used as a SuperDOM Indicator.
            IsOverlay = true;

            // Determines if plot(s) display in the chart data box.
            DisplayInDataBox = true;

            // Determines the chart panel the draw objects renders.
            DrawOnPricePanel = true;

            // Plots horizontal grid lines on the indicator panel.
            DrawHorizontalGridLines = true;

            // Plots vertical grid lines on the indicator panel.
            DrawVerticalGridLines = true;

            // If true, any indicator plot values display price markers in the y-axis.
            PaintPriceMarkers = true;

            // Determines which scale an indicator will be plotted on.
            // This property should ONLY bet set from the OnStateChange() method during State. SetDefaults or State.ConfigureSessionHoursList.
            ScaleJustification = NinjaTrader.Gui.Chart.ScaleJustification.Right;

            // Disable this property if your indicator requires custom values that cumulate with each new market data event. 
            // See Help Guide for additional information.
            IsSuspendedWhileInactive = true;


        }

        /// <summary>
        /// This method is used to initialize default values (pushed to UI).
        /// This method occurs when the user open new indicator and open the dialog.
        /// The properties initialize in this method, can be changed by the user while the indicator is alive.
        /// </summary>
        protected override void SetDefaults()
        {
        }

        /// <summary>
        /// This method occurs when the user presses the OK or Apply button in the UI.
        /// This method is used to configure the indicator.
        /// </summary>
        protected override void ConfigureSessionHoursList() { }

        /// <summary>
        /// This method occurs when the object is sessionHoursListIsConfigured and is ready to receive instructions.
        /// This method is used to set values when the object is sessionHoursListIsConfigured and is ready to receive instructions.
        /// </summary>
        protected override void Active() { }

        /// <summary>
        /// This method occurs when all the data series have been loaded.
        /// The method is used to initialize any variable or data series of the indicator.
        /// </summary>
        protected override void DataLoaded() { CustomSerie = new Series<double>(this); }

        /// <summary>
        /// This method occurs when begins to process historical data.
        /// </summary>
        protected override void Historical() { }

        /// <summary>
        /// This method occurs when finished processing historical data.
        /// </summary>
        protected override void Transition() { }

        /// <summary>
        /// This method occurs when begins to process realtime data.
        /// </summary>
        protected override void RealTime() { }

        /// <summary>
        /// This method occurs when Begins to shut down.
        /// This method is used to free memory.
        /// </summary>
        protected override void Terminated() { }

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
        }

        #endregion

        #region Private methods

        #endregion

        #region Helper methods

        #endregion

    }
    */
}
