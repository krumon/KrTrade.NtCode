using System;

namespace KrTrade.Nt.Core.Info
{
    public interface IHasKey : IEquatable<IHasKey>
    {

        /// <summary>
        /// Gets the key of the object.
        /// </summary>
        string Key { get; }

    }
}
