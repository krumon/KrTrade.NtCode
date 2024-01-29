using NinjaTrader.NinjaScript;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Cache to store the lastest market high prices.
    /// </summary>
    public class TicksCache : LongCache<NinjaScriptBase>
    {
        private readonly int _barsIndex = 0;

        /// <summary>
        /// Create <see cref="TicksCache"/> default instance with specified properties.
        /// </summary>
        /// <param name="input">The <see cref="NinjaScriptBase"/> instance used to gets elements for <see cref="TicksCache"/>.</param>
        /// <param name="period">The <see cref="ICache{T}"/> period without include displacement. <see cref="Cache.Capacity"/> property include displacement.</param>
        /// <param name="displacement">The displacement of <see cref="ICache{T}"/> respect <see cref="Input"/> object used to gets elements.</param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        public TicksCache(NinjaScriptBase input, int period, int displacement) : base(input, period, displacement)
        {
        }

        /// <summary>
        /// Create <see cref="TicksCache"/> default instance with specified properties.
        /// </summary>
        /// <param name="input">The <see cref="NinjaScriptBase"/> instance used to gets elements for <see cref="TicksCache"/>.</param>
        /// <param name="period">The <see cref="ICache{T}"/> period without include displacement. <see cref="Cache.Capacity"/> property include displacement.</param>
        /// <param name="displacement">The displacement of <see cref="ICache{T}"/> respect <see cref="Input"/> object used to gets elements.</param>
        /// <param name="barsIndex">The index of NinjaScript.Bars used to gets cache elements.</param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        public TicksCache(NinjaScriptBase input, int period, int displacement = 0, int barsIndex = 0) : base(input, period, displacement)
        {
            _barsIndex = barsIndex;
        }

        protected override long GetCandidateValue() => Input.BarsArray[_barsIndex].TickCount;
        protected override long UpdateCurrentValue() => GetCandidateValue();
        protected override bool IsValidCandidateValueToUpdate(long currentValue, long candidateValue) => candidateValue > currentValue;

        protected override NinjaScriptBase GetInput(NinjaScriptBase input) => input;

    }
}
