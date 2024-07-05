using KrTrade.Nt.Core.Data;
using System;

namespace KrTrade.Nt.Core.Infos
{
    /// <summary>
    /// Defines the information of the element.
    /// </summary>
    public interface IElementInfo : IInfo<ElementType>
    {
    }

    /// <summary>
    /// Defines the information of the element.
    /// </summary>
    public interface IElementInfo<T> : IInfo<T>
        where T: Enum
    {
    }


}
