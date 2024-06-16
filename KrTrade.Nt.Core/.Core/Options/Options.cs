namespace KrTrade.Nt.Core
{
    /// <summary>
    /// Provides the options for any object.
    /// </summary>
    public abstract class Options : IOptions
    {
        public bool IsEnable { get; set; } = true;
    }
}
