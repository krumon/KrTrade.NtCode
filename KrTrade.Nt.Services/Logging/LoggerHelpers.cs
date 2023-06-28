using KrTrade.Nt.Core.Data;
using System;

namespace KrTrade.Nt.Services
{
    public static class LoggerHelpers
    {
        /// <summary>
        /// Converts the <see cref="LogLevel"/> to short string.
        /// </summary>
        /// <param name="logLevel">The log level.</param>
        /// <returns>The short string that represents the <see cref="LogLevel"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The type of log level is out of range.</exception>
        public static string ToLogText(this LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Trace:        return "trce";
                case LogLevel.Debug:        return "dbug";
                case LogLevel.Information:  return "info";
                case LogLevel.Warning:      return "warn";
                case LogLevel.Error:        return "fail";
                case LogLevel.Critical:     return "crit";
                default: throw new ArgumentOutOfRangeException(nameof(logLevel));
            }
        }

        /// <summary>
        /// Converts the <see cref="NsEvent"/> to short string.
        /// </summary>
        /// <param name="ninjascriptEvent">The ninjascript event.</param>
        /// <returns>The short string that represents the <see cref="NsEvent"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The type of ninjascript event is out of range.</exception>
        public static string ToLogText(this NsEvent ninjascriptEvent)
        {
            switch (ninjascriptEvent)
            {
                case NsEvent.Active:                return "ACTIVE___";
                case NsEvent.BarClosed:             return "BARCLOSED";
                case NsEvent.BarUpdate:             return "BARUPDATE";
                case NsEvent.Configure:             return "CONFIGURE";
                case NsEvent.ConnectionStatusUpdate:return "CNECTUPDTE";
                case NsEvent.DataLoaded:            return "DTALOADED";
                case NsEvent.FirstTick:             return "FIRSTTICK";
                case NsEvent.FundamentalData:       return "FNDTLDATA";
                case NsEvent.Historical:            return "HSTORICAL";
                case NsEvent.MarketData:            return "MRKETDATA";
                case NsEvent.MarketDepth:           return "MKETDEPTH";
                case NsEvent.PriceChanged:          return "PRCHANGED";
                case NsEvent.Realtime:              return "REAL_TIME";
                case NsEvent.Render:                return "RENDER___";
                case NsEvent.SetDefaults:           return "SETDFAULT";
                case NsEvent.Terminated:            return "TRMINATED";
                case NsEvent.Tick:                  return "BARS_TICK";
                case NsEvent.Transition:            return "TRNSITION";
                default: throw new ArgumentOutOfRangeException(nameof(ninjascriptEvent));
            }
        }

        /// <summary>
        /// Converts the <see cref="NsName"/> to short string.
        /// </summary>
        /// <param name="ninjascriptName">The ninjascript name.</param>
        /// <returns>The short string that represents the <see cref="NsName"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The type of ninjascript name is out of range.</exception>
        public static string ToLogText(this NsName ninjascriptName)
        {
            switch (ninjascriptName)
            {
                case NsName.Ninjascript:        return "NJSCRPT";
                case NsName.Drawing:            return "DRAWING";
                case NsName.Plot:               return "PLOTTER";
                case NsName.HighPriceCache:     return "CACHEHI";
                case NsName.LowPriceCache:      return "CACHELW";
                case NsName.PriceCache:         return "CACHEPR";
                case NsName.Print:              return "PRINT__";
                case NsName.Swing:              return "SWING__";
                default: throw new ArgumentOutOfRangeException(nameof(ninjascriptName));
            }
        }

        /// <summary>
        /// Converts the <see cref="NsType"/> to short string.
        /// </summary>
        /// <param name="ninjascriptType">The ninjascript type.</param>
        /// <returns>The short string that represents the <see cref="NsType"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The type of ninjascript is out of range.</exception>
        public static string ToLogText(this NsType ninjascriptType)
        {
            switch (ninjascriptType)
            {
                case NsType.Indicator:      return "indctor";
                case NsType.Strategy:       return "stratgy";
                case NsType.Service:        return "service";
                case NsType.DrawingTool:    return "dwgtool";
                default: throw new ArgumentOutOfRangeException(nameof(ninjascriptType));
            }
        }

    }
}
