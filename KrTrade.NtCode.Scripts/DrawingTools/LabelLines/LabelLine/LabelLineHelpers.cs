#region Using declarations

using NinjaTrader.Gui;
using NinjaTrader.Gui.Chart;
using KrTrade.NtCode.Data;
using System;
using System.Windows;

#endregion

namespace KrTrade.NtCode.DrawingTools
{
    
    /// <summary>
    /// Helper methods to the label line drawing tool
    /// </summary>
    public static class LabelLineHelpers
    {

        #region Points

        /// <summary>
        /// Returns the points of the label with arrow geometry.
        /// </summary>
        /// <param name="type">The type of the label line.</param>
        /// <param name="labelOriginPoint">The label origin point.</param>
        /// <param name="textSize">The text size with margins.</param>
        /// <param name="horizontalAlignment">The horizontal alignment of the label.</param>
        /// <param name="verticalAlignment">The vertical alignment of the label.</param>
        /// <param name="pixelAdjVector">Pixel adjust for stroke with.</param>
        /// <param name="specificArrowType">Specific arrow type.</param>
        /// <param name="length">Specific length of the arrow base. Default value is the label height.</param>
        /// <param name="maxLength">The max length of the arrow base. Default value is 0, that represents the label height.</param>
        /// <param name="minLength">The minimum value of the arrow base. Default value is 5.</param>
        /// <returns>The points of the label geometry.</returns>
        public static SharpDX.Vector2[] ToLabelPoints(this LabelLineType type, Point labelOriginPoint, SharpDX.Size2F textSize, HorizontalAlignment horizontalAlignment, VerticalAlignment verticalAlignment, Vector pixelAdjVector, ArrowType specificArrowType = ArrowType.Normal, float length = 0, float maxLength = 0, float minLength = 5)
        {
            // Calculate the arrow base lengths
            float leftArrowBase = type.ToLabelArrowBaseLength(textSize.Height,horizontalAlignment,verticalAlignment,LabelArrowPlacement.OnLeft,specificArrowType,length,maxLength,minLength);
            float topArrowBase = type.ToLabelArrowBaseLength(textSize.Height,horizontalAlignment,verticalAlignment,LabelArrowPlacement.OnTop,specificArrowType,length,maxLength,minLength);
            float rightArrowBase = type.ToLabelArrowBaseLength(textSize.Height,horizontalAlignment,verticalAlignment,LabelArrowPlacement.OnRight,specificArrowType,length,maxLength,minLength);
            float bottomArrowBase = type.ToLabelArrowBaseLength(textSize.Height,horizontalAlignment,verticalAlignment,LabelArrowPlacement.OnBottom,specificArrowType,length,maxLength,minLength);

            // Calculate the arrow head lengths.
            float leftArrowHead = type.ToLabelArrowHeadLength(textSize.Height, horizontalAlignment, verticalAlignment, LabelArrowPlacement.OnLeft, specificArrowType, length, maxLength, minLength);
            float topArrowHead = type.ToLabelArrowHeadLength(textSize.Height, horizontalAlignment, verticalAlignment, LabelArrowPlacement.OnTop, specificArrowType, length, maxLength, minLength);
            float rightArrowHead = type.ToLabelArrowHeadLength(textSize.Height, horizontalAlignment, verticalAlignment, LabelArrowPlacement.OnRight, specificArrowType, length, maxLength, minLength);
            float bottomArrowHead = type.ToLabelArrowHeadLength(textSize.Height, horizontalAlignment, verticalAlignment, LabelArrowPlacement.OnBottom, specificArrowType, length, maxLength, minLength);

            // Calculate arrow offsets.
            float leftArrowOffset = (textSize.Height - leftArrowBase) / 2;
            float rightArrowOffset = (textSize.Height - rightArrowBase) / 2;
            float topArrowOffset = (textSize.Width - topArrowBase) / 2;
            float bottomArrowOffset = (textSize.Width - bottomArrowBase) / 2;

            // Left arrow
            Point point1 = new Point(
                labelOriginPoint.X, 
                labelOriginPoint.Y + topArrowHead + leftArrowOffset + leftArrowBase/2
                ) + pixelAdjVector;
            Point point2 = new Point(
                labelOriginPoint.X + leftArrowHead, 
                labelOriginPoint.Y + topArrowHead + leftArrowOffset
                ) + pixelAdjVector;
            Point point3 = new Point(
                labelOriginPoint.X + leftArrowHead, 
                labelOriginPoint.Y + topArrowHead
                ) + pixelAdjVector;

            // Top arrow
            Point point4 = new Point(
                labelOriginPoint.X + leftArrowHead + topArrowOffset, 
                labelOriginPoint.Y + topArrowHead
                ) + pixelAdjVector;
            Point point5 = new Point(
                labelOriginPoint.X + leftArrowHead + topArrowOffset + topArrowBase/2, 
                labelOriginPoint.Y
                ) + pixelAdjVector;
            Point point6 = new Point(
                labelOriginPoint.X + leftArrowHead + topArrowOffset + topArrowBase, 
                labelOriginPoint.Y + topArrowHead
                ) + pixelAdjVector;
            Point point7 = new Point(
                labelOriginPoint.X + leftArrowHead + topArrowOffset + topArrowBase + topArrowOffset, 
                labelOriginPoint.Y + topArrowHead
                ) + pixelAdjVector;

            // Right arrow
            Point point8 = new Point(
                labelOriginPoint.X + leftArrowHead + topArrowOffset + topArrowBase + topArrowOffset, 
                labelOriginPoint.Y + topArrowHead + rightArrowOffset
                ) + pixelAdjVector;
            Point point9 = new Point(
                labelOriginPoint.X + leftArrowHead + topArrowOffset + topArrowBase + topArrowOffset + rightArrowHead, 
                labelOriginPoint.Y + topArrowHead + rightArrowOffset + rightArrowBase/2
                ) + pixelAdjVector;
            Point point10 = new Point(
                labelOriginPoint.X + leftArrowHead + topArrowOffset + topArrowBase + topArrowOffset,
                labelOriginPoint.Y + topArrowHead + rightArrowOffset + rightArrowBase
                ) + pixelAdjVector;
            Point point11 = new Point(
                labelOriginPoint.X + leftArrowHead + topArrowOffset + topArrowBase + topArrowOffset,
                labelOriginPoint.Y + topArrowHead + rightArrowOffset + rightArrowBase + rightArrowOffset
                ) + pixelAdjVector;

            // Bottom arrow
            Point point12 = new Point(
                labelOriginPoint.X + leftArrowHead + bottomArrowOffset + bottomArrowBase,
                labelOriginPoint.Y + topArrowHead + rightArrowOffset + rightArrowBase + rightArrowOffset
                ) + pixelAdjVector;
            Point point13 = new Point(
                labelOriginPoint.X + leftArrowHead + bottomArrowOffset + bottomArrowBase/2,
                labelOriginPoint.Y + topArrowHead + rightArrowOffset + rightArrowBase + rightArrowOffset + bottomArrowHead
                ) + pixelAdjVector;
            Point point14 = new Point(
                labelOriginPoint.X + leftArrowHead + bottomArrowOffset,
                labelOriginPoint.Y + topArrowHead + rightArrowOffset + rightArrowBase + rightArrowOffset
                ) + pixelAdjVector;
            Point point15 = new Point(
                labelOriginPoint.X + leftArrowHead,
                labelOriginPoint.Y + topArrowHead + rightArrowOffset + rightArrowBase + rightArrowOffset
                ) + pixelAdjVector;

            return new SharpDX.Vector2[] 
            { 
                point1.ToVector2(),
                point2.ToVector2(),
                point3.ToVector2(),
                point4.ToVector2(),
                point5.ToVector2(),
                point6.ToVector2(),
                point7.ToVector2(),
                point8.ToVector2(),
                point9.ToVector2(),
                point10.ToVector2(),
                point11.ToVector2(),
                point12.ToVector2(),
                point13.ToVector2(),
                point14.ToVector2(),
                point15.ToVector2(),
            };
        }

