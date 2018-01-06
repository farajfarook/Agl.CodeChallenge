using System.Net.Http;
using AglTest.Domain.Repositories;
using AglTest.Domain.Services;
using AglTest.Infrastructure.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace AglTest.Infrastructure.Config
{
    public static class InfrastructureSetup
    {
        public static void AddAglServices(this IServiceCollection services, IConfigurationSection settings)
        {
            services.Configure<AppSettings>(settings);
            services.AddSingleton<HttpClient>();
            services.AddTransient<IPersonRepository, PersonRepository>();
            services.AddTransient<IPetSortingService, PetUtilService>();
            services.AddTransient<IPetFilteringService, PetDataService>();
            services.AddTransient<IPetCollectionService, PetDataService>();
        }
    }
}