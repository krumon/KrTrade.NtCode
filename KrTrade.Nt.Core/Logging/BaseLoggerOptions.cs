﻿using KrTrade.Nt.Core.Bars;
using KrTrade.Nt.Core.Options;

namespace KrTrade.Nt.Core.Logging
{
    public abstract class BaseLoggerOptions<TFormatter> : ServiceOptions, ILoggerOptions<TFormatter>
        where TFormatter : IFormatter, new()
    {

        /// <summary>
        /// Represents the minimum log level. 0:Trace, 1:Debug, 2:Information, 3:Warning, 4:Error, 5:Critical, 6:None
        /// </summary>
        public LogLevel LogLevel { get; set; }

        /// <summary>
        /// The minimum <see cref="Data.NinjascriptLogLevel"/> to be logged. 0:Historical, 1:Configuration, 2:Realtime
        /// Historical level logs in all states.
        /// Configuration level not logs in Historical state.
        /// Realtime logs only in Realtime state.
        /// </summary>
        public NinjascriptLogLevel NinjascriptLogLevel { get; set; }

        /// <summary>
        /// The minimum <see cref="BarsLogLevel"/> to be logged. 0:Tick, 1:PriceChanged, 2:BarClosed, 3:None
        /// </summary>
        public BarsLogLevel BarsLogLevel { get; set; }

        /// <summary>
        /// gets the format to logging in any environment.
        /// </summary>
        public TFormatter Formatter { get; protected set; }

        protected BaseLoggerOptions() 
        {
            Formatter = new TFormatter();
        }
    }
}
