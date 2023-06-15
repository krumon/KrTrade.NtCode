#region Using declarations

using NinjaTrader.Core.FloatingPoint;
using NinjaTrader.Gui;
using NinjaTrader.Gui.Chart;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Serialization;
using KrTrade.NtCode.DrawingTools;
using Nt.Core.Data;

#endregion

namespace NinjaTrader.NinjaScript.DrawingTools
{
	/// <summary>
	/// 
	/// Version			: v.0.1.
	/// Description		: Clase base para todas las herramientas que dibujan lineas con etiqueta. Este herramienta es capaz de ser dibujada tanto
	///					  desde el menú como desde cualquier indicador o estrategia.
	/// Funcionality	: Dibuja una línea que posee una etiqueta con el valor del precio, tiempo o cualquier otro valor introducido por el usuario.
	///					  La etiqueta podrá ser centrada o puesta en los márgenes del gráfico por el usuario. 
	/// 
	/// </summary>
	public class LabelLine_v_0_1 : DrawingTool
	{

		#region Consts

		/// <summary>
		/// Specific length of the arrow base.
		/// </summary>
		private const float arrowBaseLength = 0;

		/// <summary>
		/// The minimum length of the arrow. If value is 0, the maximum length is the label height.
		/// </summary>
		private const float maximumArrowBaseLength = 0;

		/// <summary>
		/// The maximum length of the arrow.
		/// </summary>
		private const float minimumArrowBaseLength = 5;

		/// <summary>
		/// Represents the cursor sensitive for change the cursor
		/// when mouse is near of the object. 
		/// </summary>
		private const double cursorSensitivity = 15;

		#endregion

		#region Private members

		/// <summary>
		/// Text of the anchor price or time.
		/// </summary>
		private string anchorText;

		/// <summary>
		/// Text to display in the label.
		/// </summary>
		private string displayText;

		/// <summary>
		/// Represents the path geometry of the label
		/// </summary>
		[CLSCompliant(false)]
#pragma warning disable CS3021 // El tipo o el miembro no necesitan un atributo CLSCompliant porque el ensamblador no tiene un atributo CLSCompliant
        protected SharpDX.Direct2D1.PathGeometry LabelPathGeometry;
#pragma warning restore CS3021 // El tipo o el miembro no necesitan un atributo CLSCompliant porque el ensamblador no tiene un atributo CLSCompliant

		#endregion

		#region Public properties

		/// <summary>
		/// Represents the type of the label line.
		/// </summary>
        public LabelLineType LabelType { get; set; }

		/// <summary>
		/// Represents the horizontal alignment of the price label.
		/// </summary>
		public HorizontalAlignment LabelHorizontalAlignment { get; set; } 

		/// <summary>
		/// Represents the vertical alignment of the time label.
		/// </summary>
		public VerticalAlignment LabelVerticalAlignment { get; set; } 

		/// <summary>
		/// Label margin
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public Margin LabelMargin { get; set; }

		/// <summary>
		/// Type of the arrow of the label.
		/// </summary>
		public ArrowType LabelArrowType { get; set; } = ArrowType.Short;

		/// <summary>
		/// Text to display in the label
		/// </summary>
		[XmlIgnore]
		public string LabelText { get; set; }

		/// <summary>
		/// Text margin
		/// </summary>
		[Browsable(false)]
		[XmlIgnore]
		public Margin LabelTextMargin { get; set; }

		/// <summary>
		/// The <see cref="LabelLine"/> anchors collection.
		/// </summary>
		public override IEnumerable<ChartAnchor> Anchors { get { return new[] { Anchor }; } }

		/// <summary>
		/// The anchor of the label line.
		/// </summary>
		[Display(Order = 1)]
		public ChartAnchor Anchor { get; set; }

        /// <summary>
        /// Represents the stroke properties of the label line.
        /// </summary>
        //[Display(ResourceType = typeof(Custom.Resource), GroupName = "NinjaScriptGeneral", Name = "NinjaScriptDrawingToolLine", Order = 99)]
		public Stroke LabelOutlineStroke { get; set; }

