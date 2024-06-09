using System;

namespace KrTrade.Nt.Core.Elements
{
    public interface IHasKey<T> : IEquatable<T>
    {

        /// <summary>
        /// Gets the key of the object.
        /// </summary>
        string Key { get; }

    }
}
