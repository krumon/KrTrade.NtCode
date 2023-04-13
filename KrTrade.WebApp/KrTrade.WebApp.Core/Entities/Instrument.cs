namespace KrTrade.WebApp.Core.Entities
{
    public class Instrument
    {

        public string InstrumentId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Currency { get; set; }
        public string TickSize { get; set; }

    }
}
