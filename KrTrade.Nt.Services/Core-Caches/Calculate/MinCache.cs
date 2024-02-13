using NinjaTrader.NinjaScript;
using System;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Cache to store the lastest market high prices.
    /// </summary>
    public class MinCache : CalculateCache
    {

        private double _lastMin;
        private double _currentMin;
        private int _lastMinBar;
        private int _currentMinBar;

        /// <summary>
        /// Create <see cref="MinCache"/> default instance with specified properties.
        /// </summary>
        /// <param name="input">The <see cref="IBarsService"/> instance used to gets <see cref="NinjaScriptBase"/> object necesary for <see cref="MinCache"/>.</param>
        /// <param name="period">The period to calculate the cache values.</param>
        /// <param name="capacity">The <see cref="ICache{T}"/> capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
        /// <param name="lengthOfRemovedCache">The length of the removed values cache. This values are at the end of cache.</param>
        /// <param name="barsIndex">The index of NinjaScript.Bars used to gets cache elements.</param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        public MinCache(IBarsService input, int period, int capacity = DEFAULT_CAPACITY, int lengthOfRemovedCache = DEFAULT_LENGTH_REMOVED_CACHE, int barsIndex = 0) : this(input?.Ninjascript.Lows[barsIndex], period, capacity, lengthOfRemovedCache)
        {
        }
        /// <summary>
        /// Create <see cref="MinCache"/> default instance with specified properties.
        /// </summary>
        /// <param name="input">The <see cref="NinjaScriptBase"/> instance used to gets elements for <see cref="MinCache"/>.</param>
        /// <param name="period">The period to calculate the cache values.</param>
        /// <param name="capacity">The <see cref="ICache{T}"/> capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
        /// <param name="lengthOfRemovedCache">The length of the removed values cache. This values are at the end of cache.</param>
        /// <param name="barsIndex">The index of NinjaScript.Bars used to gets cache elements.</param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        public MinCache(NinjaScriptBase input, int period, int capacity = DEFAULT_CAPACITY, int lengthOfRemovedCache = DEFAULT_LENGTH_REMOVED_CACHE, int barsIndex = 0) : this(input?.Lows[barsIndex], period, capacity, lengthOfRemovedCache)
        {
        }
        /// <summary>
        /// Create <see cref="MinCache"/> default instance with specified properties.
        /// </summary>
        /// <param name="input">The <see cref="ISeries{double}"/> instance used to gets elements for <see cref="MinCache"/>.</param>
        /// <param name="period">The period to calculate the cache values.</param>
        /// <param name="capacity">The <see cref="ICache{T}"/> capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
        /// <param name="lengthOfRemovedCache">The length of the removed values cache. This values are at the end of cache.</param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        public MinCache(ISeries<double> input, int period, int capacity = DEFAULT_CAPACITY, int lengthOfRemovedCache = DEFAULT_LENGTH_REMOVED_CACHE) : base(input, period, capacity, lengthOfRemovedCache)
        {
        }

        public override string Name => $"Min({Period})";
        protected override void OnLastElementRemoved()
        {
            if (Count == 0)
            {
                _lastMin = Input[0];
                _currentMin = Input[0];
                _lastMinBar = -1;
                _currentMinBar = -1;
            }
            if (_currentMinBar >= Period - 2)
            {
                _currentMin = double.MaxValue;
                for (int i = Math.Min(Count, Period - 1); i > 0; i--)
                {
                    if (this[i] <= _currentMin)
                    {
                        _currentMin = this[i];
                        _currentMinBar = i;
                    }
                }
            }
            _lastMin = _currentMin;
            _lastMinBar = _currentMinBar;

            if (Input[0] <= _lastMin)
            {
                _currentMin = Input[0];
                _currentMinBar = -1;
            }
            else
            {
                _currentMin = _lastMin;
                _currentMinBar = _lastMinBar;
            }
        }
        protected override double GetCandidateValue() 
        { 
            if (Count == 0)
            {
                _lastMin = Input[0];
                _currentMin = Input[0];
                _lastMinBar = -1;
                _currentMinBar = -1;
                return _currentMin;
            }
            if (_currentMinBar >= Period - 2)
            {
                _currentMin = double.MaxValue;
                for (int i = Math.Min(Count,Period - 1); i > 0; i--) 
                {
                    if (this[i] <= _currentMin)
                    {
                        _currentMin = this[i];
                        _currentMinBar = i;
                    }
                }
            }

            _lastMin = _currentMin;
            _lastMinBar = _currentMinBar;

            if (Input[0] <= _lastMin)
            {
                _currentMin = Input[0];
                _currentMinBar = -1;
            }
            else
            {
                _currentMin = _lastMin;
                _currentMinBar = _lastMinBar;
            }
            return _currentMin;
        }
        protected override void OnElementAdded(double addedElement)
        {
            _lastMinBar++;
            _currentMinBar++;
        }
        protected override double UpdateCurrentValue()
        {
            if (Input[0] <= _lastMin)
            {
                _currentMin = Input[0];
                _currentMinBar = -1;
            }
            else
            {
                _currentMin = _lastMin;
                _currentMinBar = _lastMinBar;
            }
            return _currentMin;
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