        /// <summary>
        /// Returns the label begin point.
        /// </summary>
        /// <param name="type">The type of the label line.</param>
        /// <param name="labelOriginPoint">The label origin point.</param>
        /// <param name="textSize">The text size with margins.</param>
        /// <param name="horizontalAlignment">The horizontal alignment of the label.</param>
        /// <param name="verticalAlignment">The vertical alignment of the label.</param>
        /// <param name="pixelAdjVector">Pixel adjust for stroke with.</param>
        /// <param name="specificArrowType">Specific arrow type.</param>
        /// <param name="length">Specific length of the arrow base. Default value is the label height.</param>
        /// <param name="maxLength">The max length of the arrow base. Default value is 0, that represents the label height.</param>
        /// <param name="minLength">The minimum value of the arrow base. Default value is 5.</param>
        /// <returns>The label begin point.</returns>
        public static SharpDX.Vector2 ToLabelBeginPoint(this LabelLineType type, Point labelOriginPoint, SharpDX.Size2F textSize, HorizontalAlignment horizontalAlignment, VerticalAlignment verticalAlignment, Vector pixelAdjVector, ArrowType specificArrowType = ArrowType.Normal, float length = 0, float maxLength = 0, float minLength = 5)
        {
            float leftArrowBase = type.ToLabelArrowBaseLength(textSize.Height, horizontalAlignment, verticalAlignment, LabelArrowPlacement.OnLeft, specificArrowType, length, maxLength, minLength);
            float leftArrowOffset = (textSize.Height - leftArrowBase) / 2;

            Point beginPoint = new Point(
                labelOriginPoint.X + type.ToLabelArrowHeadLength(textSize.Height, horizontalAlignment, verticalAlignment, LabelArrowPlacement.OnLeft, specificArrowType, length, maxLength, minLength),
                labelOriginPoint.Y + type.ToLabelArrowHeadLength(textSize.Height, horizontalAlignment, verticalAlignment, LabelArrowPlacement.OnTop, specificArrowType, length, maxLength, minLength) + leftArrowBase + leftArrowOffset
                ) + pixelAdjVector;

            return beginPoint.ToVector2();
        }

