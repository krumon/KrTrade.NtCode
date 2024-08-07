﻿using KrTrade.Nt.Core.Data;
using KrTrade.Nt.Core.Infos;
using KrTrade.Nt.Core.Series;
using KrTrade.Nt.Core.Services;

namespace KrTrade.Nt.Services.Series
{

    /// <summary>
    /// Series to store the minimum value of input series into the period value.
    /// </summary>
    public class MinSeries : BaseNumericPeriodSeries
    {

        public ISeries<double> Input { get; protected set; }

        public MinSeries(IBarsService bars, PeriodSeriesInfo info) : base(bars, info)
        {
            if (bars is BarsService barsSvc)
                if (info.Inputs != null && info.Inputs.Count > 0)
                    Input = barsSvc.GetOrAddSeries(info.Inputs[0]);

            if (Input == null)
                bars.PrintService.LogError($"ERROR. The {nameof(MinSeries)} could not be initialized.");
        }

        protected override SeriesType ToElementType() => SeriesType.MIN;
        protected override double InitializeLastValue() => double.MaxValue;
        protected override double GetCandidateValue(bool isCandidateValueToUpdate) => Input[0];
        protected override bool IsValidValueToAdd(double candidateValue, bool isFirstValueToAdd)
            => isFirstValueToAdd || candidateValue <= CurrentValue;
        protected override bool IsValidValueToUpdate(double candidateValue)
            => candidateValue <= CurrentValue;

        protected override void Configure(out bool isConfigured)
        {
            isConfigured = true;
        }
        protected override void DataLoaded(out bool isDataLoaded)
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
    //public class MinSeries : BaseNumericPeriodSeries<INumericSeries<double>>
    //{

    //    /// <summary>
    //    /// Create default instance with specified parameters.
    //    /// </summary>
    //    /// <param name="barsSeries">The bars series instance used to gets series.</param>
    //    /// <param name="period">The specified period to calculate values in cache.</param>
    //    /// <param name="barsIndex">The index of the 'NinjaScript.Series' necesary for gets the cache elements.</param>
    //    /// <exception cref="ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
    //    public MinSeries(IBarsSeries barsSeries, int period, int barsIndex) : this(barsSeries.Low, period, barsIndex)
    //    {
    //    }

    //    /// <inheritdoc/>
    //    public MinSeries(IBarsService barsService, int period) : base(barsService, period)
    //    {
    //    }

    //    /// <inheritdoc/>
    //    public MinSeries(INumericSeries<double> input, int period, int barsIndex) : base(input, period, barsIndex)
    //    {
    //    }

    //    //public override string Name => "Min";

    //    protected override double GetInitValuePreviousRecalculate()
    //        => double.MaxValue;

    //    protected override bool IsValidValueToAdd(double currentValue, double candidateValue)
    //        => candidateValue <= currentValue;

    //    protected override bool IsValidValueToUpdate(double currentValue, double candidateValue)
    //        => candidateValue <= currentValue;

    //    protected override double GetCandidateValue(int barsAgo, bool isCandidateValueForUpdate)
    //        => Input[barsAgo];
    //}
}
