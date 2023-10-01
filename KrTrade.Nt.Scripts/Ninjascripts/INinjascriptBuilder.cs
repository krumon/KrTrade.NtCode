using KrTrade.Nt.DI.DependencyInjection;

namespace KrTrade.Nt.Scripts.Ninjascripts
{
    public interface INinjascriptBuilder
    {
        /// <summary>
        /// Gets the <see cref="IServiceCollection"/> where ninjascript services are configured.
        /// </summary>
        IServiceCollection Services { get; }

    }
}
