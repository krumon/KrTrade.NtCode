namespace KrTrade.Nt.Core.Series
{

    public class PeriodSeriesInfo : BaseSeriesInfo
    {

        /// <summary>
        /// Gets series period.
        /// </summary>
        public int Period { get; set; }

        protected override object[] GetParameters() => new object[] { Period };

    }
}
