using KrTrade.NtCode.Ninjascripts;

namespace KrTrade.NtCode.Ninjascripts.Indicators
{
    /// <summary>
    /// Represents a <see cref="ISessions"/> service.
    /// </summary>
    public interface ISessions : INinjascript, IRecalculableOnBarUpdate, IRecalculableOnSessionChanged
    {
        ISessionsIterator SessionsIterator { get; }
        //ISessionsFilters Filters { get; }

        /// <summary>
        /// Indicates a new session begin.
        /// </summary>
        bool IsNewSession { get; set; }
        
    }
}
