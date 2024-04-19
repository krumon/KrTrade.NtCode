using System;

namespace KrTrade.Nt.Core.Info
{
    public abstract class BaseKey : IHasKey
    {
        protected abstract string GetKey();
        public string Key => GetKey() ?? $"NOT_KEY({Guid.NewGuid()})";

        public static bool operator ==(BaseKey key1, IHasKey key2) =>
            (key1 is null && key2 is null) ||
            (
            !(key1 is null) &&
            !(key2 is null) &&
            key1.GetKey() == key2.Key
            );
        public static bool operator !=(BaseKey key1, IHasKey key2) => !(key1 == key2);

        public static bool operator ==(IHasKey key1, BaseKey key2) =>
            (key1 is null && key2 is null) ||
            (
            !(key1 is null) &&
            !(key2 is null) &&
            key1.Key == key2.Key
            );
        public static bool operator !=(IHasKey key1, BaseKey key2) => !(key1 == key2);

        public static bool operator ==(BaseKey key1, BaseKey key2) =>
            (key1 is null && key2 is null) ||
            (
            !(key1 is null) &&
            !(key2 is null) &&
            key1.GetKey() == key2.GetKey()
            );
        public static bool operator !=(BaseKey key1, BaseKey key2) => !(key1 == key2);

        public override bool Equals(object obj) => obj is IHasKey other && this == other;
        public override int GetHashCode() => GetKey().GetHashCode();
        public bool Equals(IHasKey other) => other != null && this == other;

    }
}
