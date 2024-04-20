using KrTrade.Nt.Core.Info;
using KrTrade.Nt.Core.Options;
using KrTrade.Nt.Core.Services;
using System.Collections;
using System.Collections.Generic;

namespace KrTrade.Nt.Core.Collections
{
    public interface IKeyCollection<T> : IEnumerable, IEnumerable<T>
        where T : IKeyItem
    {
        T this[string key] { get; }
        T this[int index] { get; }

        void Add(T item) ;
        void TryAdd(T item);
        void Add<TInfo,TOptions>(IService service, TInfo itemInfo, TOptions itemOptions)
            where TInfo : IInfo
            where TOptions : IOptions;

        int Count { get; }
        void Clear();
        void Remove(string key);
        void RemoveAt(int index);
        bool ContainsKey(string key);
        bool TryGetValue(string key, out int index);

    }
}
