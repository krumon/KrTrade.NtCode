using KrTrade.Nt.Core.Collections;
using KrTrade.Nt.Core.Elements;
using KrTrade.Nt.Services.Series;
using System;
using System.Collections.Generic;

namespace KrTrade.Nt.Services
{

    public abstract class BaseSeriesCollection<TService> : BaseKeyCollection<TService,IElementInfo>, IConfigureSeries, IDataLoadedSeries, ITerminated
    where TService : ISeries, IKeyCollectionItem<IElementInfo>
    {

        protected BaseSeriesCollection() { }
        protected BaseSeriesCollection(IEnumerable<TService> elements) : base(elements) { }
        protected BaseSeriesCollection(int capacity) : base(capacity) { }

        #region Implementation

        public void Configure(IBarsService barsService)
        {
            if (_collection == null)
                throw new NullReferenceException($"The service collection is null.");
            if (_collection.Count == 0)
                throw new Exception($"The service collection is empty.");

            foreach (var series in _collection)
            {
                series.Configure(barsService);
            }
        }
        public void DataLoaded(IBarsService barsService)
        {
            if (_collection == null)
                throw new NullReferenceException($"The service collection is null.");
            if (_collection.Count == 0)
                throw new Exception($"The service collection is empty.");

            foreach (var series in _collection)
            {
                series.DataLoaded(barsService);
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

            ForEach(x => x.Dispose());
            _collection.Clear();
            _collection = null;
        }

        #endregion

    }
}
