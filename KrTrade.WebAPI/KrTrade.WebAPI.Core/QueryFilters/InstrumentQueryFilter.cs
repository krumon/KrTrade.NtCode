using System;

namespace KrTrade.WebApp.Core.QueryFilters
{
    public class InstrumentQueryFilter
    {

        public int? UserId { get; set; }
        public DateTime? Date { get; set; }
        public string Description { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }

    }
}
