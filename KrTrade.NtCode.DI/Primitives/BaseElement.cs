namespace KrTrade.Nt.DI
{
    /// <summary>
    /// Base ninjatrader element
    /// </summary>
    public abstract class BaseElement : IElement
    {
        /// <summary>
        /// The element id or index
        /// </summary>
        public int Idx { get; set; }

        /// <summary>
        /// Free the unmagnament memory of any element.
        /// </summary>
        public virtual void Dispose() { }

    }
}
