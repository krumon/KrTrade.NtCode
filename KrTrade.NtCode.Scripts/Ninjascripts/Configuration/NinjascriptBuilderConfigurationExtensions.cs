using KrTrade.NtCode.DependencyInjection;

namespace KrTrade.NtCode.Ninjascripts.Configuration
{
    /// <summary>
    /// Extension methods for setting up ninjascript services in an <see cref="INinjascriptBuilder" />.
    /// </summary>
    public static class NinjascriptBuilderConfigurationExtensions
    {
        /// <summary>
        /// Adds services required to consume <see cref="INinjascriptProviderConfigurationFactory"/> or <see cref="INinjascriptProviderConfiguration{T}"/>
        /// </summary>
        /// <param name="builder">The <see cref="INinjascriptBuilder"/> to register services on.</param>
        public static void AddConfiguration(this INinjascriptBuilder builder)
        {
            builder.Services.TryAddSingleton<INinjascriptProviderConfigurationFactory, NinjascriptProviderConfigurationFactory>();
            builder.Services.TryAddSingleton(typeof(INinjascriptProviderConfiguration<>), typeof(NinjascriptProviderConfiguration<>));
        }
    }
}
