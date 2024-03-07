using KrTrade.Nt.Core.Logging;
using NinjaTrader.Data;

namespace KrTrade.Nt.Core.DataSeries
{
    public class DataSeriesInfo__
    {

        public string Instrument { get; set; }
        public string Expiry { get; set; }
        public BarsPeriodType PeriodType { get; set; }
        public int PeriodValue { get; set; }
        public BarsPeriod BarsPeriod { get; set; }
        public string TradingHoursName { get;set; }

        public void Load(NinjaTrader.Data.Bars bars)
        {
            string[] name = bars.Instrument.MasterInstrument.Name.Split(' ');
            Instrument = name[0];
            Expiry = name.Length >= 1 ? bars.Instrument.Expiry.ToString("yy/MM") : string.Empty;
            BarsPeriod = bars.BarsPeriod;
            PeriodType = BarsPeriod.BarsPeriodType;
            PeriodValue = BarsPeriod.Value;
            TradingHoursName = bars.TradingHours.Name;
        }

        public string ToLogString()
        {
            return "(" + Instrument + "." + PeriodType.ToLogString() + BarsPeriod.Value +")";
        }
    }
}
