namespace KrTrade.Nt.DI.Data
{
    public class BarsPeriod
    {
        #region Private members

        private int _periodValue; 

        #endregion

        #region Public properties

        /// <summary>
        /// Gets or sets bars period type.
        /// </summary>
        public PeriodType PeriodType { get; set; }

        /// <summary>
        /// Gets or sets the bars period value.
        /// </summary>
        public int PeriodValue 
        {
            get => _periodValue;
            set
            {
                if (_periodValue != value)
                {
                    if (value < 1)
                        return;

                    _periodValue = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the bars market data type.
        /// </summary>
        public MarketDataType MarketDataType { get; set; }

        #endregion

        #region Constructors

        public BarsPeriod() : this(PeriodType.Minute,1)
        {
        }

        public BarsPeriod(PeriodType type) : this(type,1)
        {
        }

        public BarsPeriod(int period) : this(PeriodType.Minute,period)
        {
        }

        public BarsPeriod(PeriodType type, int period)
        {
            PeriodType = type;
            PeriodValue = period;
        }

        #endregion
    }
}
