using KrTrade.WebApp.Core.Entities;
using KrTrade.WebApp.Services.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace KrTrade.WebApp.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        /// <summary>
        /// The settings for the application
        /// </summary>
        public DbSet<Setting> Settings { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // Fluent API

            modelBuilder.Entity<Setting>().HasIndex(a => a.Name);
        }
    }
}
