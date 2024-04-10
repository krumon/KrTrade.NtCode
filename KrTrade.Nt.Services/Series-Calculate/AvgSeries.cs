using System;

namespace KrTrade.Nt.Services.Series
{
    /// <summary>
    /// Series to store the lastest average prices for specified period.
    /// </summary>
    public class AvgSeries : IndicatorSeries<SumSeries>
    {

        /// <inheritdoc/>
        public AvgSeries(IBarsService barsService, int period) : base(barsService, period)
        {
        }

        /// <inheritdoc/>
        public AvgSeries(SumSeries input, int period, int barsIndex) : base(input, period, barsIndex)
        {
        }

        public override string Name => "Avg";

        protected override double GetCandidateValue(int barsAgo, bool isCandidateValueForUpdate) 
            => Input[0] / Math.Min(Count, Period);

        protected override double GetInitValuePreviousRecalculate()
            => 0;

        protected override bool CheckAddConditions(double currentValue, double candidateValue) 
            => candidateValue != currentValue;

        protected override bool CheckUpdateConditions(double currentValue, double candidateValue) 
            => candidateValue != currentValue;

    }
}
