using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Shared
{
    public static class BranchBuilder
    {
        public static void CreateBranch(this IApplicationBuilder builder, PathString path,
            IHostingEnvironment env, ILoggerFactory loggerFactory,
            IServiceCollection mainServiceCollection, BaseStartup startup)
        {
            builder.Map(path, branch =>
            {
                IServiceCollection serviceCollection = new ServiceCollection();

                foreach (ServiceDescriptor service in mainServiceCollection)
                {
                    serviceCollection.Add(service);
                }
                startup.ConfigureServices(serviceCollection);

                var provider = serviceCollection.BuildServiceProvider();
                branch.ApplicationServices = provider;
                branch.Use(async (context, next) =>
                {
                    context.RequestServices = provider;
                    await next.Invoke();
                });
                startup.Configure(branch, env, loggerFactory);
            });
        }
    }
}