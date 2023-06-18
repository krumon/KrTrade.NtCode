using KrTrade.NtCode.Ninjascripts;
using System.Collections.Generic;

namespace KrTrade.NtCode.MasterScripts
{
    public class MasterScriptOptions
    {
        public Dictionary<string, NinjascriptLevel> Ninjascripts { get; set; } = new Dictionary<string, NinjascriptLevel>();
    }
}
