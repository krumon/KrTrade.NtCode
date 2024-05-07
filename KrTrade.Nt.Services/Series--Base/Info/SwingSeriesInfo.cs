using KrTrade.Nt.Core.Series;

namespace KrTrade.Nt.Services.Series
{
    public class SwingSeriesInfo : SeriesInfo
    {

        /// <summary>
        /// Gets swing left strength.
        /// </summary>
        public int LeftStrength { get; internal set; }

        /// <summary>
        /// Gets swing right strength.
        /// </summary>
        public int RightStrength { get; internal set; }

        protected override object[] GetParameters() => new object[] { LeftStrength, RightStrength };

    }
}
