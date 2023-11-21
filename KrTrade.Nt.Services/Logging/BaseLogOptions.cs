using KrTrade.Nt.Core.Logging;

namespace KrTrade.Nt.Services
{
    public abstract class BaseLogOptions<TFormatter> : BaseServiceOptions
        where TFormatter : BaseLogFormatter, new()
    {

        /// <summary>
        /// The minimum level to log.
        /// </summary>
        internal LogLevel LogLevel { get; set; } = LogLevel.Information;

        /// <summary>
        /// gets the format to logging in any environment.
        /// </summary>
        protected internal TFormatter Formatter { get; protected set; }

        protected BaseLogOptions() 
        {
            Formatter = new TFormatter();
        }
    }
}
