namespace KrTrade.NtCode.Ninjascripts.Indicators
{
    /// <summary>
    /// Represents the properties and methods to create a default implementation of <see cref="SessionsFiltersService"/>.
    /// </summary>
    public interface ISessionsFilters
    {
        SessionsFiltersOptions Options { get; }
        bool IsEnabled { get; }
    }
}
