namespace KrTrade.Nt.Core.Data
{

    public class SeriesInfo : BaseSeriesInfo
    {

        /// <summary>
        /// Gets series period.
        /// </summary>
        public int Period { get; set; }

        protected override object[] GetParameters() => new object[] { Period };

    }
}
