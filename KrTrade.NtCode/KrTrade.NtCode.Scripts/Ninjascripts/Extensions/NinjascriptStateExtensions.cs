using NinjaTrader.NinjaScript;

namespace Nt.Scripts.Ninjascripts
{
    /// <summary>
    /// Extension methods for convert values 'from' or 'to' <see cref="NinjascriptState"/> enum.
    /// </summary>
    public static class StateExtensions
    {
        /// <summary>
        /// Converts <see cref="State"/> value to <see cref="NinjascriptLevel"/>.
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public static NinjascriptLevel ToNinjascriptLevel(this State state)
        {
            switch (state)
            {
                case State.SetDefaults:
                case State.Configure:
                case State.Active:
                case State.DataLoaded:
                    return NinjascriptLevel.Active;
                case State.Historical:
                case State.Transition:
                    return NinjascriptLevel.Historical;
                case State.Realtime:
                    return NinjascriptLevel.Real;
                default:
                    return NinjascriptLevel.Disable;
            }
        }
    }
}
