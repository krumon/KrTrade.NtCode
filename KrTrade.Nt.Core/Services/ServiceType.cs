namespace KrTrade.Nt.Core.Services
{
    /// <summary>
    /// The type of the 'NinjaScript.IServices'.
    /// </summary>
    public enum ServiceType
    {
        UNKNOWN,
        BARS_MANAGER,
        BARS,
        PRINT,
        PLOT,

        // TO DELETE
        SERIES,
        STATS,
    }

    public static class ServiceTypeExtensions
    {
        public static string ToShortString(this ServiceType serviceType) 
            => serviceType.ToString();

        public static string ToLongString(this ServiceType serviceType) 
            => $"{serviceType} SERVICE";

    }
}
