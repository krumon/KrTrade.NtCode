using System;

namespace KrTrade.Nt.Services.Series
{
    public abstract class BaseNumericPeriodSeries : BaseNumericSeries
    {
        protected int _lastValueBar;
        protected int _currentValueBar;

        public new PeriodSeriesInfo Info { get;set; }
        public int Period 
        {
            get => Info.Period;
            internal set => Info.Period = value;
        }

        /// <summary>
        /// Create default instance with specified series info.
        /// </summary>
        /// <param name="bars">The bars service owner of the series.</param>
        /// <param name="info">The specified information of the series.</param>
        protected BaseNumericPeriodSeries(IBarsService bars, PeriodSeriesInfo info) : base(bars,info)
        {
            // Configure period value
            if (Period < 1)
            {
                Period = 1;
                Bars.PrintService?.LogWarning($"Error configuring {Name} series period. The period cannot be minor than one.");
            }
            if (Period > MaxCapacity)
            {
                Capacity = MaxCapacity;
                Period = MaxCapacity;
                Bars.PrintService?.LogInformation($"Resizing of the {Name} series capacity and period. The period value cannot be greater than maximum capacity of the serie.");
            }
           
            if (Period > Capacity)
            {
                Capacity = Period;
                Bars.PrintService?.LogInformation($"Resizing of the {Name} series capacity. The capacity value cannot be minor than period value.");
            }
        }

        public override void Add()
        {
            try
            {
                _candidateValue = GetCandidateValue(isCandidateValueToUpdate: false);

                if (Count == 0)
                {
                    if (IsValidValueToBeAdded(_candidateValue, false))
                    {
                        CurrentValue = _candidateValue;
                        _currentValueBar = -1;
                        _lastValueBar = -1;
                        Add(_candidateValue);
                    }
                    else
                    {
                        CurrentValue = default;
                        _currentValueBar = -1;
                        _lastValueBar = -1;
                        Add(default);
                    }
                    return;
                }

                if (_currentValueBar > Period - 2)
                {
                    if (Period != 1)
                    {
                        double lastValue = InitializeLastValue();

                        for (int i = Math.Min(Count, Period - 1); i > 0; i--)
                        {
                            double candidateValue = GetCandidateValue(isCandidateValueToUpdate: false);
                            if (IsValidValueToBeAdded(candidateValue, false))
                            {
                                CurrentValue = candidateValue;
                                _currentValueBar = i - 1;
                            }
                        }
                    }
                }

                LastValue = CurrentValue;
                _lastValueBar = _currentValueBar;

                if (IsValidValueToBeAdded(_candidateValue, false))
                {
                    Add(_candidateValue);
                    _currentValueBar = -1;
                }
                else
                {
                    // Mantengo el valor actual
                    Add(CurrentValue);
                }
            }
            catch
            {
                Add(default);
            }
        }
        public override void Update()
        {
            try
            {
                _candidateValue = GetCandidateValue(isCandidateValueToUpdate: true);

                if (IsValidValueToBeUpdated(_candidateValue))
                {
                    this[0] = _candidateValue;
                    _lastValueBar = _currentValueBar;
                    _currentValueBar = -1;
                }
            }
            catch { }
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

        protected abstract double InitializeLastValue();

        protected sealed override bool IsValidValueToBeAdded(double candidateValue, bool isFirstValueToAdd) => base.IsValidValueToBeAdded(candidateValue, isFirstValueToAdd);
        protected sealed override bool IsValidValueToBeUpdated(double candidateValue) => base.IsValidValueToBeUpdated(candidateValue);

    }

    //public abstract class IndicatorSeries<TInput> : IndicatorSeries, IIndicatorSeries<TInput>
    //    where TInput : ISeries<double>
    //{

    //    /// <summary>
    //    /// Create default instance with specified parameters.
    //    /// </summary>
    //    /// <param name="barsService">The bars service instance used to gets series.</param>
    //    /// <param name="period">The specified period to calculate values in cache.</param>
    //    /// <exception cref="ArgumentNullException">The <paramref name="barsService"/> cannot be null.</exception>
    //    protected IndicatorSeries(IBarsService barsService, int period) : base(period, barsService.Index)
    //    {
    //        if (barsService == null) throw new ArgumentNullException(nameof(barsService));
    //        //Input = barsService.GetSeries<TInput>();
    //    }

