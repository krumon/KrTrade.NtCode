using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using System;

namespace KrTrade.Nt.Services
{
    public class VolumeCache : NinjascriptSeriesCache
    {

        //protected VolumeSeries Series { get; set; }

        ///// <summary>
        ///// Create <see cref="ISeriesCache"/> instance with default capacity and zero displacement.
        ///// </summary>
        ///// <param name="input">The NinjaScript <see cref="VolumeSeries"/> used to gets elements for <see cref="ISeriesCache"/>.</param>
        ///// <exception cref="ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        //public VolumeCache(ISeries<double> input) : base(DEFAULT_PERIOD, 0) 
        //{
        //}

        ///// <summary>
        ///// Create <see cref="ISeriesCache"/> instance with infinite or default capacity and specified displacement.
        ///// </summary>
        ///// <param name="input">The NinjaScript <see cref="VolumeSeries"/> used to gets elements for <see cref="ISeriesCache"/>.</param>
        ///// <param name="period">The <see cref="ISeriesCache"/> period.</param>
        ///// <exception cref="ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        //public VolumeCache(ISeries<double> input, int period) : base(period, 0) 
        //{
        //}

        ///// <summary>
        ///// Create <see cref="ISeriesCache"/> instance with specified capacity and specified displacement.
        ///// </summary>
        ///// <param name="input">The NinjaScript <see cref="VolumeSeries"/> used to gets elements for <see cref="ISeriesCache"/>.</param>
        ///// <param name="period">The <see cref="ISeriesCache"/> period.</param>
        ///// <param name="displacement">The displacement of <see cref="ISeriesCache"/> respect NinjaScript <see cref="ISeries{double}"/> used to gets elements.</param>
        ///// <exception cref="ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        //public VolumeCache(ISeries<double> input, int period, int displacement) : base(period, displacement) 
        //{
        //    if (input == null) throw new ArgumentNullException($"The Cache nedd an input serie. The {nameof(input)} is null.");
        //    if (!(input is VolumeSeries)) throw new Exception("No Valid 'VolumeSerie'.");
        //    Input = input;
        //}

        //protected sealed override double GetCandidateValue(NinjaScriptBase ninjascript = null) => Series[0];
        //protected sealed override bool IsValidCandidateValueToUpdate(double currentValue, double candidateValue) => candidateValue > currentValue;

        /// <inheritdoc/>
        public VolumeCache(ISeries<double> input) : base(input) { }

        /// <inheritdoc/>
        public VolumeCache(ISeries<double> input, int capacity) : base(input, capacity) { }

        /// <inheritdoc/>
        public VolumeCache(ISeries<double> input, int capacity, int displacement) : base(input, capacity, displacement)
        {
        }

        /// <inheritdoc/>
        public VolumeCache(ISeries<double> input, int capacity, int displacement, int seriesIdx) : base(input, capacity, displacement, seriesIdx)
        {
        }

        protected override ISeries<double> GetSeries(ISeries<double> input, int seriesIdx)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));

            if (input is NinjaScriptBase ninjascript)
                return ninjascript.Volumes[seriesIdx];

            if (!(input is VolumeSeries))
                throw new ArgumentException(nameof(input));

            return input;
        }

        protected sealed override bool IsValidCandidateValueToUpdate(double currentValue, double candidateValue) => candidateValue > currentValue;

    }
}
