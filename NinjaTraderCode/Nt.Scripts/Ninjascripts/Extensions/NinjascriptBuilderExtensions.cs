using Nt.Core.Configuration;
using Nt.Core.DependencyInjection;
using Nt.Core.Options;
using Nt.Scripts.Ninjascripts.Configuration;
using Nt.Scripts.Ninjascripts.Internal;
using System;

namespace Nt.Scripts.Ninjascripts
{
    /// <summary>
    /// Extension methods for setting up ninjascript services in an <see cref="INinjascriptBuilder" />.
    /// </summary>
    public static class NinjascriptBuilderExtensions
    {

        /// <summary>
        /// Sets a minimum <see cref="NinjascriptLevel"/> requirement for ninjascripts.
        /// </summary>
        /// <param name="builder">The <see cref="INinjascriptBuilder"/> to set the minimum level on.</param>
        /// <param name="level">The <see cref="NinjascriptLevel"/> to set as the minimum.</param>
        /// <returns>The <see cref="INinjascriptBuilder"/> so that additional calls can be chained.</returns>
        public static INinjascriptBuilder SetMinimumLevel(this INinjascriptBuilder builder, NinjascriptLevel level)
        {
            builder.Services.Add(ServiceDescriptor.Singleton<IConfigureOptions<NinjascriptFilterOptions>>(
                new DefaultNinjascriptLevelConfigureOptions(level)));
            return builder;
        }

        /// <summary>
        /// Adds the given <see cref="INinjascriptFactory"/> to the <see cref="INinjascriptBuilder"/>
        /// </summary>
        /// <param name="builder">The <see cref="INinjascriptBuilder"/> to add the <paramref name="provider"/> to.</param>
        /// <param name="provider">The <see cref="INinjascriptFactory"/> to add to the <paramref name="builder"/>.</param>
        /// <returns>The <see cref="INinjascriptBuilder"/> so that additional calls can be chained.</returns>
        public static INinjascriptBuilder AddProvider(this INinjascriptBuilder builder, INinjascriptFactory provider)
        {
            builder.Services.AddSingleton(provider);
            return builder;
        }

        /// <summary>
        /// Removes all <see cref="INinjascriptFactory"/>s from <paramref name="builder"/>.
        /// </summary>
        /// <param name="builder">The <see cref="INinjascriptBuilder"/> to remove <see cref="INinjascriptFactory"/>s from.</param>
        /// <returns>The <see cref="INinjascriptBuilder"/> so that additional calls can be chained.</returns>
        public static INinjascriptBuilder ClearProviders(this INinjascriptBuilder builder)
        {
            builder.Services.RemoveAll<INinjascriptFactory>();
            return builder;
        }

        /// <summary>
        /// Configure the <paramref name="builder"/> with the <see cref="NinjascriptFactoryOptions"/>.
        /// </summary>
        /// <param name="builder">The <see cref="INinjascriptBuilder"/> to be configured with <see cref="NinjascriptFactoryOptions"/></param>
        /// <param name="action">The action used to configure the ninjascript factory</param>
        /// <returns>The <see cref="INinjascriptBuilder"/> so that additional calls can be chained.</returns>
        public static INinjascriptBuilder Configure(this INinjascriptBuilder builder, Action<NinjascriptFactoryOptions> action)
        {
            builder.Services.Configure(action);
            return builder;
        }

        /// <summary>
        /// Configures <see cref="NinjascriptFilterOptions" /> from an instance of <see cref="IConfiguration" />.
        /// </summary>
        /// <param name="builder">The <see cref="INinjascriptBuilder"/> to use.</param>
        /// <param name="configuration">The <see cref="IConfiguration" /> to add.</param>
        /// <returns>The builder.</returns>
        public static INinjascriptBuilder AddConfiguration(this INinjascriptBuilder builder, IConfiguration configuration)
        {
            builder.AddConfiguration();

            builder.Services.AddSingleton<IConfigureOptions<NinjascriptFilterOptions>>(new NinjascriptFilterConfigureOptions(configuration));
            builder.Services.AddSingleton<IOptionsChangeTokenSource<NinjascriptFilterOptions>>(new ConfigurationChangeTokenSource<NinjascriptFilterOptions>(configuration));

            builder.Services.AddSingleton(new NinjascriptConfiguration(configuration));

            return builder;
        }
    }
}
