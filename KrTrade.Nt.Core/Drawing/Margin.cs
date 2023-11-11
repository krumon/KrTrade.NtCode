namespace KrTrade.Nt.Core.Drawing
{
    /// <summary>
    /// Represents the four margin of an object
    /// </summary>
    public class Margin
    {
        #region Public properties

        /// <summary>
        /// The left margin.
        /// </summary>
        public float Left { get; set; }

        /// <summary>
        /// The top margin.
        /// </summary>
        public float Top { get; set; }

        /// <summary>
        /// The right margin
        /// </summary>
        public float Right { get; set; }

        /// <summary>
        /// The bottom margin
        /// </summary>
        public float Bottom { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Create a default instance of <see cref="Margin"/> class.
        /// </summary>
        public Margin()
        {

        }

        /// <summary>
        /// Create a new instance of <see cref="Margin"/>class with specific values for each margin.
        /// </summary>
        /// <param name="left">Left margin value.</param>
        /// <param name="top">Top margin value.</param>
        /// <param name="right">Right margin value.</param>
        /// <param name="bottom">Bottom margin value.</param>
        public Margin(float left, float top, float right, float bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        /// <summary>
        /// Create a new instance of <see cref="Margin"/> class with unique value for all margins.
        /// </summary>
        public Margin(float margin)
        {
            Left = margin;
            Top = margin;
            Right = margin;
            Bottom = margin;
        }

        /// <summary>
        /// Create a new instance of <see cref="Margin"/> class with vertical and horizontal marfin values.
        /// </summary>
        /// <param name="horizontalMargin">Left and right margin value.</param>
        /// <param name="verticalMargin">Top and bottom margin value.</param>
        public Margin(float horizontalMargin, float verticalMargin)
        {
            Left = horizontalMargin;
            Top = verticalMargin;
            Right = horizontalMargin;
            Bottom = verticalMargin;
        }

        #endregion

    }
}
