using System;

namespace KrTrade.Nt.Scripts.Ninjascripts.Internal
{
    internal class NinjascriptConfig
    {

        public NinjascriptConfig(INinjascript ninjascript, string category, string providerTypeFullName, NinjascriptLevel? minLevel, Func<string, string, NinjascriptLevel, bool> filter)
        {
            Ninjascript = ninjascript;
            Category = category;
            ProviderTypeFullName = providerTypeFullName;
            MinLevel = minLevel;
            Filter = filter;
        }

        public INinjascript Ninjascript { get; }

        public string Category { get; }

        private string ProviderTypeFullName { get; }

        public NinjascriptLevel? MinLevel { get; }

        public Func<string, string, NinjascriptLevel, bool> Filter { get; }

        public bool IsEnabled(NinjascriptLevel level)
        {
            var nLevel = (int)level;
            var nMinLevel = (int)MinLevel;
            if (MinLevel != null && (int)level < (int)MinLevel)
                return false;

            if (Filter != null)
                return Filter(ProviderTypeFullName, Category, level);

            return true;
        }

    }
}
