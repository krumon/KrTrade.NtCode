using KrTrade.Nt.Core.Bars;
using KrTrade.Nt.Core.Caches;
using KrTrade.Nt.Core.Data;
using NinjaTrader.Core.FloatingPoint;
using NinjaTrader.NinjaScript;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KrTrade.Nt.Services
{
    public class BarsCache : Cache<Bar>, IBarsCache
    {
        protected int BarsIdx { get; private set; }

        #region Public properties

        public double Open => GetBar(0, Count).Open;
        public double High => GetBar(0, Count).High;
        public double Low => GetBar(0, Count).Low;
        public double Close => GetBar(0, Count).Close;
        public double Volume => GetBar(0, Count).Volume;
        public double Range => GetRange(0, Count);

        #endregion

        #region Constructors

        public BarsCache(int capacity) : this(capacity, 0, 0)
        {
        }
        public BarsCache(int capacity, int displacement) : this(capacity, displacement, 0)
        {
        }
        public BarsCache(int capacity, int displacement, int barsInProgress) : base(capacity, displacement)
        {
            BarsIdx = barsInProgress < 0 ? 0 : barsInProgress;
        }

        #endregion

        #region Public methods

        public Bar GetBar(int initialIdx, int numberOfBars)
        {
            IsValidIndex(Displacement + initialIdx, Displacement + initialIdx + numberOfBars);

            Bar bar = new Bar();
            for (int i = Displacement + initialIdx; i < Displacement + initialIdx + numberOfBars; i++)
                bar += this[i];

            return bar;

        }
        public double GetMax(int initialIdx, int numberOfBars, SeriesType seriesType = SeriesType.High)
        {
            IsValidIndex(Displacement + initialIdx, Displacement + initialIdx + numberOfBars);

            double value = double.MinValue;
            for (int i = Displacement + initialIdx; i < Displacement + initialIdx + numberOfBars; i++)
                value = Math.Max(value, GetValue(i,seriesType));

            return value;
        }
        public double GetMin(int initialIdx, int barsBack, SeriesType seriesType = SeriesType.Low)
        {
            IsValidIndex(Displacement + initialIdx, Displacement + initialIdx + barsBack);

            double value = double.MaxValue;
            for (int i = Displacement + initialIdx; i < Displacement + initialIdx + barsBack; i++)
                value = Math.Min(value, GetValue(i, seriesType));

            return value;
        }
        public double GetSum(int initialIdx, int barsBack, SeriesType seriesType = SeriesType.Close)
        {
            IsValidIndex(Displacement + initialIdx, Displacement + initialIdx + barsBack);

            double sum = 0;
            for (int i = Displacement + initialIdx; i < Displacement + initialIdx + barsBack; i++)
                sum += GetValue(i,seriesType);
            
            return sum;
        }
        public double GetAvg(int initialIdx, int numberOfElements, SeriesType seriesType = SeriesType.Close)
        {
            IsValidIndex(Displacement + initialIdx, Displacement + initialIdx + numberOfElements);

            return GetSum(initialIdx, numberOfElements, seriesType) / Count;
        }
        public double GetStdDev(int initialIdx, int numberOfElements, SeriesType seriesType = SeriesType.Close)
        {
            IsValidIndex(Displacement + initialIdx, Displacement + initialIdx + numberOfElements);

            double avg = GetAvg(initialIdx, numberOfElements, seriesType) / Count;
            double sumx2 = 0;
            for (int i = Displacement + initialIdx; i < Displacement + initialIdx + numberOfElements; i++)
                sumx2 += Math.Pow(Math.Abs(GetValue(i,seriesType) - avg),2.0);
            return Math.Sqrt(sumx2 / Count); ;
        }
        public double GetQuartil(int numberOfQuartil, int initialIdx, int numberOfElements, SeriesType seriesType = SeriesType.Close)
        {
            if (numberOfQuartil < 1 || numberOfQuartil > 3)
                throw new Exception("The number of quartil is not valid. The quartil can be 1, 2 or 3.");

            return GetQuartils(initialIdx, numberOfElements, seriesType)[numberOfQuartil];
        }
        public double[] GetQuartils(int initialIdx, int numberOfElements, SeriesType seriesType = SeriesType.Close)
        {
            IsValidIndex(Displacement + initialIdx, Displacement + initialIdx + numberOfElements);
            Bar[] rangeCache = new Bar[numberOfElements];
            int count = 0;
            for (int i = Displacement + initialIdx; i < Displacement + initialIdx + numberOfElements; i++)
            {
                rangeCache[count] = this[i];
                count++;
            }
            IList<Bar> sortedCache = GetSortedList(rangeCache,seriesType);
            double[] quartils = new double[3];
            for (int i = 1; i <= 3; i++)
            {
                double quartil = i * (rangeCache.Length + 1) / 4;
                int idx = (int)quartil;
                double dec = quartil % idx;
                quartils[i] = GetValue(sortedCache,i,seriesType) + (GetValue(sortedCache, i+1, seriesType) - GetValue(sortedCache, i, seriesType)) * dec;
            }
            return quartils;
        }
        public double GetRange(int initialIdx, int barsBack)
        {
            return GetMax(initialIdx, barsBack, SeriesType.High) - GetMin(initialIdx, barsBack, SeriesType.Low);
        }
        public double GetSwingHigh(int initialIdx, int strength)
        {
            int numOfBars = (strength * 2) + 1;
            IsValidIndex(Displacement + initialIdx, Displacement + initialIdx + numOfBars);

            bool isSwingHigh = true;
            double candidateValue = this[Displacement + strength].High;
            for (int i = Displacement + initialIdx + numOfBars - 1; i > Displacement + initialIdx + strength; i--)
                if (candidateValue.ApproxCompare(this[i].High) <= 0.0)
                {
                    isSwingHigh = false;
                    break;
                }
            for (int i = Displacement + initialIdx + strength - 1; i >= Displacement + initialIdx; i--)
                if (candidateValue.ApproxCompare(this[i].High) < 0.0)
                {
                    isSwingHigh = false;
                    break;
                }

            return isSwingHigh ? candidateValue : -1;
        }
        public double GetSwingLow(int initialIdx, int strength)
        {
            int numOfBars = (strength * 2) + 1;
            IsValidIndex(Displacement + initialIdx, Displacement + initialIdx + numOfBars);

            bool isSwingLow = true;
            double candidateValue = this[Displacement + strength].Low;
            for (int i = Displacement + initialIdx + numOfBars - 1; i > Displacement + initialIdx + strength; i--)
                if (candidateValue.ApproxCompare(this[i].Low) >= 0.0)
                {
                    isSwingLow = false;
                    break;
                }
            for (int i = Displacement + initialIdx + strength - 1; i >= Displacement + initialIdx; i--)
                if (candidateValue.ApproxCompare(this[i].Low) > 0.0)
                {
                    isSwingLow = false;
                    break;
                }

            return isSwingLow ? candidateValue : -1;
        }


        #endregion

        #region Implementation

        protected override Bar GetCandidateValue(NinjaScriptBase ninjascript = null)
        {
            Bar bar = new Bar();
            bar.Set(ninjascript, 0, BarsIdx);
            return bar;
        }
        protected override Bar GetCandidateValue(NinjaTrader.Data.MarketDataEventArgs args)
        {
            Bar bar = new Bar();
            bar.Set(args);
            return bar;
        }
        protected override void UpdateCurrentValue(ref Bar currentValue, NinjaScriptBase ninjascript = null) => currentValue.Set(ninjascript, 0, BarsIdx);
        protected override void UpdateCurrentValue(ref Bar currentValue, NinjaTrader.Data.MarketDataEventArgs args) => currentValue.Set(args);
        protected override bool IsValidValue(Bar candidateValue) => candidateValue != null;

        #endregion

        #region Private methods

        private double GetValue(int idx, SeriesType seriesType)
        {
            if (!IsValidIndex(idx))
                throw new ArgumentOutOfRangeException(nameof(idx));
            if (seriesType == SeriesType.Close)
                return this[idx].Close;
            else if (seriesType == SeriesType.High)
                return this[idx].High;
            else if (seriesType == SeriesType.Low)
                return this[idx].Low;
            else if (seriesType == SeriesType.Open)
                return this[idx].Open;
            else if (seriesType == SeriesType.Volume)
                return this[idx].Volume;
            else if (seriesType == SeriesType.Median)
                return this[idx].Median;
            else if (seriesType == SeriesType.Typical)
                return this[idx].Typical;
            else
                throw new NotImplementedException(nameof(seriesType));
        }
        private double GetValue(IList<Bar> cache, int idx, SeriesType seriesType)
        {
            if (cache == null)
                throw new ArgumentNullException(nameof(cache));
            if (cache.Count == 0)
                throw new ArgumentException("The 'IList<Bar>' parameter is empty. Count is zero.");
            if (!IsValidIndex(idx))
                throw new ArgumentOutOfRangeException(nameof(idx));

            if (seriesType == SeriesType.Close)
                return cache[idx].Close;
            else if (seriesType == SeriesType.High)
                return cache[idx].High;
            else if (seriesType == SeriesType.Low)
                return cache[idx].Low;
            else if (seriesType == SeriesType.Open)
                return cache[idx].Open;
            else if (seriesType == SeriesType.Volume)
                return cache[idx].Volume;
            else if (seriesType == SeriesType.Median)
                return cache[idx].Median;
            else if (seriesType == SeriesType.Typical)
                return cache[idx].Typical;
            else
                throw new NotImplementedException(nameof(seriesType));
        }
        private IList<Bar> GetSortedList(IList<Bar> disortedList, SeriesType seriesType)
        {
            if (disortedList == null)
                throw new ArgumentNullException(nameof(disortedList));
            if (disortedList.Count == 0)
                throw new ArgumentException("The 'IList<Bar>' parameter is empty. Count is zero.");

            if (seriesType == SeriesType.Close)
                return disortedList.OrderBy(x => x.Close).ToList();
            else if (seriesType == SeriesType.High)
                return disortedList.OrderBy(x => x.High).ToList();
            else if (seriesType == SeriesType.Low)
                return disortedList.OrderBy(x => x.Low).ToList();
            else if (seriesType == SeriesType.Open)
                return disortedList.OrderBy(x => x.Open).ToList();
            else if (seriesType == SeriesType.Volume)
                return disortedList.OrderBy(x => x.Volume).ToList();
            else if (seriesType == SeriesType.Median)
                return disortedList.OrderBy(x => x.Median).ToList();
            else if (seriesType == SeriesType.Typical)
                return disortedList.OrderBy(x => x.Typical).ToList();
            else
                throw new NotImplementedException(nameof(seriesType));
        }

        #endregion
    }
}
