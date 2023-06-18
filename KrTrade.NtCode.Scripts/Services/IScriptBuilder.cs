using KrTrade.NtCode.DependencyInjection;

namespace KrTrade.NtCode.Services
{
    public interface IScriptBuilder
    {
        /// <summary>
        /// Gets the <see cref="IServiceCollection"/> where ninjascript services are configured.
        /// </summary>
        IServiceCollection Services { get; }

    }
}
