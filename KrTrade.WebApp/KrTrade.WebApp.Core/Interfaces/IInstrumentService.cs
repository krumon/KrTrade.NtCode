using KrTrade.WebApp.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KrTrade.WebApp.Core.Interfaces
{
    public interface IInstrumentService
    {
        IEnumerable<Instrument> GetInstruments();
        Task<Instrument> GetInstrument(int id);
        Task InsertInstrument(Instrument instrument);
        Task<bool> UpdateInstrument(Instrument instrument);
        Task<bool> DeleteInstrument(int id);
    }
}
