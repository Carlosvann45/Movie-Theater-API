using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Movie.Theater.Enterprises.Repos.Context;
using Movie.Theater.Enterprises.Repos.Interfaces;
using Movie.Theater.Enterprises.Repos.Repositories;

namespace Movie.Theater.Enterprises.Utilities.Configurations
{
    /// <summary>
    /// This class provides configuration options for repositories and context.
    /// </summary>
    public static class ServicesConfiguration
    {
        /// <summary>
        /// Connects to postgres and scopes context and all repositories
        /// </summary>
        /// <param name="services">services to configure</param>
        /// <param name="config">configuration to get connection string</param>
        /// <returns>configured services</returns>
        public static IServiceCollection AddDataServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<MovieTheaterDb>(options => options.UseNpgsql(config.GetConnectionString("MovieTheaterDb")));

            services.AddScoped<IMovieTheaterDb, MovieTheaterDb>();

            services.AddScoped<ICustomerRepository, CustomerRepository>();

            return services;
        }

    }
}
