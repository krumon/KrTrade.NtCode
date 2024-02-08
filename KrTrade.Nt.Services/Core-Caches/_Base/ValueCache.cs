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
        /// <param name="period">The <see cref="ICache{T}"/> period without displacement. <see cref="Cache.Capacity"/> property include displacement.</param>
        /// <param name="displacement">The displacement of <see cref="ICache{T}"/> respect <see cref="TInput"/> object used to gets elements.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        protected ValueCache(TInput input, int period, int displacement) : base(input,period, displacement)
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
