using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Oqtane.Infrastructure;
using Trifoia.Module.Todo.Repository;
using Trifoia.Module.Todo.Services;

namespace Trifoia.Module.Todo.Startup
{
    public class TodoServerStartup : IServerStartup
    {

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // not implemented
        }

        public void ConfigureMvc(IMvcBuilder mvcBuilder)
        {
            // not implemented
        }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddTransient<ITodoService, ServerTodoService>();
            services.AddTransient<ITodoService, ServerTodoService>();
            services.AddDbContextFactory<TodoContext>(opt => { }, ServiceLifetime.Transient);
        
        }
    }
}