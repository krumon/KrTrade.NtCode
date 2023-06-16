using KrTrade.NtCode.Hosting;
using KrTrade.NtCode.NinjatraderObjects;

namespace KrTrade.NtCode
{
    internal class KrTradeStats : BaseNinjaScript
    {
        private readonly INinjaHost _host;

        public KrTradeStats()
        {
            _host = (INinjaHost)NinjaHost.CreateNinjaHostDefaultBuilder<KrTradeStats>().Build();
        }

        public override void Configure()
        {
            _host.Configure();
        }
        public override void DataLoaded()
        {
            _host.DataLoaded();
        }
        public override void OnBarUpdate()
        {
            _host.OnBarUpdate();
        }
        public override void OnSessionUpdate()
        {
            _host.OnSessionUpdate();
        }
    }
}
