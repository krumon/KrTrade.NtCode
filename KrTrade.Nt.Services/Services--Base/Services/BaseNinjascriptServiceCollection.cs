using KrTrade.Nt.Core.Collections;
using System;
using System.Collections.Generic;

namespace KrTrade.Nt.Services
{

    public abstract class BaseNinjascriptServiceCollection<TService> : BaseKeyCollection<TService>, IConfigure, IDataLoaded, ITerminated
        where TService : INinjascriptService
    {
        public bool IsConfigure { get;protected set; }
        public bool IsDataLoaded { get; protected set; }

        protected BaseNinjascriptServiceCollection() { }
        protected BaseNinjascriptServiceCollection(IEnumerable<TService> elements) : base(elements) { }
        protected BaseNinjascriptServiceCollection(int capacity) : base(capacity) { }

        #region Implementation

        public void Configure()
        {
            if (_collection == null)
                throw new NullReferenceException($"The service collection is null.");
            if (_collection.Count == 0)
                throw new Exception($"The service collection is empty.");

            IsConfigure = true;
            foreach (var service in _collection)
            {
                service.Configure();
                if (!service.IsConfigure)
                    IsConfigure = false;
            }
        }
        public void DataLoaded()
        {
            if (_collection == null)
                throw new NullReferenceException($"The service collection is null.");
            if (_collection.Count == 0)
                throw new Exception($"The service collection is empty.");

            IsDataLoaded = true;
            foreach (var service in _collection)
            {
                service.DataLoaded();
                if (!service.IsDataLoaded)
                    IsDataLoaded = false;
            }
        }
        public void Terminated()
        {
            if (_collection == null)
                return;
            if (_collection.Count == 0)
            {
                _collection = null; 
                return;
            }

            ForEach(x => x.Terminated());
            _collection.Clear();
            _collection = null;
        }

        #endregion

    }
}
