using KrTrade.NtCode.Logging;
using KrTrade.NtCode.Options;
using KrTrade.NtCode.NinjatraderObjects;
using KrTrade.NtCode.Services;

namespace KrTrade.NtCode.Ninjascripts.Indicators
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
