using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Services.Description;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Owin;
using Owin;
using TodoApplication.Extensions;
using TodoApplication.Repositories;
using TodoApplication.Services;

[assembly: OwinStartupAttribute(typeof(TodoApplication.Startup))]
namespace TodoApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var services = new Microsoft.Extensions.DependencyInjection.ServiceCollection();
            ConfigureAuth(app);
            ConfigureServices(services);
            var resolver = new DefaultDependencyResolver(services.BuildServiceProvider());
            DependencyResolver.SetResolver(resolver);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersAsServices(typeof(Startup).Assembly.GetExportedTypes()
               .Where(t => !t.IsAbstract && !t.IsGenericTypeDefinition)
               .Where(t => typeof(IController).IsAssignableFrom(t)
                  || t.Name.EndsWith("Controller", StringComparison.OrdinalIgnoreCase)));

            // Add application services.
            services.AddTransient<ITodoRepository, TodoRepository>();
            services.AddTransient<INotifierService, EmailNotifier>();
        }
    }
}
