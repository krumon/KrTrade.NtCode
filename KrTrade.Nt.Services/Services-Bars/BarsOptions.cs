namespace KrTrade.Nt.Services
{
    public class BarsOptions : NinjascriptServiceOptions
    {
        //public const int DEFAULT_PERIOD = 12;
        //public const int DEFAULT_DISPLACEMENT = 0;
        //public int Period { get; set; }
        //public int Displacement { get; set; }

        //public BarsOptions()
        //{
        //}

        //public BarsOptions(int period, int displacement)
        //{
        //    Period = period;
        //    Displacement = displacement;
        //}

        public CacheServiceOptions CacheServiceOptions { get; set; } = new CacheServiceOptions();

    }
}
