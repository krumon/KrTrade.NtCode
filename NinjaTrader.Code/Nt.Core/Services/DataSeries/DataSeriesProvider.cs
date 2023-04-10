using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Nt.Core.Services
{
    public class DataSeriesProvider : IServiceProvider
    {

        #region Private members

        private DataSeriesDescriptor[] _descriptors;
        private ConcurrentDictionary<object, DataSeriesService> _createdServices;

        #endregion

        #region Public properties

        /// <inheritdoc/>
        //public OptionalServiceType Key { get; private set; }

        #endregion

        #region Constructors

        public DataSeriesProvider(ICollection<DataSeriesDescriptor> descriptors, DataSeriesOptions options)
        {
            if (descriptors == null)
                throw new ArgumentNullException(nameof(descriptors));
            if (descriptors.Count == 0)
                throw new ArgumentException("Descriptors count cannot be 0");

            _descriptors = new DataSeriesDescriptor[descriptors.Count];
            descriptors.CopyTo(_descriptors, 0);
        }

        #endregion

        #region Public methods

        public object GetService(Type serviceType)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetServices<T>()
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
