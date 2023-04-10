using NinjaTrader.NinjaScript;
using ConsoleApp;
using ConsoleApp;
using System;

namespace ConsoleApp
{
    public class SwingPointsCache : Cache<SwingPoint>
    {

        #region Private members

        private readonly int minSwingStrength;
        private readonly int maxSwingStrength;
        //private readonly BarsCache barsCache;

        #endregion

        #region Properties

        public BarsCache BarsCache { get; private set; }
        
        #endregion

        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        public SwingPointsCache(NinjaScriptBase ninjascript, int capacity, int period, int minSwingStrength, int maxSwingStrength) : base(ninjascript,capacity, period)
        {
            this.minSwingStrength = minSwingStrength < 1 ? 1 : minSwingStrength;
            this.maxSwingStrength = maxSwingStrength < this.minSwingStrength ? this.minSwingStrength : maxSwingStrength;
            BarsCache = new BarsCache(ninjascript,capacity, period);
            BarsCache.ElementAdded += BarsCache_ElementAdded;
        }

        #endregion

        #region Implementation methods

        public override void OnBarUpdated()
        {
            throw new NotImplementedException();
        }

        public override SwingPoint CreateCacheElement()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Public methods

        public override void Dispose()
        {
            BarsCache.ElementAdded -= BarsCache_ElementAdded;
            BarsCache.Dispose();
            base.Dispose();
        }

        #endregion

        #region Private methods

        private void BarsCache_ElementAdded(TradingBar obj)
        {
            SwingPoint swingPoint = BarsCache.GetSwing(minSwingStrength);

            if (swingPoint != null)
            {
                this.Add(swingPoint);
                return;
            }
        }

        #endregion

    }
}
