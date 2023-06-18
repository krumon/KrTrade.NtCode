using System;

namespace KrTrade.NtCode.Services
{
    /// <summary>
    /// Represents a type that can create instances of <see cref="INinjascript"/>.
    /// </summary>
    public interface IScriptProvider : IDisposable
    {
        ///// <summary>
        ///// Creates a new <see cref="INinjascript"/> instance.
        ///// </summary>
        ///// <param name="categoryName">The category name for ninjascript to be created.</param>
        ///// <returns>The instance of <see cref="INinjascript"/> that was created.</returns>
        //INinjascript CreateNinjascript(string categoryName);
    }
}
