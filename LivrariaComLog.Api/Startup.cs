using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElmahCore;
using ElmahCore.Mvc;
using ElmahCore.MySql;
using LivrariaComLog.Domain.Handlers;
using LivrariaComLog.Domain.Interfaces.Repositories;
using LivrariaComLog.Infra.Data.DataContexts;
using LivrariaComLog.Infra.Data.Repositories;
using LivrariaComLog.Infra.Settings;
using LivrariaComLog.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace LivrariaComLog
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
            #region AppSettings

            AppSettings appSettings = new();
            Configuration.GetSection("AppSettings").Bind(appSettings);
            services.AddSingleton(appSettings);
            
            #endregion
            
            #region Elmah

            services.AddElmah();
            services.AddElmah<XmlFileErrorLog>(opt =>
            {
                opt.LogPath = "~/log";
            });
            services.AddElmah<MySqlErrorLog>(opt =>
            {
                opt.ConnectionString = appSettings.ConnectionString;
            });

            #endregion

            #region DataContexts

            services.AddScoped<DataContext>();

            #endregion

            #region Repositories

            services.AddTransient<ILivroRepository, LivroRepository>();

            #endregion

            #region Handlers

            services.AddTransient<LivroHandler, LivroHandler>();

            #endregion
            
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "LivrariaComLog.Api", Version = "v1"});
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "LivrariaComLog.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.UseRouting();

            app.UseAuthorization();

            app.UseElmah();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}