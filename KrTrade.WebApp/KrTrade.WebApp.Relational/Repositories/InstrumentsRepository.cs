using KrTrade.WebApp.Core.Entities;
using KrTrade.WebApp.Core.Interfaces;
using KrTrade.WebApp.Relational.Data;

namespace KrTrade.WebApp.Relational.Repositories
{
    public class InstrumentsRepository : BaseRepository<KrTradeDbContext, Instrument>, IInstrumentsRepository
    {
        public InstrumentsRepository(KrTradeDbContext context) : base(context)
        {
        }
    }
}