        /// <summary>
        /// Returns the start point of the line.
        /// </summary>
        /// <param name="type">The type of the label line.</param>
        /// <param name="horizontalAlignment">The horizontal alignment of the label.</param>
        /// <param name="verticalAlignment">The vertical alignment of the label.</param>
        /// <param name="panel">The chart panel.</param>
        /// <param name="labelOriginPoint">The label origin point.</param>
        /// <param name="labelSize">The label size without margins.</param>
        /// <param name="anchorPoint">The point clicked or selected by the user.</param>
        /// <param name="pixelAdjVector">Pixel adjust for stroke with.</param>
        /// <returns>The start point of the line.</returns>
        public static SharpDX.Vector2 ToLineStartPoint(this LabelLineType type, HorizontalAlignment horizontalAlignment, VerticalAlignment verticalAlignment, ChartPanel panel, Point labelOriginPoint, SharpDX.Size2F labelSize, Point anchorPoint, Vector pixelAdjVector)
        {

            Point startPoint = new Point(0, 0);

            if (type == LabelLineType.Price)
            {
                switch (horizontalAlignment)
                {
                    case (HorizontalAlignment.Left):
                        startPoint = new Point(
                            labelOriginPoint.X + labelSize.Width, 
                            anchorPoint.Y);
                        break;
                    case (HorizontalAlignment.Center):
                        startPoint = new Point(
                            panel.X, 
                            anchorPoint.Y);
                        break;
                    default:
                        startPoint = new Point(
                            panel.X, 
                            anchorPoint.Y);
                        break;
                }
            }

            if (type == LabelLineType.Time)
            {
                switch (verticalAlignment)
                {
                    case (VerticalAlignment.Bottom):
                        startPoint = new Point(
                            anchorPoint.X, 
                            panel.Y);
                        break;
                    case (VerticalAlignment.Center):
                        startPoint = new Point(
                            anchorPoint.X, 
                            labelOriginPoint.Y + labelSize.Height);
                        break;
                    default:
                        startPoint = new Point(
                            anchorPoint.X, 
                            labelOriginPoint.Y + labelSize.Height);
                        break;
                }
            }

            if (type == LabelLineType.Bar)
            {


            }

            if (startPoint != null && pixelAdjVector != null)
                startPoint += pixelAdjVector;

            return startPoint.ToVector2();
        }

