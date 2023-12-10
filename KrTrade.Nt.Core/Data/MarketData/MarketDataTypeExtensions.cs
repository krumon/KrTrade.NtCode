namespace KrTrade.Nt.Core.Data
{
    public static class MarketDataTypeExtensions
    {

        public static NinjaTrader.Data.MarketDataType ToNtMarketDataType(this Core.Data.MarketDataType marketDataType)
        {
            switch (marketDataType)
            {
                case (MarketDataType.Last):
                    return NinjaTrader.Data.MarketDataType.Last;
                case (MarketDataType.Ask):
                    return NinjaTrader.Data.MarketDataType.Ask;
                case (MarketDataType.Bid):
                    return NinjaTrader.Data.MarketDataType.Bid;
                default:
                    throw new System.NotImplementedException(marketDataType.ToString());
            }
        }
    }
}
