using KrTrade.Nt.Core.Data;
using NinjaTrader.Data;
using System;

namespace KrTrade.Nt.Core.Logging
{
    public static class LoggingHelpers
    {
        /// <summary>
        /// Converts the <see cref="LogLevel"/> to short string.
        /// </summary>
        /// <param name="logLevel">The log level.</param>
        /// <returns>The short string that represents the <see cref="LogLevel"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The type of log level is out of range.</exception>
        public static string ToLogString(this LogLevel logLevel)
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
        public static string ToLogString(this NinjaScriptEvent ninjascriptEvent)
        {
            switch (ninjascriptEvent)
            {
                case NinjaScriptEvent.Unknown: return "*UNKNOWN*";

                case NinjaScriptEvent.SetDefaults: return "SETDFAULT";
                case NinjaScriptEvent.Configure: return "CONFIGURE";
                case NinjaScriptEvent.Active: return "ACTIVE___";
                case NinjaScriptEvent.DataLoaded: return "DTALOADED";
                case NinjaScriptEvent.Historical: return "HSTORICAL";
                case NinjaScriptEvent.Transition: return "TRNSITION";
                case NinjaScriptEvent.Realtime: return "REAL_TIME";
                case NinjaScriptEvent.Terminated: return "TRMINATED";

                case NinjaScriptEvent.BarUpdate: return "BARUPDATE";
                case NinjaScriptEvent.BarClosed: return "BARCLOSED";
                case NinjaScriptEvent.PriceChanged: return "PRCHANGED";
                case NinjaScriptEvent.EachTick: return "EACH_TICK";
                case NinjaScriptEvent.FirstTick: return "FIRSTTICK";
                case NinjaScriptEvent.LastBarRemoved: return "BAREMOVED";

                case NinjaScriptEvent.MarketData: return "MRKETDATA";
                case NinjaScriptEvent.MarketDepth: return "MKETDEPTH";

                case NinjaScriptEvent.ConnectionStatusUpdate: return "CNCTUPDTE";
                case NinjaScriptEvent.FundamentalData: return "FNDTLDATA";
                case NinjaScriptEvent.Render: return "RENDER___";

                default: throw new ArgumentOutOfRangeException(nameof(ninjascriptEvent));
            }
        }

        /// <summary>
        /// Converts the string of the caller member name to <see cref="NinjaScriptEvent"/>.
        /// </summary>
        /// <param name="callerMemberName">The caller member name string.</param>
        /// <returns>The <see cref="NinjaScriptEvent"/> that represents the handler method.</returns>
        public static NinjaScriptEvent ToNinjaScriptEvent(this string callerMemberName)
        {
            if (callerMemberName == "SetDefaults") return NinjaScriptEvent.SetDefaults;
            else if (callerMemberName == "Configure") return NinjaScriptEvent.Configure;
            else if (callerMemberName == "Active") return NinjaScriptEvent.Active;
            else if (callerMemberName == "DataLoaded") return NinjaScriptEvent.DataLoaded;
            else if (callerMemberName == "Historical") return NinjaScriptEvent.Historical;
            else if (callerMemberName == "Transition") return NinjaScriptEvent.Transition;
            else if (callerMemberName == "Realtime") return NinjaScriptEvent.Realtime;
            else if (callerMemberName == "Terminated") return NinjaScriptEvent.Terminated;

            else if (callerMemberName == "OnBarUpdate") return NinjaScriptEvent.BarUpdate;
            else if (callerMemberName == "OnBarClosed") return NinjaScriptEvent.BarClosed;
            else if (callerMemberName == "OnPriceChanged") return NinjaScriptEvent.PriceChanged;
            else if (callerMemberName == "OnEachTick") return NinjaScriptEvent.EachTick;
            else if (callerMemberName == "OnFirstTick") return NinjaScriptEvent.FirstTick;

            else if (callerMemberName == "OnMarketData") return NinjaScriptEvent.MarketData;
            else if (callerMemberName == "OnMarketDepth") return NinjaScriptEvent.MarketDepth;

            else if (callerMemberName == "OnConnectionStatusUpdate") return NinjaScriptEvent.ConnectionStatusUpdate;
            else if (callerMemberName == "OnFundamentalData") return NinjaScriptEvent.FundamentalData;
            else if (callerMemberName == "OnRender") return NinjaScriptEvent.Render;

            else return NinjaScriptEvent.Unknown;
        }

