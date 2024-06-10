using KrTrade.Nt.Core;

namespace KrTrade.Nt.Services.Series
{

    /// <summary>
    /// Series to store the sum value of input series in the specified period.
    /// </summary>
    public class SumSeries : BaseNumericPeriodSeries
    {

        public ISeries<double> Input { get; protected set; }

        internal SumSeries(IBarsService bars, PeriodSeriesInfo info) : base(bars, info)
        {
            if (bars is BarsService barsSvc)
                if (info.Inputs != null && info.Inputs.Count > 0)
                    Input = barsSvc.GetOrAddSeries(info.Inputs[0]);

            if (Input == null)
                bars.PrintService.LogError($"ERROR. The {nameof(SumSeries)} could not be initialized.");
        }

        protected override double InitializeLastValue()
            => 0;
        protected override double GetCandidateValue(bool isCandidateValueToUpdate)
        {
            if (!isCandidateValueToUpdate)
            {
                if (Count == 0)
                    return Input[0];
                if (Input.Count > Period)
                    return this[0] + Input[0] - Input[Period];
                else
                    return this[0] + Input[0];
            }
            return Input.Count > Period ? this[1] + Input[0] - Input[Period] : this[1] + Input[0];
        }
        protected override bool IsValidValueToAdd(double candidateValue, bool isFirstValueToAdd)
            => isFirstValueToAdd || candidateValue != CurrentValue;
        protected override bool IsValidValueToUpdate(double candidateValue)
            => candidateValue != CurrentValue;

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
    ///// Cache to store the lastest summary prices for specified period.
    ///// </summary>
    //public class SumSeries : BaseNumericPeriodSeries<INumericSeries<double>>
    //{

    //    /// <summary>
    //    /// Create default instance with specified parameters.
    //    /// </summary>
    //    /// <param name="barsSeries">The bars series instance used to gets series.</param>
    //    /// <param name="period">The specified period to calculate values in cache.</param>
    //    /// <param name="barsIndex">The index of the 'NinjaScript.Series' necesary for gets the cache elements.</param>
    //    /// <exception cref="ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
    //    public SumSeries(IBarsSeries barsSeries, int period, int barsIndex) : this(barsSeries.Low, period, barsIndex)
    //    {
    //    }

    //    /// <inheritdoc/>
    //    public SumSeries(IBarsService barsService, int period) : base(barsService, period)
    //    {
    //    }

    //    /// <inheritdoc/>
    //    public SumSeries(INumericSeries<double> input, int period, int barsIndex) : base(input, period, barsIndex)
    //    {
    //    }

    //    //public override string Name => $"Sum";
    //    protected override double GetCandidateValue(int barsAgo, bool isCandidateValueForUpdate)
    //    {
    //        if (!isCandidateValueForUpdate)
    //        {
    //            if (Count == 0 && barsAgo < Input.Count)
    //                return Input[barsAgo];
    //            if (Input.Count > Period)
    //                return this[barsAgo] + Input[barsAgo] - Input[Period];
    //            else
    //                return this[barsAgo] + Input[barsAgo];
    //        }
    //        return Input.Count > Period && barsAgo + 1 < Input.Count ? this[1] + Input[0] - Input[Period] : this[1] + Input[0];
    //    }

    //    protected override double GetInitValuePreviousRecalculate()
    //        => 0;

    //    protected override bool IsValidValueToAdd(double currentValue, double candidateValue)
    //        => candidateValue != currentValue;

    //    protected override bool IsValidValueToUpdate(double currentValue, double candidateValue)
    //        => candidateValue != currentValue;

    //}
}
