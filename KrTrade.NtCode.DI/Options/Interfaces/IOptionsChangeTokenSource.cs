using KrTrade.NtCode.Primitives;

namespace KrTrade.NtCode.Options
{
    /// <summary>
    /// Used to fetch Microsoft.Extensions.Primitives.IChangeToken used for tracking
    /// options changes.
    /// </summary>
    /// <typeparam name="TOptions">Options type.</typeparam>
    public interface IOptionsChangeTokenSource<out TOptions>
    {
        /// <summary>
        /// The name of the option instance being changed.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Returns a <see cref="IChangeToken"/> which can be used to register
        /// a change notification callback.
        /// </summary>
        /// <returns>Change token.</returns>
        IChangeToken GetChangeToken();
    }
}
