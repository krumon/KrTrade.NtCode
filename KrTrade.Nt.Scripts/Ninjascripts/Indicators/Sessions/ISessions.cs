using KrTrade.Nt.Scripts.Ninjascripts;

namespace KrTrade.Nt.Scripts.Ninjascripts.Indicators
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
