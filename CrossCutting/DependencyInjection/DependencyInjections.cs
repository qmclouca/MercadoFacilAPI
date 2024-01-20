using Domain.Interfaces.Services;
using Domain.Profiles;
using Domain.Services;
using Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Infrastructure.Interfaces;
using Infrastructure.Services;

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
            services.AddTransient<IShareService, ShareService>();
            #endregion Business Entities

            #region Automapper
            services.AddAutoMapper(typeof(UserProfile));
            services.AddAutoMapper(typeof(AddressProfile));
            services.AddAutoMapper(typeof(UserAddressProfile));
            #endregion Automapper

            #region API Configurations
            services.AddTransient<IBrapiService, BrapiService>();
            #endregion API Configurations
            return services;
        }
    }
}