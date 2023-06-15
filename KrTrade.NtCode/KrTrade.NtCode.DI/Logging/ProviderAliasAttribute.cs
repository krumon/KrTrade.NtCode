using System;
using System.Collections.Generic;
using System.Reflection;

namespace KrTrade.NtCode.Logging
{
    /// <summary>
    /// Defines alias for <see cref="ILoggerProvider"/> implementation to be used in filtering rules.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class ProviderAliasAttribute : Attribute
    {
        /// <summary>
        /// Creates a new <see cref="ProviderAliasAttribute"/> instance.
        /// </summary>
        /// <param name="alias">The alias to set.</param>
        public ProviderAliasAttribute(string alias)
        {
            Alias = alias;
        }

        /// <summary>
        /// The alias of the provider.
        /// </summary>
        public string Alias { get; }

        /// <summary>
        /// Gets the alias from privider type. If provider doesn't hasn't alias, return null.
        /// </summary>
        /// <param name="providerType">The type of the provider.</param>
        /// <returns>The alias string.</returns>
        public static string GetAlias(Type providerType)
        {
            string fullName = typeof(ProviderAliasAttribute).FullName;

            IList<CustomAttributeData> attributes = CustomAttributeData.GetCustomAttributes(providerType);

            for (int i = 0; i < attributes.Count; i++)
            {
                CustomAttributeData attributeData = attributes[i];
                if (attributeData.AttributeType.FullName == fullName &&
                    attributeData.ConstructorArguments.Count > 0)
                {
                    CustomAttributeTypedArgument arg = attributeData.ConstructorArguments[0];

                    System.Diagnostics.Debug.Assert(arg.ArgumentType == typeof(string));

                    return arg.Value?.ToString();
                }
            }

            return null;
        }
    }
}
