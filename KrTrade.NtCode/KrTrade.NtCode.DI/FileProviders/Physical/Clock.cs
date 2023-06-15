using System;

namespace KrTrade.NtCode.FileProviders.Physical
{
    internal sealed class Clock : IClock
    {
        public static readonly Clock Instance = new Clock();

        private Clock()
        {
        }

        public DateTime UtcNow => DateTime.UtcNow;
    }
}
