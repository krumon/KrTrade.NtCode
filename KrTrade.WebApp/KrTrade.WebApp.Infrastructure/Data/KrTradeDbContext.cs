using KrTrade.WebApp.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace KrTrade.WebApp.Infrastructure.Data
{
    public class KrTradeDbContext : DbContext
    {

        public virtual DbSet<Instrument> Instruments { get; set; }

        public KrTradeDbContext(DbContextOptions<KrTradeDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Instrument>(entity =>
            {
                entity.HasKey(x => x.InstrumentID);

                entity.Property(e => e.InstrumentID)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(true);

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.ToTable("Instrument");
                
            });
        }
    }
}
