﻿using KrTrade.Nt.Core;
using KrTrade.Nt.Core.Data;
using KrTrade.Nt.Core.Infos;
using KrTrade.Nt.Core.Series;
using KrTrade.Nt.Core.Services;
using System;

namespace KrTrade.Nt.Services.Series
{
    /// <summary>
    /// Series to store the average value of input series in the specified period.
    /// </summary>
    public class AvgSeries : BaseNumericPeriodSeries
    {

        public ISeries<double> Input { get; protected set; }

        public AvgSeries(IBarsService bars, PeriodSeriesInfo info) : base(bars, info)
        {
            if (bars is BarsService barsSvc)
                if (info.Inputs != null && info.Inputs.Count > 0)
                    Input = barsSvc.GetOrAddSeries(info.Inputs[0]);

            if (Input == null)
                bars.PrintService.LogError($"ERROR. The {nameof(AvgSeries)} could not be initialized.");
        }

        protected override SeriesType ToElementType() => SeriesType.AVG;
        protected override double InitializeLastValue() 
            => 0;
        protected override double GetCandidateValue(bool isCandidateValueToUpdate) 
            => Input[0] / Math.Min(Count, Period);
        protected override bool IsValidValueToAdd(double candidateValue, bool isFirstValueToAdd)
            => isFirstValueToAdd || candidateValue != CurrentValue;
        protected override bool IsValidValueToUpdate(double candidateValue)
            => candidateValue != CurrentValue;

        protected override void Configure(out bool isConfigured)
        {
            isConfigured = Input != null;
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
    ///// Series to store the lastest average prices for specified period.
    ///// </summary>
    //public class AvgSeries : BaseNumericPeriodSeries<SumSeries>
    //{

    //    /// <inheritdoc/>
    //    public AvgSeries(IBarsService barsService, int period) : base(barsService, period)
    //    {
    //    }

    //    /// <inheritdoc/>
    //    public AvgSeries(SumSeries input, int period, int barsIndex) : base(input, period, barsIndex)
    //    {
    //    }

    //    //public override string Name => "Avg";

    //    protected override double GetCandidateValue(int barsAgo, bool isCandidateValueForUpdate) 
    //        => Input[0] / Math.Min(Count, Period);

    //    protected override double GetInitValuePreviousRecalculate()
    //        => 0;

    //    protected override bool IsValidValueToAdd(double currentValue, double candidateValue) 
    //        => candidateValue != currentValue;

    //    protected override bool IsValidValueToUpdate(double currentValue, double candidateValue) 
    //        => candidateValue != currentValue;

    //}
}
