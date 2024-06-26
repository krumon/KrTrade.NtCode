using System;

namespace KrTrade.Nt.Core.Information
{
    public class Info : IInfo
    {
        public ElementType Type { get; set; }
        public string Name { get; set; }
        public virtual string Key => Type == ElementType.UNKNOWN ? $"AUTO_KEY({Guid.NewGuid()})" : Type.ToString();

        protected Info() : this(ElementType.UNKNOWN)
        {
            
        }
        protected Info(ElementType type)
        {
            Type = type;
        }

        public static bool operator ==(Info info1, IInfo info2) =>
            (info1 is null && info2 is null) ||
            (!(info1 is null) && !(info2 is null) && info1.Key == info2.Key );
        public static bool operator !=(Info info1, IInfo info2) => !(info1 == info2);

        public static bool operator ==(IInfo info1, Info info2) =>
            (info1 is null && info2 is null) ||
            (!(info1 is null) && !(info2 is null) && info1.Key == info2.Key );
        public static bool operator !=(IInfo info1, Info info2) => !(info1 == info2);

        public static bool operator ==(Info info1, Info info2) =>
            (info1 is null && info2 is null) ||
            (!(info1 is null) && !(info2 is null) && info1.Key == info2.Key );
        public static bool operator !=(Info info1, Info info2) => !(info1 == info2);

        public override bool Equals(object obj) => obj is IInfo other && this == other;
        public bool Equals(IInfo other) => other != null && this == other;

        public override int GetHashCode() => base.GetHashCode();

    }

    public abstract class Info<T> : Info, IInfo<T> 
        where T : Enum
    {
        private T _type;
        new public T Type
        {
            get => _type;
            set
            {
                base.Type = value.ToElementType();
                _type = value;
            }
        }

        protected Info()
        {
               
        }
        protected Info(T type)
        {
            Type = type;
        }

    }
}
