﻿using KrTrade.Nt.Core.Data;
using KrTrade.Nt.Core.Infos;
using KrTrade.Nt.Core.Series;
using KrTrade.Nt.Core.Services;

namespace KrTrade.Nt.Services.Series
{
    /// <summary>
    /// Price series. The new values of series are the last value of the input series. 
    /// The series current value is updated when the current value and the candidate value are different.
    /// </summary>
    public abstract class PriceSeries : BaseNumericSeries, IPriceSeries
    {
        public NinjaTrader.NinjaScript.ISeries<double> Input { get; protected set; }
        
        protected PriceSeries(IBarsService bars, SeriesInfo info) : base(bars, info)
        {
        }

        protected override SeriesType ToElementType() => SeriesType.CURRENT_BAR;
        protected override double GetCandidateValue(bool isCandidateValueForUpdate) => Input[0];

        protected override void Configure(out bool isConfigured) => isConfigured = true;
        protected override bool IsValidValueToAdd(double candidateValue, bool isFirstValueToAdd) => true;
        protected override bool IsValidValueToUpdate(double candidateValue) => candidateValue != CurrentValue;

    }
}
