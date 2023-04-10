using Nt.Core.Logging;
using System;
using System.IO;

namespace Nt.Scripts.NinjatraderObjects.Design
{
    public class DesignGlobalsData : GlobalsData
    {
        public DesignGlobalsData(ILogger<GlobalsData> logger) : base(logger)
        {
        }

        protected override void Initialize()
        {
            MaxDate = DateTime.MaxValue;
            MinDate = DateTime.MinValue;
            UserConfigureTimeZoneInfo = TimeZoneInfo.Local;
            UserDataDir = Path.Combine(Directory.GetCurrentDirectory(), "User");
            InstallDir = Path.Combine(Directory.GetCurrentDirectory(), "Install");
            CustomSubDirs = null;
        }
    }
}
