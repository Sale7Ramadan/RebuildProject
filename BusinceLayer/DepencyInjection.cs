using BusinceLayer.Interfaces;
using BusinceLayer.Services;
using DataAccessLayer.Entities;
using BusinceLayer.EntitiesDTOS;
using Microsoft.Extensions.DependencyInjection;

namespace BusinceLayer
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBusinessLayer(this IServiceCollection services)
        {
            services.AddScoped(typeof(IBaseService<,,,>), typeof(BaseService<,,,>));

            services.AddScoped<IBaseService<User, UserDto, CreateUserDto, UpdateUserDto>, UserService>();

            return services;
        }
    }
}