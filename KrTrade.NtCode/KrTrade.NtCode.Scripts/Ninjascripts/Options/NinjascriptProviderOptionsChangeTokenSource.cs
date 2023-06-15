using Nt.Core.Options;
using Nt.Scripts.Ninjascripts.Configuration;

namespace Nt.Scripts.Ninjascripts.Options
{
    /// <inheritdoc />
    public class NinjascriptProviderOptionsChangeTokenSource<TOptions, TProvider> : ConfigurationChangeTokenSource<TOptions>
    {
        public NinjascriptProviderOptionsChangeTokenSource(INinjascriptProviderConfiguration<TProvider> providerConfiguration) : base(providerConfiguration.Configuration)
        {
        }
    }
}
