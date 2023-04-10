using Nt.Scripts.Ninjascripts;

namespace Nt.Scripts.MasterScripts
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
