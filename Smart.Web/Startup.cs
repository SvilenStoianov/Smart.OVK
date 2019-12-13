using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinqToDB.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Smart.Web.Infrastructure;
using StructureMap;

namespace Smart.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            DataConnection.DefaultSettings = new MySqlSettings();

            services.AddHttpContextAccessor();
            services.AddLogging();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            var container = new Container(config =>
             {
                 config.Populate(services);
                 config.AddRegistry<MainRegistry>();
             });

            return container.GetInstance<IServiceProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Use(async (context, func) =>
            {
                context.Response.Headers["Content-Type"] = "application/json";

                await func();
            });

            app.UseAuthMiddleware();
            app.UseGatewayProtocolMiddleware();

            //app.UseHttpsRedirection();
            //app.UseMvc();

            app.Run(ctx =>
            {
                ctx.Response.StatusCode = 404;
                return Task.CompletedTask;
            });
        }
    }
}
