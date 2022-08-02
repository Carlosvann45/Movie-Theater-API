using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Movie.Theater.Enterprises.API.Jwt;
using Movie.Theater.Enterprises.API.Mapper;
using Movie.Theater.Enterprises.Models.Misc;
using Movie.Theater.Enterprises.Providers;
using Movie.Theater.Enterprises.Repos.Context;
using Movie.Theater.Enterprises.Utilities.Configurations;
using Movie.Theater.Enterprises.Utilities.ExceptionHandler;
using Movie.Theater.Enterprises.Utilities.Jwt;

namespace Movie.Theater.Enterprises.API
{
    public class Startup
    {
        public Startup(IHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }


        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime and adds services to the container.
        /// </summary>
        /// <param name="services">service collection to configure</param>
        public void ConfigureServices(IServiceCollection services)
        {
            //Registers services config file along with all controllers, services, repositories, and context's
            services.AddDataServices(Configuration);
            services.AddProviders();

            // Adds mapper profile
            services.AddAutoMapper(typeof(MapperProfile));

            // configure appsettings
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            // scope utility file
            services.AddScoped<IJwtUtility, JwtUtility>();

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                // Registers the Swagger generator, defining 1 or more Swagger documents.
                c.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Version = "v1",
                    Title = "Movie Theater Enterprises API",
                    Description = "A restful API that provides request for a streamlined Movie Theater application fo users to view and purchase tickets for a movie.",
                    TermsOfService = new Uri("https://www.example.com"),
                    Contact = new OpenApiContact()
                    {
                        Name = "Movie Theater Enterprises",
                        Email = "MovieTheaterEnterprises@example.com",
                        Url = new Uri("https://www.example.com")
                    },
                    License = new OpenApiLicense()
                    {
                        Name = "Movie Theater Enterprises API License",
                        Url = new Uri("https://www.example.com")
                    }
                });
            });

            // enables cors
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

        }

        /// <summary>
        /// This method gets called by the runtime and configures the HTTP request pipeline.
        /// </summary>
        /// <param name="app">application builder to configure</param>
        /// <param name="env">web host environment to configure</param>
        /// <param name="db"> database to interact with</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, MovieTheaterDb db)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MovieTheaterEnterprisesApi v1"));

                // enables cors
                app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

                // Resets data on API startup
                db.Database.ExecuteSqlRaw("DROP SCHEMA public CASCADE; CREATE SCHEMA public;");

            }

            db.Database.EnsureCreated();

            // handles exceptions thrown in application
            app.UseMiddleware(typeof(ExceptionHandlingMiddleware));

            // handles jwt tokens being passed in the header
            app.UseMiddleware<JwtMiddleware>();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }
}
