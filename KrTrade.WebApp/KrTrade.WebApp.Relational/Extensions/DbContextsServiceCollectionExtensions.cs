using KrTrade.WebApp.Relational.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KrTrade.WebApp.Relational.Extensions
{

    public static class DbContextsServiceCollectionExtensions
    {
        public static IServiceCollection AddKrTradeDbContexts(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<KrTradeDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            return services;

        }
    }
}
