namespace KrTrade.Nt.Scripts.NinjatraderObjects.Design
{
    public class DesignChartBarsProperties : ChartBarsProperties
    {
        public DesignChartBarsProperties() : base()
        {
        }

        protected override void Configure()
        {
            TradingHoursName = "DesignTradingHoursName";
            InstrumentName = "DesignInstrumentName";
            //BarsPeriod = new BarsPeriod
            //{
            //    PeriodType = PeriodType.Minute,
            //    PeriodValue = 1,
            //    MarketDataType = MarketDataType.Last
            //};
            IsConfigured = true;
        }
    }
}