        /// <summary>
        /// Returns the final point of the line.
        /// </summary>
        /// <param name="type">The type of the label line.</param>
        /// <param name="horizontalAlignment">The horizontal alignment of the label.</param>
        /// <param name="verticalAlignment">The vertical alignment of the label.</param>
        /// <param name="panel">The chart panel.</param>
        /// <param name="labelOriginPoint">The label origin point.</param>
        /// <param name="labelSize">The label size without margins.</param>
        /// <param name="anchorPoint">The point clicked or selected by the user.</param>
        /// <param name="pixelAdjVector">Pixel adjust for stroke with.</param>
        /// <returns>The final point of the line.</returns>
        public static SharpDX.Vector2 ToLineFinalPoint(this LabelLineType type, HorizontalAlignment horizontalAlignment, VerticalAlignment verticalAlignment, ChartPanel panel, Point labelOriginPoint, SharpDX.Size2F labelSize, Point anchorPoint, Vector pixelAdjVector)
        {
            Point finalPoint = new Point(0, 0);

            if (type == LabelLineType.Price)
            {
                switch (horizontalAlignment)
                {
                    case (HorizontalAlignment.Left):
                        finalPoint = new Point(
                            panel.X + panel.W,
                            anchorPoint.Y);
                        break;
                    case (HorizontalAlignment.Center):
                        finalPoint = new Point(
                            labelOriginPoint.X,
                            anchorPoint.Y);
                        break;
                    default:
                        finalPoint = new Point(
                            labelOriginPoint.X,
                            anchorPoint.Y);
                        break;
                }
            }

            if (type == LabelLineType.Time)
            {
                switch (verticalAlignment)
                {
                    case (VerticalAlignment.Bottom):
                        finalPoint = new Point(
                            anchorPoint.X,
                            labelOriginPoint.Y);
                        break;
                    case (VerticalAlignment.Center):
                        finalPoint = new Point(
                            anchorPoint.X,
                            panel.Y + panel.H);
                        break;
                    default:
                        finalPoint = new Point(
                            anchorPoint.X,
                            panel.Y + panel.H);
                        break;
                }
            }

            if (type == LabelLineType.Bar)
            {


            }

            if (finalPoint != null && pixelAdjVector != null)
                finalPoint += pixelAdjVector;

            return finalPoint.ToVector2();
        }

        /// <summary>
        /// Returns the text origin point.
        /// </summary>
        /// <param name="type">The label line type.</param>
        /// <param name="labelOriginPoint">The label origin point.</param>
        /// <param name="horizontalAlignment">The horizontal alignment of the label.</param>
        /// <param name="verticalAlignment">The vertical alignment of the label.</param>
        /// <param name="textSize">The text size with margins.</param>
        /// <param name="textMargin">The text margins.</param>
        /// <param name="arrowType">Specific arrow type.</param>
        /// <param name="pixelAdjVector">Pixel adjust for stroke with.</param>
        /// <returns>The text origin point.</returns>
        public static SharpDX.Vector2 ToTextOriginPoint(this LabelLineType type, Point labelOriginPoint, HorizontalAlignment horizontalAlignment, VerticalAlignment verticalAlignment, SharpDX.Size2F textSize, Margin textMargin, ArrowType arrowType, Vector pixelAdjVector)
        {

            Point textOriginPoint = new Point(
                labelOriginPoint.X + type.ToLabelArrowHeadLength(textSize.Height, horizontalAlignment, verticalAlignment, LabelArrowPlacement.OnLeft, arrowType) + textMargin.Left,
                labelOriginPoint.Y + type.ToLabelArrowHeadLength(textSize.Height, horizontalAlignment, verticalAlignment, LabelArrowPlacement.OnTop, arrowType) + textMargin.Top
                );

            if (pixelAdjVector != null)
                textOriginPoint += pixelAdjVector;

            return textOriginPoint.ToVector2();
        }