		/// <summary>
		/// Represents the stroke properties of the label line.
		/// </summary>
		//[Display(ResourceType = typeof(Custom.Resource), GroupName = "NinjaScriptGeneral", Name = "NinjaScriptDrawingToolLine", Order = 98)]
		public Stroke LabelAreaStroke { get; set; }

		/// <summary>
		/// Represents the stroke properties of the label text.
		/// </summary>
		//[Display(ResourceType = typeof(Custom.Resource), GroupName = "NinjaScriptGeneral", Name = "NinjaScriptDrawingToolLine", Order = 98)]
		public Stroke LabelTextStroke { get; set; }

		/// <summary>
		/// Indicates if the drawing tool shows the anchor text in the label.
		/// </summary>
		public bool AnchorTextIsVisible { get; set; }

		/// <summary>
		/// Indicates if the label line support alerts.
		/// </summary>
		public override bool SupportsAlerts { get { return true; } }

        #endregion

        #region State changed methods

        /// <summary>
        /// An event driven method which is called whenever the script enters a new State. 
        /// The OnStateChange() method can be used to configure script properties, 
        /// create one-time behavior when going from historical to real-time, 
        /// as well as manage clean up resources on termination.
        /// </summary>
        protected override void OnStateChange()
		{
			if (State == State.SetDefaults)
			{
				//LabelType = LabelType.Price;
				Name = "FileName"; // Custom.Resource.NinjaScriptDrawingToolLine;
				DrawingState = DrawingState.Building;
				DisplayOnChartsMenus = false;

				Anchor = new ChartAnchor
				{
					IsEditing = true,
					DrawingTool = this,
					DisplayName = "LabelLineDisplayName", // Implentar diciconario con cadenas
					IsBrowsable = true
				};
				
				LabelOutlineStroke = new Stroke(Brushes.White, 2f);
				LabelAreaStroke = new Stroke(Brushes.Red);

			}
			else if (State == State.Configure)
            {
                // Initialize the margins.
                LabelTextMargin = new Margin(4);
                LabelMargin = new Margin(4);
            }
			else if (State == State.Terminated)
			{
				// release any device resources
				Dispose();
			}
		}

		#endregion

		#region Rendering methods

		/// <summary>
		/// An event driven method which is called while the chart scale is being updated.  
		/// This method is used to determine the highest and lowest value that can be used 
		/// for the chart scale and is only called when the chart object is set to IsAutoScale.
		/// </summary>
		public override void OnCalculateMinMax()
		{
			// Store initial values.
			MinValue = double.MaxValue;
			MaxValue = double.MinValue;

			// Make sure object is visible.
			if (!IsVisible)
				return;

			// Make sure to set good min/max values on single click lines as well, in case anchor left in editing
			if (LabelType == LabelLineType.Price)
				MinValue = MaxValue = Anchors.First().Price;

			else
			{
				// return min/max values only if something has been actually drawn
				if (Anchors.Any(a => !a.IsEditing))
					foreach (ChartAnchor anchor in Anchors)
					{
						MinValue = Math.Min(anchor.Price, MinValue);
						MaxValue = Math.Max(anchor.Price, MaxValue);
					}
			}
		}

		/// <summary>
		/// Indicates a chart object is visible on the chart. When the IsVisibleOnChart() method determines a chart object is not visible and returns false, 
		/// the object will not be used in a render pass, will not be considered in a hit test, and will not be used for alerting.  
		/// The base implementation is to always return true on all chart objects, however this behavior can be overridden for your custom object if desired.  
		/// </summary>
		/// <param name="chartControl"></param>
		/// <param name="chartScale"></param>
		/// <param name="firstTimeOnChart"></param>
		/// <param name="lastTimeOnChart"></param>
		/// <returns></returns>
		public override bool IsVisibleOnChart(ChartControl chartControl, ChartScale chartScale, DateTime firstTimeOnChart, DateTime lastTimeOnChart)
		{
			if (DrawingState == DrawingState.Building)
				return true;

			if (LabelType == LabelLineType.Time)
				return Anchor.Time >= firstTimeOnChart && Anchor.Time <= lastTimeOnChart;

			// check offscreen vertically. make sure to check the line doesnt cut through the scale, so check both are out
			if (LabelType == LabelLineType.Price && (Anchor.Price < chartScale.MinValue || Anchor.Price > chartScale.MaxValue) && !IsAutoScale)
				return false; // horizontal line only has one anchor to whiff

			return true;

		}

