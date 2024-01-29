//using NinjaTrader.NinjaScript;
//using System;

//namespace KrTrade.Nt.Services
//{
//    public abstract class NinjascriptSeriesCache : BaseSeriesCache
//    {
        
//        /// <summary>
//        /// Create <see cref="ISeriesCache"/> instance with default capacity and zero displacement.
//        /// </summary>
//        /// <param name="input">The NinjaScript <see cref="PriceSeries"/> used to gets elements for <see cref="ISeriesCache"/>.</param>
//        /// <exception cref="ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
//        public NinjascriptSeriesCache(ISeries<double> input) : this(input, DEFAULT_PERIOD, 0) 
//        {
//        }

//        /// <summary>
//        /// Create <see cref="ISeriesCache"/> instance with infinite or default capacity and specified displacement.
//        /// </summary>
//        /// <param name="input">The NinjaScript <see cref="PriceSeries"/> used to gets elements for <see cref="ISeriesCache"/>.</param>
//        /// <param name="period">The <see cref="ISeriesCache"/> period.</param>
//        /// <exception cref="ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
//        public NinjascriptSeriesCache(ISeries<double> input, int period) : this(input, period, 0) 
//        {
//        }

//        /// <summary>
//        /// Create <see cref="ISeriesCache"/> instance with specified capacity and specified displacement.
//        /// </summary>
//        /// <param name="input">The NinjaScript <see cref="PriceSeries"/> used to gets elements for <see cref="ISeriesCache"/>.</param>
//        /// <param name="period">The <see cref="ISeriesCache"/> period.</param>
//        /// <param name="displacement">The displacement of <see cref="ISeriesCache"/> respect NinjaScript <see cref="ISeries{double}"/> used to gets elements.</param>
//        /// <exception cref="ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
//        public NinjascriptSeriesCache(ISeries<double> input, int period, int displacement) : this(input, period, displacement,0) 
//        {
//        }

//        /// <summary>
//        /// Create <see cref="ISeriesCache"/> instance with specified capacity and specified displacement.
//        /// </summary>
//        /// <param name="input">The NinjaScript <see cref="PriceSeries"/> used to gets elements for <see cref="ISeriesCache"/>.</param>
//        /// <param name="period">The <see cref="ISeriesCache"/> period.</param>
//        /// <param name="displacement">The displacement of <see cref="ISeriesCache"/> respect NinjaScript <see cref="ISeries{double}"/> used to gets elements.</param>
//        /// <param name="seriesIdx">The index of NinjaScript <see cref="ISeries{double}"/> used to gets elements.</param>
//        /// <exception cref="ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
//        public NinjascriptSeriesCache(ISeries<double> input, int period, int displacement, int seriesIdx) : base(input, period, displacement,seriesIdx) 
//        {
//            if (!(input is PriceSeries) && !(input is VolumeSeries)) throw new Exception("No Valid 'input' serie.");
//        }

//        //protected abstract ISeries<double> GetNinjaScriptSerie(ISeries<double> series);
//        //protected sealed override void UpdateCurrentValue(ref double currentValue, NinjaScriptBase ninjascript = null) => base.UpdateCurrentValue(ref currentValue, ninjascript);
//        protected sealed override double GetCandidateValue(NinjaScriptBase ninjascript = null) => Series[0];
//        protected sealed override bool UpdateCurrentValue(ref double currentValue, NinjaScriptBase ninjascript = null) => base.UpdateCurrentValue(ref currentValue, ninjascript);

//    }
//}
