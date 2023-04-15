using KrTrade.WebApp.Core.Entities;
using System;
using System.Threading.Tasks;

namespace KrTrade.WebApp.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IInstrumentsRepository InstrumentsRepository { get; }
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
