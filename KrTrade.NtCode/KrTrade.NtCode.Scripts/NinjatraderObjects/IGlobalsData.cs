using KrTrade.NtCode.Hosting;
using KrTrade.NtCode.Ninjascripts;
using System;

namespace KrTrade.NtCode.NinjatraderObjects
{
    /// <summary>
    /// Represents the properties and methods to create a default implementation of <see cref="GlobalsDataService"/>.
    /// </summary>
    public interface IGlobalsData
    {

        /// <summary>
        /// Gets the maximum available date for any ninjascript.
        /// </summary>
        DateTime MaxDate { get; }

        /// <summary>
        /// ets the minimum available date for any ninjascript.
        /// </summary>
        DateTime MinDate { get; }

        /// <summary>
        /// Gets the <see cref="TimeZoneInfo"/> configure in the platform.
        /// </summary>
        TimeZoneInfo UserConfigureTimeZoneInfo { get; }

        /// <summary>
        /// Gets the ninjatrader user data directory.
        /// </summary>
        string UserDataDir { get; }

        /// <summary>
        /// Gets the ninjatrader install directory.
        /// </summary>
        string InstallDir { get; }

        /// <summary>
        /// Gets the custom subdirectories created by the user in the <see cref="UserDataDir"/>.
        /// </summary>
        string[] CustomSubDirs { get; }

    }
}
