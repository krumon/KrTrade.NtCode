using System.Collections;
using System.Collections.Generic;

namespace KrTrade.Nt.Core.Elements
{
    public interface IInfoCollection<T> : IInfo, IEnumerable, IEnumerable<T>
        where T : IInfo
    {
        T this[string key] { get; }
        T this[int index] { get; }

        int Count { get; }
        void Clear();
        void RemoveAt(int index);
        
        void Add(T item) ;
        void TryAdd(T item);
        void Remove(T item);
        void Remove(string key);
        bool Contains(T item);
        bool Contains(string key);
        bool TryGetValue(T item, out int index);
        bool TryGetValue(string key, out int index);

    }
}
