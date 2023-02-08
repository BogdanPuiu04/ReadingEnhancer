using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ReadingEnhancer.Common;
using ReadingEnhancer.IoC;

namespace ReadingEnhancer
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public IConfiguration Configuration { get; }

       
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDatabase(Configuration);
            services.AddAuthentication(Configuration);
            services.AddControllersService();
            services.AddServices();

            services.AddRepositories();

            services.RegisterSwagger();
        }

        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(corsPolicyBuilder =>
                corsPolicyBuilder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
            );
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ReadingEnhancer.API v1"));

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseMiddleware<Middleware>();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}