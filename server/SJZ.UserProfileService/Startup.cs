using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SJZ.UserProfile.Repository;

namespace SJZ.UserProfileService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(sp => new Neo4jConfig
            {
                Uri = Environment.GetEnvironmentVariable("NEO4J_URI"),
                Username = Environment.GetEnvironmentVariable("NEO4J_USERNAME"),
                Password = Environment.GetEnvironmentVariable("NEO4J_PASSWORD")
            });

            services.AddScoped<IUserRepository, UserRepository>();

            services.AddHealthChecks();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "User Profile API" });
            });

            // services.AddGrpc(options => options.EnableDetailedErrors = true);
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "User Profile API v1"));

            app.UseHealthChecks(new PathString("/health"));
            //app.UseHttpsRedirection();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                // bypass Grpc
                // endpoints.MapGrpcService<Services.UserService>();
            });
        }
    }
}
