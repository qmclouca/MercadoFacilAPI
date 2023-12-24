using Domain.Interfaces.Services;
using Domain.Profiles;
using Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CrossCutting.DependencyInjection
{
    public static class DependencyInjections
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            #region Business Entities
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IAddressService, AddressService>();
            services.AddTransient<IUserAddressService, UserAddressService>();
            #endregion Business Entities

            #region Automapper
            services.AddAutoMapper(typeof(UserProfile));
            services.AddAutoMapper(typeof(AddressProfile));
            services.AddAutoMapper(typeof(UserAddressProfile));
            #endregion Automapper

            return services;
        }
    }
}