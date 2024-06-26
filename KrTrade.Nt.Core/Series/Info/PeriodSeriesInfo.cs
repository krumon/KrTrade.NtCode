using KrTrade.Nt.Core.Data;

namespace KrTrade.Nt.Core.Series
{

    public class PeriodSeriesInfo : InputSeriesInfo<PeriodSeriesType>, IInputSeriesInfo<PeriodSeriesType>
    {

        /// <summary>
        /// Gets series period.
        /// </summary>
        public int Period { get; set; }

        protected override object[] GetParameters() => new object[] { Period };

    }
}
