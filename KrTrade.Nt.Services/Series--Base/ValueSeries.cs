using System;

namespace KrTrade.Nt.Services
{
    public abstract class ValueSeries<TElement> : NinjaSeries<TElement>, IValueSeries<TElement>
        where TElement : struct
    {
        protected TElement _candidateValue;

        protected TElement _lastValue;
        protected TElement _currentValue;

        /// <summary>
        /// Create <see cref="ValueSeries{TElement,TInput}"/> default instance with specified properties.
        /// </summary>
        /// <param name="input">The object instance used to gets <see cref="INinjaSeries{TElement,TInput}"/> elements.</param>
        /// <param name="period">The specified period to calculate values in cache.</param>
        /// <param name="capacity">The <see cref="ICache{T}"/> capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
        /// <param name="oldValuesCapacity">The length of the old values cache. This values are at the end of cache.</param>
        /// <param name="barsIndex">The index of the 'NinjaScript.Series' necesary for gets the cache elements.</param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        protected ValueSeries(object input, int period, int capacity, int oldValuesCapacity, int barsIndex) : base(input, period, capacity, oldValuesCapacity, barsIndex)
        {
        }

        public override bool Add()
        {
            _candidateValue = GetCandidateValue(0, isCandidateValueForUpdate: false);

            if (Count == 0)
            {
                _currentValue = _candidateValue;
                _lastValue = _candidateValue;
                Add(_currentValue);
                return true;
            }
            _lastValue = _currentValue;

            if (CheckAddConditions(_lastValue, _candidateValue))
            {
                Add(_candidateValue);
                return true;
            }
            else
            {
                Add(default);
                return false;
            }
        }
        public override bool Update()
        {
            _candidateValue = GetCandidateValue(0, isCandidateValueForUpdate: true);

            if (CheckUpdateConditions(_currentValue, _candidateValue))
            {
                _lastValue = _currentValue;
                _currentValue = _candidateValue;
                this[0] = _currentValue;
                return true;
            }
            else
                return false;
        }

        protected abstract TElement GetCandidateValue(int barsAgo, bool isCandidateValueForUpdate);
        protected abstract bool CheckAddConditions(TElement currentValue, TElement candidateValue);
        protected abstract bool CheckUpdateConditions(TElement currentValue, TElement candidateValue);


        //protected abstract TElement UpdateCurrentValue();
        //protected abstract bool IsValidCandidateValueToAdd(TElement candidateValue);
        //protected abstract bool IsValidCandidateValueToUpdate(TElement currentValue, TElement candidateValue);

    }
}
