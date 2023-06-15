using KrTrade.WebApp.Core.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KrTrade.WebApp.Relational.Extensions
{

    public static class OptionsServiceCollectionExtensions
    {
        public static IServiceCollection AddKrTradeOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<PaginationOptions>(options => configuration.GetSection("Pagination").Bind(options));
            return services;

        }
    }
}
