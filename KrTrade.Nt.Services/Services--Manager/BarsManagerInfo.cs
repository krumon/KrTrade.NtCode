using KrTrade.Nt.Core.Data;
using KrTrade.Nt.Core.Elements;

namespace KrTrade.Nt.Services
{
    public class BarsManagerInfo : NinjascriptServiceInfo
    {
        public BarsManagerInfo()
        {
        }
        public BarsManagerInfo(ServiceType type) : base(type)
        {
        }

        protected override string ToUniqueString()
        {
            throw new System.NotImplementedException();
        }
    }
}
