using System;
using System.Collections.Generic;

namespace Nt.Scripts.MasterScripts
{
    public class MasterScriptFilters
    {
        //public const string Id = "MasterScripts";
        public Dictionary<string, string[]> MasterScripts { get; set; } = new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase);
    }
}
