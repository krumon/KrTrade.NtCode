using System;
using System.Threading.Tasks;

namespace KrTrade.WebApp.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IInstrumentRepository InstrumentRepository { get; }
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
