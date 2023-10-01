using KrTrade.Nt.Scripts.Ninjascripts;
using System.Collections.Generic;

namespace KrTrade.Nt.Scripts.MasterScripts
{
    public class MasterScriptOptions
    {
        public Dictionary<string, NinjascriptLevel> Ninjascripts { get; set; } = new Dictionary<string, NinjascriptLevel>();
    }
}
