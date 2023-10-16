﻿using KrTrade.Nt.Core;
using KrTrade.Nt.Core.Logging;

namespace KrTrade.Nt.Services
{
    public class PrintLoggerOptions
    {
        public LogLevel LogLevel { get; set; } = LogLevel.Information;
        public NinjaScriptState NsLogLevel { get; set; } = NinjaScriptState.Realtime;
        public PriceState PriceLogLevel { get; set; } = PriceState.Bar;
        public int Capacity { get; set; } = 100;
    }
}
