namespace KrTrade.Nt.Console
{
    /// <summary>
    /// The base class of any strategy configuration.
    /// </summary>
    public abstract class BaseStrategyConfiguration<TOptions> : BaseConfiguration<TOptions>, IStrategyConfiguration
        where TOptions : BaseStrategyConfiguration<TOptions>, IStrategyConfiguration
    {
    }
}
