using System;

namespace KrTrade.Nt.Core.Elements
{
    public interface IElementInfo : IEquatable<IElementInfo>
    {
        /// <summary>
        /// Gets the name of the service.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets the unique key of the service.
        /// </summary>
        /// <returns>The string thats represents the unique key of the service.</returns>
        string GetKey();

    }
}