    //    /// <summary>
    //    /// Create default instance with specified parameters.
    //    /// </summary>
    //    /// <param name="input">The input instance used to gets series.</param>
    //    /// <param name="period">The specified period to calculate values in cache.</param>
    //    /// <param name="barsIndex">The index of the 'NinjaScript.Series' necesary for gets the cache elements.</param>
    //    /// <exception cref="ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
    //    protected IndicatorSeries(TInput input, int period, int barsIndex) : base(period, barsIndex)
    //    {
    //        if (input == null) throw new ArgumentNullException(nameof(input));
    //        Input = input;
    //    }

    //    public TInput Input { get; protected set; }
    //    //public override string Key => $"{Name}({Input.Name},{Period})";

    //}

    //public abstract class IndicatorSeries<TInput, TEntry> : IndicatorSeries, IIndicatorSeries<TInput, TEntry>
    //    where TInput : ISeries<double>
    //{

    //    /// <summary>
    //    /// Create default instance with specified parameters.
    //    /// </summary>
    //    /// <param name="barsService">The bars service instance used to gets series.</param>
    //    /// <param name="period">The specified period to calculate values in cache.</param>
    //    /// <exception cref="ArgumentNullException">The <paramref name="barsService"/> cannot be null.</exception>
    //    protected IndicatorSeries(IBarsService barsService, int period) : base(period, barsService.Index)
    //    {
    //        if (barsService == null) throw new ArgumentNullException(nameof(barsService));
    //        //Input = barsService.GetSeries<TInput>();
    //    }

    //    /// <summary>
    //    /// Create default instance with specified parameters.
    //    /// </summary>
    //    /// <param name="entry">The entry instance used to gets the input series. The input series is necesary for gets elements of the series.</param>
    //    /// <param name="period">The specified period to calculate values in cache.</param>
    //    /// <param name="barsIndex">The index of the 'NinjaScript.Series' necesary for gets the cache elements.</param>
    //    /// <exception cref="ArgumentNullException">The <paramref name="entry"/> cannot be null.</exception>
    //    protected IndicatorSeries(TEntry entry, int period, int barsIndex) : base(period, barsIndex)
    //    {
    //        Input = entry != null ? GetInput(entry) : throw new ArgumentNullException(nameof(entry));
    //    }

    //    /// <summary>
    //    /// Create default instance with specified parameters.
    //    /// </summary>
    //    /// <param name="input">The input instance used to gets elements of the series.</param>
    //    /// <param name="period">The specified period to calculate values in cache.</param>
    //    /// <param name="barsIndex">The index of the 'NinjaScript.Series' necesary for gets the cache elements.</param>
    //    /// <exception cref="ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
    //    protected IndicatorSeries(TInput input, int period, int barsIndex) : base(period, barsIndex)
    //    {
    //        Input = input != null ? input : throw new ArgumentNullException(nameof(input));
    //    }

    //    public TInput Input { get; protected set; }
    //    public abstract TInput GetInput(TEntry entry);
    //    //public override string Key => $"{Name}({Input.Name},{Period})";

    //}

    //public abstract class IndicatorSeries<TInput1, TInput2, TEntry> : IndicatorSeries, IIndicatorSeries<TInput1, TInput2, TEntry>
    //    where TInput1 : ISeries<double>
    //    where TInput2 : ISeries<double>
    //{

    //    /// <summary>
    //    /// Create default instance with specified parameters.
    //    /// </summary>
    //    /// <param name="barsService">The bars service instance used to gets series.</param>
    //    /// <param name="period">The specified period to calculate values in cache.</param>
    //    /// <exception cref="ArgumentNullException">The <paramref name="barsService"/> cannot be null.</exception>
    //    protected IndicatorSeries(IBarsService barsService, int period) : base(period, barsService.Index)
    //    {
    //        if (barsService == null) throw new ArgumentNullException(nameof(barsService));
    //        //Input1 = barsService.GetSeries<TInput1>();
    //        //Input2 = barsService.GetSeries<TInput2>();
    //    }

    //    /// <summary>
    //    /// Create default instance with specified parameters.
    //    /// </summary>
    //    /// <param name="entry">The entry instance used to gets the input series. The input series is necesary for gets series elements.</param>
    //    /// <param name="period">The specified period to calculate values in cache.</param>
    //    /// <param name="barsIndex">The index of the 'NinjaScript.Series' necesary for gets the cache elements.</param>
    //    /// <exception cref="ArgumentNullException">The <paramref name="entry"/> cannot be null.</exception>
    //    protected IndicatorSeries(TEntry entry, int period, int barsIndex) : base(period, barsIndex)
    //    {
    //        Input1 = entry != null ? GetInput1(entry) : throw new ArgumentNullException(nameof(entry));
    //        Input2 = entry != null ? GetInput2(entry) : throw new ArgumentNullException(nameof(entry));
    //    }

