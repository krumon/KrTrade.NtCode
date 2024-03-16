namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Cache to store the lastest summary prices for specified period.
    /// </summary>
    public class SumSeries : IndicatorSeries<INumericSeries<double>>
    {

        /// <summary>
        /// Create default instance with specified parameters.
        /// </summary>
        /// <param name="barsSeries">The bars series instance used to gets series.</param>
        /// <param name="period">The specified period to calculate values in cache.</param>
        /// <param name="barsIndex">The index of the 'NinjaScript.Series' necesary for gets the cache elements.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        public SumSeries(IBarsSeries barsSeries, int period, int barsIndex) : this(barsSeries.Low, period, barsIndex)
        {
        }

        /// <inheritdoc/>
        public SumSeries(IBarsService barsService, int period) : base(barsService, period)
        {
        }

        /// <inheritdoc/>
        public SumSeries(INumericSeries<double> input, int period, int barsIndex) : base(input, period, barsIndex)
        {
        }

        public override string Name => $"Sum";
        protected override double GetCandidateValue(int barsAgo, bool isCandidateValueForUpdate)
        {
            if (!isCandidateValueForUpdate)
            {
                if (Count == 0 && barsAgo < Input.Count)
                    return Input[barsAgo];
                if (Input.Count > Period)
                    return this[barsAgo] + Input[barsAgo] - Input[Period];
                else
                    return this[barsAgo] + Input[barsAgo];
            }
            return Input.Count > Period && barsAgo + 1 < Input.Count ? this[1] + Input[0] - Input[Period] : this[1] + Input[0];
        }

        protected override double GetInitValuePreviousRecalculate()
            => 0;

        protected override bool CheckAddConditions(double currentValue, double candidateValue)
            => candidateValue != currentValue;

        protected override bool CheckUpdateConditions(double currentValue, double candidateValue)
            => candidateValue != currentValue;

    }
}
