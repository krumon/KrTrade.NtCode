using System;

namespace KrTrade.Nt.Core.Infos
{
    public interface IInfo : IHasKey<IInfo>
    {
        /// <summary>
        /// Gets or sets the name of the element.
        /// </summary>
        string Name { get; set; }

    }
    public interface IInfo<T> : IInfo
        where T : Enum
    {

        /// <summary>
        /// Gets or sets the type of the element.
        /// </summary>
        T Type { get; set; }
    }

}
