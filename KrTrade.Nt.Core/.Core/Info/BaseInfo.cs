using System;

namespace KrTrade.Nt.Core
{
    public abstract class BaseInfo : IInfo
    {
        public string Name { get; set; }
        public ElementType Type { get; set; }
        protected abstract string ToUniqueString();
        public string Key => ToUniqueString() ?? $"AUTO_KEY({Guid.NewGuid()})";

        public static bool operator ==(BaseInfo info1, IInfo info2) =>
            (info1 is null && info2 is null) ||
            (!(info1 is null) && !(info2 is null) && info1.Key == info2.Key );
        public static bool operator !=(BaseInfo info1, IInfo info2) => !(info1 == info2);

        public static bool operator ==(IInfo info1, BaseInfo info2) =>
            (info1 is null && info2 is null) ||
            (!(info1 is null) && !(info2 is null) && info1.Key == info2.Key );
        public static bool operator !=(IInfo info1, BaseInfo info2) => !(info1 == info2);

        public static bool operator ==(BaseInfo info1, BaseInfo info2) =>
            (info1 is null && info2 is null) ||
            (!(info1 is null) && !(info2 is null) && info1.Key == info2.Key );
        public static bool operator !=(BaseInfo info1, BaseInfo info2) => !(info1 == info2);

        public override bool Equals(object obj) => obj is IInfo other && this == other;
        public bool Equals(IInfo other) => other != null && this == other;

        public override int GetHashCode() => base.GetHashCode();

    }
}
