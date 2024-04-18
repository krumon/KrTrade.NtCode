namespace KrTrade.Nt.Core.Elements
{
    public abstract class BaseElementInfo : IElementInfo
    {
        private string _name;
        public string Name 
        { 
            get => string.IsNullOrEmpty(_name) ? GetKey() : _name; 
            set => _name = value;
        }
        public abstract string GetKey();

        public static bool operator ==(BaseElementInfo elementInfo1, BaseElementInfo elementInfo2) =>
            (elementInfo1 is null && elementInfo2 is null) ||
            (
            !(elementInfo1 is null) &&
            !(elementInfo2 is null) &&
            elementInfo1.GetKey() == elementInfo2.GetKey()
            );
        public static bool operator !=(BaseElementInfo elementInfo1, BaseElementInfo elementInfo2) => !(elementInfo1 == elementInfo2);

        public static bool operator ==(BaseElementInfo elementInfo1, IElementInfo elementInfo2) =>
            (elementInfo1 is null && elementInfo2 is null) ||
            (
            !(elementInfo1 is null) &&
            !(elementInfo2 is null) &&
            elementInfo1.GetKey() == elementInfo2.GetKey()
            );
        public static bool operator !=(BaseElementInfo elementInfo1, IElementInfo elementInfo2) => !(elementInfo1 == elementInfo2);

        public static bool operator ==(IElementInfo elementInfo1, BaseElementInfo elementInfo2) =>
            (elementInfo1 is null && elementInfo2 is null) ||
            (
            !(elementInfo1 is null) &&
            !(elementInfo2 is null) &&
            elementInfo1.GetKey() == elementInfo2.GetKey()
            );
        public static bool operator !=(IElementInfo elementInfo1, BaseElementInfo elementInfo2) => !(elementInfo1 == elementInfo2);

        public override bool Equals(object obj) => obj is IElementInfo other && this == other;
        public override int GetHashCode() => GetKey().GetHashCode();
        public bool Equals(IElementInfo other) => other != null && this == other;

    }
}
