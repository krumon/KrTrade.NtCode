using KrTrade.WebApp.Core.Entities;
using KrTrade.WebApp.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KrTrade.WebApp.Relational.Repositories
{
    public class BaseRepository<TDbContext, TEntity> : IRepository<TEntity> 
        where TDbContext : DbContext
        where TEntity : BaseEntity
    {
        private readonly TDbContext _context;
        private readonly DbSet<TEntity> _entities;

        public BaseRepository(TDbContext context)
        {
            _context = context;
            _entities = context.Set<TEntity>();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _entities.AsEnumerable();
        }

        public async Task<TEntity> GetById(int id)
        {
            TEntity? entity = await _entities.FindAsync(id);
            return entity;
        }

        public async Task Add(TEntity entity)
        {
            await _entities.AddAsync(entity);
        }

        public void Update(TEntity entity)
        {
            _entities.Update(entity);
        }

        public async Task Delete(int id)
        {
            TEntity? entity = await GetById(id);
            if (entity != null) 
                _entities.Remove(entity);
        }
    }
}
