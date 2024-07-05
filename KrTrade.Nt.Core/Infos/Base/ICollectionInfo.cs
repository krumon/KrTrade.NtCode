using System;
using System.Collections;
using System.Collections.Generic;

namespace KrTrade.Nt.Core.Infos
{
    public interface ICollectionInfo<TElementInfo,TElementType,TCollectionType> : IElementInfo<TCollectionType>, IEnumerable, IEnumerable<TElementInfo>
        where TElementInfo : IInfo<TElementType>
        where TCollectionType : Enum
        where TElementType : Enum
    {
        TElementInfo this[string key] { get; }
        TElementInfo this[int index] { get; }

        int Count { get; }
        void Clear();
        void RemoveAt(int index);
        
        void Add(TElementInfo item) ;
        void TryAdd(TElementInfo item);
        void Remove(TElementInfo item);
        void Remove(string key);
        bool Contains(TElementInfo item);
        bool Contains(string key);
        bool TryGetValue(TElementInfo item, out int index);
        bool TryGetValue(string key, out int index);

    }
}
