﻿using KrTrade.Nt.Core.Data;
using KrTrade.Nt.Core.Extensions;
using KrTrade.Nt.Core.Logging;
using KrTrade.Nt.Core.Print;
using NinjaTrader.NinjaScript;
using System;
using System.IO;

namespace KrTrade.Nt.Services
{
    public class PrintFormatter : BaseLogFormatter
    {
        private const string LogInfoPadding = ": ";
        private const string LogContentPadding = " >> ";

        /// <summary>
        /// Indicates whether the log information will be shown.
        /// </summary>
        protected internal bool IsLogInfoVisible { get; set; } = true;

        /// <summary>
        /// Indicates whether the log data series information will be shown.
        /// </summary>
        protected internal bool IsDataSeriesInfoVisible { get; set; } = true;

        /// <summary>
        /// Indicates whether the log time information will be shown.
        /// </summary>
        protected internal bool IsTimeVisible { get; set; } = true;

        /// <summary>
        /// Indicates whether the number of the will be shown in the log message.
        /// </summary>
        protected internal bool IsNumOfBarVisible { get; set; } = true;

        /// <summary>
        /// Indicates the length of the strings. This property affects to the ninjascript state string, the data series string
        /// </summary>
        protected internal FormatLength StringFormatLength { get; set; } = FormatLength.Long;

        /// <summary>
        /// Writes the log message to the specified TextWriter.
        /// </summary>
        /// <param name="logEntry">The log entry.</param>
        /// <param name="textWriter">The string writer.</param>
        /// <typeparam name="T">The type of the object to be written.</typeparam>
        public override void Write(in LogEntry logEntry, TextWriter textWriter)
        {
            if (IsLogInfoVisible)
            {
                textWriter.Write(logEntry.State.ToLogString(StringFormatLength));
                
                if (logEntry.State == State.Historical || logEntry.State == State.Realtime)
                {
                    textWriter.Write('[');
                    textWriter.Write(logEntry.BarsInProgress.ToString());

                    if (IsDataSeriesInfoVisible)
                    {
                        textWriter.Write(',');
                        textWriter.Write(logEntry.InstrumentName);
                        textWriter.Write('(');
                        textWriter.Write(logEntry.BarsPeriod.ToShortString());
                        textWriter.Write(')');
                    }

                    if (IsNumOfBarVisible)
                    {
                        textWriter.Write(',');
                        textWriter.Write(logEntry.CurrentBar.ToString());
                    }

                    textWriter.Write(']');
                }
            }

            if (IsTimeVisible)
            {
                if (IsLogInfoVisible)
                    textWriter.Write(LogInfoPadding);
                textWriter.Write(logEntry.Time.ToString(logEntry.BarsPeriod.ToTimeFormat(),FormatType.Log, StringFormatLength));
            }
            CreateDefaultLogMessage(textWriter, logEntry);
        }

        private void CreateDefaultLogMessage(TextWriter textWriter, in LogEntry logEntry)
        {
            string message = logEntry.Message != null ? logEntry.Message.ToString() : string.Empty;
            Exception exception = logEntry.Exception;

            if (exception == null && string.IsNullOrEmpty(message))
                return;

            if (IsTimeVisible)
                textWriter.Write(LogContentPadding);
            else if(IsLogInfoVisible)
                textWriter.Write(LogInfoPadding);

            if (!string.IsNullOrEmpty(message))
            {
                textWriter.Write(message);
                if (exception != null)
                {
                    string _messagePadding = new string(' ', textWriter.ToString().Length);
                    string _newLineWithMessagePadding = Environment.NewLine + _messagePadding;
                    textWriter.Write(_newLineWithMessagePadding);
                }
            }

            if (exception != null)
                textWriter.Write(exception.ToString());

        }
        private static void WriteReplacing(TextWriter writer, string oldValue, string newValue, string message)
        {
            string newMessage = message.Replace(oldValue, newValue);
            writer.Write(newMessage);
        }
        private DateTimeOffset GetCurrentDateTime()
        {
            //return FormatterOptions.UseUtcTimestamp ? DateTimeOffset.UtcNow : DateTimeOffset.Now;
            return DateTimeOffset.Now;
        }

    }
}
