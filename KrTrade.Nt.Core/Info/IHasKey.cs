using System;

namespace KrTrade.Nt.Core.Info
{
    public interface IHasKey : IEquatable<IHasKey>
    {

        /// <summary>
        /// Gets the key of the object.
        /// </summary>
        string Key { get; }

        ///// <summary>
        ///// Gets the unique key of the object.
        ///// </summary>
        ///// <returns>The string thats represents the unique key of the object.</returns>
        //string GetKey();

    }
}
