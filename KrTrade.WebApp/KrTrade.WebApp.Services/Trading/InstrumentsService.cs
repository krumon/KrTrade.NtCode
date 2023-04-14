using KrTrade.WebApp.Core.Entities;
using KrTrade.WebApp.Core.Interfaces;

namespace KrTrade.WebApp.Services.Trading
{
    public class InstrumentsService : IInstrumentsService
    {

        public InstrumentsService()
        {

        }

        public Task<bool> DeleteInstrument(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Instrument> GetInstrument(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Instrument> GetInstruments()
        {
            throw new NotImplementedException();
        }

        public Task InsertInstrument(Instrument instrument)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateInstrument(Instrument instrument)
        {
            throw new NotImplementedException();
        }
    }
}
