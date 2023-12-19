using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace CrossCutting.DependencyInjection.DependencyInjection
{
    public class ConfigureServices
    {
        public static void ConfigureDependenciesServices (IServiceCollection serviceCollection)
        {
            //serviceCollection.AddTransient<IUserService, UserService>();
        }
    }
}