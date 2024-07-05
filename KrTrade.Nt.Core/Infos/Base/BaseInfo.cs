using KrTrade.Nt.Core.Data;
using System;

namespace KrTrade.Nt.Core.Infos
{
    public abstract class BaseInfo : IInfo
    {
        public string Name { get; set; }
        public string Key => ToUniqueString();

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
        protected virtual string ToUniqueString() => $"AUTO_KEY({Guid.NewGuid()})";
    }
    public abstract class BaseInfo<T> : BaseInfo, IInfo<T>
        where T : Enum
    {
        public T Type { get; set; }

        protected BaseInfo()
        {
        }
        protected BaseInfo(T type)
        {
            Type = type;
        }

        public static bool operator ==(BaseInfo<T> info1, IInfo info2) =>
            (info1 is null && info2 is null) ||
            (!(info1 is null) && !(info2 is null) && info1.Key == info2.Key );
        public static bool operator !=(BaseInfo<T> info1, IInfo info2) => !(info1 == info2);

        public static bool operator ==(IInfo info1, BaseInfo<T> info2) =>
            (info1 is null && info2 is null) ||
            (!(info1 is null) && !(info2 is null) && info1.Key == info2.Key );
        public static bool operator !=(IInfo info1, BaseInfo<T> info2) => !(info1 == info2);

        public static bool operator ==(BaseInfo<T> info1, BaseInfo<T> info2) =>
            (info1 is null && info2 is null) ||
            (!(info1 is null) && !(info2 is null) && info1.Key == info2.Key );
        public static bool operator !=(BaseInfo<T> info1, BaseInfo<T> info2) => !(info1 == info2);

        public override bool Equals(object obj) => obj is IInfo<T> other && this == other;
        public bool Equals(IInfo<T> other) => other != null && this == other;

        public override int GetHashCode() => base.GetHashCode();

    }
}
