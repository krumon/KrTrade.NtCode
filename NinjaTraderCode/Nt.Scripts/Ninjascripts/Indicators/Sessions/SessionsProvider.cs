using Nt.Core.Logging;
using Nt.Core.Options;
using Nt.Scripts.NinjatraderObjects;
using Nt.Scripts.Services;

namespace Nt.Scripts.Ninjascripts.Indicators
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
