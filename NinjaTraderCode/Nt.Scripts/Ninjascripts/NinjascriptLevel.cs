namespace Nt.Scripts.Ninjascripts
{
    /// <summary>
    /// The ninjascript object level states.
    /// </summary>
    public enum NinjascriptLevel
    {
        /// <summary>
        /// The ninjascript is always active.
        /// </summary>
        /// <remarks>
        /// The ninjascript enter in 'DataLoaded', 'Historical', 'Transition' and 'Realtime' states.
        /// </remarks>
        Active,

        /// <summary>
        /// The ninjascript is active only in the historical state
        /// </summary>
        /// <remarks>
        /// This value is used for ninjascripts that gets historical data for calculate values and inyect to other ninjascript.
        /// The ninjascript enter in 'Historical' and 'Transition' states.
        /// </remarks>
        Historical,

        /// <summary>
        /// The ninjascript is enable only in the real state. This ninjascript doesn´t nedd historical values. 
        /// </summary>
        /// <remarks>
        /// The ninjascript enter in 'Realtime' state.
        /// </remarks>
        Real,

        /// <summary>
        /// The ninjascript is disable.
        /// </summary>
        Disable
    }
}
