using KrTrade.Nt.Core.Series;

namespace KrTrade.Nt.Services.Series
{

    /// <summary>
    /// Series to store the maximum value of input series into the period value.
    /// </summary>
    public class MaxSeries : BaseNumericPeriodSeries
    {

        public ISeries<double> Input {  get; protected set; }

        public MaxSeries(IBarsService bars, PeriodSeriesInfo info) : base(bars, info)
        {
            if (bars is BarsService barsSvc)
                if (info.Inputs != null && info.Inputs.Count > 0)
                    Input = barsSvc.GetOrAddSeries(info.Inputs[0]);

            if (Input == null)
                bars.PrintService.LogError($"ERROR. The {nameof(MaxSeries)} could not be initialized.");
        }

        protected override double InitializeLastValue() => double.MinValue;
        protected override double GetCandidateValue(bool isCandidateValueToUpdate) => Input[0];
        protected override bool IsValidValueToAdd(double candidateValue, bool isFirstValueToAdd) 
            => isFirstValueToAdd || candidateValue >= CurrentValue;
        protected override bool IsValidValueToUpdate(double candidateValue)
            => candidateValue >= CurrentValue;

        internal override void Configure(out bool isConfigured)
        {
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
                        // Obtener la serie desde Bars.SeriesCollection<INumericSeries>.
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
    ///// Cache to store the lastest market high prices.
    ///// </summary>
    //public class MaxSeries : BaseNumericPeriodSeries<INumericSeries<double>>
    //{

    //    /// <summary>
    //    /// Create default instance with specified parameters.
    //    /// </summary>
    //    /// <param name="barsSeries">The bars series instance used to gets series.</param>
    //    /// <param name="period">The specified period to calculate values in cache.</param>
    //    /// <param name="barsIndex">The index of the 'NinjaScript.Series' necesary for gets the cache elements.</param>
    //    /// <exception cref="ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
    //    public MaxSeries(IBarsSeries barsSeries, int period, int barsIndex) : this(barsSeries.Low, period, barsIndex)
    //    {
    //    }

    //    /// <inheritdoc/>
    //    public MaxSeries(IBarsService barsService, int period) : base(barsService, period)
    //    {
    //    }

    //    /// <inheritdoc/>
    //    public MaxSeries(INumericSeries<double> input, int period, int barsIndex) : base(input, period, barsIndex)
    //    {
    //    }

    //    //public override string Name => "Max";

    //    protected override double GetInitValuePreviousRecalculate() 
    //        => double.MinValue;

    //    protected override bool IsValidValueToAdd(double currentValue, double candidateValue) 
    //        => candidateValue >= currentValue;

    //    protected override bool IsValidValueToUpdate(double currentValue, double candidateValue) 
    //        => candidateValue >= currentValue;

    //    protected override double GetCandidateValue(int barsAgo, bool isCandidateValueForUpdate)
    //        => Input[barsAgo];

    //}
}
