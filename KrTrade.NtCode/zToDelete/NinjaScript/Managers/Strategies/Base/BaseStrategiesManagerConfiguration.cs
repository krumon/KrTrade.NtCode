namespace KrTrade.Nt.Console
{
    /// <summary>
    /// The script options
    /// </summary>
    public abstract class BaseStrategiesManagerConfiguration<TManagerConfiguration> : BaseManagerConfiguration<TManagerConfiguration>, IStrategiesManagerConfiguration
        where TManagerConfiguration : BaseStrategiesManagerConfiguration<TManagerConfiguration>, IStrategiesManagerConfiguration
    {
    }
}
