using KrTrade.Nt.DI.Attributes;
using KrTrade.Nt.DI.DependencyInjection;
using KrTrade.Nt.DI.Logging.Configuration;
using KrTrade.Nt.DI.Options;

namespace KrTrade.Nt.DI.Logging.Options
{
    /// <summary>
    /// Loads settings for <typeparamref name="TProvider"/> into <typeparamref name="TOptions"/> type.
    /// </summary>
    internal sealed class LoggerProviderConfigureOptions<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] TOptions, TProvider> : ConfigureFromConfigurationOptions<TOptions> 
        where TOptions : class
    {
        [RequiresUnreferencedCode(LoggerProviderOptions.TrimmingRequiresUnreferencedCodeMessage)]
        public LoggerProviderConfigureOptions(ILoggerProviderConfiguration<TProvider> providerConfiguration)
            : base(providerConfiguration.Configuration)
        {
        }
    }
}
