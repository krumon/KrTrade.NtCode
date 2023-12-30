//using System;
//using System.Collections.Generic;

//namespace KrTrade.Nt.Core.Caches
//{
//    /// <summary>
//    /// Base class for all caches.
//    /// </summary>
//    /// <typeparam name="T">The type of cache element.</typeparam>
//    public class Cache<T>
//    {

//        #region Private members

//        private List<T> _cache = new List<T>();
//        private readonly int _capacity;

//        #endregion

//        #region Public properties

//        /// <summary>
//        /// Gets the element of a sepecific index.
//        /// </summary>
//        /// <param name="index">The specific index.</param>
//        /// <returns><see cref="T"/> element.</returns>
//        public T this[int index]
//        {
//            get
//            {
//                if (index < 0 || index >= _cache.Count)
//                    throw new Exception(string.Format("'BaseCache' exception. The index to access to the cache is out of range. The index value is {0}", index));
//                return _cache[index];
//            }
//        }

//        /// <summary>
//        /// Represents the cache capacity.
//        /// </summary>
//        public int Capacity => _capacity;

//        /// <summary>
//        /// The number of elements that exists in cache.
//        /// </summary>
//        public int Count => _cache.Count;

//        /// <summary>
//        /// The number of elements that exists in cache.
//        /// </summary>
//        public bool IsFull => Count == Capacity;

//        #endregion

//        #region Constructors

//        /// <summary>
//        /// Create <see cref="Cache{T}"/> new instance with a specific capacity.
//        /// </summary>
//        /// <param name="capacity">The cache capacity.</param>
//        public Cache(int capacity)
//        {
//            _capacity = capacity <= 0 ? int.MaxValue : capacity;
//        }

//        #endregion

//        #region Public methods

//        /// <summary>
//        /// Dispose the service.
//        /// </summary>
//        public virtual void Dispose()
//        {
//            _cache.Clear();
//            _cache = null;
//        }

//        #endregion

//        #region protected methods

//        /// <summary>
//        /// Adds a new element to the end of the cache.
//        /// </summary>
//        /// <param name="element">The element to add.</param>
//        public void Add(T element)
//        {
//            _cache.Add(element);
//            if (_cache.Count > _capacity)
//                RemoveAt(0);
//        }

//        /// <summary>
//        /// Inserts a new element at the specified cache index.
//        /// </summary>
//        /// <param name="idx">The specified index.</param>
//        /// <param name="element">The element to add.</param>
//        public void Insert(int idx, T element)
//        {
//            _cache.Insert(idx, element);
//        }

//        /// <summary>
//        /// Replace two elements at the specified cache bars ago.
//        /// </summary>
//        /// <param name="element">The element to replace.</param>
//        /// <param name="barsAgo">The bars ago of the element to replace.</param>
//        public void Replace(T element, int barsAgo = 0)
//        {
//            int idx = _cache.Count - 1 - barsAgo;
//            _cache[idx] = element;
//        }

//        /// <summary>
//        /// Determines if an element is in the cache.
//        /// </summary>
//        /// <param name="element">The element to find.</param>
//        /// <returns>True, if the element is in cache.</returns>
//        public bool Contains(T element)
//        {
//            return _cache.Contains(element);
//        }

//        /// <summary>
//        /// Create a lightweight copy of a range of cache items.
//        /// </summary>
//        /// <param name="startIdx">The start index.</param>
//        /// <param name="count">Nmber of elements to gets.</param>
//        /// <returns></returns>
//        public List<T> GetList(int startIdx, int count)
//        {
//            return _cache.GetRange(startIdx, count);
//        }

//        /// <summary>
//        /// Adds the elements of a collection to the end of the cache.
//        /// </summary>
//        /// <param name="elements">The collection to add.</param>
//        public void AddRange(IEnumerable<T> elements)
//        {
//            _cache.AddRange(elements);
//        }

//        /// <summary>
//        /// Remove range of elements of the cache.
//        /// </summary>
//        /// <param name="startIdx">The start index of the elements.</param>
//        /// <param name="count">The number of elements to remove.</param>
//        public void RemoveRange(int startIdx, int count)
//        {
//            _cache.RemoveRange(startIdx, count);
//        }

//        /// <summary>
//        /// Removes a specific element from the cache.
//        /// </summary>
//        /// <param name="item"></param>
//        public void Remove(T item)
//        {
//            _cache.Remove(item);
//        }

//        /// <summary>
//        /// Remove an element at the specified cache index.
//        /// </summary>
//        /// <param name="index">The specified index.</param>
//        public void RemoveAt(int index)
//        {
//            _cache.RemoveAt(index);
//        }

//        /// <summary>
//        /// Removes all elements from the cache.
//        /// </summary>
//        public void RemoveAll()
//        {
//            _cache.RemoveAll((t) => true);
//        }

//        /// <summary>
//        /// Clear the cache.
//        /// </summary>
//        public void Clear()
//        {
//            _cache.Clear();
//        }

//        #endregion

//    }
//}