using KrTrade.WebApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace KrTrade.WebApp.Infrastructure.Data
{
    public class KrTradeDbContext : DbContext
    {

        public virtual DbSet<Instrument> Instruments { get; set; }

        public KrTradeDbContext(DbContextOptions<KrTradeDbContext> options) : base(options)
        {
            //var s = this.Database.GetDbConnection();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-VFT7HDS\\SQLEXPRESS;Initial Catalog=KrTradeDB;Integrated Security=True; Trusted_Connection=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
