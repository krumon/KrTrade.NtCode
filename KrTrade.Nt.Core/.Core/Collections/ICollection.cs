﻿using System.Collections;
using System.Collections.Generic;

namespace KrTrade.Nt.Core
{
    public interface ICollection<TElement,TInfo> : IService<TInfo>, IEnumerable, IEnumerable<TElement>
        where TElement : IElement
        where TInfo : IServiceInfo, IInfoCollection<IInfo>, new()
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
