using System;

namespace Nt.Core.Data
{
    /// <summary>
    /// Represents consts, fields and properties of the Ninjatrader user configuration.
    /// </summary>
    public class UserSettings
    {

        #region Consts

        /// <summary>
        /// The root of all paths
        /// </summary>
        public const string NinjatraderCustomPath = @"C:\Users\Usuario\Documents\NinjaTrader 8";

        /// <summary>
        /// The trading hours templates path
        /// </summary>
        public const string NinjatraderTradingHoursPath = @"C:\Users\Usuario\Documents\NinjaTrader 8\templates\TradingHours";

        /// <summary>
        /// The import files path
        /// </summary>
        public const string NinjatraderImportPath = @"C:\Users\Usuario\Documents\NinjaTrader 8\import";

        /// <summary>
        /// The export files path
        /// </summary>
        public const string NinjatraderExportPath = @"C:\Users\Usuario\Documents\NinjaTrader 8\export";

        /// <summary>
        /// Display name for anchor.
        /// </summary>
        public const string AnchorDisplayName = "Y";

        /// <summary>
        /// Display name for anchor.
        /// </summary>
        public const string AnchorYDisplayName = "Price";

        /// <summary>
        /// Display name for anchor.
        /// </summary>
        public const string AnchorXDisplayName = "Time";

        /// <summary>
        /// Display name for start anchor.
        /// </summary>
        public const string AnchorStartDisplayName = "Configure Y";

        /// <summary>
        /// Display name for end anchor.
        /// </summary>
        public const string AnchorEndDisplayName = "End Y";

        /// <summary>
        /// The root of all paths
        /// </summary>
        public const string LabelLineDisplayName = "Label Line";

        /// <summary>
        /// The root of all paths
        /// </summary>
        public const string PriceLineDisplayName = "Price Line";

        /// <summary>
        /// The root of all paths
        /// </summary>
        public const string TimeLineDisplayName = "Time Line";

        #endregion

        #region Fields

        #endregion

        #region Properties

        /// <summary>
        /// The user <see cref="TimeZoneInfo"/> configure in the platafform.
        /// </summary>
        public TimeZoneInfo UserTimeZoneInfo { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Create a default instance of the <see cref="UserSettings"/> class.
        /// </summary>
        public UserSettings()
        {

        }

        #endregion

    }
}
