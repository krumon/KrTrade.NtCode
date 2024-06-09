﻿using KrTrade.Nt.Core.Data;

namespace KrTrade.Nt.Core.Elements
{

    public class ServiceCollectionInfo : BaseInfoCollection<IServiceInfo>, IServiceCollectionInfo
    {
        new public ServiceCollectionType Type { get => base.Type.ToServiceCollectionType(); set => base.Type = value.ToElementType(); }

        protected override string ToUniqueString()
        {
            throw new System.NotImplementedException();
        }
    }
}
