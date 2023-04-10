using NinjaTrader.Core.FloatingPoint;
using NinjaTrader.NinjaScript;
using System;
using Nt.Core.Data;

namespace ConsoleApp
{
    public class BarsCache : Cache<TradingBar>
    {

        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        public BarsCache(NinjaScriptBase ninjascript, int capacity, int period = 0) : base(ninjascript,capacity, period)
        {
        }

        #endregion

        #region Implementations methods

        public override void OnBarUpdated()
        {
            Add(CreateCacheElement());
        }

        public override TradingBar CreateCacheElement()
        {
            return new TradingBar(
                Idx, 
                ninjascript.Open[Displacement], 
                ninjascript.High[Displacement], 
                ninjascript.Low[Displacement],
                ninjascript.Close[Displacement], 
                (long)ninjascript.Volume[Displacement], 
                ninjascript.Time[Displacement]);
        }

        #endregion

        #region Public global methods

        public bool HasPattern(ChartPattern pattern, int strength = 0, int instance = 1, int barsAgo = 0, CacheSearchMode searchMode = CacheSearchMode.Current)
        {
            return GetCacheIdx(pattern, strength, instance, barsAgo, searchMode) != -1;
        }

        public BaseElement GetPattern(ChartPattern pattern, int strength = 0, int instance = 1, int barsAgo = 0, CacheSearchMode searchMode = CacheSearchMode.Current)
        {
            return GetCachePattern(pattern, strength, instance, barsAgo, searchMode);
        }

        public int GetPatternBarsAgo(ChartPattern pattern, int strength = 0, int instance = 1, int barsAgo = 0, CacheSearchMode searchMode = CacheSearchMode.Current)
        {
            return (CurrentBar - GetPattern(pattern, strength, instance, barsAgo, searchMode)?.Idx) ?? -1;
        }

        public override string ToString()
        {
            string text = "Bars Cache = [ ";
            for (int i = 0; i < Count; i++)
                text += (this[i].Close).ToString("N2") + " ";
            text += "]";
            return text;
        }

        public string ToString(BaseBar priceType)
        {
            string text = "Bars Cache = [ ";
            for (int i = 0; i < Count; i++)
            {
                switch (priceType)
                {
                    case (BaseBar.Open):
                        text += (this[i].Close).ToString("N2") + " ";
                        break;
                    case (BaseBar.High):
                        text += (this[i].High).ToString("N2") + " ";
                        break;
                    case (BaseBar.Low):
                        text += (this[i].Low).ToString("N2") + " ";
                        break;
                    default:
                        text += (this[i].Close).ToString("N2") + " ";
                        break;
                }
            }
            text += "]";
            return text;
        }

        #endregion

        #region Swing methods

        public bool HasSwing(SwingType swingType, int strength, int instance = 1, int barsAgo = 0, CacheSearchMode searchMode = CacheSearchMode.Current)
        {
            return GetCacheIdxOfSwingPattern(swingType, strength, instance, barsAgo, searchMode) != -1;
        }

        public bool HasSwing(int strength, int instance = 1, int barsAgo = 0, CacheSearchMode searchMode = CacheSearchMode.Current)
        {
            return GetCacheIdxOfSwingPattern(SwingType.Indifferent, strength, instance, barsAgo, searchMode) != -1;
        }

        public SwingPoint GetSwing (SwingType swingType, int strength, int instance = 1, int barsAgo = 0, CacheSearchMode searchMode = CacheSearchMode.Current)
        {
            //if (this.HasSwingPoint(swingType, strength, instance, barsAgo, finderType))
            //    return new SwingPoint(swingType, this.GetRange(Count - barsAgo - (strength * 2 + 1), Count - barsAgo - 1));

            int idx = GetCacheIdxOfSwingPattern(swingType, strength, instance, barsAgo, searchMode);
            if (idx != -1)
                return new SwingPoint(GetRange(idx - strength, idx + strength));

            return null;
        }

        public SwingPoint GetSwing (int strength, int instance = 1, int barsAgo = 0, CacheSearchMode searchMode = CacheSearchMode.Current)
        {
            return GetSwing(SwingType.Indifferent, strength, instance, barsAgo, searchMode);
        }

        public int GetSwingBarsAgo(SwingType swingType, int strength, int instance = 1, int barsAgo = 0, CacheSearchMode searchMode = CacheSearchMode.Current)
        {
            int? idx = GetSwing(swingType, strength, instance, barsAgo, searchMode)?.Idx;
            return idx != null ? CurrentBar - (int)idx : -1;
        }

        /// <summary>
		/// Returns the number of bars ago a swing occurred. Returns a value of -1 if a swing low is not found within the look back period.
		/// </summary>
		/// <param name="barsAgo"></param>
		/// <param name="instance"></param>
		/// <param name="lookBackPeriod"></param>
		/// <returns></returns>
		public int GetSwingBarsAgo(int strength, int instance = 1, int barsAgo = 0, CacheSearchMode searchMode = CacheSearchMode.Current)
        {
            return GetSwingBarsAgo(SwingType.Indifferent, strength, instance, barsAgo, searchMode);
        }

