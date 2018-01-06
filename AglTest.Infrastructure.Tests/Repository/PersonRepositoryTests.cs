using System.Linq;
using System.Threading.Tasks;
using AglTest.Domain.Exceptions;
using AglTest.Infrastructure.Client;
using AglTest.Infrastructure.Client.Errors;
using AglTest.Infrastructure.Config;
using AglTest.Infrastructure.Repository;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace AglTest.Infrastructure.Tests.Repository
{
    public class PersonRepositoryTests
    {
        private const string ValidUrl = "http://agl-developer-test.azurewebsites.net/people.json";
        private const string WrongDataValidUrl = "http://ip.jsontest.com/";
        private const string InValidUrl = "http://example/api.json";
        private const string ErrorFulUrl = "--htmple-api-json";
        
        [Theory]
        [InlineData(InValidUrl)]
        [InlineData(ErrorFulUrl)]        
        [InlineData(WrongDataValidUrl)]
        public async Task ListAsync_FetchFailing(string url)
        {
            var repo = GetRepo(url);
            await Assert.ThrowsAsync<AglFetchFailedException>(() => repo.ListAsync());
        }

        [Fact]
        public async Task ListAsync_Valid()
        {
            var repo = GetRepo(ValidUrl);
            var data = await repo.ListAsync();
            Assert.True(data.Any());
        }

        private PersonRepository GetRepo(string url)
        {
            var repoLogger = Mock.Of<ILogger<PersonRepository>>();
            var webClientLogger = Mock.Of<ILogger<WebResourceClient>>();
            var client = new WebResourceClient(webClientLogger);
            var options = new OptionsWrapper<AppSettings>(new AppSettings()
            {
                ResourceUrl = url
            });
            return new PersonRepository(client, options, repoLogger);       
        }
    }
}