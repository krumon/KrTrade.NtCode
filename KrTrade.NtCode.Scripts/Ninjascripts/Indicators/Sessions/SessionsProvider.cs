using KrTrade.Nt.DI.Logging;
using KrTrade.Nt.DI.Options;
using KrTrade.Nt.Scripts.NinjatraderObjects;

namespace KrTrade.Nt.Scripts.Ninjascripts.Indicators
{
    [NinjascriptProviderAlias("Sessions")]
    public class SessionsProvider : INinjascriptProvider
    {
        public SessionsProvider(INinjaScriptBase ninjascript, IGlobalsData globalsData, ILogger<Sessions> logger, IOptionsMonitor<SessionsOptions> options)
        {
            var op = options.CurrentValue;
        }

        public INinjascript CreateNinjascript(string categoryName)
        {
            return new Sessions();
        }

        public void Dispose()
        {
        }
    }
}
