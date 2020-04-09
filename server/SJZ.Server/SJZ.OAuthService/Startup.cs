using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace SJZ.OAuthService
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>  options.AddDefaultPolicy(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

            services.AddIdentityServer()
                .AddInMemoryClients(Config.Clients)
                .AddInMemoryApiResources(Config.Apis)
                .AddInMemoryIdentityResources(Config.IdentityResources)
                .AddDeveloperSigningCredential();

            services.AddAuthentication()
                .AddGitHub(options =>
                {
                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                    options.ClientId = Environment.GetEnvironmentVariable("GITHUB_CLIENTID");
                    options.ClientSecret = Environment.GetEnvironmentVariable("GITHUB_CLIENTSECRET");

                    options.CallbackPath = new PathString("/oauth/github");
                    options.SaveTokens = true;
                    options.Events = new OAuthEvents()
                    {
                        OnRemoteFailure = loginFailureHandler =>
                        {
                            var authProperties = options.StateDataFormat.Unprotect(loginFailureHandler.Request.Query["state"]);
                            loginFailureHandler.Response.Redirect("/login");
                            loginFailureHandler.HandleResponse();
                            return Task.FromResult(0);
                        }
                    };
                })
                .AddLinkedIn(options =>
                {
                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                    options.ClientId = Environment.GetEnvironmentVariable("LINKEDIN_CLIENTID");
                    options.ClientSecret = Environment.GetEnvironmentVariable("LINKEDIN_CLIENTSECRET");

                    options.CallbackPath = new PathString("/oauth/linkedin");
                    options.SaveTokens = true;
                    options.Events = new OAuthEvents()
                    {
                        OnRemoteFailure = loginFailureHandler =>
                        {
                            var authProperties = options.StateDataFormat.Unprotect(loginFailureHandler.Request.Query["state"]);
                            loginFailureHandler.Response.Redirect("/login");
                            loginFailureHandler.HandleResponse();
                            return Task.FromResult(0);
                        }
                    };
                });

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "OAuth API" });
            });

            services.AddMvcCore().AddApiExplorer();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "OAuth API v1"));

            app.UseStaticFiles();

            app.UseRouting();

            app.UseCors();
            app.UseAuthentication();
            app.UseIdentityServer();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
