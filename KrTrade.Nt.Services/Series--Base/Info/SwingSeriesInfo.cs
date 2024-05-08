using KrTrade.Nt.Core.Data;
using KrTrade.Nt.Core.Series;

namespace KrTrade.Nt.Services.Series
{
    public class SwingSeriesInfo : SeriesInfo<SwingSeriesType>, ISeriesInfo<SwingSeriesType>
    {

        /// <summary>
        /// Gets swing left strength.
        /// </summary>
        public int LeftStrength { get; set; }

        /// <summary>
        /// Gets swing right strength.
        /// </summary>
        public int RightStrength { get; set; }

        protected override object[] GetParameters() => new object[] { LeftStrength, RightStrength };

    }
}
