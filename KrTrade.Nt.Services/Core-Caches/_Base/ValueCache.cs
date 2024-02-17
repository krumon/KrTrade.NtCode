using KrTrade.Nt.Core.Caches;
using System;

namespace KrTrade.Nt.Services
{
    public abstract class ValueCache<TElement,TInput> : NinjaCache<TElement,TInput>, IValueCache<TElement,TInput>
        where TElement : struct
    {
        private TElement _candidateValue;

        /// <summary>
        /// Create <see cref="NinjaCache{TElement,TInput}"/> default instance with specified properties.
        /// </summary>
        /// <param name="input">The object instance used to gets elements for <see cref="INinjaCache{TElement,TInput}"/>.</param>
        /// <param name="period">The specified period to calculate values in cache.</param>
        /// <param name="capacity">The <see cref="ICache{T}"/> capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
        /// <param name="barsIndex">The index of the 'NinjaScript.Series' necesary for gets the cache elements.</param>
        /// <param name="oldValuesCapacity">The length of the old values cache. This values are at the end of cache.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        protected ValueCache(TInput input, int period = 1, int capacity = DEFAULT_CAPACITY, int oldValuesCapacity = DEFAULT_OLD_VALUES_CAPACITY,int barsIndex = 0) : base(input,capacity,period,oldValuesCapacity,barsIndex)
        {
        }

        public sealed override bool Add()
        {
            _candidateValue = GetCandidateValue();
            if (IsValidValue(_candidateValue))
                Add(_candidateValue);
            else
                Add(default);
            return true;
        }
        public sealed override bool Update()
        {
            _candidateValue = UpdateCurrentValue();
            if (IsValidValue(_candidateValue) && IsValidCandidateValueToUpdate(CurrentValue,_candidateValue))
            {
                CurrentValue = _candidateValue;
                return true;
            }
            return false;
        }
        
        protected abstract TElement GetCandidateValue();
        protected abstract TElement UpdateCurrentValue();
        protected abstract bool IsValidCandidateValueToUpdate(TElement currentValue, TElement candidateValue);

    }
}
