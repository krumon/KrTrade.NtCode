using NinjaTrader.NinjaScript;
using System;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Cache to store the lastest market high prices.
    /// </summary>
    public class MinCache : DoubleCache<ISeries<double>>
    {

        private readonly int _barsIndex = 0;
        private double _lastMin;
        private double _currentMin;
        private int _lastMinBar;
        private int _currentMinBar;

        /// <summary>
        /// Create <see cref="MinCache"/> default instance with specified properties.
        /// </summary>
        /// <param name="input">The <see cref="ISeries{double}"/> instance used to gets elements for <see cref="MinCache"/>.</param>
        /// <param name="period">The <see cref="ICache{T}"/> period without include displacement. <see cref="Cache.Capacity"/> property include displacement.</param>
        /// <param name="displacement">The displacement of <see cref="ICache{T}"/> respect <see cref="Input"/> object used to gets elements.</param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        public MinCache(ISeries<double> input, int period, int displacement = 0) : base(input, period, displacement)
        {
        }

        /// <summary>
        /// Create <see cref="HighCache"/> default instance with specified properties.
        /// </summary>
        /// <param name="input">The <see cref="NinjaScriptBase"/> instance used to gets elements for <see cref="HighCache"/>.</param>
        /// <param name="period">The <see cref="ICache{T}"/> period without include displacement. <see cref="Cache.Capacity"/> property include displacement.</param>
        /// <param name="displacement">The displacement of <see cref="ICache{T}"/> respect <see cref="Input"/> object used to gets elements.</param>
        /// <param name="barsIndex">The index of NinjaScript.Bars used to gets cache elements.</param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        public MinCache(NinjaScriptBase input, int period, int displacement = 0, int barsIndex = 0) : base(input?.Inputs[barsIndex], period, displacement)
        {
            _barsIndex = barsIndex;
        }

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
                return ninjascript.Inputs[_barsIndex];

            return input;
        }
    }
}
