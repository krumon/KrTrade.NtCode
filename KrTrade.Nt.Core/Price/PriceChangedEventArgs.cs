namespace KrTrade.Nt.Core
{
    public class PriceChangedEventArgs
    {
        public double LastPrice { get;set; }
        public double CurrentPrice { get;set; }
        public double Gap => CurrentPrice - LastPrice;

        public PriceChangedEventArgs(double lastPrice, double currentPrice)
        {
            LastPrice = lastPrice;
            CurrentPrice = currentPrice;
        }
    }
}
