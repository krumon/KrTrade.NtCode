using Nt.Core.Attributes;
using Nt.Core.DependencyInjection;
using Nt.Core.Options;
using Nt.Scripts.Ninjascripts.Configuration;

namespace Nt.Scripts.Ninjascripts.Options
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
