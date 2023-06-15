using Nt.Core.Options;
using KrTrade.NtCode.Ninjascripts.Configuration;

namespace KrTrade.NtCode.Ninjascripts.Options
{
    /// <inheritdoc />
    public class NinjascriptProviderOptionsChangeTokenSource<TOptions, TProvider> : ConfigurationChangeTokenSource<TOptions>
    {
        public NinjascriptProviderOptionsChangeTokenSource(INinjascriptProviderConfiguration<TProvider> providerConfiguration) : base(providerConfiguration.Configuration)
        {
        }
    }
}
