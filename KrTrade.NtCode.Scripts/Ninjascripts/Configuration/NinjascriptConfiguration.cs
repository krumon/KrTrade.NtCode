﻿using KrTrade.NtCode.Configuration;

namespace KrTrade.NtCode.Ninjascripts.Configuration
{
    internal sealed class NinjascriptConfiguration
    {
        public IConfiguration Configuration { get; }

        public NinjascriptConfiguration(IConfiguration configuration)
        {
            Configuration = configuration;
        }
    }
}