using System;
using System.Collections.Generic;
using System.Reflection;

namespace KrTrade.NtCode.Logging.Internal
{
    internal static class ProviderAliasUtilities
    {
        private const string AliasAttibuteTypeFullName = "KrTrade.NtCode.Logging.ProviderAliasAttribute";

        internal static string GetAlias(Type providerType)
        {
            IList<CustomAttributeData> attributes = CustomAttributeData.GetCustomAttributes(providerType);

            for (int i = 0; i < attributes.Count; i++)
            {
                CustomAttributeData attributeData = attributes[i];
                if (attributeData.AttributeType.FullName == AliasAttibuteTypeFullName &&
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
