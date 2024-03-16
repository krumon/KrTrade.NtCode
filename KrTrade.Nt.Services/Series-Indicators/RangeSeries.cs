using System;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Cache to store the lastest market range price.
    /// </summary>
    public class RangeSeries : IndicatorSeries<MaxSeries,MinSeries,INumericSeries<double>,INumericSeries<double>>
    {
        public override string Name => "Range";

        public RangeSeries(IBarsService barsService, int period) : base(barsService, period)
        {
        }

        public RangeSeries(IBarsSeries barsSeries, int period, int barsIndex) : this(barsSeries.High, barsSeries.Low, period, barsIndex)
        {
        }

        public RangeSeries(INumericSeries<double> entry1, INumericSeries<double> entry2, int period, int barsIndex) : base(entry1, entry2, period, barsIndex)
        {
        }

        public RangeSeries(MaxSeries input1, MinSeries input2, int period, int barsIndex) : base(input1, input2, period, barsIndex)
        {
            if (Input1.Period != Input2.Period)
                throw new Exception("Los indicadores 'MAX' y 'MIN' deben tener el mismo periodo.");
            Period = Input1.Period;
        }

        public override MaxSeries GetInput1(INumericSeries<double> entry1)
        {
            if (entry1 is MaxSeries maxSeries) return maxSeries;
            return new MaxSeries(entry1,Period,BarsIndex);
        }

        public override MinSeries GetInput2(INumericSeries<double> entry2)
        {
            if (entry2 is MinSeries minSeries) return minSeries;
            return new MinSeries(entry2, Period, BarsIndex);
        }

        protected override double GetCandidateValue(int barsAgo, bool isCandidateValueForUpdate)
            => Input1[0] - Input2[0];

        protected override double GetInitValuePreviousRecalculate() 
            => 0;

        protected override bool CheckAddConditions(double currentValue, double candidateValue)
            => candidateValue >= currentValue; 

        protected override bool CheckUpdateConditions(double currentValue, double candidateValue)
            => candidateValue >= currentValue;

    }
}
