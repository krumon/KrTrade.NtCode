using KrTrade.Nt.Core.Data;
using KrTrade.Nt.Core.Elements;

namespace KrTrade.Nt.Services.Series
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
