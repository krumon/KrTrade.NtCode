using KrTrade.Nt.Core.Elements;

namespace KrTrade.Nt.Core.Collections
{
    public interface IKeyCollectionItem
    {
        /// <summary>
        /// Gets the information of the collection element.
        /// </summary>
        IElementInfo Info { get; }
    }
    public interface IKeyCollectionItem<TInfo> : IKeyCollectionItem
        where TInfo : IElementInfo
    {
        /// <summary>
        /// Gets the information of the collection element.
        /// </summary>
        new TInfo Info { get; }
    }
}
