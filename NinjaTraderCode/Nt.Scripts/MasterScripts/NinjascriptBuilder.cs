using Nt.Core.DependencyInjection;
using Nt.Scripts.Services;

namespace Nt.Scripts.MasterScripts
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
