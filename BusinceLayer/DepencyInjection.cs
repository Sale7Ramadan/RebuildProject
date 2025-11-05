using BusinceLayer.Interfaces;
using BusinceLayer.Services;
using BusinceLayer.Interfaces;
using BusinceLayer.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BusinceLayer
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBusinessLayer(this IServiceCollection services)
        {
            services.AddScoped(typeof(IBaseService<,,,>), typeof(BaseService<,,,>));
            services.AddScoped<UserService>();
            return services;

        }
    }
}
