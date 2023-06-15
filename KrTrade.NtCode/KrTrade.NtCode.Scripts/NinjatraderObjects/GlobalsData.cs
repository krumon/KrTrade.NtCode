using NinjaTrader.Core;
using Nt.Core.Logging;
using System;
using System.IO;

namespace KrTrade.NtCode.NinjatraderObjects
{
    public class GlobalsData : IGlobalsData
    {
        private readonly ILogger _logger;

        public DateTime MaxDate {get;protected set;}
        public DateTime MinDate { get; protected set; }
        public TimeZoneInfo UserConfigureTimeZoneInfo { get; protected set; }
        public string UserDataDir { get; protected set; }
        public string InstallDir { get; protected set; }
        public string[] CustomSubDirs { get; protected set; }

        public bool IsConfigured => throw new NotImplementedException();

        public GlobalsData(ILogger<GlobalsData> logger)
        {
            _logger = logger;

            Initialize();

            // TODO: Delete. Testing method.
            PrintDirectories();
        }

        public GlobalsData()
        {
            DesignInitialize();

            //// TODO: Delete. Testing method.
            //PrintDirectories();
        }

        protected virtual void Initialize()
        {
            MaxDate = Globals.MaxDate;
            MinDate = Globals.MinDate;
            UserConfigureTimeZoneInfo = Globals.GeneralOptions.TimeZoneInfo;
            UserDataDir = Globals.UserDataDir;
            InstallDir = Globals.InstallDir;
            CustomSubDirs = Globals.CustomSubDirs;
        }

        private void DesignInitialize()
        {
            MaxDate = DateTime.MaxValue;
            MinDate = DateTime.MinValue;
            UserConfigureTimeZoneInfo = TimeZoneInfo.Local;
            UserDataDir = Path.Combine(Directory.GetCurrentDirectory(), "User");
            InstallDir = Path.Combine(Directory.GetCurrentDirectory(), "Install");
            CustomSubDirs = null;
        }

        protected void PrintDirectories()
        {
            _logger.LogInformation("Install Directory: {0}", InstallDir);
            _logger.LogInformation("User Directory: {0}", UserDataDir);
            if (CustomSubDirs != null && CustomSubDirs.Length > 0)
                _logger.LogInformation("Subdirectory[0]: {0}", CustomSubDirs[0].ToString());
        }
    }
}

