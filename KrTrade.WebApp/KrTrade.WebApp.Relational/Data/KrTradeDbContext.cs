using KrTrade.WebApp.Core.Entities;
using KrTrade.WebApp.Services.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace KrTrade.WebApp.Relational.Data
{
    public class KrTradeDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {

        public DbSet<Instrument> Instruments => Set<Instrument>();
        public DbSet<Setting> Settings {get;set;}

        public KrTradeDbContext(DbContextOptions<KrTradeDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
