using KrTrade.Nt.Core.Bars;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Define the <see cref="MultiTimeFrameService"/> options. 
    /// </summary>
    public class BarsLogOptions //: ServicesLogOptions
    {

        /// <summary>
        /// the minimum level to be logged.
        /// </summary>
        public BarsLogLevel LogLevel { get; set; } = BarsLogLevel.BarClosed;

        /// <summary>
        /// Create <see cref="BarsLogOptions"/> instance with specified values.
        /// </summary>
        public BarsLogOptions()
        {
        }

        /// <summary>
        /// Create <see cref="ServicesLogOptions"/> instance with specified values.
        /// </summary>
        /// <param name="label">The service label to show.</param>
        public BarsLogOptions(string label) : this(true, BarsLogLevel.BarClosed, true, label, " - ")
        {
        }

        /// <summary>
        /// Create <see cref="ServicesLogOptions"/> instance with specified values.
        /// </summary>
        /// <param name="label">The service label to show.</param>
        /// <param name="statesSeparator">The string separator between states.</param>
        public BarsLogOptions(string label, string statesSeparator) : this(true, BarsLogLevel.BarClosed, true, label, statesSeparator)
        {
        }

        /// <summary>
        /// Create <see cref="ServicesLogOptions"/> instance with specified values.
        /// </summary>
        /// <param name="isEnable">True if the log service is enable.</param>
        /// <param name="isLabelVisible">True, if we want to show the service label.</param>
        /// <param name="label">The service label to show.</param>
        /// <param name="statesSeparator">The string separator between states.</param>
        public BarsLogOptions(bool isEnable, BarsLogLevel barsLogLevel, bool isLabelVisible, string label, string statesSeparator)
        {
            LogLevel = barsLogLevel;
            //IsEnable = isEnable;
            //IsLabelVisible = isLabelVisible;
            //Label = label;
            //StatesSeparator = statesSeparator;
        }

    }
}
