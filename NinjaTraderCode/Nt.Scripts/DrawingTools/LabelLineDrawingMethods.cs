/*

#region Using declarations

using NinjaTrader.Core.FloatingPoint;
using NinjaTrader.Gui;
using NinjaTrader.Gui.Chart;
using System;
using System.Windows.Media;

#endregion

namespace NinjaTrader.NinjaScript.DrawingTools
{

    #region Draw Methods

    /// <summary>
    /// Represents an class that content static methods of <see cref="LabelLine"/> IDrawingTool.
    /// </summary>
    public static partial class Draw
	{

		#region Core methods

		private static T DrawLabelLineTypeCore<T>(NinjaScriptBase owner, bool isAutoScale, string tag, int barsAgo, DateTime time, double price,
			Brush brush, DashStyleHelper dashStyle, int width, bool isGlobal, string templateName)
			where T : LabelLine
		{
			// Make sure ninjascript is not null.
			if (owner == null)
				throw new ArgumentException("owner");

			// Make sure tag exist.
			if (string.IsNullOrWhiteSpace(tag))
				throw new ArgumentException(@"tag can't be null or empty", "tag");

			// Make normalize tag
			if (isGlobal && tag[0] != GlobalDrawingToolManager.GlobalDrawingToolTagPrefix)
				tag = string.Format("{0}{1}", GlobalDrawingToolManager.GlobalDrawingToolTagPrefix, tag);

			// Get the drawing tool
			T lineT = DrawingTool.GetByTagOrNew(owner, typeof(T), tag, templateName) as T;

			// Make sure the drawing tool is not null.
			if (lineT == null)
				return null;

			if (lineT is TimeLine)
			{
				if (time == Core.Globals.InitialDate && barsAgo == int.MinValue)
					throw new ArgumentException("missing time line time / bars ago");
			}
			else if (lineT is PriceLine)
			{
				if (price.ApproxCompare(double.MinValue) == 0)
					throw new ArgumentException("missing price line Y");
			}

			DrawingTool.SetDrawingToolCommonValues(lineT, tag, isAutoScale, owner, isGlobal);

			// dont nuke existing anchor refs on the instance
			ChartAnchor anchor;

			// check if its one of the single anchor lines
			if (lineT is PriceLine || lineT is TimeLine)
			{
				anchor = DrawingTool.CreateChartAnchor(owner, barsAgo, time, price);
				anchor.CopyDataValues(lineT.Anchor);
			}

			if (brush != null)
				lineT.LabelOutlineStroke = new Stroke(brush, dashStyle, width) { RenderTarget = lineT.LabelOutlineStroke.RenderTarget };

			lineT.SetState(State.Active);
			return lineT;
		}

		// horizontal line overloads
		private static PriceLine PriceLineCore(NinjaScriptBase owner, bool isAutoScale, string tag,
												double y, Brush brush, DashStyleHelper dashStyle, int width)
		{
			return DrawLabelLineTypeCore<PriceLine>(owner, isAutoScale, tag, 0, Core.Globals.InitialDate, y, brush, dashStyle, width, false, null);
		}

		// vertical line overloads
		private static TimeLine TimeLineCore(NinjaScriptBase owner, bool isAutoScale, string tag,
												int barsAgo, DateTime time, Brush brush, DashStyleHelper dashStyle, int width)
		{
			return DrawLabelLineTypeCore<TimeLine>(owner, isAutoScale, tag, barsAgo, time, double.MinValue, brush, dashStyle, width, false, null);
		}

		#endregion

		#region Price line methods

		/// <summary>
		/// Draws a price label line.
		/// </summary>
		/// <param name="owner">The hosting NinjaScript object which is calling the draw method</param>
		/// <param name="tag">A user defined unique id used to reference the draw object</param>
		/// <param name="y">The y value or Price for the object</param>
		/// <param name="brush">The brush used to color draw object</param>
		/// <returns></returns>
		public static PriceLine PriceLine(NinjaScriptBase owner, string tag, double y, Brush brush)
		{
			return PriceLineCore(owner, false, tag, y, brush, DashStyleHelper.Solid, 1);
		}

		/// <summary>
		/// Draws a price label line.
		/// </summary>
		/// <param name="owner">The hosting NinjaScript object which is calling the draw method</param>
		/// <param name="tag">A user defined unique id used to reference the draw object</param>
		/// <param name="y">The y value or Price for the object</param>
		/// <param name="brush">The brush used to color draw object</param>
		/// <param name="dashStyle">The dash style used for the lines of the object.</param>
		/// <param name="width">The width of the draw object</param>
		/// <returns></returns>
		public static PriceLine PriceLine(NinjaScriptBase owner, string tag, double y, Brush brush,
													DashStyleHelper dashStyle, int width)
		{
			return PriceLineCore(owner, false, tag, y, brush, dashStyle, width);
		}

		/// <summary>
		/// Draws a price label line.
		/// </summary>
		/// <param name="owner">The hosting NinjaScript object which is calling the draw method</param>
		/// <param name="tag">A user defined unique id used to reference the draw object</param>
		/// <param name="y">The y value or Price for the object</param>
		/// <param name="brush">The brush used to color draw object</param>
		/// <param name="drawOnPricePanel">Determines if the draw-object should be on the price panel or a separate panel</param>
		/// <returns></returns>
		public static PriceLine PriceLine(NinjaScriptBase owner, string tag, double y, Brush brush, bool drawOnPricePanel)
		{
			return DrawingTool.DrawToggledPricePanel(owner, drawOnPricePanel, () =>
				PriceLineCore(owner, false, tag, y, brush, DashStyleHelper.Solid, 1));
		}

		/// <summary>
		/// Draws a price label line.
		/// </summary>
		/// <param name="owner">The hosting NinjaScript object which is calling the draw method</param>
		/// <param name="tag">A user defined unique id used to reference the draw object</param>
		/// <param name="y">The y value or Price for the object</param>
		/// <param name="brush">The brush used to color draw object</param>
		/// <param name="dashStyle">The dash style used for the lines of the object.</param>
		/// <param name="width">The width of the draw object</param>
		/// <param name="drawOnPricePanel">Determines if the draw-object should be on the price panel or a separate panel</param>
		/// <returns></returns>
		public static PriceLine PriceLine(NinjaScriptBase owner, string tag, double y, Brush brush,
													DashStyleHelper dashStyle, int width, bool drawOnPricePanel)
		{
			return DrawingTool.DrawToggledPricePanel(owner, drawOnPricePanel, () =>
				PriceLineCore(owner, false, tag, y, brush, dashStyle, width));
		}

		/// <summary>
		/// Draws a price label line.
		/// </summary>
		/// <param name="owner">The hosting NinjaScript object which is calling the draw method</param>
		/// <param name="tag">A user defined unique id used to reference the draw object</param>
		/// <param name="y">The y value or Price for the object</param>
		/// <param name="isGlobal">Determines if the draw object will be global across all charts which match the instrument</param>
		/// <param name="templateName">The name of the drawing tool template the object will use to determine various visual properties</param>
		/// <returns></returns>
		public static PriceLine PriceLine(NinjaScriptBase owner, string tag, double y, bool isGlobal, string templateName)
		{
			return DrawLabelLineTypeCore<PriceLine>(owner, false, tag, int.MinValue, Core.Globals.InitialDate, y, null, DashStyleHelper.Solid, 1, isGlobal, templateName);
		}

		/// <summary>
		/// Draws a price label line.
		/// </summary>
		/// <param name="owner">The hosting NinjaScript object which is calling the draw method</param>
		/// <param name="tag">A user defined unique id used to reference the draw object</param>
		/// <param name="isAutoScale">Determines if the draw object will be included in the y-axis scale</param>
		/// <param name="y">The y value or Price for the object</param>
		/// <param name="brush">The brush used to color draw object</param>
		/// <param name="dashStyle">The dash style used for the lines of the object.</param>
		/// <param name="width">The width of the draw object</param>
		/// <returns></returns>
		public static PriceLine PriceLine(NinjaScriptBase owner, string tag, bool isAutoScale, double y, Brush brush,
													DashStyleHelper dashStyle, int width)
		{
			return PriceLineCore(owner, isAutoScale, tag, y, brush, dashStyle, width);
		}

		/// <summary>
		/// Draws a price label line.
		/// </summary>
		/// <param name="owner">The hosting NinjaScript object which is calling the draw method</param>
		/// <param name="tag">A user defined unique id used to reference the draw object</param>
		/// <param name="isAutoscale">if set to <c>true</c> [is autoscale].</param>
		/// <param name="y">The y value or Price for the object</param>
		/// <param name="brush">The brush used to color draw object</param>
		/// <param name="drawOnPricePanel">Determines if the draw-object should be on the price panel or a separate panel</param>
		/// <returns></returns>
		public static PriceLine PriceLine(NinjaScriptBase owner, string tag, bool isAutoscale, double y, Brush brush, bool drawOnPricePanel)
		{
			return DrawingTool.DrawToggledPricePanel(owner, drawOnPricePanel, () =>
				PriceLineCore(owner, isAutoscale, tag, y, brush, DashStyleHelper.Solid, 1));
		}


		#endregion

		#region Time Line methods

		/// <summary>
		/// Draws a time label line.
		/// </summary>
		/// <param name="owner">The hosting NinjaScript object which is calling the draw method</param>
		/// <param name="tag">A user defined unique id used to reference the draw object</param>
		/// <param name="time"> The time the object will be drawn at.</param>
		/// <param name="brush">The brush used to color draw object</param>
		/// <returns></returns>
		public static TimeLine TimeLine(NinjaScriptBase owner, string tag, DateTime time, Brush brush)
		{
			return TimeLineCore(owner, false, tag, int.MinValue, time, brush, DashStyleHelper.Solid, 1);
		}

		/// <summary>
		/// Draws a time label line.
		/// </summary>
		/// <param name="owner">The hosting NinjaScript object which is calling the draw method</param>
		/// <param name="tag">A user defined unique id used to reference the draw object</param>
		/// <param name="time"> The time the object will be drawn at.</param>
		/// <param name="brush">The brush used to color draw object</param>
		/// <param name="dashStyle">The dash style used for the lines of the object.</param>
		/// <param name="width">The width of the draw object</param>
		/// <returns></returns>
		public static TimeLine TimeLine(NinjaScriptBase owner, string tag, DateTime time, Brush brush,
													DashStyleHelper dashStyle, int width)
		{
			return TimeLineCore(owner, false, tag, int.MinValue, time, brush, dashStyle, width);
		}

		/// <summary>
		/// Draws a time label line.
		/// </summary>
		/// <param name="owner">The hosting NinjaScript object which is calling the draw method</param>
		/// <param name="tag">A user defined unique id used to reference the draw object</param>
		/// <param name="barsAgo">The bar the object will be drawn at. A value of 10 would be 10 bars ago</param>
		/// <param name="brush">The brush used to color draw object</param>
		/// <returns></returns>
		public static TimeLine TimeLine(NinjaScriptBase owner, string tag, int barsAgo, Brush brush)
		{
			return TimeLineCore(owner, false, tag, barsAgo, Core.Globals.InitialDate, brush, DashStyleHelper.Solid, 1);
		}

		/// <summary>
		/// Draws a time label line.
		/// </summary>
		/// <param name="owner">The hosting NinjaScript object which is calling the draw method</param>
		/// <param name="tag">A user defined unique id used to reference the draw object</param>
		/// <param name="barsAgo">The bar the object will be drawn at. A value of 10 would be 10 bars ago</param>
		/// <param name="brush">The brush used to color draw object</param>
		/// <param name="dashStyle">The dash style used for the lines of the object.</param>
		/// <param name="width">The width of the draw object</param>
		/// <returns></returns>
		public static TimeLine TimeLine(NinjaScriptBase owner, string tag, int barsAgo, Brush brush,
													DashStyleHelper dashStyle, int width)
		{
			return TimeLineCore(owner, false, tag, barsAgo, Core.Globals.InitialDate, brush, dashStyle, width);
		}

		/// <summary>
		/// Draws a time label line.
		/// </summary>
		/// <param name="owner">The hosting NinjaScript object which is calling the draw method</param>
		/// <param name="tag">A user defined unique id used to reference the draw object</param>
		/// <param name="time"> The time the object will be drawn at.</param>
		/// <param name="brush">The brush used to color draw object</param>
		/// <param name="dashStyle">The dash style used for the lines of the object.</param>
		/// <param name="width">The width of the draw object</param>
		/// <param name="drawOnPricePanel">Determines if the draw-object should be on the price panel or a separate panel</param>
		/// <returns></returns>
		public static TimeLine TimeLine(NinjaScriptBase owner, string tag, DateTime time, Brush brush,
													DashStyleHelper dashStyle, int width, bool drawOnPricePanel)
		{
			return DrawingTool.DrawToggledPricePanel(owner, drawOnPricePanel, () =>
				 TimeLineCore(owner, false, tag, int.MinValue, time, brush, dashStyle, width));
		}

		/// <summary>
		/// Draws a time label line.
		/// </summary>
		/// <param name="owner">The hosting NinjaScript object which is calling the draw method</param>
		/// <param name="tag">A user defined unique id used to reference the draw object</param>
		/// <param name="barsAgo">The bar the object will be drawn at. A value of 10 would be 10 bars ago</param>
		/// <param name="brush">The brush used to color draw object</param>
		/// <param name="dashStyle">The dash style used for the lines of the object.</param>
		/// <param name="width">The width of the draw object</param>
		/// <param name="drawOnPricePanel">Determines if the draw-object should be on the price panel or a separate panel</param>
		/// <returns></returns>
		public static TimeLine TimeLine(NinjaScriptBase owner, string tag, int barsAgo, Brush brush,
													DashStyleHelper dashStyle, int width, bool drawOnPricePanel)
		{
			return DrawingTool.DrawToggledPricePanel(owner, drawOnPricePanel, () =>
				TimeLineCore(owner, false, tag, barsAgo, Core.Globals.InitialDate, brush, dashStyle, width));
		}

		/// <summary>
		/// Draws a time label line.
		/// </summary>
		/// <param name="owner">The hosting NinjaScript object which is calling the draw method</param>
		/// <param name="tag">A user defined unique id used to reference the draw object</param>
		/// <param name="barsAgo">The bar the object will be drawn at. A value of 10 would be 10 bars ago</param>
		/// <param name="isGlobal">Determines if the draw object will be global across all charts which match the instrument</param>
		/// <param name="templateName">The name of the drawing tool template the object will use to determine various visual properties</param>
		/// <returns></returns>
		public static TimeLine TimeLine(NinjaScriptBase owner, string tag, int barsAgo, bool isGlobal, string templateName)
		{
			return DrawLabelLineTypeCore<TimeLine>(owner, false, tag, barsAgo, Core.Globals.InitialDate,
				double.MinValue, null, DashStyleHelper.Solid, 1, isGlobal, templateName);
		}

		/// <summary>
		/// Draws a time label line.
		/// </summary>
		/// <param name="owner">The hosting NinjaScript object which is calling the draw method</param>
		/// <param name="tag">A user defined unique id used to reference the draw object</param>
		/// <param name="time"> The time the object will be drawn at.</param>
		/// <param name="isGlobal">Determines if the draw object will be global across all charts which match the instrument</param>
		/// <param name="templateName">The name of the drawing tool template the object will use to determine various visual properties</param>
		/// <returns></returns>
		public static TimeLine TimeLine(NinjaScriptBase owner, string tag, DateTime time, bool isGlobal, string templateName)
		{
			return DrawLabelLineTypeCore<TimeLine>(owner, false, tag, int.MinValue, time,
				double.MinValue, null, DashStyleHelper.Solid, 1, isGlobal, templateName);
		}

		#endregion

	}

	#endregion

}

*/