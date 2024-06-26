using KrTrade.Nt.Core.Services;

namespace KrTrade.Nt.Core.Series
{
    public abstract class BaseValueSeries<T> : BaseSeries<T>, IValueSeries<T>
        where T : struct
    {

        protected T _candidateValue;
        protected bool IsFirstValueToBeAdded => Count == 0 && IsValidValueToBeAdded(_candidateValue, true);

        protected BaseValueSeries(IBarsService bars, SeriesInfo info) : base(bars, info) { }

        public override void Add()
        {
            _candidateValue = GetCandidateValue(isCandidateValueToUpdate: false);

            if (Count == 0)
            {
                if (IsValidValueToBeAdded(_candidateValue, false))
                {
                    CurrentValue = _candidateValue;
                    Add(_candidateValue);
                }
                else
                {
                    CurrentValue = default;
                    Add(default);
                }
                return;
            }

            if (IsValidValueToBeAdded(_candidateValue, false))
                Add(_candidateValue);
            else
                Add(default);
        }
        public override void Update()
        {
            _candidateValue = GetCandidateValue(isCandidateValueToUpdate: true);

            if (IsValidValueToBeUpdated(_candidateValue))
                this[0] = _candidateValue;
        }

        protected abstract T GetCandidateValue(bool isCandidateValueToUpdate);
        protected abstract bool IsValidValue(T candidateValue);
        protected abstract bool IsValidValueToAdd(T candidateValue, bool isFirstValueToAdd);
        protected abstract bool IsValidValueToUpdate(T candidateValue);

        protected virtual bool IsValidValueToBeAdded(T candidateValue, bool isFirstValueToAdd) => IsValidValue(candidateValue) && IsValidValueToAdd(candidateValue, isFirstValueToAdd);
        protected virtual bool IsValidValueToBeUpdated(T candidateValue) => IsValidValue(candidateValue) && IsValidValueToUpdate(candidateValue);

    }
}
