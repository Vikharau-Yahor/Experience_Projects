using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Primitives;

namespace ASPNet5_Projec
{
    public class Startup
    {
        public Startup()
        {
            //var builder = new ConfigurationBuilder()
            //                .AddInMemoryCollection(new Dictionary<string, string>
            //                {
            //                    {"name", "Tom"},
            //                    {"age", "31"}
            //                })
            //               .AddConfiguration(config);
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var service = services.First();
            
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //This will return static files if it requested in url e.g:
            //1. localhost/index.html
            // Static files will be taken from overridden in Program.cs folder -- 'test' (instead of defaul 'wwwroot')
            app.UseStaticFiles();
        }

    }

    public interface IConfigurationRoot : IConfiguration
    {
        IEnumerable<IConfigurationProvider> Providers { get; }
        void Reload();
    }
}
