using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TweetAPP.Repositories;
using TweetAPP.Service;
using Microsoft.OpenApi.Models;
using TweetAPP.Exceptions;
using TweetApp.Entities;

namespace TweetAPP
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
            services.AddDbContext<TweetAppDBContext>();
            services.AddTransient<ITweetAppService, TweetAppService>();
            services.AddTransient<ITweetRepository, TweetRepository>();
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options =>
               options.AllowAnyOrigin()
                      .AllowAnyMethod()
                     .AllowAnyHeader()
               );

            });
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "TWEET APP",
                    Description = "Tweet API",
                });

            });
            services.AddMvc(
                config =>
                {
                    config.Filters.Add(typeof(CustomException));
                }
            );
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors("AllowOrigin");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tweet APP");
            });
        }
    }
}
