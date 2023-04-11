using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace KrTrade.WebApp.Infrastructure.Extensions
{
    public static class IdentityServiceCollectionExtensions
    {

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
