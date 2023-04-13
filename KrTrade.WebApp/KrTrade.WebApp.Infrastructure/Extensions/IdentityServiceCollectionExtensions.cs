using KrTrade.WebApp.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace KrTrade.WebApp.Infrastructure.Extensions
{
    
    public static class IdentityServiceCollectionExtensions
    {
        public const string defaultConnectionString = @"Server=DESKTOP-J9DFBPR\KRUMONET;Database=KrTradeDB;User Id=sa;Password=KrumonTrade-20";

        public static IServiceCollection AddDbContexts(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<KrTradeDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            return services;

        }

        //public static IServiceCollection AddDefaultIdentity<TUser>(this IServiceCollection services)
        //    where TUser : IdentityUser
        //{
        //    services.AddAuthentication(o =>
        //    {
        //        o.DefaultScheme = IdentityConstants.ApplicationScheme;
        //        o.DefaultSignInScheme = IdentityConstants.ExternalScheme;
        //    })
        //    .AddIdentityCookies(o => { });

        //    services.AddIdentityCore<TUser>(o =>
        //    {
        //        o.Stores.MaxLengthForKeys = 128;
        //        o.SignIn.RequireConfirmedEmail = true;
        //    })
        //    .AddDefaultUI()
        //    .AddDefaultTokenProviders();

        //    return services;
        //}
    }
}
