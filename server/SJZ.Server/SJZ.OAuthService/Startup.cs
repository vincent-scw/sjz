using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Net.Client;
using IdentityServer4;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SJZ.UserProfileService;

namespace SJZ.OAuthService
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options => options.AddDefaultPolicy(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

            services.AddSingleton(sp =>
            {
                var channel = GrpcChannel.ForAddress(Environment.GetEnvironmentVariable("UPS_SVC"),
                    new GrpcChannelOptions
                    {
                        HttpClient = new HttpClient(new HttpClientHandler
                        {
                            // Trust certificates for debugging purpose
                            ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                        }),
                        LoggerFactory = LoggerFactory.Create(logging =>
                        {
                            logging.AddConsole();
                            logging.SetMinimumLevel(LogLevel.Information);
                        })
                    });

                return new UserSvc.UserSvcClient(channel);
            });

            services.AddTransient<IProfileService, ProfileService>();

            services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseSuccessEvents = true;
                options.UserInteraction.LoginUrl = "/login.html";
                options.UserInteraction.LogoutUrl = "/Account/Signout";
                options.UserInteraction.ConsentUrl = "/Consent";
            })
                .AddInMemoryClients(Config.Clients)
                .AddInMemoryApiResources(Config.Apis)
                .AddInMemoryIdentityResources(Config.IdentityResources)
                .AddProfileService<ProfileService>()
                .AddDeveloperSigningCredential();

            services.AddAuthentication()
                .AddGitHub(options =>
                {
                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                    options.ClientId = Environment.GetEnvironmentVariable("GITHUB_CLIENTID");
                    options.ClientSecret = Environment.GetEnvironmentVariable("GITHUB_CLIENTSECRET");

                    options.CallbackPath = new PathString("/oauth/github");
                    options.SaveTokens = true;
                })
                .AddLinkedIn(options =>
                {
                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                    options.ClientId = Environment.GetEnvironmentVariable("LINKEDIN_CLIENTID");
                    options.ClientSecret = Environment.GetEnvironmentVariable("LINKEDIN_CLIENTSECRET");

                    options.CallbackPath = new PathString("/oauth/linkedin");
                    options.SaveTokens = true;
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
