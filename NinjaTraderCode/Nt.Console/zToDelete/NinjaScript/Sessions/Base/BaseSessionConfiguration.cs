namespace ConsoleApp
{
    /// <summary>
    /// The base class class for any session configuration.
    /// </summary>
    public abstract class BaseSessionConfiguration<TSessionConfiguration> : BaseConfiguration<TSessionConfiguration>, ISessionConfiguration
        where TSessionConfiguration : BaseSessionConfiguration<TSessionConfiguration>, ISessionConfiguration
    {
    }
}
