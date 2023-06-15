namespace KrTrade.WebApp.Core.Entities
{
    public class Instrument : BaseEntity
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public string Currency { get; set; }
        public string TickSize { get; set; }

    }
}
