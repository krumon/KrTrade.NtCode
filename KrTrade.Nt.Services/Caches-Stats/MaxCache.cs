using NinjaTrader.NinjaScript;
using System;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Cache to store the lastest market high prices.
    /// </summary>
    public class MaxCache : DoubleCache<ISeries<double>>
    {

        private double _lastMax;
        private double _currentMax;
        private int _lastMaxBarsAgo;
        private int _currentMaxBarsAgo;

        /// <summary>
        /// Create <see cref="MaxCache"/> default instance with specified properties.
        /// </summary>
        /// <param name="input">The <see cref="IBarsService"/> instance used to gets <see cref="NinjaScriptBase"/> object necesary for <see cref="MaxCache"/>.</param>
        /// <param name="period">The period to calculate the cache values.</param>
        /// <param name="capacity">The <see cref="ICache{T}"/> capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
        /// <param name="oldValuesCapacity">The length of the old values cache. This values are at the end of cache.</param>
        /// <param name="barsIndex">The index of NinjaScript.Bars used to gets cache elements.</param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        public MaxCache(IBarsService input, int period, int capacity = DEFAULT_CAPACITY, int oldValuesCapacity = DEFAULT_OLD_VALUES_CAPACITY, int barsIndex = 0) : this(input?.Ninjascript.Highs[barsIndex], period, capacity, oldValuesCapacity)
        {
        }
        /// <summary>
        /// Create <see cref="MaxCache"/> default instance with specified properties.
        /// </summary>
        /// <param name="input">The <see cref="NinjaScriptBase"/> instance used to gets elements for <see cref="MaxCache"/>.</param>
        /// <param name="period">The period to calculate the cache values.</param>
        /// <param name="capacity">The <see cref="ICache{T}"/> capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
        /// <param name="oldValuesCapacity">The length of the old values cache. This values are at the end of cache.</param>
        /// <param name="barsIndex">The index of NinjaScript.Bars used to gets cache elements.</param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        public MaxCache(NinjaScriptBase input, int period, int capacity = DEFAULT_CAPACITY, int oldValuesCapacity = DEFAULT_OLD_VALUES_CAPACITY, int barsIndex = 0) : this(input?.Highs[barsIndex], period, capacity, oldValuesCapacity)
        {
        }
        /// <summary>
        /// Create <see cref="MaxCache"/> default instance with specified properties.
        /// </summary>
        /// <param name="input">The <see cref="ISeries{double}"/> instance used to gets elements for <see cref="MaxCache"/>.</param>
        /// <param name="period">The period to calculate the cache values.</param>
        /// <param name="capacity">The <see cref="ICache{T}"/> capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
        /// <param name="oldValuesCapacity">The length of the old values cache. This values are at the end of cache.</param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        public MaxCache(ISeries<double> input, int period, int capacity = DEFAULT_CAPACITY, int oldValuesCapacity = DEFAULT_OLD_VALUES_CAPACITY) : base(input, period, capacity, oldValuesCapacity)
        {
        }

        public override string Name => $"Max({Period})";
        protected override void OnLastElementRemoved(double removedValue)
        {
            if (Count == 0)
            {
                _lastMax = Input[0];
                _currentMax = Input[0];
                _lastMaxBarsAgo = -1;
                _currentMaxBarsAgo = -1;
            }
            if (_currentMaxBarsAgo >= Period - 2)
            {
                _currentMax = double.MinValue;
                for (int i = Math.Min(Count, Period - 1); i > 0; i--)
                {
                    if (this[i] >= _currentMax)
                    {
                        _currentMax = this[i];
                        _currentMaxBarsAgo = i;
                    }
                }
            }
            _lastMax = _currentMax;
            _lastMaxBarsAgo = _currentMaxBarsAgo;

            if (Input[0] >= _lastMax)
            {
                _currentMax = Input[0];
                _currentMaxBarsAgo = -1;
            }
            else
            {
                _currentMax = _lastMax;
                _currentMaxBarsAgo = _lastMaxBarsAgo;
            }
        }
        protected override double GetCandidateValue() 
        { 
            if (Count == 0)
            {
                _lastMax = Input[0];
                _currentMax = Input[0];
                _lastMaxBarsAgo = -1;
                _currentMaxBarsAgo = -1;
                return _currentMax;
            }
            if (_currentMaxBarsAgo >= Period - 2)
            {
                _currentMax = double.MinValue;
                for (int i = Math.Min(Count,Period - 1); i > 0; i--) 
                {
                    if (this[i] > _currentMax)
                    {
                        _currentMax = this[i];
                        _currentMaxBarsAgo = i;
                    }
                }
            }

            _lastMax = _currentMax;
            _lastMaxBarsAgo = _currentMaxBarsAgo;

            if (Input[0] >= _lastMax)
            {
                _currentMax = Input[0];
                _currentMaxBarsAgo = -1;
            }
            else
            {
                _currentMax = _lastMax;
                _currentMaxBarsAgo = _lastMaxBarsAgo;
            }
            return _currentMax;
        }
        protected override void OnElementAdded(double addedElement)
        {
            _lastMaxBarsAgo++;
            _currentMaxBarsAgo++;
        }
        protected override double UpdateCurrentValue()
        {
            if (Input[0] >= _lastMax)
            {
                _currentMax = Input[0];
                _currentMaxBarsAgo = -1;
            }
            else
            {
                _currentMax = _lastMax;
                _currentMaxBarsAgo = _lastMaxBarsAgo;
            }
            return _currentMax;
        }
        protected override bool IsValidCandidateValueToUpdate(double currentValue, double candidateValue) => candidateValue != currentValue;

        protected override ISeries<double> GetInput(ISeries<double> input)
        {
            if (input is NinjaScriptBase ninjascript)
                return ninjascript.Inputs[BarsIndex];

            return input;
        }
    }
}
