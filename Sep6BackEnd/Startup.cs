using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Sep6BackEnd.BusinessLogic;
using Sep6BackEnd.BusinessLogic.Logic;
using Sep6BackEnd.DataAccess.DatabaseAccess;
using Sep6BackEnd.DataAccess.TMDBAccess;

namespace Sep6BackEnd
{
    public class Startup
    {
        private readonly string SpecificOrigin = "_specificOrigin";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "Sep6BackEnd", Version = "v1"});
            });

            services.AddSingleton(new Keys(Configuration["APIKEY"], Configuration["DBSKEY"]));
            services.AddScoped<TmdbApiRequestHandler>();
            services.AddScoped<TmdbAccess>();
            services.AddScoped<UsersRequestHandler>();
            services.AddScoped<DatabaseAccess>();
            services.AddScoped<StatisticHandler>();
            services.AddCors(cors =>
            {
                cors.AddPolicy(name:
                    SpecificOrigin,
                    builder =>
                    {
                        builder.AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowAnyOrigin();
                    });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sep6BackEnd v1"));

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(SpecificOrigin);

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}