        /// <summary>
        /// Converts the string of the caller member name to log string.
        /// </summary>
        /// <param name="callerMemberName">The caller member name string.</param>
        /// <returns>The log string that represents the handler method.</returns>
        public static string ToLogString(this string callerMemberName)
        {
            if (callerMemberName == "SetDefaults") return "SETDFAULT";
            else if (callerMemberName == "Configure") return "CONFIGURE";
            else if (callerMemberName == "Active") return "ACTIVE___";
            else if (callerMemberName == "DataLoaded") return "DTALOADED";
            else if (callerMemberName == "Historical") return "HSTORICAL";
            else if (callerMemberName == "Transition") return "TRNSITION";
            else if (callerMemberName == "Realtime") return "REAL_TIME";
            else if (callerMemberName == "Terminated") return "TRMINATED";

            else if (callerMemberName == "OnBarUpdate") return "BARUPDATE";
            else if (callerMemberName == "OnBarClosed") return "BARCLOSED";
            else if (callerMemberName == "OnPriceChanged") return "PRCHANGED";
            else if (callerMemberName == "OnEachTick") return "EACH_TICK";
            else if (callerMemberName == "OnFirstTick") return "FIRSTTICK";

            else if (callerMemberName == "OnMarketData") return "MRKETDATA";
            else if (callerMemberName == "OnMarketDepth") return "MKETDEPTH";

            else if (callerMemberName == "OnConnectionStatusUpdate") return "CNCTUPDTE";
            else if (callerMemberName == "OnFundamentalData") return "FNDTLDATA";
            else if (callerMemberName == "OnRender") return "RENDER___";

            else return "*UNKNOWN*";
        }

        /// <summary>
        /// Converts the <see cref="NinjaScriptName"/> to short string.
        /// </summary>
        /// <param name="ninjascriptName">The ninjascript name.</param>
        /// <returns>The short string that represents the <see cref="NinjaScriptName"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The type of ninjascript name is out of range.</exception>
        public static string ToLogString(this NinjaScriptName ninjascriptName)
        {
            switch (ninjascriptName)
            {
                case NinjaScriptName.Unknown: return "UNKNOWN";
                case NinjaScriptName.Ninjascript: return "NJSCRPT";
                case NinjaScriptName.DataSeries: return "DTSRIES";
                case NinjaScriptName.Drawing: return "DRAWING";
                case NinjaScriptName.Plot: return "PLOTTER";
                case NinjaScriptName.HighPriceCache: return "CACHEHI";
                case NinjaScriptName.LowPriceCache: return "CACHELW";
                case NinjaScriptName.PriceCache: return "CACHEPR";
                case NinjaScriptName.Print: return "PRINT__";
                case NinjaScriptName.Swing: return "SWING__";
                default: throw new ArgumentOutOfRangeException(nameof(ninjascriptName));
            }
        }

        /// <summary>
        /// Converts the <see cref="NinjaScriptType"/> to short string.
        /// </summary>
        /// <param name="ninjascriptType">The ninjascript type.</param>
        /// <returns>The short string that represents the <see cref="NinjaScriptType"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The type of ninjascript is out of range.</exception>
        public static string ToLogString(this NinjaScriptType ninjascriptType)
        {
            switch (ninjascriptType)
            {
                case NinjaScriptType.Unknown: return "unknown";
                case NinjaScriptType.Indicator: return "indctor";
                case NinjaScriptType.Strategy: return "stratgy";
                case NinjaScriptType.Service: return "service";
                case NinjaScriptType.DrawingTool: return "dwgtool";
                default: throw new ArgumentOutOfRangeException(nameof(ninjascriptType));
            }
        }

        /// <summary>
        /// Converts the <see cref="BarsType"/> to log string.
        /// </summary>
        /// <param name="periodType">The ninjascript type.</param>
        /// <returns>The short string that represents the <see cref="NinjaScriptType"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The type of ninjascript is out of range.</exception>
        public static string ToLogString(this BarsPeriodType periodType)
        {
            switch (periodType)
            {
                case BarsPeriodType.Tick: return "t";
                case BarsPeriodType.Second: return "s";
                case BarsPeriodType.Minute: return "m";
                case BarsPeriodType.Day: return "D";
                case BarsPeriodType.Week: return "W";
                case BarsPeriodType.Month: return "M";
                case BarsPeriodType.Year: return "Y";
                case BarsPeriodType.LineBreak: return "l";
                case BarsPeriodType.HeikenAshi: return "h";
                case BarsPeriodType.Kagi: return "k";
                case BarsPeriodType.PointAndFigure: return "p";
                case BarsPeriodType.Volumetric: return "v";
                case BarsPeriodType.Range: return "r";
                default: throw new ArgumentOutOfRangeException(nameof(periodType));
            }
        }
    }
}
