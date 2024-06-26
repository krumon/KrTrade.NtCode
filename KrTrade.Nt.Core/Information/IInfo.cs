using System;

namespace KrTrade.Nt.Core.Information
{
    public interface IInfo : IHasKey<IInfo>
    {

        /// <summary>
        /// Gets or sets the name of the element.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the type of the element.
        /// </summary>
        ElementType Type { get; set; }

    }

    public interface IInfo<T> : IInfo, IHasKey<IInfo>
        where T : Enum
    {

        /// <summary>
        /// Gets or sets the type of the element.
        /// </summary>
        new T Type { get; set; }

    }
}
