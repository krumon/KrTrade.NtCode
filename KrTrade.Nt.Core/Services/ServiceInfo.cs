using KrTrade.Nt.Core.Elements;

namespace KrTrade.Nt.Core.Services
{
    public class ServiceInfo : BaseElementInfo
    {
        public override string GetKey() => Name;

        //public static bool operator ==(ServiceInfo elementInfo1, ServiceInfo elementInfo2) =>
        //    (elementInfo1 is null && elementInfo2 is null) ||
        //    (
        //    !(elementInfo1 is null) &&
        //    !(elementInfo2 is null) &&
        //    elementInfo1.GetKey() == elementInfo2.GetKey()
        //    );
        //public static bool operator !=(ServiceInfo elementInfo1, ServiceInfo elementInfo2) => !(elementInfo1 == elementInfo2);

        //public static bool operator ==(ServiceInfo elementInfo1, IElementInfo elementInfo2) =>
        //    (elementInfo1 is null && elementInfo2 is null) ||
        //    (
        //    !(elementInfo1 is null) &&
        //    !(elementInfo2 is null) &&
        //    elementInfo1.GetKey() == elementInfo2.GetKey()
        //    );
        //public static bool operator !=(ServiceInfo elementInfo1, IElementInfo elementInfo2) => !(elementInfo1 == elementInfo2);

        //public static bool operator ==(IElementInfo elementInfo1, ServiceInfo elementInfo2) =>
        //    (elementInfo1 is null && elementInfo2 is null) ||
        //    (
        //    !(elementInfo1 is null) &&
        //    !(elementInfo2 is null) &&
        //    elementInfo1.GetKey() == elementInfo2.GetKey()
        //    );
        //public static bool operator !=(IElementInfo elementInfo1, ServiceInfo elementInfo2) => !(elementInfo1 == elementInfo2);

        //public override bool Equals(object obj) => obj is IElementInfo other && this == other;
        //public override int GetHashCode() => GetKey().GetHashCode();
        //public bool Equals(IElementInfo other) => other != null && this == other;
    }
}
