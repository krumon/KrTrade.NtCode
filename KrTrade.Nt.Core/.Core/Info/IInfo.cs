namespace KrTrade.Nt.Core
{
    public interface IInfo : IHasKey<IInfo>
    {

        /// <summary>
        /// Gets or sets the name of the object.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the type of the <see cref="IInfo"/> objects.
        /// </summary>
        ElementType Type { get; set; }

    }
}
