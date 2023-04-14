using System;
using System.Threading.Tasks;

namespace KrTrade.WebApp.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IInstrumentsRepository InstrumentRepository { get; }
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
