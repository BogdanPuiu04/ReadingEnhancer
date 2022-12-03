using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using ReadingEnhancer.Application.Services;
using ReadingEnhancer.Application.Services.Interfaces;
using ReadingEnhancer.DataAccess.Configurations;
using ReadingEnhancer.DataAccess.Repositories;
using ReadingEnhancer.Domain.Repositories;

namespace ReadingEnhancer.IoC
{
    public static class DependencyInjection
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IEnhancedTextRepository, EnhancedTextRepository>();
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IEnhancedTextService, EnhancedTextService>();
            services.AddHttpClient<IEnhancedTextService, EnhancedTextService>(client =>
            {
                client.BaseAddress = new Uri("https://bionic-reading1.p.rapidapi.com/convert");
            });
        }

        public static void RegisterSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "ReadingEnhancer.API", Version = "v1"}));
        }

        public static void AddControllersService(this IServiceCollection services)
        {
            services.AddControllers();
            //services.AddValidatorsFromAssemblyContaining<LabelRequestValidator>();
            services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });
        }

        public static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DatabaseSettings>(
                configuration.GetSection("ReadingEnhancerDatabase"));
        }
    }
}