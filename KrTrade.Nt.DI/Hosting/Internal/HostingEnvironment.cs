using KrTrade.Nt.DI.FileProviders;

namespace KrTrade.Nt.DI.Hosting.Internal
{
#pragma warning disable CS0618 // Type or member is obsolete
    /// <summary>
    /// This API supports infrastructure and is not intended to be used
    /// directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public class HostingEnvironment : IHostEnvironment
#pragma warning restore CS0618 // Type or member is obsolete
    {
        public string EnvironmentName { get; set; }

        public string ApplicationName { get; set; }

        public string ContentRootPath { get; set; }

        public IFileProvider ContentRootFileProvider { get; set; }

        public bool IsInDesignMode { get;set; }
    }
}
