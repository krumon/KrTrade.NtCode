namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Provides the options for any <see cref="IService"/>.
    /// </summary>
    public class ServiceOptions
    {
        public const string DefaultName = "Unnamed Service";

        /// <summary>
        /// Gets the name of the service.
        /// </summary>
        public string Name { get; } = DefaultName;

        /// <summary>
        /// Indicates if the service is enabled.
        /// </summary>
        public bool IsEnable { get; set; } = true;
    }
}
