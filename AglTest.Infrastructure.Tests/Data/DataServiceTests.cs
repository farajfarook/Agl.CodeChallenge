using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AglTest.Infrastructure.Data;
using AglTest.Infrastructure.RestClient;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using NSubstitute;
using Xunit;

namespace AglTest.Infrastructure.Tests.Data
{
    public class DataServiceTests
    {
        private const string Endpoint = "http://agl-developer-test.azurewebsites.net/people.json";
        
        private readonly IServiceProvider _provider;
        
        public DataServiceTests()
        {
            var services = new ServiceCollection();
            services.AddOptions();
            services.Configure<DataOptions>(o =>
            {
                o.People = new DataOptions.PeopleDataOptions {Uri = Endpoint};
            });
            services.AddSingleton<ILogger<DataService>>(NullLogger<DataService>.Instance);
            services.AddSingleton(Substitute.For<IRestClient>());
            services.AddSingleton(Substitute.For<IMemoryCache>());
            services.AddSingleton<IDataService, DataService>();
            _provider = services.BuildServiceProvider();
        }
        
        [Fact]
        public async Task FetchAsync_NoCacheSuccess()
        {
            var dataService = _provider.GetService<IDataService>();
            var restClient = _provider.GetService<IRestClient>();
            await dataService.FetchAsync(CancellationToken.None);
            await restClient.Received().GetAsync<List<PersonDto>>(Arg.Is(Endpoint));
        }
        
        [Fact]
        public async Task FetchAsync_CacheSuccess()
        {
            var dataService = _provider.GetService<IDataService>();
            var restClient = _provider.GetService<IRestClient>();
            var cache = _provider.GetService<IMemoryCache>();
            cache.TryGetValue(Arg.Any<string>(), out Arg.Any<object>()).Returns(true);

            await dataService.FetchAsync(CancellationToken.None);
            await restClient.DidNotReceive().GetAsync<List<PersonDto>>(Arg.Is(Endpoint));
        }
    }
}