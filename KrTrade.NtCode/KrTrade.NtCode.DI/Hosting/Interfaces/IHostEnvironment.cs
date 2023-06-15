using KrTrade.NtCode.FileProviders;

namespace KrTrade.NtCode.Hosting
{
    /// <summary>
    /// Provides information about the hosting environment an application is running in.
    /// </summary>
    public interface IHostEnvironment
    {
        /// <summary>
        /// Gets or sets the name of the environment. The host automatically sets this property
        /// to the value of the "environment" key as specified in configuration.
        /// </summary>
        string EnvironmentName { get; set; }

        /// <summary>
        /// Gets or sets the name of the application. This property is automatically set
        /// by the host to the assembly containing the application entry point.
        /// </summary>
        string ApplicationName { get; set; }

        /// <summary>
        /// Gets or sets the absolute path to the directory that contains the application
        /// content files.
        /// </summary>
        string ContentRootPath { get; set; }

        /// <summary>
        /// Gets or sets an <see cref="IFileProvider"/> pointing at <see cref="IHostEnvironment.ContentRootPath"/>
        /// </summary>
        IFileProvider ContentRootFileProvider { get; set; }

        /// <summary>
        /// Indicates if the host is in design mode.
        /// </summary>
        bool IsInDesignMode { get; set; }
    }
}
