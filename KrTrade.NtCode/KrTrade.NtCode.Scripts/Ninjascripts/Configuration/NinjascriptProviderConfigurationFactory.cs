using Nt.Core.Configuration;
using System;
using System.Collections.Generic;

namespace Nt.Scripts.Ninjascripts.Configuration
{
    internal sealed class NinjascriptProviderConfigurationFactory : INinjascriptProviderConfigurationFactory
    {
        private readonly IEnumerable<NinjascriptConfiguration> _configurations;

        public NinjascriptProviderConfigurationFactory(IEnumerable<NinjascriptConfiguration> configurations)
        {
            _configurations = configurations;
        }

        public IConfiguration GetConfiguration(Type providerType)
        {
            if (providerType == null)
                throw new ArgumentNullException(nameof(providerType));

            string fullName = providerType.FullName;
            string alias = NinjascriptProviderAliasAttribute.GetAlias(providerType);
            var configurationBuilder = new ConfigurationBuilder();
            foreach (NinjascriptConfiguration configuration in _configurations)
            {
                IConfigurationSection sectionFromFullName = configuration.Configuration.GetSection(fullName);
                configurationBuilder.AddConfiguration(sectionFromFullName);

                if (!string.IsNullOrWhiteSpace(alias))
                {
                    IConfigurationSection sectionFromAlias = configuration.Configuration.GetSection(alias);
                    configurationBuilder.AddConfiguration(sectionFromAlias);
                }
            }
            return configurationBuilder.Build();
        }
    }
}
