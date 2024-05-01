using KrTrade.Nt.Core.Series;

namespace KrTrade.Nt.Services.Series
{

    /// <summary>
    /// Series to store the range value between two series in the specified period.
    /// </summary>
    public class RangeSeries : BaseNumericPeriodSeries
    {

        public ISeries<double> Input1 { get; protected set; }
        public ISeries<double> Input2 { get; protected set; }

        public RangeSeries(IBarsService bars, PeriodSeriesInfo info) : base(bars, info)
        {
            // Comprobar la Info
        }

        protected override double InitializeLastValue()
            => 0;
        protected override double GetCandidateValue(bool isCandidateValueToUpdate)
            => Input1[0] - Input2[0];
        protected override bool IsValidValueToAdd(double candidateValue, bool isFirstValueToAdd)
            => isFirstValueToAdd || candidateValue >= CurrentValue;
        protected override bool IsValidValueToUpdate(double candidateValue)
            => candidateValue >= CurrentValue;

        internal override void Configure(out bool isConfigured)
        {
            // Configurar la Info
            isConfigured = true;
        }
        internal override void DataLoaded(out bool isDataLoaded)
        {
            if (Info.Inputs == null || Info.Inputs.Count == 0)
                isDataLoaded = false;
            else
            {
                bool loaded = false;
                for (int i = 0; i < Info.Inputs.Count; i++)
                {
                    if (i == 0)
                    {
                        // Obtener la serie desde Bars.SeriesCollection<INumericSeries>. La tengo que convertir en SUM(Series)
                        loaded = true;
                    }
                    else
                    {
                        // Mensaje de warning por fallo del usuario al configurar la series e introducir más inputs de los que la serie admite.
                    }
                }
                isDataLoaded = loaded;
            }
        }
    }

    ///// <summary>
    ///// Cache to store the lastest market range price.
    ///// </summary>
    //public class RangeSeries : BaseNumericPeriodSeries<MaxSeries,MinSeries,INumericSeries<double>,INumericSeries<double>>
    //{
    //    //public override string Name => "Range";

    //    public RangeSeries(IBarsService barsService, int period) : base(barsService, period)
    //    {
    //    }

    //    public RangeSeries(IBarsSeries barsSeries, int period, int barsIndex) : this(barsSeries.High, barsSeries.Low, period, barsIndex)
    //    {
    //    }

    //    public RangeSeries(INumericSeries<double> entry1, INumericSeries<double> entry2, int period, int barsIndex) : base(entry1, entry2, period, barsIndex)
    //    {
    //    }

    //    public RangeSeries(MaxSeries input1, MinSeries input2, int period, int barsIndex) : base(input1, input2, period, barsIndex)
    //    {
    //        if (Input1.Period != Input2.Period)
    //            throw new Exception("Los indicadores 'MAX' y 'MIN' deben tener el mismo periodo.");
    //        Period = Input1.Period;
    //    }

    //    public override MaxSeries GetInput1(INumericSeries<double> entry1)
    //    {
    //        if (entry1 is MaxSeries maxSeries) return maxSeries;
    //        return new MaxSeries(entry1,Period,BarsIndex);
    //    }

    //    public override MinSeries GetInput2(INumericSeries<double> entry2)
    //    {
    //        if (entry2 is MinSeries minSeries) return minSeries;
    //        return new MinSeries(entry2, Period, BarsIndex);
    //    }

    //    protected override double GetCandidateValue(int barsAgo, bool isCandidateValueForUpdate)
    //        => Input1[0] - Input2[0];

    //    protected override double GetInitValuePreviousRecalculate() 
    //        => 0;

    //    protected override bool IsValidValueToAdd(double currentValue, double candidateValue)
    //        => candidateValue >= currentValue; 

    //    protected override bool IsValidValueToUpdate(double currentValue, double candidateValue)
    //        => candidateValue >= currentValue;

    //}
}
