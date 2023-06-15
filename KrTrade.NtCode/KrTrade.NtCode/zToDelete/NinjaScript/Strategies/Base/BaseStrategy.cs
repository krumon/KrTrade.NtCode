namespace KrTrade.NtCode
{
    /// <summary>
    /// Base class of any strategy.
    /// </summary>
    public abstract class BaseStrategy<TScript, TOptions,TBuilder> : BaseNinjascript<TScript, TOptions, TBuilder>, IStrategy
        where TScript : BaseStrategy<TScript, TOptions,TBuilder>, IStrategy
        where TOptions : BaseStrategyConfiguration<TOptions>, IStrategyConfiguration
        where TBuilder : BaseStrategyBuilder<TScript,TOptions,TBuilder>, IStrategyBuilder
    {

        #region Constructors

        ///// <summary>
        ///// Creates <see cref="BaseStrategy{TScript, TOptions, TBuilder}"/> default instance.
        ///// </summary>
        //protected BaseStrategy() : base()
        //{
        //}

        #endregion

    }
}
