namespace KrTrade.Nt.Core.Elements
{
    public interface IHasInfo
    {
        /// <summary>
        /// Gets the information of the object.
        /// </summary>
        IInfo Info { get; }

    }
    public interface IHasInfo<TInfo>
        where TInfo : IInfo
    {
        /// <summary>
        /// Gets the information of the object.
        /// </summary>
        TInfo Info { get; }

    }
}
