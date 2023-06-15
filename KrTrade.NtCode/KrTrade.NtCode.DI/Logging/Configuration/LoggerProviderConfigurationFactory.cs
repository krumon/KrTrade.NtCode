using Nt.Core.Configuration;
using Nt.Core.Logging.Internal;
using System;
using System.Collections.Generic;

namespace Nt.Core.Logging.Configuration
{
    internal sealed class LoggerProviderConfigurationFactory : ILoggerProviderConfigurationFactory
    {
        private readonly IEnumerable<LoggingConfiguration> _configurations;

        public LoggerProviderConfigurationFactory(IEnumerable<LoggingConfiguration> configurations)
        {
            _configurations = configurations;
        }

        public IConfiguration GetConfiguration(Type providerType)
        {
            if (providerType == null)
                throw new ArgumentNullException(nameof(providerType));

            string fullName = providerType.FullName;
            string alias = ProviderAliasUtilities.GetAlias(providerType);
            var configurationBuilder = new ConfigurationBuilder();
            foreach (LoggingConfiguration configuration in _configurations)
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
