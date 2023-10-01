using System;

namespace KrTrade.Nt.DI.Configuration
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class ConfigurationKeyNameAttribute : Attribute
    {
        public ConfigurationKeyNameAttribute(string name) => Name = name;

        public string Name { get; }
    }
}
