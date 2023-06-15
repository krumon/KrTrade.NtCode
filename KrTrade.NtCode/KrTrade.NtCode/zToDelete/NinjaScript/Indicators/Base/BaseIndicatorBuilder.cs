using NinjaTrader.NinjaScript;

namespace KrTrade.NtCode
{
    /// <summary>
    /// The base class for any indicator builder.
    /// </summary>
    public abstract class BaseIndicatorBuilder<TScript,TConfiguration,TBuilder> : BaseBuilder<TScript,TConfiguration,TBuilder>, IIndicatorBuilder
        where TScript : BaseIndicator<TScript,TConfiguration,TBuilder>, IIndicator
        where TConfiguration : BaseIndicatorConfiguration<TConfiguration>, IIndicatorConfiguration
        where TBuilder : BaseIndicatorBuilder<TScript,TConfiguration,TBuilder>, IIndicatorBuilder
    {

        #region Constructors

        /// <summary>
        /// Creates <see cref="BaseIndicatorBuilder"/> default instance.
        /// </summary>
        /// <param name="script">The script to build.</param>
        public BaseIndicatorBuilder(INinjascript script) : base(script)
        {
        }

        #endregion

    }
}
