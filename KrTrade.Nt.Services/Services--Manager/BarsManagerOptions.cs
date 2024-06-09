namespace KrTrade.Nt.Services
{
    public class BarsManagerOptions : NinjascriptServiceOptions
    {

        // Default series options
        public int DefaultCachesCapacity {  get; set; } = Core.Elements.Series.DEFAULT_CAPACITY;
        public int DefaultRemovedCachesCapacity {  get; set; } = Core.Elements.Series.DEFAULT_OLD_VALUES_CAPACITY;

    }
}
