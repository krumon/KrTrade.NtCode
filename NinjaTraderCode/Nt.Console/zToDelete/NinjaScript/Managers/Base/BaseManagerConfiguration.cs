namespace ConsoleApp
{
    /// <summary>
    /// The base class to ninjascripts manager options.
    /// </summary>
    public abstract class BaseManagerConfiguration<TManagerConfiguration> : BaseConfiguration<TManagerConfiguration>, IManagerConfiguration
        where TManagerConfiguration : BaseManagerConfiguration<TManagerConfiguration>, IManagerConfiguration
    {
    }
}
