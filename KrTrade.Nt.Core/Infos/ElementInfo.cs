using KrTrade.Nt.Core.Data;

namespace KrTrade.Nt.Core.Infos
{
    public abstract class ElementInfo : BaseInfo<ElementType>, IElementInfo
    {
        protected ElementInfo() : base() { }
        protected ElementInfo(ElementType type) : base(type) {  }

    }
}
