using NinjaTrader.NinjaScript;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Cache to store the lastest market volumes.
    /// </summary>
    public class VolumeSeries : DoubleSeries<ISeries<double>>, IVolumeSeries
    {

        /// <summary>
        /// Create <see cref="VolumeSeries"/> default instance with specified properties.
        /// </summary>
        /// <param name="input">The <see cref="IBarsService"/> instance used to gets <see cref="NinjaScriptBase"/> object necesary for <see cref="VolumeSeries"/>.</param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        public VolumeSeries(IBarsService input) : this(input?.Ninjascript?.Volumes[input?.Index ?? 0], input.CacheCapacity, input.RemovedCacheCapacity, input?.Index ?? 0)
        {
        }

        /// <summary>
        /// Create <see cref="VolumeSeries"/> default instance with specified properties.
        /// </summary>
        /// <param name="input">The <see cref="NinjaScriptBase"/> instance used to gets elements for <see cref="VolumeSeries"/>.</param>
        /// <param name="capacity">The <see cref="ICache{T}"/> capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
        /// <param name="oldValuesCapacity">The length of the removed values cache. This values are at the end of cache.</param>
        /// <param name="barsIndex">The index of NinjaScript.Bars used to gets cache elements.</param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        public VolumeSeries(NinjaScriptBase input, int capacity, int oldValuesCapacity, int barsIndex) : this(input?.Volumes[barsIndex], capacity, oldValuesCapacity, barsIndex)
        {
        }

        /// <summary>
        /// Create <see cref="VolumeSeries"/> default instance with specified properties.
        /// </summary>
        /// <param name="input">The <see cref="int"/> array used to gets elements for <see cref="VolumeSeries"/>.</param>
        /// <param name="capacity">The <see cref="ICache{T}"/> capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
        /// <param name="oldValuesCapacity">The length of the removed values cache. This values are at the end of cache.</param>
        /// <param name="barsIndex">The index of NinjaScript.Bars used to gets cache elements.</param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        internal VolumeSeries(NinjaTrader.NinjaScript.VolumeSeries input, int capacity, int oldValuesCapacity, int barsIndex) : base(input, period: 1, capacity, oldValuesCapacity, barsIndex)
        {
        }

        public override string Name
            => $"Volume({Capacity})";

        public override ISeries<double> GetInput(object input)
        {
            if (input is NinjaTrader.NinjaScript.VolumeSeries volumeSeries)
                return volumeSeries;
            return null;
        }

        protected override double GetCandidateValue(int barsAgo, bool isCandidateValueForUpdate)
            => Input[barsAgo];

        protected override bool CheckAddConditions(double lastValue, double candidateValue)
            => true;

        protected override bool CheckUpdateConditions(double currentValue, double candidateValue)
            => candidateValue != currentValue;

    }
}
