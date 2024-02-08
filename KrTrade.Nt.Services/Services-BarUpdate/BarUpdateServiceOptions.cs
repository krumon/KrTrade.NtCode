namespace KrTrade.Nt.Services
{
    public class BarUpdateServiceOptions : NinjascriptServiceOptions
    {

        /// <summary>
        /// Gets the number of bars of the service. 
        /// </summary>
        public int Period { get; internal set; }

        /// <summary>
        /// Gets the bars ago of the service respect <see cref="IBarsService"/>. 
        /// The most recent bar is 0.
        /// </summary>
        public int Displacement { get; internal set; }

    }
}
