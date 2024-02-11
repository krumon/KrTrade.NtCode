namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Represents the options of <see cref="BarService"/>.
    /// </summary>
    public class BarOptions : BarUpdateServiceOptions
    {
        private int _displacement = 0;
        private int _barsIdx = 0;

        #region Public properties

        /// <summary>
        /// Gets the bars ago of the bar in the bars collection. 
        /// This bars ago 0 is the last bar of the bars collection.
        /// </summary>
        public int Displacement 
        {
            get => _displacement;
            set
            {
                if (_displacement != value && _displacement >= 0)
                    _displacement = value;
            }
        } 

        /// <summary>
        /// Gets the index of the bars series. 
        /// </summary>
        public int BarsIdx
        {
            get => _barsIdx;
            set
            {
                if (_barsIdx != value && _barsIdx >= 0)
                    _barsIdx = value;
            }
        }

        #endregion

    }
}
