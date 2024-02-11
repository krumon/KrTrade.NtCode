namespace KrTrade.Nt.Services
{
    public class BarsOptions : NinjascriptServiceOptions
    {
        public const int DEFAULT_PERIOD = 12;
        public const int DEFAULT_DISPLACEMENT = 0;
        public int Period { get; set; }
        public int Displacement { get; set; }

        public BarsOptions()
        {
            Period = DEFAULT_PERIOD;
            Displacement = DEFAULT_DISPLACEMENT;
        }

        public BarsOptions(int period, int displacement)
        {
            Period = period;
            Displacement = displacement;
        }

        public CacheOptions CacheOptions { get; set; } = new CacheOptions();

    }
}
