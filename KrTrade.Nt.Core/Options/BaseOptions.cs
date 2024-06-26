namespace KrTrade.Nt.Core.Options
{
    /// <summary>
    /// Provides the options for any object.
    /// </summary>
    public abstract class BaseOptions : IOptions
    {
        public bool IsEnable { get; set; } = true;
    }
}
