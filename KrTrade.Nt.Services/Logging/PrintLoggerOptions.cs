using KrTrade.Nt.Core.Data;

namespace KrTrade.Nt.Services
{
    public class PrintLoggerOptions
    {
        public LogLevel LogLevel { get; set; } = LogLevel.Information;
        public NsLogLevel NsLogLevel { get; set; } = NsLogLevel.Realtime;
        public PriceLogLevel PriceLogLevel { get; set; } = PriceLogLevel.BarUpdate;
        public int Capacity { get; set; } = 100;
    }
}
