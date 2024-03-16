using System;

namespace KrTrade.Nt.Services
{
    public abstract class IndicatorSeries : DoubleSeries, IIndicatorSeries
    {
        protected int _lastValueBar;
        protected int _currentValueBar;

        /// <summary>
        /// Create default instance with specified parameters.
        /// </summary>
        /// <param name="period">The specified period to calculate values in cache.</param>
        /// <param name="barsIndex">The index of the 'NinjaScript.Series' necesary for gets the cache elements.</param>
        protected IndicatorSeries(int period, int barsIndex) : base(period, DEFAULT_CAPACITY, DEFAULT_OLD_VALUES_CAPACITY, barsIndex)
        {
            Capacity = Period > Capacity ? Period : Capacity;
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

        protected abstract double GetInitValuePreviousRecalculate();

    }

    public abstract class IndicatorSeries<TInput> : IndicatorSeries, IIndicatorSeries<TInput>
        where TInput : ISeries<double>
    {

        /// <summary>
        /// Create default instance with specified parameters.
        /// </summary>
        /// <param name="barsService">The bars service instance used to gets series.</param>
        /// <param name="period">The specified period to calculate values in cache.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="barsService"/> cannot be null.</exception>
        protected IndicatorSeries(IBarsService barsService, int period) : base(period, barsService.Index)
        {
            if (barsService == null) throw new ArgumentNullException(nameof(barsService));
            Input = barsService.GetSeries<TInput>();
        }

        /// <summary>
        /// Create default instance with specified parameters.
        /// </summary>
        /// <param name="input">The input instance used to gets series.</param>
        /// <param name="period">The specified period to calculate values in cache.</param>
        /// <param name="barsIndex">The index of the 'NinjaScript.Series' necesary for gets the cache elements.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        protected IndicatorSeries(TInput input, int period, int barsIndex) : base(period, barsIndex)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));
            Input = input;
        }

        public TInput Input { get; protected set; }
        public override string Key => $"{Name}({Input.Name},{Period})";

    }

    public abstract class IndicatorSeries<TInput, TEntry> : IndicatorSeries, IIndicatorSeries<TInput, TEntry>
        where TInput : ISeries<double>
    {

        /// <summary>
        /// Create default instance with specified parameters.
        /// </summary>
        /// <param name="barsService">The bars service instance used to gets series.</param>
        /// <param name="period">The specified period to calculate values in cache.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="barsService"/> cannot be null.</exception>
        protected IndicatorSeries(IBarsService barsService, int period) : base(period, barsService.Index)
        {
            if (barsService == null) throw new ArgumentNullException(nameof(barsService));
            Input = barsService.GetSeries<TInput>();
        }

        /// <summary>
        /// Create default instance with specified parameters.
        /// </summary>
        /// <param name="entry">The entry instance used to gets the input series. The input series is necesary for gets elements of the series.</param>
        /// <param name="period">The specified period to calculate values in cache.</param>
        /// <param name="barsIndex">The index of the 'NinjaScript.Series' necesary for gets the cache elements.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="entry"/> cannot be null.</exception>
        protected IndicatorSeries(TEntry entry, int period, int barsIndex) : base(period, barsIndex)
        {
            Input = entry != null ? GetInput(entry) : throw new ArgumentNullException(nameof(entry));
        }

        /// <summary>
        /// Create default instance with specified parameters.
        /// </summary>
        /// <param name="input">The input instance used to gets elements of the series.</param>
        /// <param name="period">The specified period to calculate values in cache.</param>
        /// <param name="barsIndex">The index of the 'NinjaScript.Series' necesary for gets the cache elements.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        protected IndicatorSeries(TInput input, int period, int barsIndex) : base(period, barsIndex)
        {
            Input = input != null ? input : throw new ArgumentNullException(nameof(input));
        }

        public TInput Input { get; protected set; }
        public abstract TInput GetInput(TEntry entry);
        public override string Key => $"{Name}({Input.Name},{Period})";

    }

