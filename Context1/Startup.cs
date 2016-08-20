using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Shared;
using Shared.Mvc;

namespace Context1
{
    

    public class Startup
        : BaseStartup
    {
        public Startup(IConfigurationRoot configurationRoot)
        {
            Configuration = configurationRoot;
        }

        public IConfigurationRoot Configuration { get; }

        public override void ConfigureServices(IServiceCollection services)
        {
            var assemblies = new[] { typeof(Startup).GetTypeInfo().Assembly };
            var mvc = services.AddMvc(opt =>
            {

            });
            mvc.PartManager.FeatureProviders.Clear();
            mvc.PartManager.FeatureProviders.Add(new AssemblyBasedControllerFeatureProvider(assemblies));
        }

        public override void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
