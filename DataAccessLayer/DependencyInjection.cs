using DataAccessLayer.Context;
using DataAccessLayer.Repositories;
using DataAccessLayer.Repositries;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccessLayer
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataAccessLayer(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped(typeof(IBaseRepositories<>), typeof(BaseRepository<>));

            return services;
        }
    }
}
