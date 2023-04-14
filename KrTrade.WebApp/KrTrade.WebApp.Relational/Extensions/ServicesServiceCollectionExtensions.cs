using KrTrade.WebApp.Core.Interfaces;
using KrTrade.WebApp.Services.Trading;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace KrTrade.WebApp.Relational.Extensions
{

    public static class ServicesServiceCollectionExtensions
    {
        public static IServiceCollection AddKrTradeServices(this IServiceCollection services)
        {
            services.AddTransient<IInstrumentsService, InstrumentsService>();
            //services.AddTransient<ISecurityService, SecurityService>();
            //services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            //services.AddTransient<IUnitOfWork, UnitOfWork>();
            //services.AddSingleton<IPasswordService, PasswordService>();
            //services.AddSingleton<IUriService>(provider =>
            //{
            //    var accesor = provider.GetRequiredService<IHttpContextAccessor>();
            //    var request = accesor.HttpContext.Request;
            //    var absoluteUri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
            //    return new UriService(absoluteUri);
            //});
            return services;

        }
    }
}
