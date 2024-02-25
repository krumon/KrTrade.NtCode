namespace KrTrade.Nt.Services
{
    public class BarUpdateServiceOptions : NinjascriptServiceOptions
    {

        ///// <summary>
        ///// Gets the number of bars of the service. 
        ///// </summary>
        //public int Period { get; set; } = Cache.DEFAULT_PERIOD;

        ///// <summary>
        ///// Gets the bars ago of the service respect <see cref="IBarsService"/>. 
        ///// The most recent bar is 0.
        ///// </summary>
        //public int Displacement { get; set; } = Cache.DEFAULT_DISPLACEMENT;

        ///// <summary>
        ///// Gets the number of elements to store in cache, before will be removed forever.
        ///// </summary>
        //public int LengthOfRemovedValuesCache { get; set; } = Cache.DEFAULT_OLD_VALUES_CAPACITY;

        /// <summary>
        /// Gets the index of the bars thats raised the updated signal.
        /// </summary>
        public int BarsIndex { get; set; } = 0;

        /// <summary>
        /// Create <see cref="BarUpdateServiceOptions"/> default instance.
        /// </summary>
        public BarUpdateServiceOptions()
        {
        }

        /// <summary>
        /// Create <see cref="BarUpdateServiceOptions"/> instance with specified properties.
        /// </summary>
        public BarUpdateServiceOptions(int barsIndex)
        {
            BarsIndex = barsIndex;
        }
        
        ///// <summary>
        ///// Create <see cref="BarUpdateServiceOptions"/> instance with specified properties.
        ///// </summary>
        ///// <param name="period">The specified period.</param>
        ///// <param name="displacement">The specified displacement.</param>
        ///// <param name="lengthOfRemovedValuesCache">The specified length of removed values cache.</param>
        //public BarUpdateServiceOptions(int period, int displacement, int lengthOfRemovedValuesCache, int barsIndex)
        //{
        //    Period = period;
        //    Displacement = displacement;
        //    LengthOfRemovedValuesCache = lengthOfRemovedValuesCache;
        //    BarsIndex = barsIndex;
        //}

    }
}
