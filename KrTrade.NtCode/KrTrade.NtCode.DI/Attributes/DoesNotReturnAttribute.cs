using System;

namespace KrTrade.NtCode.Attributes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    internal sealed class DoesNotReturnAttribute : Attribute
    {
    }
}
