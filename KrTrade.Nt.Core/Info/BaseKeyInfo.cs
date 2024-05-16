namespace KrTrade.Nt.Core.Info
{
    public abstract class BaseKeyInfo : BaseKey, IKeyInfo
    {
        //private string _name = string.Empty;
        //protected abstract string GetName();
        //public virtual string Name { get => string.IsNullOrEmpty(_name) ? Key : _name; set => _name = value; }
        public string Name { get; set; }
    }
}
