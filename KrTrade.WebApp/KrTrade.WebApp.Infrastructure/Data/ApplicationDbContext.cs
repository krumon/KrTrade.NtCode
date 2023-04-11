using KrTrade.WebApp.Core.Entities;
using KrTrade.WebApp.Services.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;

namespace KrTrade.WebApp.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser,IdentityRole,string>
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
            base.OnModelCreating(modelBuilder);

            //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // Fluent API

            modelBuilder.Entity<Setting>().HasIndex(a => a.Name);

            //modelBuilder.Entity<IdentityUser>(b =>
            //{
            //    // Primary key
            //    b.HasKey(u => u.Id);

            //    // Indexes for "normalized" username and email, to allow efficient lookups
            //    b.HasIndex(u => u.NormalizedUserName).HasName("UserNameIndex").IsUnique();
            //    b.HasIndex(u => u.NormalizedEmail).HasName("EmailIndex");

            //    // Maps to the AspNetUsers table
            //    b.ToTable("AspNetUsers");

            //    // A concurrency token for use with the optimistic concurrency checking
            //    b.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();

            //    // Limit the size of columns to use efficient database types
            //    b.Property(u => u.UserName).HasMaxLength(256);
            //    b.Property(u => u.NormalizedUserName).HasMaxLength(256);
            //    b.Property(u => u.Email).HasMaxLength(256);
            //    b.Property(u => u.NormalizedEmail).HasMaxLength(256);

            //    // The relationships between User and other entity types
            //    // Note that these relationships are configured with no navigation properties

            //    // Each User can have many UserClaims
            //    b.HasMany<TUserClaim>().WithOne().HasForeignKey(uc => uc.UserId).IsRequired();

            //    // Each User can have many UserLogins
            //    b.HasMany<TUserLogin>().WithOne().HasForeignKey(ul => ul.UserId).IsRequired();

            //    // Each User can have many UserTokens
            //    b.HasMany<TUserToken>().WithOne().HasForeignKey(ut => ut.UserId).IsRequired();

            //    // Each User can have many entries in the UserRole join table
            //    b.HasMany<TUserRole>().WithOne().HasForeignKey(ur => ur.UserId).IsRequired();
            //});

            //modelBuilder.Entity<IdentityUserClaim>(b =>
            //{
            //    // Primary key
            //    b.HasKey(uc => uc.Id);

            //    // Maps to the AspNetUserClaims table
            //    b.ToTable("AspNetUserClaims");
            //});

            //modelBuilder.Entity<IdentityUserLogin>(b =>
            //{
            //    // Composite primary key consisting of the LoginProvider and the key to use
            //    // with that provider
            //    b.HasKey(l => new { l.LoginProvider, l.ProviderKey });

            //    // Limit the size of the composite key columns due to common DB restrictions
            //    b.Property(l => l.LoginProvider).HasMaxLength(128);
            //    b.Property(l => l.ProviderKey).HasMaxLength(128);

            //    // Maps to the AspNetUserLogins table
            //    b.ToTable("AspNetUserLogins");
            //});

            //modelBuilder.Entity<IdentityUserToken>(b =>
            //{
            //    // Composite primary key consisting of the UserId, LoginProvider and Name
            //    b.HasKey(t => new { t.UserId, t.LoginProvider, t.Name });

            //    // Limit the size of the composite key columns due to common DB restrictions
            //    b.Property(t => t.LoginProvider).HasMaxLength(maxKeyLength);
            //    b.Property(t => t.Name).HasMaxLength(maxKeyLength);

            //    // Maps to the AspNetUserTokens table
            //    b.ToTable("AspNetUserTokens");
            //});

            //modelBuilder.Entity<IdentityRole>(b =>
            //{
            //    // Primary key
            //    b.HasKey(r => r.Id);

            //    // Index for "normalized" role name to allow efficient lookups
            //    b.HasIndex(r => r.NormalizedName).HasName("RoleNameIndex").IsUnique();

            //    // Maps to the AspNetRoles table
            //    b.ToTable("AspNetRoles");

            //    // A concurrency token for use with the optimistic concurrency checking
            //    b.Property(r => r.ConcurrencyStamp).IsConcurrencyToken();

            //    // Limit the size of columns to use efficient database types
            //    b.Property(u => u.Name).HasMaxLength(256);
            //    b.Property(u => u.NormalizedName).HasMaxLength(256);

            //    // The relationships between Role and other entity types
            //    // Note that these relationships are configured with no navigation properties

            //    // Each Role can have many entries in the UserRole join table
            //    b.HasMany<TUserRole>().WithOne().HasForeignKey(ur => ur.RoleId).IsRequired();

            //    // Each Role can have many associated RoleClaims
            //    b.HasMany<TRoleClaim>().WithOne().HasForeignKey(rc => rc.RoleId).IsRequired();
            //});

            //modelBuilder.Entity<IdentityRoleClaim>(b =>
            //{
            //    // Primary key
            //    b.HasKey(rc => rc.Id);

            //    // Maps to the AspNetRoleClaims table
            //    b.ToTable("AspNetRoleClaims");
            //});

            //modelBuilder.Entity<IdentityUserRole>(b =>
            //{
            //    // Primary key
            //    b.HasKey(r => new { r.UserId, r.RoleId });

            //    // Maps to the AspNetUserRoles table
            //    b.ToTable("AspNetUserRoles");
            //});
        }
    }
}
