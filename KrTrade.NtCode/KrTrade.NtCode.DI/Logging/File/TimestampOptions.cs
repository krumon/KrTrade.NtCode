using System.IO;
using System.Text;

namespace KrTrade.NtCode.Logging.File
{
    public class TimestampOptions
    {
        /// <summary>
        /// Whether to log the date as part of the message.
        /// </summary>
        public bool LogDate { get; set; }

        /// <summary>
        /// Whether to log the time as part of the message.
        /// </summary>
        public bool LogTime { get; set; }

        /// <summary>
        /// Whether to log the milliseconds as part of the message.
        /// </summary>
        public bool LogMilliseconds { get; set; }

        /// <summary>
        /// Gets or sets indication whether or not UTC timezone should be used to for timestamps in logging messages. Defaults to <c>false</c>.
        /// </summary>
        public bool UseUtcTimestamp { get; set; }

        /// <summary>
        /// Gets or sets format string used to format timestamp in logging messages. Defaults to <c>null</c>.
        /// </summary>
        public string TimestampFormat { get; set; }

        public TimestampOptions() : this(false)
        {
        }
        public TimestampOptions(bool useUtcTimestamp) : this(useUtcTimestamp,true,true)
        {
        }
        public TimestampOptions(bool useUtcTimestamp, bool logDate, bool logTime) : this(useUtcTimestamp,logDate,logTime,false)
        {
        }
        public TimestampOptions(bool useUtcTimestamp, bool logDate, bool logTime, bool logMilliseconds)
        {
            UseUtcTimestamp = useUtcTimestamp;
            LogDate = logDate;
            LogTime = logTime;
            LogMilliseconds = logMilliseconds;
            TimestampFormat = GetTimestampFormat(logDate,logTime,logMilliseconds);
        }

        private string GetTimestampFormat(bool includeDate, bool includeTime, bool includeMilliseconds)
        {
            StringWriter format = new StringWriter();

            string dateFormat = "yyyy-mm-dd";
            string timeFormat = "HH:mm:ss";
            string millisecondsFormat = "ffff";

            if (includeDate)
            {
                if (includeTime)
                {
                    if (includeMilliseconds)
                    {
                        format.Write(dateFormat);
                        format.Write(" ");
                        format.Write(timeFormat);
                        format.Write(" ");
                        format.Write(millisecondsFormat);
                    }
                    else
                    {
                        format.Write(dateFormat);
                        format.Write(" ");
                        format.Write(timeFormat);
                    }
                }
                else
                {
                    format.Write(dateFormat);
                }
            }

            else
            {
                if (includeTime)
                {
                    if (includeMilliseconds)
                    {
                        format.Write(timeFormat);
                        format.Write(" ");
                        format.Write(millisecondsFormat);
                    }
                    else
                    {
                        format.Write(timeFormat);
                    }
                }
            }

            StringBuilder sb = format.GetStringBuilder();
            if (sb.Length > 0)
                return format.ToString();
            else
                return string.Empty;
        }
    }
}
