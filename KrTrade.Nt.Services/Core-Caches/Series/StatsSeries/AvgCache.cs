using NinjaTrader.NinjaScript;
using System;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Cache to store the lastest market range price.
    /// </summary>
    public class AvgCache : CacheSeriesCache
    {
        /// <inheritdoc/>
        public AvgCache(ISeries<double> input) : base(input) 
        {
            if (input is SumCache)
                HasInputSeriesCache = true;
                
            Cache = HasInputSeriesCache ? null : new SumCache(input);
        }

        protected override ISeries<double> GetSeries(ISeries<double> input, int seriesIdx)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));

            if (input is NinjaScriptBase ninjascript)
                return ninjascript.Inputs[SeriesIdx];

            return input;
        }
        protected sealed override double GetCandidateValue(NinjaScriptBase ninjascript = null)
        {
            if (Cache != null)
            {
                // TODO: Incluir Condicion If para añadir elementos de la cache.
                Cache.Add();
                return Cache[0] / Period;
            }

            return Input[0] / Period;
        }
        protected override double UpdateCurrentValue()
        {
            if (!HasInputSeriesCache)
            {
                // TODO: Incluir Condicion If para actualizar elementos de la cache.
                Cache.Update();
                return Cache[0] / Period;
            }

            return Input[0] / Period;
        }
        protected sealed override bool IsValidCandidateValueToUpdate(double currentValue, double candidateValue) => true;

    }
}
