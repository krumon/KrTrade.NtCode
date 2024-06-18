using System;

namespace KrTrade.Nt.Core
{
    public interface IHasKey<TSelf> : IEquatable<TSelf>
    {

        /// <summary>
        /// Gets the key of the object.
        /// </summary>
        string Key { get; }

    }
}
