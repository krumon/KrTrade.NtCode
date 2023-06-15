using Nt.Core.DependencyInjection;
using KrTrade.NtCode.Services;

namespace KrTrade.NtCode.MasterScripts
{
    internal class MasterScriptBuilder : IScriptBuilder
    {

        public MasterScriptBuilder(IServiceCollection services)
        {
            Services = services;
        }

        public IServiceCollection Services { get; }

    }
}
