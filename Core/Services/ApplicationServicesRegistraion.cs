using Microsoft.Extensions.DependencyInjection;
using Services.Abstractions;
using Services.MappingProfiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public static class ApplicationServicesRegistraion
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IServicesManager, ServiceManager>();
            services.AddAutoMapper(config =>
            {
                config.AddMaps(typeof(AssemblyReference1).Assembly);
            });
            return services;
        }
    }
}
