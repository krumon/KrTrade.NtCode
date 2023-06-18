using System;
using System.Collections.Generic;

namespace KrTrade.NtCode.Services
{
    /// <summary>
    /// Builds data series service objects.
    /// </summary>
    public class DataSeriesBuilder
    {

        #region Private members

        //private readonly DataSeriesCollection _descriptors = new DataSeriesCollection();
        private readonly DataSeriesOptions _options = new DataSeriesOptions();
        private DataSeriesProvider _dataSeriesProvider;

        private List<Action<DataSeriesOptions>> _dataSeriesOptionsActions;
        //private List<Action<DataSeriesCollection>> _dataSeriesServicesActions;

        private bool _isBuild;

        #endregion

        #region Constructors

        /// <summary>
        /// Create <see cref="DataSeriesBuilder"/> default instance.
        /// </summary>
        public DataSeriesBuilder()
        {
        }

        #endregion

        #region Implementation methods

        public DataSeriesProvider Build()
        {
            // The trading session can be only once time created.
            if (_isBuild)
                return _dataSeriesProvider;

            CreateDataSeriesOptions();
            //CreateDataSeriesDescriptors();

            //_dataSeriesProvider = new DataSeriesProvider(_descriptors, _dataSeriesOptionsActions == null ? _options : _options);

            // Sets the flag to indicate the instrument provider is created.
            _isBuild = true;

            return _dataSeriesProvider;
        }

        public DataSeriesBuilder ConfigureServiceOptions(Action<DataSeriesOptions> configureOptionsDelegate)
        {
            if (_dataSeriesOptionsActions == null)
                _dataSeriesOptionsActions = new List<Action<DataSeriesOptions>>();
            _dataSeriesOptionsActions.Add(configureOptionsDelegate ?? throw new ArgumentNullException(nameof(configureOptionsDelegate)));
            return this;
        }

        //public DataSeriesBuilder ConfigureServices(Action<DataSeriesCollection> configureServiceDelegate)
        //{
        //    if (_dataSeriesServicesActions == null)
        //        _dataSeriesServicesActions = new List<Action<DataSeriesCollection>>();
        //    _dataSeriesServicesActions.Add(configureServiceDelegate ?? throw new ArgumentNullException(nameof(configureServiceDelegate)));
        //    return this;
        //}

        #endregion

        #region Public Methods


        #endregion

        #region Private methods

        private void CreateDataSeriesOptions()
        {
            if (_dataSeriesOptionsActions != null)
            {
                foreach (Action<DataSeriesOptions> action in _dataSeriesOptionsActions)
                    action(_options);
            }
        }

        //private void CreateDataSeriesDescriptors()
        //{
        //    if (_dataSeriesServicesActions != null)
        //    {
        //        foreach (Action<DataSeriesCollection> action in _dataSeriesServicesActions)
        //            action(_descriptors);
        //    }
        //}

        #endregion

    }
}
