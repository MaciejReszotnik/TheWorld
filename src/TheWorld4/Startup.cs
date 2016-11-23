using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TheWorld4.Services;
using Microsoft.Extensions.Configuration;
using TheWorld4.Models;

namespace TheWorld4
{
    public class Startup
    {
        public static IConfigurationRoot Configuration;
        public Startup(IHostingEnvironment appEnv)
        {
            var builder = new ConfigurationBuilder().SetBasePath(appEnv.ContentRootPath).AddJsonFile("config.json").AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddEntityFrameworkSqlServer().AddDbContext<WorldContext>();
            services.AddScoped<IMailService, DebugMailService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //app.UseDefaultFiles();
            //loggerFactory.AddConsole();

            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});

            //app.UseStaticFiles();
            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();
            loggerFactory.AddConsole();




            app.UseMvc(config =>
            {
                config.MapRoute(
                    name: "Default",
                template: "{controller}/{action}/{id?}",
                defaults: new { controller = "App", Action = "Index" }
                );
            });

        }
    }
}
