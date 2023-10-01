using KrTrade.Nt.DI.DependencyInjection;

namespace KrTrade.Nt.Scripts.Services
{
    public interface IScriptBuilder
    {
        /// <summary>
        /// Gets the <see cref="IServiceCollection"/> where ninjascript services are configured.
        /// </summary>
        IServiceCollection Services { get; }

    }
}
