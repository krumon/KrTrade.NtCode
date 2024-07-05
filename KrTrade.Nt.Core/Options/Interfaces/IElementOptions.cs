namespace KrTrade.Nt.Core.Options
{
    public interface IElementOptions : IOptions
    {
        /// <summary>
        /// Indicates if the object logger is enable.
        /// </summary>
        bool IsLogEnable { get; set; }

    }
}
