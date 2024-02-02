using RpaAeC.Data.Repositories;
using RpaAeC.Data;
using RpaAeC.Domain.Interfaces;
using RpaAeC.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace RpaAeC.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection ConfiureServices(this IServiceCollection services)
        {
            return services
                .AddDatabase()
                .AddDI();
        }

        private static IServiceCollection AddDatabase(this IServiceCollection services)
        {
            return services
                .AddDbContext<AppDbContext>(options =>
                {
                    options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=AeC;Trusted_Connection=True;");
                });
        }

        private static IServiceCollection AddDI(this IServiceCollection services)
        {
            return services
                .AddScoped<ISearchSearchTrainingService, SearchTrainingService>()
                .AddTransient<ISearchResultRepository, SearchResultRepository>();
        }
    }
}
