using System;
using System.Reflection;
using System.Runtime.Serialization;

namespace KrTrade.NtCode.DependencyInjection.ServiceLookup
{
    internal static class ParameterDefaultValue
    {
        public static bool CheckHasDefaultValue(ParameterInfo parameter, out bool tryToGetDefaultValue)
        {
            tryToGetDefaultValue = true;
            try
            {
                return parameter.HasDefaultValue;
            }
            catch (FormatException) when (parameter.ParameterType == typeof(DateTime))
            {
                // Workaround for https://github.com/dotnet/runtime/issues/18844
                // If HasDefaultValue throws FormatException for DateTime
                // we expect it to have default value
                tryToGetDefaultValue = false;
                return true;
            }
        }

        public static bool TryGetDefaultValue(ParameterInfo parameter, out object defaultValue)
        {
            bool hasDefaultValue = CheckHasDefaultValue(parameter, out bool tryToGetDefaultValue);
            defaultValue = null;

            if (hasDefaultValue)
            {
                if (tryToGetDefaultValue)
                {
                    defaultValue = parameter.DefaultValue;
                }

                bool isNullableParameterType = parameter.ParameterType.IsGenericType &&
                    parameter.ParameterType.GetGenericTypeDefinition() == typeof(Nullable<>);

                // Workaround for https://github.com/dotnet/runtime/issues/18599
                if (defaultValue == null && parameter.ParameterType.IsValueType
                    && !isNullableParameterType) // Nullable types should be left null
                {
                    defaultValue = CreateValueType(parameter.ParameterType);
                }

                // Handle nullable enums
                if (defaultValue != null && isNullableParameterType)
                {
                    Type underlyingType = Nullable.GetUnderlyingType(parameter.ParameterType);
                    if (underlyingType != null && underlyingType.IsEnum)
                    {
                        defaultValue = Enum.ToObject(underlyingType, defaultValue);
                    }
                }
            }

            return hasDefaultValue;
        }

        private static object CreateValueType(Type t) 
            => FormatterServices.GetUninitializedObject(t);
        
    }
}
