using KrTrade.Nt.Core.Infos;
using KrTrade.Nt.Core.Options;
using System;
using System.Collections;
using System.Collections.Generic;

namespace KrTrade.Nt.Core
{
    public interface ICollection<TElement, TElementType, TElementInfo, TCollectionInfo,TCollectionType> : IElement<TCollectionType, TCollectionInfo,IElementOptions>, IEnumerable, IEnumerable<TElement>
        where TElement : IElement<TElementType>
        where TElementInfo: IElementInfo<TElementType>
        where TCollectionInfo : ICollectionInfo<TElementInfo,TElementType,TCollectionType>
        where TElementType : Enum
        where TCollectionType : Enum
    {
        TElement this[string key] { get; }
        TElement this[int index] { get; }

        void Add(TElement item) ;
        void TryAdd(TElement item);

        int Count { get; }
        void Clear();
        void Remove(string key);
        void RemoveAt(int index);
        bool ContainsKey(string key);
        bool TryGetValue(string key, out int index);

    }
}
