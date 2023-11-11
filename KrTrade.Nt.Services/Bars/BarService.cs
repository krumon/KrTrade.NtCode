using KrTrade.Nt.Core.Bars;
using KrTrade.Nt.Core.Extensions;
using KrTrade.Nt.Core.Helpers;
using KrTrade.Nt.Core.Interfaces;
using NinjaTrader.NinjaScript;
using System;

namespace KrTrade.Nt.Services.Bars
{
    /// <summary>
    /// Represents the service of only one bar.
    /// </summary>
    public class BarService : Bar, INeedBarsService, IOnBarUpdateService
    {

        #region Private members

        private readonly NinjaScriptBase _ninjascript;
        private readonly BarsService _barsService;
        private readonly int _barsArrayIdx;

        #endregion

        #region Public properties

        /// <summary>
        /// Indicates if the last bar of the bars in progress is closed.
        /// </summary>
        public bool IsClosed => _barsService.GetIsClosed(_barsArrayIdx);

        /// <summary>
        /// Indicates if the last bar of the bars in progress is removed.
        /// </summary>
        public bool IsRemoved => _barsService.GetIsRemoved(_barsArrayIdx);

        /// <summary>
        /// Indicates if success the first tick in the current bar of the bars in progress.
        /// </summary>
        public bool IsFirstTick => _barsService.GetIsFirstTick(_barsArrayIdx);

        /// <summary>
        /// Indicates if success a new tick in the current bar of the bars in progress.
        /// </summary>
        public bool IsNewTick => _barsService.GetHasNewTick(_barsArrayIdx);

        /// <summary>
        /// Indicates if the price changed in the current bar of the bars in progress.
        /// </summary>
        public bool IsNewPrice => _barsService.GetHasNewPrice(_barsArrayIdx);

        #endregion

        #region Constructors

        /// <summary>
        /// Create <see cref="BarService"/> instance.
        /// </summary>
        /// <param name="ninjascript">The NinjaScript where the service is being executed.</param>
        /// <param name="barsService">The bars service necessary for the service to function correctly.</param>
        /// <param name="barsArrayIdx">The bars array index that corresponds to the bar service.</param>
        /// <exception cref="ArgumentNullException">The NinjaScript or BarsService arguments cannot be null anyone.</exception>
        public BarService(NinjaScriptBase ninjascript, BarsService barsService, int barsArrayIdx)
        {
            _ninjascript = ninjascript ?? throw new ArgumentNullException(nameof(ninjascript));
            _barsService = barsService ?? throw new ArgumentNullException(nameof(barsService));
            _barsArrayIdx = barsArrayIdx;
        }

        #endregion

        #region Implementation methods
        
        /// <inheritdoc/>
        public void OnBarUpdate()
        {
            if (!_ninjascript.State.IsInProgress())
                ThrowHelper.ThrowOnBarUpdateInvalidStateException(_ninjascript.State);

            if (_ninjascript.BarsInProgress < 0 || _ninjascript.CurrentBars[_ninjascript.BarsInProgress] < 0)
                return;

            Idx = GetCurrentBar(_ninjascript.BarsInProgress,0);
            Open = GetOpen(_ninjascript.BarsInProgress, 0);
            High = GetHigh(_ninjascript.BarsInProgress, 0);
            Low = GetLow(_ninjascript.BarsInProgress, 0);
            Close = GetClose(_ninjascript.BarsInProgress, 0);
            Volume = GetVolume(_ninjascript.BarsInProgress, 0);
            Time = GetTime(_ninjascript.BarsInProgress, 0);
        }

        #endregion

        #region Private methods

        private int GetCurrentBar(int barsInProgress, int barsAgo)
        {
            if (barsInProgress < 0 || barsInProgress != _ninjascript.BarsInProgress)
                return -1;
            if (barsAgo < 0 || barsAgo >= _ninjascript.BarsArray[barsInProgress].Count)
                return -1;

            return _ninjascript.CurrentBars[barsInProgress];
        }

        private double GetOpen(int barsInProgress, int barsAgo)
        {
            if (barsInProgress < 0 || barsInProgress != _ninjascript.BarsInProgress)
                return 0.0;
            if (barsAgo < 0 || barsAgo >= _ninjascript.BarsArray[barsInProgress].Count)
                return 0.0;

            return _ninjascript.Opens[barsInProgress][barsAgo];
        }

        private double GetHigh(int barsInProgress, int barsAgo)
        {
            if (barsInProgress < 0 || barsInProgress != _ninjascript.BarsInProgress)
                return double.MinValue;
            if (barsAgo < 0 || barsAgo >= _ninjascript.BarsArray[barsInProgress].Count)
                return double.MinValue;

            return _ninjascript.Highs[barsInProgress][barsAgo];
        }

        private double GetLow(int barsInProgress, int barsAgo)
        {
            if (barsInProgress < 0 || barsInProgress != _ninjascript.BarsInProgress)
                return double.MaxValue;
            if (barsAgo < 0 || barsAgo >= _ninjascript.BarsArray[barsInProgress].Count)
                return double.MaxValue;

            return _ninjascript.Lows[barsInProgress][barsAgo];
        }

        private double GetClose(int barsInProgress, int barsAgo)
        {
            if (barsInProgress < 0 || barsInProgress != _ninjascript.BarsInProgress)
                return 0.0;
            if (barsAgo < 0 || barsAgo >= _ninjascript.BarsArray[barsInProgress].Count)
                return 0.0;

            return _ninjascript.Closes[barsInProgress][barsAgo];
        }

        private double GetVolume(int barsInProgress, int barsAgo)
        {
            if (barsInProgress < 0 || barsInProgress != _ninjascript.BarsInProgress)
                return -1.0;
            if (barsAgo < 0 || barsAgo >= _ninjascript.BarsArray[barsInProgress].Count)
                return -1.0;

            return _ninjascript.Volumes[barsInProgress][barsAgo];
        }

        private DateTime GetTime(int barsInProgress, int barsAgo)
        {
            if (barsInProgress < 0 || barsInProgress != _ninjascript.BarsInProgress)
                return new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Local);
            if (barsAgo < 0 || barsAgo >= _ninjascript.BarsArray[barsInProgress].Count)
                return new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Local);

            return _ninjascript.Times[barsInProgress][barsAgo];
        }

        #endregion

    }
}
