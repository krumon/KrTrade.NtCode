using System;

namespace KrTrade.Nt.Core
{
    public interface IHasKey<T> : IEquatable<T>
    {

        /// <summary>
        /// Gets the key of the object.
        /// </summary>
        string Key { get; }

    }
}
