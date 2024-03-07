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
        /// <param name="input">The <see cref="IBarsMaster"/> instance used to gets <see cref="NinjaScriptBase"/> object necesary for <see cref="VolumeSeries"/>.</param>
        /// <param name="capacity">The <see cref="ICache{T}"/> capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
        /// <param name="oldValuesCapacity">The length of the removed values cache. This values are at the end of cache.</param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        public VolumeSeries(IBarsService input, int capacity = DEFAULT_CAPACITY, int oldValuesCapacity = DEFAULT_OLD_VALUES_CAPACITY) : this(input?.Ninjascript.Volumes[input?.Index ?? 0], input?.Index ?? 0, capacity, oldValuesCapacity)
        {
        }

        /// <summary>
        /// Create <see cref="VolumeSeries"/> default instance with specified properties.
        /// </summary>
        /// <param name="input">The <see cref="NinjaScriptBase"/> instance used to gets elements for <see cref="VolumeSeries"/>.</param>
        /// <param name="barsIndex">The index of NinjaScript.Bars used to gets cache elements.</param>
        /// <param name="capacity">The <see cref="ICache{T}"/> capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
        /// <param name="oldValuesCapacity">The length of the removed values cache. This values are at the end of cache.</param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        public VolumeSeries(NinjaScriptBase input, int barsIndex, int capacity = DEFAULT_CAPACITY, int oldValuesCapacity = DEFAULT_OLD_VALUES_CAPACITY) : this(input?.Volumes[barsIndex], barsIndex, capacity, oldValuesCapacity)
        {
        }

        /// <summary>
        /// Create <see cref="VolumeSeries"/> default instance with specified properties.
        /// </summary>
        /// <param name="input">The <see cref="ISeries{double}"/> instance used to gets elements for <see cref="VolumeSeries"/>.</param>
        /// <param name="barsIndex">The index of NinjaScript.Bars used to gets cache elements.</param>
        /// <param name="capacity">The <see cref="ICache{T}"/> capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
        /// <param name="oldValuesCapacity">The length of the removed values cache. This values are at the end of cache.</param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        internal VolumeSeries(ISeries<double> input, int barsIndex, int capacity , int oldValuesCapacity) : base(input, period : 1, capacity, oldValuesCapacity, barsIndex)
        {
        }

        public override string Name 
            => $"Volume({Capacity})";
        protected override double GetCandidateValue() 
            => Input[0];
        protected override double ReplaceCurrentValue() 
            => GetCandidateValue();
        protected override bool IsValidCandidateValueToReplace(double currentValue, double candidateValue) 
            => candidateValue != currentValue;
        public override ISeries<double> GetInput(object input)
        {
            if (input is NinjaTrader.NinjaScript.VolumeSeries volumeSeries)
                return volumeSeries;
            return null;
        }

    }
}
