using Microsoft.Extensions.DependencyInjection;
using Movie.Theater.Enterprises.Providers.Interfaces;
using Movie.Theater.Enterprises.Providers.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Theater.Enterprises.Providers
{
    /// <summary>
    /// This class provides configuration options for providers
    /// </summary>
    public static class ServicesConfiguration
    {

        /// <summary>
        /// Configures services and scopes all service classes and their interfaces
        /// </summary>
        /// <param name="services">services to configure</param>
        /// <returns></returns>
        public static IServiceCollection AddProviders(this IServiceCollection services)
        {
            services.AddScoped<ICustomerProvider, CustomerProvider>();

            return services;
        }

    }
}
