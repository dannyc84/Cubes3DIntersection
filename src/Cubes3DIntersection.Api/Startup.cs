using Cubes3DIntersection.Core.Repository;
using Cubes3DIntersection.Infrastructure.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AutoMapper;
using Cubes3DIntersection.Application.Interfaces;
using Cubes3DIntersection.Application.Services;
using Cubes3DIntersection.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Cubes3DIntersection.Infrastructure.Logging;
using Microsoft.OpenApi.Models;

namespace Cubes3DIntersection.Application
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureCubes3DIntersectionServices(services);

            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                    options.JsonSerializerOptions.PropertyNamingPolicy = null;
                    options.JsonSerializerOptions.IgnoreNullValues = true;
                    options.JsonSerializerOptions.IgnoreReadOnlyProperties = true;
                    options.JsonSerializerOptions.WriteIndented = true;
                });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Cubes 3D Intersection Api", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private void ConfigureCubes3DIntersectionServices(IServiceCollection services)
        {
            // Add Infrastructure Layer
            ConfigureDatabases(services);
            services.AddScoped(typeof(IGenericRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

            // Add Application Layer
            services.AddScoped<ICube3DIntersectionService, Cube3DIntersectionService>();
            services.AddScoped<ICube3DService, Cube3DService>();
            services.AddScoped<IPoint3DService, Point3DService>();
            services.AddScoped<IEdgeService, EdgeService>();

            // Add Web Layer
            services.AddAutoMapper(typeof(Startup));
        }

        public void ConfigureDatabases(IServiceCollection services)
        {
            // use in-memory database
            //services.AddDbContext<Cube3DIntersectionDbContext>(c =>
            //    c.UseInMemoryDatabase("Cubes3DIntersectionConnection")
            //    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

            // use real database
            services.AddDbContext<Cube3DIntersectionDbContext>(c =>
                c.UseSqlServer(Configuration.GetConnectionString("Cubes3DIntersectionConnection"))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
        }
    }
}
