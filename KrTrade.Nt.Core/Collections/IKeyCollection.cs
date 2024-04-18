using KrTrade.Nt.Core.Elements;
using KrTrade.Nt.Core.Services;
using System.Collections;
using System.Collections.Generic;

namespace KrTrade.Nt.Core.Collections
{
    public interface IKeyCollection<TElement, TInfo> : IEnumerable, IEnumerable<TElement>
        where TElement : IKeyCollectionItem<TInfo>
        where TInfo : IElementInfo
    {
        TElement this[string key] { get; }
        TElement this[int index] { get; }

        void Add(TElement series) ;
        void Add(string name, TElement element);
        void TryAdd(TElement series);
        void TryAdd(string name, TElement element);
        void Add<TOptions>(TInfo elementInfo, TOptions elementOptions)
            where TOptions : ServiceOptions;

        int Count { get; }
        //void Clear();
        //void Remove(string key);
        //void RemoveAt(int index);
        //bool ContainsKey(string key);
        //bool TryGetValue(string key, out int index);

    }
}
