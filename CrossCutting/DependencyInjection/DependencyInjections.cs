using Domain.Interfaces.Services;
using Domain.Profiles;
using Domain.Services;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Interfaces;
using Infrastructure.Services;
using Infrastructure.Utils;

namespace CrossCutting.DependencyInjection
{
    public static class DependencyInjections
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {            
            #region Business Entities
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IAddressService, AddressService>();
            services.AddTransient<IUserAddressService, UserAddressService>(); 
            services.AddTransient<IShareService, ShareService>();
            services.AddTransient<IAuthService, AuthService>();
            #endregion Business Entities

            #region Automapper
            services.AddAutoMapper(typeof(UserProfile));
            services.AddAutoMapper(typeof(AddressProfile));
            services.AddAutoMapper(typeof(UserAddressProfile));
            #endregion Automapper

            #region API Configurations
            services.AddTransient<IBrapiService, BrapiService>();
            services.AddTransient<IPaginationService, PaginationService>();
            #endregion API Configurations            

            return services;
        }
    }
}