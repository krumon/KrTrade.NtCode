using KrTrade.Nt.Core.Series;

namespace KrTrade.Nt.Services.Series
{

    public class PeriodSeriesInfo : SeriesInfo<PeriodSeriesType>, ISeriesInfo<PeriodSeriesType>
    {

        /// <summary>
        /// Gets series period.
        /// </summary>
        public int Period { get; set; }

        protected override object[] GetParameters() => new object[] { Period };

    }
}
