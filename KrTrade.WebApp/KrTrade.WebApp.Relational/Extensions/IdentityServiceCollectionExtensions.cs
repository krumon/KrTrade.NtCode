using KrTrade.WebApp.Relational.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace KrTrade.WebApp.Relational.Extensions
{
    
    public static class IdentityServiceCollectionExtensions
    {
        public static IServiceCollection AddKrTradeIdentity(this IServiceCollection services)
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

            //// AddIdentity adds cookie based authentication
            //// Adds scoped classes for things like UserManager, SignInManager, PasswordHashers etc..
            //// NOTE: Automatically adds the validated user from a cookie to the HttpContext.User
            //// https://github.com/aspnet/Identity/blob/85f8a49aef68bf9763cd9854ce1dd4a26a7c5d3c/src/Identity/IdentityServiceCollectionExtensions.cs
            //builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
            //    // Adds UserStore and RoleStore from this context
            //    // That are consumed by the UserManager and RoleManager
            //    // https://github.com/aspnet/Identity/blob/dev/src/EF/IdentityEntityFrameworkBuilderExtensions.cs
            //    .AddEntityFrameworkStores<ApplicationDbContext>()

            //    // Adds a provider that generates unique keys and hashes for things like
            //    // forgot password links, phone number verification codes etc...
            //    .AddDefaultTokenProviders();

            //// Force Identity's security stamp to be validated every minute.
            //builder.Services.Configure<SecurityStampValidatorOptions>(o =>
            //                   o.ValidationInterval = TimeSpan.FromMinutes(1));

            //builder.Services.Configure<PasswordHasherOptions>(option =>
            //{
            //    option.IterationCount = 12000;
            //});

            //builder.Services.Configure<IdentityOptions>(options =>
            //{
            //    // Default Lockout settings.
            //    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            //    options.Lockout.MaxFailedAccessAttempts = 5;
            //    options.Lockout.AllowedForNewUsers = true;
            //    // Default Password settings.
            //    options.Password.RequireDigit = true;
            //    options.Password.RequireLowercase = true;
            //    options.Password.RequireNonAlphanumeric = true;
            //    options.Password.RequireUppercase = true;
            //    options.Password.RequiredLength = 6;
            //    options.Password.RequiredUniqueChars = 1;
            //    // Default SignIn settings.
            //    options.SignIn.RequireConfirmedEmail = false;
            //    options.SignIn.RequireConfirmedPhoneNumber = false;
            //    // Default User settings.
            //    options.User.AllowedUserNameCharacters =
            //            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            //    options.User.RequireUniqueEmail = false;
            //});

            //// Add cookies configuration. This method should be called after AddIdentity or AddDefaultIdentity
            //builder.Services.ConfigureApplicationCookie(options =>
            //{
            //    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
            //    options.Cookie.Name = "YourAppCookieName";
            //    options.Cookie.HttpOnly = true;
            //    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
            //    options.LoginPath = "/Identity/Account/Login";
            //    // ReturnUrlParameter requires 
            //    //using Microsoft.AspNetCore.Authentication.Cookies;
            //    options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
            //    options.SlidingExpiration = true;
            //});


            return services;

        }
    }
}
