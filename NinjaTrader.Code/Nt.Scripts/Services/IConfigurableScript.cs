namespace Nt.Scripts.Services
{

    /// <summary>
    /// Represents a configurable ninjascript.
    /// </summary>
    public interface IConfigurableScript
    {
        /// <summary>
        /// Method to configure the service.        
        /// </summary>
        void Configure();

        /// <summary>
        /// indicates whether the service is configured. 
        /// </summary>
        bool IsConfigured { get; }

    }
}