		/// <summary>
		/// Used to render custom drawing to a chart from various chart objects, such as an Indicator, DrawingTool or Strategy.
		/// </summary>
		/// <param name="chartControl"></param>
		/// <param name="chartScale"></param>
		public override void OnRender(ChartControl chartControl, ChartScale chartScale)
		{
			// Make sure we have stroke object.
			if (LabelOutlineStroke == null)
				return;

			// Reference the render target.
			LabelOutlineStroke.RenderTarget = RenderTarget;
			LabelAreaStroke.RenderTarget = RenderTarget;

			// Store the current antilias mode and changes it.
			SharpDX.Direct2D1.AntialiasMode oldAntiAliasMode = RenderTarget.AntialiasMode;
			RenderTarget.AntialiasMode = SharpDX.Direct2D1.AntialiasMode.PerPrimitive;

			// Store the chart panel
			ChartPanel panel = chartControl.ChartPanels[chartScale.PanelIndex];

            #region CREATE TEXT 

            // Get the text to display
			// TODO: Añadir el texto para el tipo (LabelLineType.Bar)
			anchorText = LabelType == LabelLineType.Price
				? Anchor.Price.ToString("N2", CultureInfo.CreateSpecificCulture("es-ES"))
				: Anchor.Time.ToString();

			// Set the value to the display text
			if (string.IsNullOrEmpty(LabelText))
				displayText = anchorText;
			else if (!AnchorTextIsVisible)
				displayText = LabelText;
			else
				displayText = string.Format("{0} # {1}", LabelText, anchorText);

			//Create the text format
			SharpDX.DirectWrite.TextFormat textFormat = chartControl.Properties.LabelFont.ToDirectWriteTextFormat();

			//Create the text layout
			SharpDX.DirectWrite.TextLayout textLayout = new SharpDX.DirectWrite.TextLayout(
				NinjaTrader.Core.Globals.DirectWriteFactory,
				displayText, textFormat, panel.W, panel.H);

			// define the brush used for text
			SharpDX.Direct2D1.SolidColorBrush textDXBrush = new
					SharpDX.Direct2D1.SolidColorBrush(RenderTarget, SharpDX.Color.White);

			#endregion

			#region Helper sizes

			SharpDX.Size2F textSize = new SharpDX.Size2F(
				// Width
				LabelTextMargin.Left 
				+ textLayout.Metrics.Width 
				+ LabelTextMargin.Right
				, 
				// Height
				LabelTextMargin.Top 
				+ textLayout.Metrics.Height 
				+ LabelTextMargin.Bottom
				);

			SharpDX.Size2F labelSize = new SharpDX.Size2F(
				// Width
				LabelType.ToLabelArrowHeadLength(textSize.Height,LabelHorizontalAlignment,LabelVerticalAlignment,LabelArrowPlacement.OnLeft,LabelArrowType,arrowBaseLength,maximumArrowBaseLength,minimumArrowBaseLength) 
				+ textSize.Width 
				+ LabelType.ToLabelArrowHeadLength(textSize.Height, LabelHorizontalAlignment, LabelVerticalAlignment, LabelArrowPlacement.OnRight, LabelArrowType,arrowBaseLength,maximumArrowBaseLength,minimumArrowBaseLength) 
				,
				// Height
				LabelType.ToLabelArrowHeadLength(textSize.Height, LabelHorizontalAlignment, LabelVerticalAlignment, LabelArrowPlacement.OnTop, LabelArrowType, arrowBaseLength, maximumArrowBaseLength, minimumArrowBaseLength)
				+ textSize.Height
				+ LabelType.ToLabelArrowHeadLength(textSize.Height, LabelHorizontalAlignment, LabelVerticalAlignment, LabelArrowPlacement.OnBottom, LabelArrowType, arrowBaseLength, maximumArrowBaseLength, minimumArrowBaseLength) 
				);

			#endregion

			#region CALCULATE POINTS

			// Store the clicked point
			Point anchorPoint = Anchor.GetPoint(chartControl, panel, chartScale);

			// Align to full pixel to avoid unneeded aliasing
			double strokePixAdj = ((double)(LabelOutlineStroke.Width % 2)).ApproxCompare(0) == 0 ? 0.5d : 0d;
			Vector pixelAdjustVec = new Vector(strokePixAdj, strokePixAdj);

			// Origin point for calculete all necesary points.
			Point labelOriginPoint = LabelType.ToLabelOriginPoint(LabelHorizontalAlignment,LabelVerticalAlignment,panel,LabelMargin,labelSize,anchorPoint) + pixelAdjustVec;

			// TEXT start point
			SharpDX.Vector2 textOriginPoint = LabelType.ToTextOriginPoint(labelOriginPoint, LabelHorizontalAlignment, LabelVerticalAlignment, textSize, LabelTextMargin, LabelArrowType, pixelAdjustVec);

			// LINE start point
			SharpDX.Vector2 lineStartPoint = LabelType.ToLineStartPoint(LabelHorizontalAlignment,LabelVerticalAlignment,panel,labelOriginPoint,labelSize,anchorPoint,pixelAdjustVec);

			// LINE end point
			SharpDX.Vector2 lineEndPoint = LabelType.ToLineFinalPoint(LabelHorizontalAlignment, LabelVerticalAlignment, panel, labelOriginPoint, labelSize, anchorPoint, pixelAdjustVec);

			// LABEL begin point
			SharpDX.Vector2 labelBeginPoint = LabelType.ToLabelBeginPoint(labelOriginPoint,textSize,LabelHorizontalAlignment,LabelVerticalAlignment,pixelAdjustVec,LabelArrowType,arrowBaseLength,maximumArrowBaseLength,minimumArrowBaseLength);

			// LABEL points
			SharpDX.Vector2[] points = LabelType.ToLabelPoints(labelOriginPoint, textSize, LabelHorizontalAlignment, LabelVerticalAlignment, pixelAdjustVec, LabelArrowType, arrowBaseLength, maximumArrowBaseLength, minimumArrowBaseLength);

			#endregion

			#region CONSTRUCT PATH GEOMETRY

			//if (LabelPathGeometry == null)
			// create our directx geometry.
			// just make a static size we will scale when drawing all relative to top of line
			// nudge up y slightly to cover up top of stroke (instead of using zero), half the stroke will hide any overlap

			// Create a path geometry and the geometry sink
			LabelPathGeometry = new SharpDX.Direct2D1.PathGeometry(Core.Globals.D2DFactory);
			SharpDX.Direct2D1.GeometrySink geometrySink = LabelPathGeometry.Open();
			geometrySink.BeginFigure(labelBeginPoint, SharpDX.Direct2D1.FigureBegin.Filled);
			geometrySink.AddLines(points);
			geometrySink.EndFigure(SharpDX.Direct2D1.FigureEnd.Closed);
			geometrySink.Close();

            #endregion

            #region DRAWING METHODS

            // Draw the line
            RenderTarget.DrawLine(lineStartPoint, lineEndPoint, LabelOutlineStroke.BrushDX, LabelOutlineStroke.Width, LabelOutlineStroke.StrokeStyle);

            // Draw the fill of the geometry
            RenderTarget.FillGeometry(LabelPathGeometry, LabelAreaStroke.BrushDX);

			// Draw the outline of the geometry
			RenderTarget.DrawGeometry(LabelPathGeometry, LabelOutlineStroke.BrushDX, 1);

			// Draw the text
			RenderTarget.DrawTextLayout(textOriginPoint, textLayout, textDXBrush);

            #endregion

            #region DISPOSE RESOURCES

            // always dispose of text format and layout when finished.
            textLayout.Dispose();
			textFormat.Dispose();

			// always dispose of a PathGeometry when finished.
			LabelPathGeometry.Dispose();

			// always dispose of a brush when finished.
			textDXBrush.Dispose();

			// Clear the strings
			anchorText = string.Empty;

			#endregion

			// Restore the antilias mode
			RenderTarget.AntialiasMode = oldAntiAliasMode;

		}

