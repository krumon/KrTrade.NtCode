using System.Threading.Tasks;

namespace KrTrade.NtCode.DependencyInjection
{
    /// <summary>
    /// Provides a mechanism for releasing unmanaged resources asynchronously.
    /// </summary>
    public interface IAsyncDisposable
    {
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting
        /// unmanaged resources asynchronously.
        /// </summary>
        /// <returns></returns>
        ValueTask DisposeAsync();
    }
}