        /// <summary>
        /// Returns the origin point of the label. This point is location in the top-left of the label without label margins.
        /// </summary>
        /// <param name="type">The label line type.</param>
        /// <param name="horizontalAlignment">The horizontal alignment of the label.</param>
        /// <param name="verticalAlignment">The vertical alignment of the label.</param>
        /// <param name="panel">The chart panel.</param>
        /// <param name="labelMargin">The label margins.</param>
        /// <param name="labelSize">The label size without margins.</param>
        /// <param name="anchorPoint">The point clicked or selected by the user.</param>
        /// <returns>The origin (top-left) point of the label.</returns>
        public static Point ToLabelOriginPoint(this LabelLineType type, HorizontalAlignment horizontalAlignment, VerticalAlignment verticalAlignment, ChartPanel panel, Margin labelMargin, SharpDX.Size2F labelSize, Point anchorPoint)
        {
            if (type == LabelLineType.Price)
            {
                switch (horizontalAlignment)
                {
                    case (HorizontalAlignment.Left):
                        return new Point(
                            panel.X + labelMargin.Left,
                            (float)anchorPoint.Y - labelSize.Height/2);
                    case (HorizontalAlignment.Center):
                        return new Point(
                            (panel.X + panel.W - labelSize.Width)/2,
                            (float)anchorPoint.Y - labelSize.Height / 2);
                    default:
                        return new Point(
                            panel.X + panel.W - labelMargin.Right - labelSize.Width,
                            (float)anchorPoint.Y - labelSize.Height / 2);
                }
            }

            if (type == LabelLineType.Time)
            {
                switch (verticalAlignment)
                {
                    case (VerticalAlignment.Bottom):
                        return new Point(
                            (float)anchorPoint.X - labelSize.Width / 2, 
                            panel.Y + panel.H - labelMargin.Bottom - labelSize.Height);
                    case (VerticalAlignment.Center):
                        return new Point(
                            (float)anchorPoint.X - labelSize.Width / 2, 
                            (panel.Y + panel.H - labelSize.Height)/2);
                    default:
                        return new Point(
                            (float)anchorPoint.X - labelSize.Width / 2,
                            panel.Y + labelMargin.Top);
                }
            }

            if (type == LabelLineType.Bar)
            {


            }

            return new Point();
        }

        #endregion

        #region Arrow

        /// <summary>
        /// Returns the arrow base length.
        /// </summary>
        /// <param name="type">The label line type.</param>
        /// <param name="textHeight">The height of the text.</param>
        /// <param name="horizontalAlignment">The horizontal alignment of the label.</param>
        /// <param name="verticalAlignment">The vertical alignment of the label.</param>
        /// <param name="placement">The arrow placement.</param>
        /// <param name="specificArrowType">The specific arrow type select by the user interface. Default value is 'Normal'.</param>
        /// <param name="length">Specific length of the arrow base. Default value is the label height.</param>
        /// <param name="maxLength">The max length of the arrow base. Default value is 0, that represents the label height.</param>
        /// <param name="minLength">The minimum value of the arrow base. Default value is 5.</param>
        /// <returns>The length of the arrow base.</returns>
        public static float ToLabelArrowBaseLength(this LabelLineType type, float textHeight, HorizontalAlignment horizontalAlignment, VerticalAlignment verticalAlignment, LabelArrowPlacement placement, ArrowType specificArrowType = ArrowType.Normal, float length = 0, float maxLength = 0, float minLength = 5)
        {
            // Store the specific arrow type.
            ArrowType arrowType = type.ToArrowType(horizontalAlignment, verticalAlignment, placement, specificArrowType);

            // If arrow type is flat the base is 0.
            if (arrowType == ArrowType.Flat)
                return 0f;

            // Make sure the minimum length of the arrow base is minor than the maximum length.
            if (maxLength > 0 && minLength > maxLength)
                throw new Exception("The minimum length of the base an't be more large than maximum length of the base.");

            // Store the default final length.
            float finalLength = length == 0f ? textHeight : length > textHeight ? textHeight : length;

            // The maximum length of the arrow base for price label lines is the label height.
            if (maxLength == 0 || (type == LabelLineType.Price && maxLength > textHeight))
                maxLength = textHeight;

            // Adjust the superior limit.
            if (finalLength > maxLength)
                finalLength = maxLength;

            // Adjust the inferior limit.
            if (finalLength < minLength)
                finalLength = minLength;

            // Return the value.
            return finalLength;
        }

