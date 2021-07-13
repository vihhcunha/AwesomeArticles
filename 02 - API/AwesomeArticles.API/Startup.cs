using AwesomeArticles.Application.Mapper;
using AwesomeArticles.Application.Services.Implementations;
using AwesomeArticles.Application.Services.Interfaces;
using AwesomeArticles.CrossCutting.Documentation;
using AwesomeArticles.CrossCutting.Security;
using AwesomeArticles.CrossCutting.User;
using AwesomeArticles.Data;
using AwesomeArticles.Data.Repository;
using AwesomeArticles.Domain.Entities;
using AwesomeArticles.Domain.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace AwesomeArticles.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostEnvironment hostEnvironment)
        {
            Configuration = configuration;
            HostEnvironment = hostEnvironment;
        }

        public IConfiguration Configuration { get; }
        public IHostEnvironment HostEnvironment { get;  }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            var key = System.Text.Json.JsonSerializer.Deserialize<JsonWebKey>
                (File.ReadAllText
                    (Path.Combine
                        (Environment.CurrentDirectory, "mysecretkey.json")));

            services.AddCors();

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddControllers().AddNewtonsoftJson(opt => {
                opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                opt.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local;
                opt.SerializerSettings.DateFormatString = "yyyy'-'MM'-'dd'T'HH':'mm':'ss";
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddDbContext<AwesomeArticlesContext>(opt => {
                opt.UseSqlServer(Configuration.GetConnectionString("SqlConnection"))
                .LogTo(Console.WriteLine, LogLevel.Information);
            });


            #region RegisterServices

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IArticleRepository, ArticleRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IArticleService, ArticleService>();
            services.AddScoped<IAuthenticatedUser, AuthenticatedUser>();
            services.AddScoped<ITokenService, TokenService>();

            #endregion

            services.AddAutoMapper(typeof(DomainToViewModelMapping));

            services.AddHttpContextAccessor();
            
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = Path.Combine(@"..\..\", @"01 - Presentation\AwesomeArticlesWebApp\dist\AwesomeArticlesWebApp");
            });
            services.ConfigureSwagger();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwaggerConfig();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
            );

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            if (env.IsProduction())
            {
                app.UseSpaStaticFiles();
            }

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSpa(spa =>
            {
// To learn more about options for serving an Angular SPA from ASP.NET Core,
// see https://go.microsoft.com/fwlink/?linkid=864501

                 spa.Options.SourcePath = "AwesomeArticlesWebApp";

                if (env.IsDevelopment())
                {
                    //spa.UseAngularCliServer(npmScript: "start");
                    spa.UseProxyToSpaDevelopmentServer("http://localhost:4200/");
                    spa.Options.StartupTimeout = new TimeSpan(0, 5, 0);
                }
            });
        }
    }
}
