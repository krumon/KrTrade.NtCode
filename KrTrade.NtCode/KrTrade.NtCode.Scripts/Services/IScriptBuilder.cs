using Nt.Core.DependencyInjection;

namespace Nt.Scripts.Services
{
    public interface IScriptBuilder
    {
        /// <summary>
        /// Gets the <see cref="IServiceCollection"/> where ninjascript services are configured.
        /// </summary>
        IServiceCollection Services { get; }

    }
}
