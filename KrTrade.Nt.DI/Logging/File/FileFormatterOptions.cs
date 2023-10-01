namespace KrTrade.Nt.DI.Logging.File
{
    public class FileFormatterOptions : BaseFileFormatterOptions
    {

        /// <summary>
        /// Gets or sets the timestamp options.
        /// </summary>
        public TimestampOptions TimestampOptions { get; set; } = new TimestampOptions();

    }
}
