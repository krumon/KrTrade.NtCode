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
        /// Converts the <see cref="NinjaScriptEvent"/> to short string.
        /// </summary>
        /// <param name="ninjascriptEvent">The ninjascript event.</param>
        /// <returns>The short string that represents the <see cref="NinjaScriptEvent"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The type of ninjascript event is out of range.</exception>
        public static string ToLogText(this NinjaScriptEvent ninjascriptEvent)
        {
            switch (ninjascriptEvent)
            {
                case NinjaScriptEvent.Active:                return "ACTIVE___";
                case NinjaScriptEvent.BarClosed:             return "BARCLOSED";
                case NinjaScriptEvent.BarUpdate:             return "BARUPDATE";
                case NinjaScriptEvent.Configure:             return "CONFIGURE";
                case NinjaScriptEvent.ConnectionStatusUpdate:return "CNECTUPDTE";
                case NinjaScriptEvent.DataLoaded:            return "DTALOADED";
                case NinjaScriptEvent.FirstTick:             return "FIRSTTICK";
                case NinjaScriptEvent.FundamentalData:       return "FNDTLDATA";
                case NinjaScriptEvent.Historical:            return "HSTORICAL";
                case NinjaScriptEvent.MarketData:            return "MRKETDATA";
                case NinjaScriptEvent.MarketDepth:           return "MKETDEPTH";
                case NinjaScriptEvent.PriceChanged:          return "PRCHANGED";
                case NinjaScriptEvent.Realtime:              return "REAL_TIME";
                case NinjaScriptEvent.Render:                return "RENDER___";
                case NinjaScriptEvent.SetDefaults:           return "SETDFAULT";
                case NinjaScriptEvent.Terminated:            return "TRMINATED";
                case NinjaScriptEvent.EachTick:                  return "BARS_TICK";
                case NinjaScriptEvent.Transition:            return "TRNSITION";
                default: throw new ArgumentOutOfRangeException(nameof(ninjascriptEvent));
            }
        }

        /// <summary>
        /// Converts the <see cref="NinjaScriptName"/> to short string.
        /// </summary>
        /// <param name="ninjascriptName">The ninjascript name.</param>
        /// <returns>The short string that represents the <see cref="NinjaScriptName"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The type of ninjascript name is out of range.</exception>
        public static string ToLogText(this NinjaScriptName ninjascriptName)
        {
            switch (ninjascriptName)
            {
                case NinjaScriptName.Ninjascript:        return "NJSCRPT";
                case NinjaScriptName.Drawing:            return "DRAWING";
                case NinjaScriptName.Plot:               return "PLOTTER";
                case NinjaScriptName.HighPriceCache:     return "CACHEHI";
                case NinjaScriptName.LowPriceCache:      return "CACHELW";
                case NinjaScriptName.PriceCache:         return "CACHEPR";
                case NinjaScriptName.Print:              return "PRINT__";
                case NinjaScriptName.Swing:              return "SWING__";
                default: throw new ArgumentOutOfRangeException(nameof(ninjascriptName));
            }
        }

        /// <summary>
        /// Converts the <see cref="NinjaScriptType"/> to short string.
        /// </summary>
        /// <param name="ninjascriptType">The ninjascript type.</param>
        /// <returns>The short string that represents the <see cref="NinjaScriptType"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The type of ninjascript is out of range.</exception>
        public static string ToLogText(this NinjaScriptType ninjascriptType)
        {
            switch (ninjascriptType)
            {
                case NinjaScriptType.Indicator:      return "indctor";
                case NinjaScriptType.Strategy:       return "stratgy";
                case NinjaScriptType.Service:        return "service";
                case NinjaScriptType.DrawingTool:    return "dwgtool";
                default: throw new ArgumentOutOfRangeException(nameof(ninjascriptType));
            }
        }

    }
}
