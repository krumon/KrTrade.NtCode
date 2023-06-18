using KrTrade.NtCode.Data;
using System;

namespace KrTrade.NtCode.Brokers
{
    public static class BrokerExtensions
    {
        
        public static TradingTime ToDailyBeginTime(this Broker broker)
        {
            switch (broker)
            {
                case (Broker.Ninjatrader):
                    return new TradingTime(new TimeSpan(17,0,0), TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time"));
                default:
                    throw new ArgumentException($"The {broker} converter is not yet implemnted");
            }
        } 
    }
}
