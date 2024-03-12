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
        protected int _lastValueBar;
        protected int _currentValueBar;

        /// <summary>
        /// Create <see cref="IndicatorSeries"/> default instance with specified properties.
        /// </summary>
        /// <param name="input">The object instance used to gets elements for <see cref="IndicatorSeries"/>.</param>
        /// <param name="name">The short name of indicator series.</param>
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
                _candidateValue = GetCandidateValue(0, isCandidateValueToUpdate: false);

                if (Count == 0)
                    return OnInit(_candidateValue);

                if (_currentValueBar > Period - 2)
                {
                    if (Period != 1)
                    {
                        double lastValue = GetInitValuePreviousRecalculate();

                        for (int i = Math.Min(Count, Period - 1); i > 0; i--)
                        {
                            double candidateValue = GetCandidateValue(i, isCandidateValueToUpdate: false);
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

                if (CheckAddConditions(_currentValue, _candidateValue))
                    return OnCandidateValueToAddChecked(_candidateValue);
                else
                    return OnCandidateValueToAddUnchecked(_candidateValue);
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
                _candidateValue = GetCandidateValue(0, isCandidateValueToUpdate: true);

                if (CheckUpdateConditions(_currentValue, _candidateValue))
                    return OnCandidateValueToUpdateChecked(_candidateValue);
                else
                    return OnCandidateValueToUpdateUnchecked(_candidateValue);
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
        protected override bool OnInit(double candidateValue)
        {
            _currentValue = _candidateValue;
            _lastValue = _candidateValue;
            _currentValueBar = -1;
            _lastValueBar = -1;
            Add(_currentValue);
            return true;
        }
        protected override bool OnCandidateValueToAddChecked(double candidateValue)
        {
            Add(candidateValue);
            _currentValue = candidateValue;
            _currentValueBar = -1;
            return true;
        }
        protected override bool OnCandidateValueToAddUnchecked(double candidateValue)
        {
            Add(_currentValue);
            return true;
        }
        protected override bool OnCandidateValueToUpdateChecked(double candidateValue)
        {
            this[0] = candidateValue;
            _lastValue = _currentValue;
            _lastValueBar = _currentValueBar;
            _currentValue = candidateValue;
            _currentValueBar = -1;
            return true;
        }
        protected override bool OnCandidateValueToUpdateUnchecked(double candidateValue)
        {
            this[0] = _currentValue;
            return true;
        }

        public override string Name
            => $"{_name}({Input.Name},{Period})";

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

    public abstract class IndicatorSeries<TInput1> : IndicatorSeries, IIndicatorSeries<TInput1>
        where TInput1 : IIndicatorSeries
    {
        internal IndicatorSeries(TInput1 input1, string name, int period, int capacity, int oldValuesCapacity, int barsIndex) : base(input1, name, period, capacity, oldValuesCapacity, barsIndex)
        {
            if (input1 == null)
                throw new ArgumentNullException(nameof(input1));
            Input1 = input1;
            Period = Input1.Period;
        }

        public TInput1 Input1 { get; protected set; }

    }

    public abstract class IndicatorSeries<TInput1,TInput2> : IndicatorSeries<TInput1>, IIndicatorSeries<TInput1,TInput2>
        where TInput1 : IIndicatorSeries
        where TInput2 : IIndicatorSeries

    {
        internal IndicatorSeries(TInput1 input1, TInput2 input2, string name, int period, int capacity, int oldValuesCapacity, int barsIndex) : base(input1, name, period, capacity, oldValuesCapacity, barsIndex)
        {
            if (input2 == null)
                throw new ArgumentNullException(nameof(input2));
            Input2 = input2;
            if (Input1.Period != Input2.Period)
                throw new Exception("Los indicadores 'MAX' y 'MIN' deben tener el mismo periodo.");
        }

        public TInput2 Input2 { get; protected set; }

    }
}