		/// <summary>
		/// Called whenever a Chart's RenderTarget is created or destroyed. 
		/// This method is used for creating / cleaning up resources such as a SharpDX.Direct2D1.Brush used throughout your NinjaScript class.
		/// </summary>
		public override void OnRenderTargetChanged()
        {
			// Explicitly set the Stroke RenderTarget
			if (RenderTarget != null)
            {
				LabelOutlineStroke.RenderTarget = RenderTarget;
				LabelAreaStroke.RenderTarget = RenderTarget;
            }
		}

        #endregion

        #region Drawing tool methods

        /// <summary>
        /// An event driven method which is called when a chart object is selected.  
        /// This method can be used to change the cursor image used in various states.
        /// </summary>
        public override Cursor GetCursor(ChartControl chartControl, ChartPanel chartPanel, ChartScale chartScale, Point point)
		{
			switch (DrawingState)
			{
				case DrawingState.Building: return Cursors.Pen;
				case DrawingState.Moving: return IsLocked ? Cursors.No : Cursors.SizeAll;
				default:
					// draw move cursor if cursor is near line path anywhere
					Point startPoint = Anchor.GetPoint(chartControl, chartPanel, chartScale);

					// just go by single axis since we know the entire lines position
					if (LabelType == LabelLineType.Time && Math.Abs(point.X - startPoint.X) <= cursorSensitivity)
						return IsLocked ? Cursors.Arrow : Cursors.SizeAll;
					if (LabelType == LabelLineType.Price && Math.Abs(point.Y - startPoint.Y) <= cursorSensitivity)
						return IsLocked ? Cursors.Arrow : Cursors.SizeAll;
					return null;
			}
		}

