namespace KrTrade.Nt.Core.Options
{
    public interface IOptions
    {
        /// <summary>
        /// Indicates if the object is enabled.
        /// </summary>
        bool IsEnable { get; set; }

        /// <summary>
        /// Indicates if the object logger is enable.
        /// </summary>
        bool IsLogEnable { get; set; }

    }
}
