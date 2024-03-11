using NinjaTrader.NinjaScript;
using System;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Double values cache. The new values of cache are the last value of the input series. 
    /// The cache current value is updated when the current value and the candidate value are different.
    /// </summary>
    public abstract class IndicatorSeries : DoubleSeries<INumericSeries<double>>, IIndicatorSeries
    {
        private readonly string _name;
        //private double _lastValue;
        //private double _currentValue;
        protected int _lastValueBar;
        protected int _currentValueBar;

        //protected TElement _lastValue;
        //protected TElement _currentValue;
        //protected int _lastValueBar;
        //protected int _currentValueBar;

        /// <summary>
        /// Create <see cref="IndicatorSeries"/> default instance with specified properties.
        /// </summary>
        /// <param name="input">The object instance used to gets elements for <see cref="IndicatorSeries"/>.</param>
        /// <param name="name">the name of indicator.</param>
        /// <param name="period">The period to calculate the cache values.</param>
        /// <param name="capacity">The series capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
        /// <param name="oldValuesCapacity">The length of the removed values cache. This values are at the end of cache.</param>
        /// <param name="barsIndex">The index of NinjaScript.Bars used to gets cache elements.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        internal IndicatorSeries(object input, string name, int period, int capacity, int oldValuesCapacity, int barsIndex) : base(input, period, capacity, oldValuesCapacity, barsIndex)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
                _name = "UnknownIndicator";
            else _name = name;
        }

        public override bool Add()
        {
            try
            {
                _candidateValue = GetCandidateValue(0, isCandidateValueForUpdate: false);

                if (Count == 0)
                {
                    _currentValue = _candidateValue;
                    _lastValue = _candidateValue;
                    _currentValueBar = -1;
                    _lastValueBar = -1;
                    Add(_currentValue);
                    return true;
                }

                if (_currentValueBar > Period - 2)
                {
                    if (Period != 1)
                    {
                        double lastValue = GetInitValuePreviousRecalculate();

                        for (int i = Math.Min(Count, Period - 1); i > 0; i--)
                        {
                            double candidateValue = GetCandidateValue(i, isCandidateValueForUpdate: false);
                            if (CheckAddConditions(lastValue, candidateValue))
                            {
                                _currentValue = candidateValue;
                                _currentValueBar = i - 1;
                            }
                        }
                    }
                }

                _lastValue = _currentValue;
                _lastValueBar = _currentValueBar;

                if (CheckAddConditions(_lastValue, _candidateValue))
                {
                    _currentValue = _candidateValue;
                    _currentValueBar = -1;
                }

                Add(_currentValue);
                return true;

            }
            catch
            {
                Add(default);
                return false;
            }
        }
        public override bool Update()
        {
            try
            {
                _candidateValue = GetCandidateValue(0, isCandidateValueForUpdate: true);

                if (CheckUpdateConditions(_currentValue, _candidateValue))
                {
                    double updateValue = _candidateValue;
                    _lastValue = _currentValue;
                    _lastValueBar = _currentValueBar;
                    _currentValue = updateValue;
                    _currentValueBar = -1;
                    this[0] = _currentValue;
                }

                return true;

            }
            catch { return false; }
        }

        protected override void OnElementAdded(double addedElement)
        {
            _lastValueBar++;
            _currentValueBar++;
        }
        protected override void OnLastElementRemoved(double removedElement)
        {
            _currentValueBar = Period;
            Add();
        }

        public override string Name
            => $"{_name}({Input.Name}({Period}))";


        public override INumericSeries<double> GetInput(object input)
        {
            if (input is NinjaScriptBase ninjascript)
                return (INumericSeries<double>)ninjascript.Inputs[BarsIndex];
            if (input is BarsService barsService)
                return barsService.Close;
            if (input is BarsManager barsManager)
                return barsManager.Closes[BarsIndex];

            return null;
        }

        protected abstract double GetInitValuePreviousRecalculate();
        //protected abstract double CalculateCurrentValue(double lastValue, double candidateValue);
        //protected abstract double CalculateUpdateValue(double lastValue, double currentValue, double candidateValue);

    }
}