		/// <summary>
		/// Returns the chart object's data points where the user can interact.   
		/// These points are used to visually indicate that the chart object is 
		/// selected and allow the user to manipulate the chart object.  
		/// This method is only called when IsSelected is set to true.
		/// </summary>
		/// <param name="chartControl"></param>
		/// <param name="chartScale"></param>
		/// <returns></returns>
		public sealed override Point[] GetSelectionPoints(ChartControl chartControl, ChartScale chartScale)
		{
			ChartPanel chartPanel = chartControl.ChartPanels[chartScale.PanelIndex];
			Point startPoint = Anchor.GetPoint(chartControl, chartPanel, chartScale);

			int totalWidth = chartPanel.W + chartPanel.X;
			int totalHeight = chartPanel.Y + chartPanel.H;

			if (LabelType == LabelLineType.Time)
				return new[] { new Point(startPoint.X, chartPanel.Y), new Point(startPoint.X, chartPanel.Y + ((totalHeight - chartPanel.Y) / 2d)), new Point(startPoint.X, totalHeight) };
			else 
				return new[] { new Point(chartPanel.X, startPoint.Y), new Point(totalWidth / 2d, startPoint.Y), new Point(totalWidth, startPoint.Y) };

		}

		/// <summary>
		/// An event driven method which is called any time the mouse pointer over the chart control has the mouse button pressed.
		/// </summary>
		/// <param name="chartControl"></param>
		/// <param name="chartPanel"></param>
		/// <param name="chartScale"></param>
		/// <param name="dataPoint"></param>
		public override void OnMouseDown(ChartControl chartControl, ChartPanel chartPanel, ChartScale chartScale, ChartAnchor dataPoint)
		{
			switch (DrawingState)
			{
				case DrawingState.Building:

					if (Anchor.IsEditing)
					{
						// Set values of start and end anchor.
						dataPoint.CopyDataValues(Anchor);

						// Disable editing mode.
						Anchor.IsEditing = false;
						DrawingState = DrawingState.Normal;
						IsSelected = false;
					}

					break;

				case DrawingState.Normal:

					// Get the mouse point.
					Point point = dataPoint.GetPoint(chartControl, chartPanel, chartScale);

					// see if they clicked near a point to edit, if so start editing
					if (GetCursor(chartControl, chartPanel, chartScale, point) == null)
						IsSelected = false;
                    else
						DrawingState = DrawingState.Moving;
					break;
			}
		}

