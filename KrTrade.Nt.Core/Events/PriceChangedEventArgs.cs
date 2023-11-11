namespace KrTrade.Nt.Core.Events
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
