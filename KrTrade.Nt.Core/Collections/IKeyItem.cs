using KrTrade.Nt.Core.Info;

namespace KrTrade.Nt.Core.Collections
{
    public interface IKeyItem : IHasKey, IHasName
    {
        /// <summary>
        /// Converts the actual instance to string.
        /// </summary>
        /// <param name="tabOrder">The tabulation if the string.</param>
        /// <returns>The specified string of the object.</returns>
        string ToString(int tabOrder);
    }
}
