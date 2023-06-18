using KrTrade.Nt.DI.DependencyInjection;
using KrTrade.Nt.Scripts.Services;

namespace KrTrade.Nt.Scripts.MasterScripts
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
