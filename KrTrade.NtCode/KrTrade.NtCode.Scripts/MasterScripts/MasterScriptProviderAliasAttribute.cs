using System;
using System.Collections.Generic;
using System.Reflection;

namespace Nt.Scripts.MasterScripts
{
    /// <summary>
    /// Defines alias to be used in configure options.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class MasterScriptProviderAliasAttribute : Attribute
    {
        /// <summary>
        /// Creates a new <see cref="NinjascriptProviderAliasAttribute"/> instance.
        /// </summary>
        /// <param name="alias">The alias to set.</param>
        public MasterScriptProviderAliasAttribute(string alias)
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
            string fullName = typeof(MasterScriptProviderAliasAttribute).FullName;

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
