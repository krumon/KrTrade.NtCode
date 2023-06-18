using KrTrade.NtCode.Ninjascripts;

namespace KrTrade.NtCode.MasterScripts
{
    [MasterScriptProviderAlias("MasterStats")]
    public class MasterStats
    {
        private readonly INinjascript<MasterStats> _ninjascript;

        public MasterStats(INinjascript<MasterStats> ninjascript)
        {
            _ninjascript = ninjascript;
        }
    }
}
