using KrTrade.Nt.Scripts.Ninjascripts.Configuration;
using KrTrade.Nt.DI.DependencyInjection;
using KrTrade.Nt.DI.Attributes;
using KrTrade.Nt.DI.Options;

namespace KrTrade.Nt.Scripts.Ninjascripts.Options
{
    /// <summary>
    /// Loads settings for <typeparamref name="TProvider"/> into <typeparamref name="TOptions"/> type.
    /// </summary>
    internal sealed class NinjascriptProviderConfigureOptions<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] TOptions, TProvider> : ConfigureFromConfigurationOptions<TOptions> 
        where TOptions : class
    {
        [RequiresUnreferencedCode(NinjascriptProviderOptions.TrimmingRequiresUnreferencedCodeMessage)]
        public NinjascriptProviderConfigureOptions(INinjascriptProviderConfiguration<TProvider> providerConfiguration)
            : base(providerConfiguration.Configuration)
        {
        }
    }
}