    public abstract class IndicatorSeries<TInput1, TInput2, TEntry> : IndicatorSeries, IIndicatorSeries<TInput1, TInput2, TEntry>
        where TInput1 : ISeries<double>
        where TInput2 : ISeries<double>
    {

        /// <summary>
        /// Create default instance with specified parameters.
        /// </summary>
        /// <param name="barsService">The bars service instance used to gets series.</param>
        /// <param name="period">The specified period to calculate values in cache.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="barsService"/> cannot be null.</exception>
        protected IndicatorSeries(IBarsService barsService, int period) : base(period, barsService.Index)
        {
            if (barsService == null) throw new ArgumentNullException(nameof(barsService));
            Input1 = barsService.GetSeries<TInput1>();
            Input2 = barsService.GetSeries<TInput2>();
        }

        /// <summary>
        /// Create default instance with specified parameters.
        /// </summary>
        /// <param name="entry">The entry instance used to gets the input series. The input series is necesary for gets series elements.</param>
        /// <param name="period">The specified period to calculate values in cache.</param>
        /// <param name="barsIndex">The index of the 'NinjaScript.Series' necesary for gets the cache elements.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="entry"/> cannot be null.</exception>
        protected IndicatorSeries(TEntry entry, int period, int barsIndex) : base(period, barsIndex)
        {
            Input1 = entry != null ? GetInput1(entry) : throw new ArgumentNullException(nameof(entry));
            Input2 = entry != null ? GetInput2(entry) : throw new ArgumentNullException(nameof(entry));
        }

        /// <summary>
        /// Create default instance with specified parameters.
        /// </summary>
        /// <param name="input1">The first object used to gets series elements.</param>
        /// <param name="input2">The second object used to gets series elements.</param>
        /// <param name="period">The specified period to calculate values in cache.</param>
        /// <param name="barsIndex">The index of the 'NinjaScript.Series' necesary for gets the cache elements.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="input1"/> or <paramref name="input2"/> cannot be null.</exception>
        protected IndicatorSeries(TInput1 input1, TInput2 input2, int period, int barsIndex) : base(period, barsIndex)
        {
            Input1 = input1 != null ? input1 : throw new ArgumentNullException(nameof(input1));
            Input2 = input1 != null ? input2 : throw new ArgumentNullException(nameof(input2));
        }


        public TInput1 Input1 { get; protected set; }
        public TInput2 Input2 { get; protected set; }
        public abstract TInput1 GetInput1(TEntry entry);
        public abstract TInput2 GetInput2(TEntry entry);
        public override string Key => $"{Name}({Input1.Name},{Input2.Name},{Period})";

    }

    public abstract class IndicatorSeries<TInput1, TInput2, TEntry1, TEntry2> : IndicatorSeries, IIndicatorSeries<TInput1, TInput2, TEntry1, TEntry2>
        where TInput1 : ISeries<double>
        where TInput2 : ISeries<double>
    {

        /// <summary>
        /// Create default instance with specified parameters.
        /// </summary>
        /// <param name="barsService">The bars service instance used to gets series.</param>
        /// <param name="period">The specified period to calculate values in cache.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="barsService"/> cannot be null.</exception>
        protected IndicatorSeries(IBarsService barsService, int period) : base(period, barsService.Index)
        {
            if (barsService == null) throw new ArgumentNullException(nameof(barsService));
            Input1 = barsService.GetSeries<TInput1>();
            Input2 = barsService.GetSeries<TInput2>();
        }

        /// <summary>
        /// Create default instance with specified parameters.
        /// </summary>
        /// <param name="entry1">The first entry instance used to gets the first input series. The input series is necesary for gets series elements.</param>
        /// <param name="entry1">The second entry instance used to gets the second input series. The input series is necesary for gets series elements.</param>
        /// <param name="period">The specified period to calculate values in cache.</param>
        /// <param name="barsIndex">The index of the 'NinjaScript.Series' necesary for gets the cache elements.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        protected IndicatorSeries(TEntry1 entry1, TEntry2 entry2, int period, int barsIndex) : base(period, barsIndex)
        {
            Input1 = entry1 != null ? GetInput1(entry1) : throw new ArgumentNullException(nameof(entry1));
            Input2 = entry2 != null ? GetInput2(entry2) : throw new ArgumentNullException(nameof(entry2));
        }

        /// <summary>
        /// Create default instance with specified parameters.
        /// </summary>
        /// <param name="input1">The first object used to gets series elements.</param>
        /// <param name="input2">The second object used to gets series elements.</param>
        /// <param name="period">The specified period to calculate values in cache.</param>
        /// <param name="barsIndex">The index of the 'NinjaScript.Series' necesary for gets the cache elements.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="input1"/> or <paramref name="input2"/> cannot be null.</exception>
        protected IndicatorSeries(TInput1 input1, TInput2 input2, int period, int barsIndex) : base(period, barsIndex)
        {
            Input1 = input1 != null ? input1 : throw new ArgumentNullException(nameof(input1));
            Input2 = input1 != null ? input2 : throw new ArgumentNullException(nameof(input2));
        }

        public TInput1 Input1 { get; protected set; }
        public TInput2 Input2 { get; protected set; }
        public abstract TInput1 GetInput1(TEntry1 entry1);
        public abstract TInput2 GetInput2(TEntry2 entry2);
        public override string Key => $"{Name}({Input1.Name},{Input2.Name},{Period})";

    }
}