		/// <summary>
		/// An event driven method which is called any time the mouse pointer is over the chart control and a mouse is moving.
		/// </summary>
		/// <param name="chartControl"></param>
		/// <param name="chartPanel"></param>
		/// <param name="chartScale"></param>
		/// <param name="dataPoint"></param>
		public override void OnMouseMove(ChartControl chartControl, ChartPanel chartPanel, ChartScale chartScale, ChartAnchor dataPoint)
		{
			if (IsLocked && DrawingState != DrawingState.Building)
				return;

			if (DrawingState == DrawingState.Moving) // else if
				foreach (ChartAnchor anchor in Anchors)
					// only move anchor values as needed depending on line type
					if (LabelType == LabelLineType.Price)
						anchor.MoveAnchorPrice(InitialMouseDownAnchor, dataPoint, chartControl, chartPanel, chartScale, this);
					else
						anchor.MoveAnchorTime(InitialMouseDownAnchor, dataPoint, chartControl, chartPanel, chartScale, this);
		}

		/// <summary>
		/// An event driven method is called any time the mouse pointer is over the chart control and a mouse button is being released.
		/// </summary>
		/// <param name="chartControl"></param>
		/// <param name="chartPanel"></param>
		/// <param name="chartScale"></param>
		/// <param name="dataPoint"></param>
		public override void OnMouseUp(ChartControl chartControl, ChartPanel chartPanel, ChartScale chartScale, ChartAnchor dataPoint)
		{
			// simply end whatever moving
			if (DrawingState == DrawingState.Moving || DrawingState == DrawingState.Editing)
				DrawingState = DrawingState.Normal;

		}

		#endregion

		#region Alert methods

		public override IEnumerable<AlertConditionItem> GetAlertConditionItems()
		{
			yield return new AlertConditionItem
			{
				Name = "FileName", //Custom.Resource.NinjaScriptDrawingToolLine,
				ShouldOnlyDisplayName = true
			};
		}

		public override bool IsAlertConditionTrue(AlertConditionItem conditionItem, Condition condition, ChartAlertValue[] values, ChartControl chartControl, ChartScale chartScale)
		{
			// Make sure we have values.
			if (values.Length < 1)
				return false;

			// Get the chart panel.
			ChartPanel chartPanel = chartControl.ChartPanels[PanelIndex];

			// Horzontal line alerts.
			if (LabelType == LabelLineType.Price)
			{
				double barVal = values[0].Value;
				double lineVal = conditionItem.Offset.Calculate(Anchor.Price, AttachedTo.Instrument);

				switch (condition)
				{
					case Condition.Equals: return barVal.ApproxCompare(lineVal) == 0;
					case Condition.NotEqual: return barVal.ApproxCompare(lineVal) != 0;
					case Condition.Greater: return barVal > lineVal;
					case Condition.GreaterEqual: return barVal >= lineVal;
					case Condition.Less: return barVal < lineVal;
					case Condition.LessEqual: return barVal <= lineVal;
					case Condition.CrossAbove:
					case Condition.CrossBelow:
						Predicate<ChartAlertValue> predicate = v =>
						{
							if (condition == Condition.CrossAbove)
								return v.Value > lineVal;
							return v.Value < lineVal;
						};
						return MathHelper.DidPredicateCross(values, predicate);
				}
				return false;
			}

			// Vertical line alerts.
			if (LabelType == LabelLineType.Time)
			{
				// time comparison only make sense here
				if (values[0].ValueType == ChartAlertValueType.StaticValue)
					return false;

				DateTime barTime = values[0].Time;
				if (barTime == Core.Globals.MinDate || barTime == Core.Globals.MaxDate)
					return false;

				DateTime lineTime = Anchor.Time;
				switch (condition)
				{
					case Condition.Greater: return barTime > lineTime;
					case Condition.GreaterEqual: return barTime >= lineTime;
					case Condition.Less: return barTime < lineTime;
					case Condition.LessEqual: return barTime <= lineTime;
					case Condition.NotEqual: return barTime != lineTime;
					case Condition.Equals: return barTime == lineTime;
				}
				return false;
			}

			// Otherwise...
			return false;
		}

		#endregion

	}
}
