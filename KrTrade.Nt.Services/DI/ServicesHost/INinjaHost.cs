using System;

namespace KrTrade.Nt.Services
{
    public interface INinjaHost
    {

        /// <summary>
        /// The programs configured services.
        /// </summary>
        IServiceProvider Services { get; }

    }
}
