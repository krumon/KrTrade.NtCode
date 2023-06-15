using Nt.Core.Attributes;
using Nt.Core.DependencyInjection;
using Nt.Core.Options;

namespace KrTrade.NtCode.Ninjascripts.Options
{
    /// <summary>
    /// Provides a set of helpers to initialize options objects from logger provider configuration.
    /// </summary>
    public static class NinjascriptProviderOptions
    {
        internal const string TrimmingRequiresUnreferencedCodeMessage = "TOptions's dependent types may have their members trimmed. Ensure all required members are preserved.";

        /// <summary>
        /// Indicates that settings for <typeparamref name="TProvider"/> should be loaded into <typeparamref name="TOptions"/> type.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to register on.</param>
        /// <typeparam name="TOptions">The options class </typeparam>
        /// <typeparam name="TProvider">The provider class</typeparam>
        [RequiresUnreferencedCode(TrimmingRequiresUnreferencedCodeMessage)]
        public static void RegisterProviderOptions<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] TOptions, TProvider>(IServiceCollection services) 
            where TOptions : class
        {
            services.TryAddEnumerable(ServiceDescriptor.Singleton<IConfigureOptions<TOptions>, NinjascriptProviderConfigureOptions<TOptions, TProvider>>());
            services.TryAddEnumerable(ServiceDescriptor.Singleton<IOptionsChangeTokenSource<TOptions>, NinjascriptProviderOptionsChangeTokenSource<TOptions, TProvider>>());
        }
    }
}
