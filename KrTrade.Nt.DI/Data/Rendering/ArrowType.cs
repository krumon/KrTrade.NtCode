namespace KrTrade.Nt.DI.Data
{
    /// <summary>
    /// Type of arrows.
    /// </summary>
    public enum ArrowType
    {

        /// <summary>
        /// Arrow with the base and the head with the same length.
        /// </summary>
        Normal,

        /// <summary>
        /// Arrow with the base more large than the head.
        /// </summary>
        Short,

        /// <summary>
        /// Arrow with the base more short than the head.
        /// </summary>
        Large,

        /// <summary>
        /// Flat arrow.
        /// </summary>
        Flat,

        /// <summary>
        /// Arrow with the base very much short than the head.
        /// </summary>
        VeryShort,

        /// <summary>
        /// Arrow with the base very much large than the head.
        /// </summary>
        ExtraLarge,

    }
}
