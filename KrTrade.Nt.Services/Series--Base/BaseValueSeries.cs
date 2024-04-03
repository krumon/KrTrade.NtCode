namespace KrTrade.Nt.Services.Series
{
    public abstract class BaseValueSeries<TElement> : BaseSeries<TElement>, IValueSeries<TElement>
        where TElement : struct
    {
        protected TElement _candidateValue;

        protected TElement _lastValue;
        protected TElement _currentValue;

        /// <summary>
        /// Create <see cref="BaseValueSeries{TElement}"/> default instance with specified parameters.
        /// </summary>
        /// <param name="period">The specified period to calculate values in cache.</param>
        /// <param name="capacity">The <see cref="ICache{T}"/> capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
        /// <param name="oldValuesCapacity">The length of the old values cache. This values are at the end of cache.</param>
        /// <param name="barsIndex">The index of the 'NinjaScript.Series' necesary for gets the cache elements.</param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        protected BaseValueSeries(int period, int capacity, int oldValuesCapacity, int barsIndex) : base(period, capacity, oldValuesCapacity, barsIndex)
        {
        }

        public override bool Add()
        {
            _candidateValue = GetCandidateValue(0, isCandidateValueToUpdate: false);

            if (Count == 0)
                return OnInit(_candidateValue);

            _lastValue = _currentValue;

            if (CheckAddConditions(_lastValue, _candidateValue))
                return OnCandidateValueToAddChecked(_candidateValue);
            else
                return OnCandidateValueToAddUnchecked(_candidateValue);
        }
        public override bool Update()
        {
            _candidateValue = GetCandidateValue(0, isCandidateValueToUpdate: true);

            if (CheckUpdateConditions(_currentValue, _candidateValue))
                return OnCandidateValueToUpdateUnchecked(_candidateValue);
            else
                return OnCandidateValueToUpdateUnchecked(_candidateValue);
        }

        protected abstract TElement GetCandidateValue(int barsAgo, bool isCandidateValueToUpdate);
        protected abstract bool CheckAddConditions(TElement currentValue, TElement candidateValue);
        protected abstract bool CheckUpdateConditions(TElement currentValue, TElement candidateValue);

        /// <summary>
        /// Method that is executed in the <see cref="Add"/> method when the series is empty.
        /// </summary>
        /// <remarks>
        /// By default the method add the candidate value to the series and update the current value and the last value
        /// with the candidate value.
        /// </remarks>
        /// <param name="candidateValue">The candidate value to be added to the series.</param>
        /// <returns><c>true</c>, if an element is added to the series, otherwise <c>false</c>.</returns>
        protected virtual bool OnInit(TElement candidateValue)
        {
            Add(candidateValue);
            _currentValue = candidateValue;
            _lastValue = candidateValue;
            return true;
        }

        /// <summary>
        /// Method that is executed when an element is going to be added and the check conditions are passed.
        /// </summary>
        /// <remarks>
        /// By default the method add the candidate value to the series and update the current value with the candidate value.
        /// </remarks>
        /// <param name="candidateValue">The candidate value that has passed the necessary conditions to be added to the series.</param>
        /// <returns><c>true</c>, if an element is added to the series, otherwise <c>false</c>.</returns>
        protected virtual bool OnCandidateValueToAddChecked(TElement candidateValue) 
        {
            Add(candidateValue);
            _currentValue = candidateValue;
            return true;
        }

        /// <summary>
        /// Method that is executed when an element is going to be added and the check conditions are not passed.
        /// By default the method add default value to the series and update the current value with the candidate value.
        /// </summary>
        /// <param name="candidateValue">The candidate value that has passed the necessary conditions to be added to the series.</param>
        /// <returns><c>true</c>, if an element is added to the series, otherwise <c>false</c>.</returns>
        protected virtual bool OnCandidateValueToAddUnchecked(TElement candidateValue) 
        {
            Add(default);
            _currentValue = default;
            return true;
        }

        /// <summary>
        /// Method that is executed when an element is going to be updated and the check conditions are passed.
        /// By default the method update the series current value with the candidate value and update the current and last values.
        /// </summary>
        /// <param name="candidateValue">The candidate value that has passed the necessary conditions to be added to the series.</param>
        /// <returns><c>true</c>, if an element is added to the series, otherwise <c>false</c>.</returns>
        protected virtual bool OnCandidateValueToUpdateChecked(TElement candidateValue) 
        {
            this[0] = candidateValue;
            _lastValue = _currentValue;
            _currentValue = candidateValue;
            return true;
        }

        /// <summary>
        /// Method that is executed when an element is going to be updated and the check conditions are not passed.
        /// By default the method is empty.
        /// </summary>
        /// <param name="candidateValue">The candidate value that has passed the necessary conditions to be added to the series.</param>
        /// <returns><c>true</c>, if an element is added to the series, otherwise <c>false</c>.</returns>
        protected virtual bool OnCandidateValueToUpdateUnchecked(TElement candidateValue)
        {
            this[0] = _currentValue;
            return true;
        }
    }
}
