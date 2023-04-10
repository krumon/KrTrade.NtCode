using Nt.Core.Options;
using Nt.Scripts.Configuration;
using Nt.Scripts.Ninjascripts;
using System;

namespace Nt.Scripts.MasterScripts
{
    public class MasterScriptFactory: INinjascriptFactory
    {
        
        private IOptionsMonitor<MasterScriptOptions> _masterScriptOptions;
        private IOptionsMonitor<MasterScriptFilters> _masterScriptFilters;

        private MasterScriptOptions _masterStatsOptions;
        private MasterScriptOptions _masterSwingsOptions;

        public MasterScriptFactory(IOptionsMonitor<MasterScriptFilters> filters, IOptionsMonitor<MasterScriptOptions> masterScriptOptions)
        {
            _masterScriptOptions = masterScriptOptions ?? throw new ArgumentNullException(nameof(masterScriptOptions));
            _masterScriptFilters = filters ?? throw new ArgumentNullException(nameof(masterScriptOptions));

            var filtersOptions = filters.CurrentValue;
            _masterStatsOptions = _masterScriptOptions.Get(MasterScriptSections.MasterStats);
            _masterSwingsOptions = _masterScriptOptions.Get(MasterScriptSections.MasterSwings);
        }

        public void AddProvider(INinjascriptProvider provider)
        {
            throw new NotImplementedException();
        }

        public INinjascript CreateNinjascript(string categoryName)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
