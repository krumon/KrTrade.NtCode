using NinjaTrader.NinjaScript;
using System;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Cache to store the lastest market high prices.
    /// </summary>
    public class MinSeries : DoubleSeries<ISeries<double>>
    {

        private double _lastMin;
        private double _currentMin;
        private int _lastMinBar;
        private int _currentMinBar;

        ///// <summary>
        ///// Create <see cref="MinSeries"/> default instance with specified properties.
        ///// </summary>
        ///// <param name="input">The <see cref="IBarsMaster"/> instance used to gets <see cref="NinjaScriptBase"/> object necesary for <see cref="MinSeries"/>.</param>
        ///// <param name="period">The period to calculate the cache values.</param>
        ///// <param name="capacity">The <see cref="ICache{T}"/> capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
        ///// <param name="oldValuesCapacity">The length of the old values cache. This values are at the end of cache.</param>
        ///// <param name="barsIndex">The index of NinjaScript.Bars used to gets cache elements.</param>
        ///// <exception cref="System.ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        //public MinSeries(IBarsMaster input, int period, int capacity = DEFAULT_CAPACITY, int oldValuesCapacity = DEFAULT_OLD_VALUES_CAPACITY, int barsIndex = 0) : this(input?.Ninjascript.Lows[barsIndex], period, capacity, oldValuesCapacity)
        //{
        //}
        ///// <summary>
        ///// Create <see cref="MinSeries"/> default instance with specified properties.
        ///// </summary>
        ///// <param name="input">The <see cref="NinjaScriptBase"/> instance used to gets elements for <see cref="MinSeries"/>.</param>
        ///// <param name="period">The period to calculate the cache values.</param>
        ///// <param name="capacity">The <see cref="ICache{T}"/> capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
        ///// <param name="oldValuesCapacity">The length of the old values cache. This values are at the end of cache.</param>
        ///// <param name="barsIndex">The index of NinjaScript.Bars used to gets cache elements.</param>
        ///// <exception cref="System.ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        //public MinSeries(NinjaScriptBase input, int period, int capacity = DEFAULT_CAPACITY, int oldValuesCapacity = DEFAULT_OLD_VALUES_CAPACITY, int barsIndex = 0) : this(input?.Lows[barsIndex], period, capacity, oldValuesCapacity)
        //{
        //}

        /// <summary>
        /// Create <see cref="MinSeries"/> default instance with specified properties.
        /// </summary>
        /// <param name="input">The <see cref="ISeries{double}"/> instance used to gets elements for <see cref="MinSeries"/>.</param>
        /// <param name="period">The period to calculate the cache values.</param>
        /// <param name="capacity">The <see cref="ICache{T}"/> capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
        /// <param name="oldValuesCapacity">The length of the old values cache. This values are at the end of cache.</param>
        /// <param name="barsIndex">The index of NinjaScript.Bars used to gets cache elements.</param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        public MinSeries(ISeries<double> input, int period, int capacity = DEFAULT_CAPACITY, int oldValuesCapacity = DEFAULT_OLD_VALUES_CAPACITY, int barsIndex = 0) : base(input, period, capacity, oldValuesCapacity, barsIndex)
        {
        }

        public override string Name => $"Min({Period})";
        protected override void OnLastElementRemoved(double removedValue)
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
        protected override double ReplaceCurrentValue()
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
        protected override bool IsValidCandidateValueToReplace(double currentValue, double candidateValue) => candidateValue != currentValue;

        public override ISeries<double> GetInput(object input)
        {
            if (input is NinjaScriptBase ninjascript)
                return ninjascript.Highs[_barsIndex];
            if (input is BarsService barsService)
                return barsService.High;
            if (input is BarsMaster barsMaster)
                return barsMaster.Highs[_barsIndex];

            return null;
        }
    }
}
