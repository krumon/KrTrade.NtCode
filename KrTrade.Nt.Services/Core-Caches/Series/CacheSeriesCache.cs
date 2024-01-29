//using NinjaTrader.NinjaScript;
//using System;

//namespace KrTrade.Nt.Services
//{
//    public abstract class CacheSeriesCache : BaseSeriesCache
//    {
//        protected bool HasInputSeriesCache = false;
//        protected ISeriesCache Cache = null;

//        /// <summary>
//        /// Create <see cref="ISeriesCache"/> instance based in other <see cref="ISeriesCache"/> instance with default capacity and zero displacement.
//        /// </summary>
//        /// <param name="input">The cache series used to calculate the <see cref="ISeriesCache"/> elements for add.</param>
//        /// <exception cref="ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
//        public CacheSeriesCache(ISeries<double> input) : base(input, DEFAULT_PERIOD,0,0) 
//        {
//            if (input is ISeriesCache cache)
//            {
//                Period = cache.Period;
//                Displacement = cache.Displacement;
//            }
//        }

//        protected sealed override bool UpdateCurrentValue(ref double currentValue, NinjaScriptBase ninjascript = null)
//        {
//            bool isUpdated = false;
//            double candidateValue = UpdateCurrentValue();
//            if (IsValidCandidateValueToUpdate(currentValue, candidateValue))
//            {
//                currentValue = candidateValue;
//                isUpdated = true;
//            }
//            return isUpdated;
//        }
//        protected abstract double UpdateCurrentValue();
//    }
//}
