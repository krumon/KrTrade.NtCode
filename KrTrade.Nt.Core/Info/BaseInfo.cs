using System;

namespace KrTrade.Nt.Core.Info
{
    public abstract class BaseInfo : IInfo
    {
        //protected string _name = string.Empty;
        //public virtual string Name { get => string.IsNullOrEmpty(_name) ? "UNKNOWN" : _name; set => _name = value; }
        public string Name { get; set; }
    }
}
