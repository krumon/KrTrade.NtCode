using NinjaTrader.NinjaScript;
using System;

namespace KrTrade.Nt.Services
{
    public abstract class SeriesCalculateCache : BaseSeriesCache
    {

        private readonly Func<NinjaScriptBase, int, int, double> _calculateActions;

        /// <summary>
        /// Create <see cref="SeriesCalculateCache"/> instance with infinite or default capacity and zero displacement.
        /// </summary>
        /// <param name="calculateActions">The actions to calculate the cache next value.</param>
        /// <param name="infiniteCapacity">Indicates infinite <see cref="BaseSeriesCache"/> capacity.</param>
        /// <param name="displacement">The displacement of <see cref="SeriesCache"/> respect NinjaScript <see cref="ISeries{double}"/> used to gets elements.</param>
        /// <param name="seriesIdx">The index of the 'NinjaScript.Series' used to calculate the cache values.</param>
        protected SeriesCalculateCache(Func<NinjaScriptBase, int, int, double> calculateActions, bool infiniteCapacity, int displacement, int seriesIdx) : this(calculateActions, DEFAULT_CAPACITY, displacement, infiniteCapacity, seriesIdx)
        {
        }

        /// <summary>
        /// Create <see cref="SeriesCalculateCache"/> instance with specified capacity, specified displacement and specified series index.
        /// </summary>
        /// <param name="calculateActions">The actions to calculate the cache next value.</param>
        /// <param name="capacity">The <see cref="SeriesCache"/> capacity.</param>
        /// <param name="displacement">The displacement of <see cref="SeriesCache"/> respect NinjaScript <see cref="ISeries{double}"/> used to gets elements.</param>
        /// <param name="seriesIdx">The index of the 'NinjaScript.Series' used to calculate the cache values.</param>
        public SeriesCalculateCache(Func<NinjaScriptBase, int, int, double> calculateActions, int capacity, int displacement, int seriesIdx) : this(calculateActions, capacity, displacement, false, seriesIdx)
        {
        }

        protected SeriesCalculateCache(Func<NinjaScriptBase, int, int, double> calculateActions, int capacity, int displacement, bool infiniteCapacity, int seriesIdx) : base(capacity, displacement, infiniteCapacity, seriesIdx)
        {
            _calculateActions = calculateActions ?? CalculateCandidateValue;
        }


        #region Implementation

        protected sealed override double GetCandidateValue(NinjaScriptBase ninjascript) => _calculateActions.Invoke(ninjascript,Displacement, SeriesIdx);
        protected virtual double CalculateCandidateValue(NinjaScriptBase ninjascript, int displacement, int seriesIdx)
        {
            throw new Exception("Si no pasas un delegado en el constructor para el cálculo del 'candidateValue' debes sobrescribir este método para asignar valores a la 'cache'.");
        }

        #endregion

    }
}
