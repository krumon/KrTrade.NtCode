namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Cache to store the lastest market high prices.
    /// </summary>
    public class MaxSeries : IndicatorSeries<INumericSeries<double>>
    {

        /// <summary>
        /// Create default instance with specified parameters.
        /// </summary>
        /// <param name="barsSeries">The bars series instance used to gets series.</param>
        /// <param name="period">The specified period to calculate values in cache.</param>
        /// <param name="barsIndex">The index of the 'NinjaScript.Series' necesary for gets the cache elements.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        public MaxSeries(IBarSeries barsSeries, int period, int barsIndex) : this(barsSeries.Low, period, barsIndex)
        {
        }

        /// <inheritdoc/>
        public MaxSeries(IBarsService barsService, int period) : base(barsService, period)
        {
        }

        /// <inheritdoc/>
        public MaxSeries(INumericSeries<double> input, int period, int barsIndex) : base(input, period, barsIndex)
        {
        }

        public override string Name => "Max";

        protected override double GetInitValuePreviousRecalculate() 
            => double.MinValue;

        protected override bool CheckAddConditions(double currentValue, double candidateValue) 
            => candidateValue >= currentValue;

        protected override bool CheckUpdateConditions(double currentValue, double candidateValue) 
            => candidateValue >= currentValue;

        protected override double GetCandidateValue(int barsAgo, bool isCandidateValueForUpdate)
            => Input[barsAgo];

    }
}
