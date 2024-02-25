namespace KrTrade.Nt.Core.Data
{
    public static class MarketDataTypeExtensions
    {

        public static NinjaTrader.Data.MarketDataType ToNtMarketDataType(this MarketDataType marketDataType)
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
        public static MarketDataType ToKrMarketDataType(this NinjaTrader.Data.MarketDataType marketDataType)
        {
            switch (marketDataType)
            {
                case (NinjaTrader.Data.MarketDataType.Last):
                    return MarketDataType.Last;
                case (NinjaTrader.Data.MarketDataType.Ask):
                    return MarketDataType.Ask;
                case (NinjaTrader.Data.MarketDataType.Bid):
                    return MarketDataType.Bid;
                default:
                    throw new System.NotImplementedException(marketDataType.ToString());
            }
        }
    }
}