        #endregion

        #region Private methods

        private int GetCacheIdx(ChartPattern pattern, int strength = 0, int instance = 1, int barsAgo = 0, CacheSearchMode searchMode = CacheSearchMode.Current)
        {
            if (instance < 1)
                throw new Exception(string.Format("Instance should be greater or equal than one.", GetType().Name, instance));
            if (barsAgo < 0)
                throw new Exception(string.Format("BarsAgo should be greater or equal than zero.", GetType().Name, barsAgo));
            if (Count != 0 && barsAgo >= Count)
                throw new Exception(string.Format("BarsAgo is out of range. Bars Ago can not be greater or equal than cache count.", GetType().Name, (Count - 1), barsAgo));

            switch (pattern)
            {
                case (ChartPattern.SwingHigh):
                    return GetCacheIdxOfSwingPattern(SwingType.High, strength, instance, barsAgo, searchMode);
                case (ChartPattern.SwingLow):
                    return GetCacheIdxOfSwingPattern(SwingType.Low, strength, instance, barsAgo, searchMode);
                case (ChartPattern.Swing):
                    return GetCacheIdxOfSwingPattern(SwingType.Indifferent, strength, instance, barsAgo, searchMode);
                default:
                    return -1;
            }
        }

        private BaseElement GetCachePattern(ChartPattern pattern, int strength = 0, int instance = 1, int barsAgo = 0, CacheSearchMode searchMode = CacheSearchMode.Current)
        {
            if (instance < 1)
                throw new Exception(string.Format("Instance should be greater or equal than one.", GetType().Name, instance));
            if (barsAgo < 0)
                throw new Exception(string.Format("BarsAgo should be greater or equal than zero.", GetType().Name, barsAgo));
            if (Count != 0 && barsAgo >= Count)
                throw new Exception(string.Format("BarsAgo is out of range. Bars Ago can not be greater or equal than cache count.", GetType().Name, (Count - 1), barsAgo));

            switch (pattern)
            {
                case (ChartPattern.SwingHigh):
                    return GetSwing(SwingType.High, strength, instance, barsAgo, searchMode);
                case (ChartPattern.SwingLow):
                    return GetSwing(SwingType.Low, strength, instance, barsAgo, searchMode);
                case (ChartPattern.Swing):
                    return GetSwing(strength, instance, barsAgo, searchMode);
                default:
                    return null;
            }
        }

        #endregion

        #region Private pattern methods

        private int GetCacheIdxOfSwingPattern(SwingType swingType, int strength, int instance = 1, int barsAgo = 0, CacheSearchMode searchMode = CacheSearchMode.Current)
        {

            if (instance < 1)
                throw new Exception(string.Format("Instance should be greater or equal than one.", GetType().Name, instance));
            if (barsAgo < 0)
                throw new Exception(string.Format("BarsAgo should be greater or equal than zero.", GetType().Name, barsAgo));
            if (Count != 0 && barsAgo >= Count)
                throw new Exception(string.Format("BarsAgo is out of range. Bars Ago can not be greater or equal than cache count.", GetType().Name, (Count - 1), barsAgo));


            int constant = strength * 2 + 1;
            int newCount = Count - barsAgo;

            if (newCount < constant)
                return -1;

            int tempCount;
            double highCandidateValue;
            double lowCandidateValue;
            bool isSwingHigh;
            bool isSwingLow;

            if (searchMode == CacheSearchMode.Current)
            {
                instance = 1;
                newCount = barsAgo + 1;
            }

            if (searchMode == CacheSearchMode.Last)
                instance = 1;


            for (int i = barsAgo; i < newCount; i++)
            {
                isSwingHigh = true;
                isSwingLow = true;
                tempCount = Count - i;

                if (tempCount < constant)
                    break;

                highCandidateValue = this[tempCount - strength - 1].High;
                lowCandidateValue = this[tempCount - strength - 1].Low;
                for (int j = tempCount - constant; j < tempCount - strength - 1; j++)
                {
                    if (this[j].High.ApproxCompare(highCandidateValue) > 0)
                        isSwingHigh = false;
                    if (this[j].Low.ApproxCompare(lowCandidateValue) < 0)
                        isSwingLow = false;
                }

                for (int j = tempCount - strength; j < tempCount; j++)
                {
                    if (this[j].High.ApproxCompare(highCandidateValue) > 0)
                        isSwingHigh = false;
                    if (this[j].Low.ApproxCompare(lowCandidateValue) < 0)
                        isSwingLow = false;
                }

                if (swingType == SwingType.High && isSwingHigh)
                {
                    if (instance == 1)
                        return tempCount - strength - 1;

                    instance--;
                }

                else if (swingType == SwingType.Low && isSwingLow)
                {
                    if (instance == 1)
                        return tempCount - strength - 1;

                    instance--;
                }

                else if (swingType == SwingType.Indifferent && (isSwingHigh || isSwingLow))
                {
                    if (instance == 1)
                        return tempCount - strength - 1;

                    instance--;
                }
            }

            return -1;

        }

        #endregion

    }
}
