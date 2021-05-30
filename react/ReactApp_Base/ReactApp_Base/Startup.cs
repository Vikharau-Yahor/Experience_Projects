using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SpaServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactApp_Base
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddOptions<SpaOptions>("SpaConfiguration");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // 1st -- tries return static files for requiest
            // -- if development then hot reload/rebuild are enabled via webpack dev server (must be run using npm run 
            if (env.IsDevelopment())
            {
                app.Map(
                    "/js",
                    ctx => ctx.UseSpa(
                        spa =>
                        {
                            spa.UseProxyToSpaDevelopmentServer("http://localhost:3001/");
                        })); ;
            }
            else
            {
                app.UseStaticFiles();
            }
            // 2nd -- tries process request as API call
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // 3rd -- just return SPA view
            // Note: default page path and other settings are configured by SpaOptions
            app.UseSpa(x => { });
            
        }
    }
}
