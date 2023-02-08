using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using ReadingEnhancer.Application.Services;
using ReadingEnhancer.Application.Services.Interfaces;
using ReadingEnhancer.Common.Handlers;
using ReadingEnhancer.DataAccess.Configurations;
using ReadingEnhancer.DataAccess.Repositories;
using ReadingEnhancer.Domain.Repositories;

namespace ReadingEnhancer.IoC
{
    public static class DependencyInjection
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IEnhancedTextRepository, EnhancedTextRepository>();
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IEnhancedTextService, EnhancedTextService>();
            services.AddHttpClient<IEnhancedTextService, EnhancedTextService>();
            services.AddScoped<IUserService, UserService>();
        }

        public static void RegisterSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "ReadingEnhancer.API", Version = "v1"});
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
            });
        }

        public static void AddControllersService(this IServiceCollection services)
        {
            services.AddControllers(options => options.Filters.Add(typeof(ValidateDataToCustomResponse)))
                .AddNewtonsoftJson(o => { o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore; });
            services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });
        }

        public static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DatabaseSettings>(
                configuration.GetSection("ReadingEnhancerDatabase"));
        }

        public static void AddAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(option =>
                {
                    option.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidAudience = configuration["Jwt:Audience"],
                        IssuerSigningKey =
                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("Jwt:Key").Value))
                    };
                });
        }
    }
}