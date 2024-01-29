﻿using NinjaTrader.NinjaScript;
using System;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Cache to store the lastest market volumes.
    /// </summary>
    public class TimeCache : DateTimeCache<ISeries<DateTime>>
    {

        /// <summary>
        /// Create <see cref="TimeCache"/> default instance with specified properties.
        /// </summary>
        /// <param name="input">The <see cref="ISeries{DateTime}"/> instance used to gets elements for <see cref="TimeCache"/>.</param>
        /// <param name="period">The <see cref="ICache{T}"/> period without include displacement. <see cref="Cache.Capacity"/> property include displacement.</param>
        /// <param name="displacement">The displacement of <see cref="ICache{T}"/> respect <see cref="Input"/> object used to gets elements.</param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        /// <exception cref="System.Exception">The <paramref name="input"/> must be <see cref="NinjaScriptBase"/> object or <see cref="ISeries{DateTime}"/> object.</exception>
        public TimeCache(ISeries<DateTime> input, int period, int displacement) : base(input, period, displacement)
        {
        }

        /// <summary>
        /// Create <see cref="TimeCache"/> default instance with specified properties.
        /// </summary>
        /// <param name="input">The <see cref="NinjaScriptBase"/> instance used to gets elements for <see cref="TimeCache"/>.</param>
        /// <param name="period">The <see cref="ICache{T}"/> period without include displacement. <see cref="Cache.Capacity"/> property include displacement.</param>
        /// <param name="displacement">The displacement of <see cref="ICache{T}"/> respect <see cref="Input"/> object used to gets elements.</param>
        /// <param name="barsIndex">The index of NinjaScript.Bars used to gets cache elements.</param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        public TimeCache(NinjaScriptBase input, int period, int displacement = 0, int barsIndex = 0) : base((ISeries<DateTime>)(input?.Times[barsIndex]), period, displacement)
        {
        }

        protected override DateTime GetCandidateValue() => Input[0];
        protected override DateTime UpdateCurrentValue() => GetCandidateValue();
        protected override bool IsValidCandidateValueToUpdate(DateTime currentValue, DateTime candidateValue) => candidateValue > currentValue;

        protected override ISeries<DateTime> GetInput(ISeries<DateTime> input) => input;
        
    }
}
