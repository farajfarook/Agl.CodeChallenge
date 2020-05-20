using AglTest.Domain;
using AglTest.Infrastructure.Data;
using AglTest.Infrastructure.RestClient;
using Enbiso.NLib.Cqrs;
using Enbiso.NLib.DependencyInjection;
using Enbiso.NLib.GlobalExceptions;
using Enbiso.NLib.OpenApi;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AglTest.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.Configure<DataOptions>(Configuration.GetSection("AglData"));
            
            services.AddControllers(c => c.Filters.Add<GlobalExceptionFilter>());
            services.AddCqrs();
            services.AddHttpClient();
            services.AddSingleton<IMemoryCache, MemoryCache>();
            services.AddOpenApi(Configuration.GetSection("OpenApi").Bind);
            services.AddServices(typeof(Startup), typeof(DomainException), typeof(RestClient));
        }
        
        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseOpenApi();
            app.UseCors(o => o.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}