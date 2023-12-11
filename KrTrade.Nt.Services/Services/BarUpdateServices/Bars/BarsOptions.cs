namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Represents the options of <see cref="BarsService"/>.
    /// </summary>
    public class BarsOptions : NinjascriptServiceOptions
    {
        private int _displacement = 0;
        private int _period = 0;

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
        /// Gets the number of the bars. 
        /// </summary>
        public int Period
        {
            get => _period;
            set
            {
                if (_period != value && _period >= 0)
                    _period = value;
            }
        }

        #endregion

    }
}
