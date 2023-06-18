using System;

namespace KrTrade.Nt.Scripts.Ninjascripts.Internal
{
    internal struct NinjascriptInfo
    {
        public NinjascriptInfo(INinjascriptProvider provider, string category) : this()
        {
            ProviderType = provider.GetType();
            Ninjascript = provider.CreateNinjascript(category);
            Category = category;
        }

        public INinjascript Ninjascript { get; }

        public string Category { get; }

        public Type ProviderType { get; }

    }
}
