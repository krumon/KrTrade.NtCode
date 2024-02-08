using NinjaTrader.NinjaScript;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Cache to store the lastest market high prices.
    /// </summary>
    public class IndexCache : IntCache<int[]>
    {

        private readonly int _barsIndex = 0;

        ///// <summary>
        ///// Create <see cref="IndexCache"/> default instance with specified properties.
        ///// </summary>
        ///// <param name="input">The <see cref="int"/> array instance used to gets elements for <see cref="IndexCache"/>.</param>
        ///// <param name="period">The <see cref="ICache{T}"/> period without include displacement. <see cref="Cache.Capacity"/> property include displacement.</param>
        ///// <param name="displacement">The displacement of <see cref="ICache{T}"/> respect <see cref="Input"/> object used to gets elements.</param>
        ///// <exception cref="System.ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        //public IndexCache(int[] input, int period, int displacement = 0) : base(input, period, displacement)
        //{
        //}

        /// <summary>
        /// Create <see cref="IndexCache"/> default instance with specified properties.
        /// </summary>
        /// <param name="input">The <see cref="NinjaScriptBase"/> instance used to gets elements for <see cref="IndexCache"/>.</param>
        /// <param name="period">The <see cref="ICache{T}"/> period without include displacement. <see cref="Cache.Capacity"/> property include displacement.</param>
        /// <param name="displacement">The displacement of <see cref="ICache{T}"/> respect <see cref="Input"/> object used to gets elements.</param>
        /// <param name="barsIndex">The index of NinjaScript.Bars used to gets cache elements.</param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        public IndexCache(NinjaScriptBase input, int period, int displacement = 0, int barsIndex = 0) : base(input?.CurrentBars, period, displacement)
        {
            _barsIndex = barsIndex;
        }

        protected override int GetCandidateValue() => Input[_barsIndex];
        protected override int UpdateCurrentValue() => GetCandidateValue();
        protected override bool IsValidCandidateValueToUpdate(int currentValue, int candidateValue) => candidateValue > currentValue;

        protected override int[] GetInput(int[] input) => input;
    }
}