    //    /// <summary>
    //    /// Create default instance with specified parameters.
    //    /// </summary>
    //    /// <param name="input1">The first object used to gets series elements.</param>
    //    /// <param name="input2">The second object used to gets series elements.</param>
    //    /// <param name="period">The specified period to calculate values in cache.</param>
    //    /// <param name="barsIndex">The index of the 'NinjaScript.Series' necesary for gets the cache elements.</param>
    //    /// <exception cref="ArgumentNullException">The <paramref name="input1"/> or <paramref name="input2"/> cannot be null.</exception>
    //    protected IndicatorSeries(TInput1 input1, TInput2 input2, int period, int barsIndex) : base(period, barsIndex)
    //    {
    //        Input1 = input1 != null ? input1 : throw new ArgumentNullException(nameof(input1));
    //        Input2 = input1 != null ? input2 : throw new ArgumentNullException(nameof(input2));
    //    }


    //    public TInput1 Input1 { get; protected set; }
    //    public TInput2 Input2 { get; protected set; }
    //    public abstract TInput1 GetInput1(TEntry entry);
    //    public abstract TInput2 GetInput2(TEntry entry);
    //    //public override string Key => $"{Name}({Input1.Name},{Input2.Name},{Period})";

    //}

    //public abstract class IndicatorSeries<TInput1, TInput2, TEntry1, TEntry2> : IndicatorSeries, IIndicatorSeries<TInput1, TInput2, TEntry1, TEntry2>
    //    where TInput1 : ISeries<double>
    //    where TInput2 : ISeries<double>
    //{

    //    /// <summary>
    //    /// Create default instance with specified parameters.
    //    /// </summary>
    //    /// <param name="barsService">The bars service instance used to gets series.</param>
    //    /// <param name="period">The specified period to calculate values in cache.</param>
    //    /// <exception cref="ArgumentNullException">The <paramref name="barsService"/> cannot be null.</exception>
    //    protected IndicatorSeries(IBarsService barsService, int period) : base(period, barsService.Index)
    //    {
    //        if (barsService == null) throw new ArgumentNullException(nameof(barsService));
    //        //Input1 = barsService.GetSeries<TInput1>();
    //        //Input2 = barsService.GetSeries<TInput2>();
    //    }

    //    /// <summary>
    //    /// Create default instance with specified parameters.
    //    /// </summary>
    //    /// <param name="entry1">The first entry instance used to gets the first input series. The input series is necesary for gets series elements.</param>
    //    /// <param name="entry1">The second entry instance used to gets the second input series. The input series is necesary for gets series elements.</param>
    //    /// <param name="period">The specified period to calculate values in cache.</param>
    //    /// <param name="barsIndex">The index of the 'NinjaScript.Series' necesary for gets the cache elements.</param>
    //    /// <exception cref="ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
    //    protected IndicatorSeries(TEntry1 entry1, TEntry2 entry2, int period, int barsIndex) : base(period, barsIndex)
    //    {
    //        Input1 = entry1 != null ? GetInput1(entry1) : throw new ArgumentNullException(nameof(entry1));
    //        Input2 = entry2 != null ? GetInput2(entry2) : throw new ArgumentNullException(nameof(entry2));
    //    }

    //    /// <summary>
    //    /// Create default instance with specified parameters.
    //    /// </summary>
    //    /// <param name="input1">The first object used to gets series elements.</param>
    //    /// <param name="input2">The second object used to gets series elements.</param>
    //    /// <param name="period">The specified period to calculate values in cache.</param>
    //    /// <param name="barsIndex">The index of the 'NinjaScript.Series' necesary for gets the cache elements.</param>
    //    /// <exception cref="ArgumentNullException">The <paramref name="input1"/> or <paramref name="input2"/> cannot be null.</exception>
    //    protected IndicatorSeries(TInput1 input1, TInput2 input2, int period, int barsIndex) : base(period, barsIndex)
    //    {
    //        Input1 = input1 != null ? input1 : throw new ArgumentNullException(nameof(input1));
    //        Input2 = input1 != null ? input2 : throw new ArgumentNullException(nameof(input2));
    //    }

    //    public TInput1 Input1 { get; protected set; }
    //    public TInput2 Input2 { get; protected set; }
    //    public abstract TInput1 GetInput1(TEntry1 entry1);
    //    public abstract TInput2 GetInput2(TEntry2 entry2);
    //    //public override string Key => $"{Name}({Input1.Name},{Input2.Name},{Period})";

    //}
}
