using KrTrade.Nt.Core.Data;

namespace KrTrade.Nt.Core.Series
{
    public class SwingSeriesInfo : InputSeriesInfo<StrengthSeriesType>, IInputSeriesInfo<StrengthSeriesType>
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
