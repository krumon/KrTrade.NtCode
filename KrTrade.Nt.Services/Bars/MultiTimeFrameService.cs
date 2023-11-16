using KrTrade.Nt.Core.Interfaces;
using KrTrade.Nt.Core.Print;
using NinjaTrader.NinjaScript;
using System;
using System.Collections;
using System.Collections.Generic;

namespace KrTrade.Nt.Services.Bars
{
    public class MultiTimeFrameService : BaseService,
        IConfigureService,
        IDataLoadedService,
        IOnBarUpdateService
    {

        #region Private members

        private bool _isInitialized;
        private bool _isConfigured;

        private List<BarsService> _barsServices;

        #endregion

        #region Public properties

        /// <summary>
        /// True, if service is configured, otherwise false.
        /// For the service to be configured, the 'Configure' and 'DataLoaded' methods must be executed.
        /// </summary>
        public bool IsConfigured => _isConfigured;

        /// <summary>
        /// Gets the last bar of the bars in progress.
        /// </summary>
        public LastBarService LastBar => GetLastBar(_ninjascript.BarsInProgress);

        /// <summary>
        /// Gets the last bar of the bars in progress.
        /// </summary>
        public LastBarService CurrentBar => GetLastBar(_ninjascript.BarsInProgress);

        /// <summary>
        /// Gets the BarsService of a sepecific index.
        /// </summary>
        /// <param name="index">The specific index.</param>
        /// <returns><see cref="BarsService"/>.</returns>
        /// <exception cref="Exception"></exception>
        public BarsService this[int index]
        {
            get
            {
                if (_barsServices == null)
                    throw new ArgumentNullException("'MultiTimeFrameService' exception. The value is null");
                if (index < 0 || index >= _barsServices.Count)
                    throw new Exception(string.Format("'MultiTimeFrameService' exception. The index to access to the cache is out of range. The index value is {0}", index));
                return _barsServices[index];
            }
        }

        /// <summary>
        /// The number of BarsServices that exists.
        /// </summary>
        public int Count => _barsServices == null ? -1 : _barsServices.Count;

        #endregion

        #region Constructors

        /// <summary>
        /// Create <see cref="BaseBars"/> new instance.
        /// </summary>
        /// <param name="ninjascript">The NinjaScript to inject in the service.</param>
        /// <param name="printSvc">The <see cref="BasePrint"/> service injected.</param>
        public MultiTimeFrameService(NinjaScriptBase ninjascript, PrintService printService) : base(ninjascript, printService)
        {
            _barsServices = new List<BarsService>();
        }

        #endregion

        #region Public methods

        /// <summary>
        /// A method which is called we want configure the service.
        /// </summary>
        public void Configure()
        {
            if (_isConfigured || _isInitialized)
                return;

            if (!IsInConfigurationStates())
                throw new Exception($"The configuration methods of {Name}, must be executed when 'NinjaScript.State' is 'Configure' or 'DataLoaded'.");

            if (_barsServices == null)
                _barsServices = new List<BarsService>();

            if (_barsServices.Count == 0)
                _barsServices.Add(new BarsService(_ninjascript));

            for (int i=0; i< _barsServices.Count; i++)
                _barsServices[i].Configure();

            _isInitialized = true;

        }

        /// <summary>
        /// A method which is called we want configure the service when ninjascript data is loaded.
        /// </summary>
        public void DataLoaded()
        {
            if (_isConfigured)
                return;

            if (_ninjascript.State != State.DataLoaded)
                throw new Exception($"The configuration methods of {Name}, must be executed when 'NinjaScript.State' is 'Configure' or 'DataLoaded'.");

            if (!_isInitialized)
                Configure();

            for (int i = 0; i < _barsServices.Count; i++)
                _barsServices[i].DataLoaded();

            _isConfigured = true;
        }

        /// <summary>
        /// An event driven method which is called whenever a bar is updated.
        /// </summary>
        public void OnBarUpdate()
        {
            if (!IsInRunningStates())
                return;

            if (!_isConfigured)
            {
                _printService.Write("The services must be configured in the 'NinjaScript.OnStateChanged' method, when the 'State==State.Configure' and 'State==State.DataLoaded'.");
                return;
            }

            if (_ninjascript.BarsInProgress < 0 || _ninjascript.CurrentBar < 0)
                return;

            if (_barsServices != null && _barsServices.Count > 0)
                for (int i = 0; i < _barsServices.Count; i++)
                    _barsServices[i].OnBarUpdate();
        }

        /// <summary>
        /// Prints the states of the bars.
        /// </summary>
        public void PrintState()
        {
            _barsServices[_ninjascript.BarsInProgress].PrintState();
        }

        ///// <summary>
        ///// Adds BarsSeries to the milti TimeFrame service.
        ///// </summary>
        ///// <param name="timeFrame">The time frame of the new data series.</param>
        //public void AddDataSeries(TimeFrame timeFrame)
        //{
        //    BarsService barsService = new BarsService(_ninjascript, timeFrame);
        //    AddDataSeries(barsService);
        //}

        ///// <summary>
        ///// Adds BarsSeries to the milti TimeFrame service.
        ///// </summary>
        ///// <param name="instrumentCode">Tue instrument code of the new data series.</param>
        ///// <param name="timeFrame">The time frame of the new data series.</param>
        //public void AddDataSeries(InstrumentCode instrumentCode, TimeFrame timeFrame)
        //{
        //    BarsService barsService = new BarsService(_ninjascript, instrumentCode ,timeFrame);
        //    AddDataSeries(barsService);
        //}

        /// <summary>
        /// Adds BarsSeries to the milti TimeFrame service.
        /// </summary>
        /// <param name="barsService">The new data series.</param>
        /// <exception cref="ArgumentNullException"></exception>
        internal void AddDataSeries(BarsService barsService)
        {
            if (barsService == null)
                throw new ArgumentNullException("The BarsService to add, cannot be null.");

            if (_barsServices == null)
                _barsServices = new List<BarsService>();

            _barsServices.Add(barsService);
        }

        #endregion

        #region Private methods

        internal bool GetIsClosed() => GetIsClosed(_ninjascript.BarsInProgress);
        internal bool GetIsClosed(int barsInProgress) => GetCurrentBar(barsInProgress).IsClosed;
        internal bool GetHasNewTick() => GetHasNewTick(_ninjascript.BarsInProgress);
        internal bool GetHasNewTick(int barsInProgress) => GetCurrentBar(barsInProgress).IsNewTick;
        internal bool GetIsRemoved() => GetIsRemoved(_ninjascript.BarsInProgress);
        internal bool GetIsRemoved(int barsInProgress) => GetCurrentBar(barsInProgress).IsRemoved;
        internal bool GetHasNewPrice() => GetHasNewPrice(_ninjascript.BarsInProgress);
        internal bool GetHasNewPrice(int barsInProgress) => GetCurrentBar(barsInProgress).IsNewPrice;
        internal bool GetIsFirstTick() => GetIsFirstTick(_ninjascript.BarsInProgress);
        internal bool GetIsFirstTick(int barsInProgress) => GetCurrentBar(barsInProgress).IsFirstTick;

        internal LastBarService GetCurrentBar() => GetCurrentBar(_ninjascript.BarsInProgress);
        internal LastBarService GetCurrentBar(int barsInProgress)
        {
            if (IsOutOfRange(_barsServices, barsInProgress))
                return null;

            return _barsServices[barsInProgress].CurrentBar;
        }
        internal LastBarService GetLastBar() => GetLastBar(_ninjascript.BarsInProgress);
        internal LastBarService GetLastBar(int barsInProgress)
        {
            if (IsOutOfRange(_barsServices, barsInProgress))
                return null;

            return _barsServices[barsInProgress].LastBar;
        }

        private bool IsOutOfRange(IList list, int index)
        {
            if (list == null)
                throw new ArgumentNullException("BarsServices collection is null.");

            if (list.Count == 0)
                throw new Exception("BarsServices collection is empty.");

            if (index < 0 || index >= list.Count)
                throw new ArgumentOutOfRangeException("The index used to serach in the BarsServices Collection is out of range.");
            return false;
        }

        #endregion

    }
}
