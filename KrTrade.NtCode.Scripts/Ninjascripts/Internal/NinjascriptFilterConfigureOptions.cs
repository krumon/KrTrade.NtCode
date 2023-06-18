using KrTrade.NtCode.Configuration;
using KrTrade.NtCode.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KrTrade.NtCode.Ninjascripts.Internal
{
    internal sealed class NinjascriptFilterConfigureOptions : IConfigureOptions<NinjascriptFilterOptions>
    {
        private const string NinjascriptLevelKey = "Level";
        private const string DefaultCategory = "Default";
        private readonly IConfiguration _configuration;

        public NinjascriptFilterConfigureOptions(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(NinjascriptFilterOptions options)
        {
            LoadDefaultConfigValues(options);
        }

        private void LoadDefaultConfigValues(NinjascriptFilterOptions options)
        {
            if (_configuration == null)
                return;

            //options.CaptureScopes = GetCaptureScopesValue(options);
            foreach (IConfigurationSection configurationSection in _configuration.GetChildren())
            {
                if (configurationSection.Key.Equals(NinjascriptLevelKey, StringComparison.OrdinalIgnoreCase))
                {
                    // Load global category defaults
                    LoadRules(options, configurationSection, null);
                }
                else
                {
                    IConfigurationSection ninjascriptLevelSection = configurationSection.GetSection(NinjascriptLevelKey);
                    if (ninjascriptLevelSection != null)
                    {
                        // Load logger specific rules
                        string ninjascript = configurationSection.Key;
                        LoadRules(options, ninjascriptLevelSection, ninjascript);
                    }
                }
            }
        }

        //[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026:RequiresUnreferencedCode",
        //    Justification = "IConfiguration.GetValue is safe when T is a bool.")]
        //private bool GetCaptureScopesValue(NinjascriptFilterOptions options) => _configuration.GetValue(nameof(options.CaptureScopes), options.CaptureScopes);

        private void LoadRules(NinjascriptFilterOptions options, IConfigurationSection configurationSection, string ninjascript)
        {
            foreach (KeyValuePair<string, string> section in configurationSection.AsEnumerable(true))
            {
                if (TryGetSwitch(section.Value, out NinjascriptLevel level))
                {
                    string category = section.Key;
                    if (category.Equals(DefaultCategory, StringComparison.OrdinalIgnoreCase))
                    {
                        category = null;
                    }
                    var newRule = new NinjascriptFilterRule(ninjascript, category, level, null);
                    options.Rules.Add(newRule);
                }
            }
        }

        private static bool TryGetSwitch(string value, out NinjascriptLevel level)
        {
            if (string.IsNullOrEmpty(value))
            {
                level = NinjascriptLevel.Disable;
                return false;
            }
            else if (Enum.TryParse(value, true, out level))
            {
                return true;
            }
            else
            {
                throw new InvalidOperationException($"Value Not Supported, {value}.");
            }
        }
    }
}