        /// <summary>
        /// Return the arrow head length.
        /// </summary>
        /// <param name="type">The label line type.</param>
        /// <param name="textHeight">The height of the text.</param>
        /// <param name="horizontalAlignment">The horizontal alignment of the label.</param>
        /// <param name="verticalAlignment">The vertical alignment of the label.</param>
        /// <param name="placement">The arrow placement.</param>
        /// <param name="specificArrowType">The specific arrow type select by the user interface. Default value is 'Normal'.</param>
        /// <param name="length">Specific length of the arrow base. Default value is the label height.</param>
        /// <param name="maxLength">The max length of the arrow base. Default value is 0, that represents the label height.</param>
        /// <param name="minLength">The minimum value of the arrow base. Default value is 5.</param>
        /// <returns>The length of the arrow head.</returns>
        public static float ToLabelArrowHeadLength(this LabelLineType type, float textHeight, HorizontalAlignment horizontalAlignment, VerticalAlignment verticalAlignment, LabelArrowPlacement placement, ArrowType specificArrowType = ArrowType.Normal, float length = 0, float maxLength = 0, float minLength = 5)
        {
            // Store the specific arrow type.
            ArrowType arrowType = type.ToArrowType(horizontalAlignment, verticalAlignment, placement, specificArrowType);
            float arrowBase = type.ToLabelArrowBaseLength(textHeight, horizontalAlignment, verticalAlignment, placement, specificArrowType, length, maxLength, minLength);

            switch (arrowType)
            {
                case (ArrowType.Normal):
                    return arrowBase * 0.6f;
                case (ArrowType.Large):
                    return arrowBase * 0.9f;
                case (ArrowType.Short):
                    return arrowBase * 0.3f;
                case (ArrowType.ExtraLarge):
                    return arrowBase * 1.5f;
                case (ArrowType.VeryShort):
                    return arrowBase * 0.1f;
                default:
                    return 0f;
            }
        }

        /// <summary>
        /// Returns the specific arrow for the specific label line with specific alignment.
        /// </summary>
        /// <param name="type">The label line type.</param>
        /// <param name="horizontalAlignment">The horizontal alignment of the label.</param>
        /// <param name="verticalAlignment">The vertical alignment of the label.</param>
        /// <param name="placement">The arrow placement.</param>
        /// <param name="specificArrowType">The specific arrow type select by the user interface. Default value is 'Normal'.</param>
        /// <returns>The arrow type of the specific label line.</returns>
        public static ArrowType ToArrowType(this LabelLineType type, HorizontalAlignment horizontalAlignment, VerticalAlignment verticalAlignment, LabelArrowPlacement placement, ArrowType specificArrowType = ArrowType.Normal)
        {

            if (type == LabelLineType.Price)
            {
                if (placement == LabelArrowPlacement.OnTop || placement == LabelArrowPlacement.OnBottom)
                    return ArrowType.Flat;

                else if (placement == LabelArrowPlacement.OnLeft)
                {
                    switch (horizontalAlignment)
                    {
                        case (HorizontalAlignment.Left):
                            return ArrowType.Flat;

                        default:
                            return specificArrowType;
                    }
                }
                else
                    switch (horizontalAlignment)
                    {
                        case (HorizontalAlignment.Left):
                            return specificArrowType;
                        default:
                            return ArrowType.Flat;
                    }
            }

            else if (type == LabelLineType.Time)
            {
                if (placement == LabelArrowPlacement.OnLeft || placement == LabelArrowPlacement.OnRight)
                    return ArrowType.Flat;

                else if (placement == LabelArrowPlacement.OnTop)
                {
                    switch (verticalAlignment)
                    {
                        case (VerticalAlignment.Bottom):
                            return specificArrowType;
                        default:
                            return ArrowType.Flat;
                    }
                }
                else 
                    switch (verticalAlignment)
                    {
                        case (VerticalAlignment.Bottom):
                            return ArrowType.Flat;
                        default:
                            return specificArrowType;
                    }
            }

            else
            {
                if (placement == LabelArrowPlacement.OnLeft || placement == LabelArrowPlacement.OnRight)
                    return ArrowType.Flat;

                else if (placement == LabelArrowPlacement.OnTop)
                {
                    switch (verticalAlignment)
                    {
                        case (VerticalAlignment.Bottom):
                            return specificArrowType;
                        default:
                            return ArrowType.Flat;
                    }
                }
                else
                {
                    switch (verticalAlignment)
                    {
                        case (VerticalAlignment.Top):
                            return specificArrowType;
                        default:
                            return ArrowType.Flat;
                    }
                }
            }
        }

        #endregion

    }
    
}
