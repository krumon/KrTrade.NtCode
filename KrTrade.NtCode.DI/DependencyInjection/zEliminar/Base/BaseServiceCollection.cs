using System.Collections;
using System.Collections.Generic;

namespace KrTrade.Nt.DI.DependencyInjection
{
    /// <summary>
    /// Represents any implemented collection.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseServiceCollection<T> : IServiceCollection<T> 
        where T : IServiceDescriptor
    {

        #region Private members

        protected IList<T> _descriptors = new List<T>();

        #endregion

        #region Implementation methods

        /// <inheritdoc/>
        public T this[int index]
        {
            get => _descriptors[index];
            set => _descriptors[index] = value;
        }

        /// <inheritdoc/>
        public int Count => _descriptors.Count;

        /// <inheritdoc/>
        public bool IsReadOnly => false;

        /// <inheritdoc/>
        public virtual void Add(T item)
        {
            _descriptors.Add(item);
        }

        /// <inheritdoc/>
        public virtual bool Remove(T item)
        {
            return _descriptors.Remove(item);
        }

        /// <inheritdoc/>
        public void Clear()
        {
            _descriptors.Clear();
        }

        /// <inheritdoc/>
        public bool Contains(T item)
        {
            return _descriptors.Contains(item);
        }

        /// <inheritdoc/>
        public void CopyTo(T[] array, int arrayIndex)
        {
            _descriptors.CopyTo(array, arrayIndex);
        }

        /// <inheritdoc/>
        public IEnumerator<T> GetEnumerator()
        {
            return _descriptors.GetEnumerator();
        }

        /// <inheritdoc/>
        public int IndexOf(T item)
        {
            return _descriptors.IndexOf(item);
        }

        /// <inheritdoc/>
        public void Insert(int index, T item)
        {
            _descriptors.Insert(index, item);
        }

        /// <inheritdoc/>
        public void RemoveAt(int index)
        {
            _descriptors.RemoveAt(index);
        }

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region Public methods

        public bool Exist(T descriptor)
        {
            if (_descriptors == null || _descriptors.Count == 0)
                return false;

            foreach (T item in _descriptors)
                if (item.Key == descriptor.Key)
                    return true;

            return false;
        }

        #endregion

    }
}
