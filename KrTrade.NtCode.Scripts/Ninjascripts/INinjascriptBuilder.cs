using KrTrade.NtCode.DependencyInjection;

namespace KrTrade.NtCode.Ninjascripts
{
    public interface INinjascriptBuilder
    {
        /// <summary>
        /// Gets the <see cref="IServiceCollection"/> where ninjascript services are configured.
        /// </summary>
        IServiceCollection Services { get; }

    }
}
