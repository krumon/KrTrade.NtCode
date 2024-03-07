using KrTrade.Nt.Core.Caches;
using NinjaTrader.NinjaScript;
using System;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Double values cache. The new values of cache are the last value of the input series. 
    /// The cache current value is updated when the current value and the candidate value are different.
    /// </summary>
    public abstract class IndicatorSeries : DoubleSeries<ISeries<double>>, IIndicatorSeries
    {
        private readonly string _name;
        private double _lastValue;
        private double _currentValue;
        private int _lastValueBarsAgo;
        private int _currentValueBarsAgo;

        /// <summary>
        /// Create <see cref="PriceSeries"/> default instance with specified properties.
        /// </summary>
        /// <param name="input">The object instance used to gets elements for <see cref="IndicatorSeries"/>.</param>
        /// <param name="name">the name of indicator.</param>
        /// <param name="barsIndex">The index of NinjaScript.Bars used to gets cache elements.</param>
        /// <param name="capacity">The series capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
        /// <param name="oldValuesCapacity">The length of the removed values cache. This values are at the end of cache.</param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        internal IndicatorSeries(object input, string name, int barsIndex, int capacity, int oldValuesCapacity) : base(input, period: 1, capacity, oldValuesCapacity, barsIndex)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
                _name = "UnknownIndicator";
            else _name = name;
        }


        public override string Name
            => $"{_name}({Capacity})";
        protected override void OnLastElementRemoved(double removedValue)
        {
            if (Count == 0)
            {
                _lastValue = Input[0];
                _currentValue = Input[0];
                _lastValueBarsAgo = -1;
                _currentValueBarsAgo = -1;
            }
            if (_currentValueBarsAgo >= Period - 2)
            {
                _currentValue = GetInitValuePreviousRecalculate();
                for (int i = Math.Min(Count, Period - 1); i > 0; i--)
                {
                    if (IsValidCandidateValueToReplace(_currentValue, this[i]))
                    {
                        _currentValue = this[i];
                        _currentValueBarsAgo = i;
                    }
                }
            }
            _lastValue = _currentValue;
            _lastValueBarsAgo = _currentValueBarsAgo;

            if (IsValidCandidateValueToReplace(_lastValue, Input[0]))
            {
                _currentValue = Input[0];
                _currentValueBarsAgo = -1;
            }
            else
            {
                _currentValue = _lastValue;
                _currentValueBarsAgo = _lastValueBarsAgo;
            }
        }
        protected override double GetCandidateValue()
        {
            if (Count == 0)
            {
                _lastValue = Input[0];
                _currentValue = Input[0];
                _lastValueBarsAgo = -1;
                _currentValueBarsAgo = -1;
                return _currentValue;
            }
            if (_currentValueBarsAgo >= Period - 2)
            {
                _currentValue = GetInitValuePreviousRecalculate();
                for (int i = Math.Min(Count, Period - 1); i > 0; i--)
                {
                    if (IsValidCandidateValueToReplace(_currentValue, this[i]))
                    {
                        _currentValue = this[i];
                        _currentValueBarsAgo = i;
                    }
                }
            }

            _lastValue = _currentValue;
            _lastValueBarsAgo = _currentValueBarsAgo;

            if (IsValidCandidateValueToReplace(_lastValue, Input[0]))
            {
                _currentValue = Input[0];
                _currentValueBarsAgo = -1;
            }
            else
            {
                _currentValue = _lastValue;
                _currentValueBarsAgo = _lastValueBarsAgo;
            }
            return _currentValue;
        }
        protected override void OnElementAdded(double addedElement)
        {
            _lastValueBarsAgo++;
            _currentValueBarsAgo++;
        }
        protected override double ReplaceCurrentValue()
        {
            if (IsValidCandidateValueToReplace(_lastValue, Input[0]))
            {
                _currentValue = Input[0];
                _currentValueBarsAgo = -1;
            }
            else
            {
                _currentValue = _lastValue;
                _currentValueBarsAgo = _lastValueBarsAgo;
            }
            return _currentValue;
        }
        protected override bool IsValidCandidateValueToReplace(double currentValue, double candidateValue) 
            => candidateValue != currentValue;

        public override ISeries<double> GetInput(object input)
        {
            if (input is NinjaScriptBase ninjascript)
                return ninjascript.Inputs[_barsIndex];
            if (input is BarsService barsService)
                return barsService.Close;
            if (input is BarsMaster barsMaster)
                return barsMaster.Closes[_barsIndex];

            return null;
        }

        public abstract double GetInitValuePreviousRecalculate();
        public abstract bool CheckUpdateCondition(double currentValue, double candidateValue);
    }
}
