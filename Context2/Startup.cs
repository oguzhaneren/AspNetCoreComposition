using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Shared;
using Shared.Mvc;
using  System.Reflection;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace Context2
{
    public class Startup
        :BaseStartup
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
                opt.OutputFormatters.Clear();
                opt.OutputFormatters.Add(new XmlSerializerOutputFormatter());
            });
            mvc.PartManager.FeatureProviders.Clear();
            mvc.PartManager.FeatureProviders.Add(new AssemblyBasedControllerFeatureProvider(assemblies));
        }

        public override void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseMvc();
        }
    }
}
