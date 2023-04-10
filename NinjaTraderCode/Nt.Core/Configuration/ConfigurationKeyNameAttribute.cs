using System;

namespace Nt.Core.Configuration
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class ConfigurationKeyNameAttribute : Attribute
    {
        public ConfigurationKeyNameAttribute(string name) => Name = name;

        public string Name { get; }
    }
}
