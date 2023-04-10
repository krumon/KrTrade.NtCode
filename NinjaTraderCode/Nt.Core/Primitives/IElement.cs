namespace Nt.Core
{
    /// <summary>
    /// The interfece for any ninjascript element.
    /// </summary>
    public interface IElement
    {

        /// <summary>
        /// The element id or index
        /// </summary>
        int Idx { get; set; }

        /// <summary>
        /// Free the unmagnament memory of any element.
        /// </summary>
        void Dispose();

    }
}
