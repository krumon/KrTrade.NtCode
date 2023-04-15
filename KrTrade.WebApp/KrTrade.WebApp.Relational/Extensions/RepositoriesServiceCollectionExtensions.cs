using Microsoft.Extensions.DependencyInjection;

namespace KrTrade.WebApp.Relational.Extensions
{

    public static class RepositoriesServiceCollectionExtensions
    {
        public static IServiceCollection AddKrTradeRepositories(this IServiceCollection services)
        {
            //services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            //services.AddTransient<IUnitOfWork, UnitOfWork>();

            return services;

        }
    }
}
