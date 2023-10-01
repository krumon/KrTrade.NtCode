using KrTrade.Nt.Scripts.Ninjascripts.Configuration;
using KrTrade.Nt.DI.Options;

namespace KrTrade.Nt.Scripts.Ninjascripts.Options
{
    /// <inheritdoc />
    public class NinjascriptProviderOptionsChangeTokenSource<TOptions, TProvider> : ConfigurationChangeTokenSource<TOptions>
    {
        public NinjascriptProviderOptionsChangeTokenSource(INinjascriptProviderConfiguration<TProvider> providerConfiguration) : base(providerConfiguration.Configuration)
        {
        }
    }
}
