using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using AglTest.Infrastructure.RestClient;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using NSubstitute;
using Xunit;

namespace AglTest.Infrastructure.Tests.RestClient
{
    public class RestClientTests
    {
        private const string UrlSample = "http://example/api.json";
        
        private readonly IServiceProvider _provider;
        
        public RestClientTests()
        {
            var services = new ServiceCollection();
            services.AddSingleton<ILogger<Infrastructure.RestClient.RestClient>>(NullLogger<Infrastructure.RestClient.RestClient>.Instance);
            services.AddSingleton<IRestClient, Infrastructure.RestClient.RestClient>();
            services.AddSingleton(Substitute.For<IHttpClientFactory>());
            
            _provider = services.BuildServiceProvider();
        }

        [Fact]
        public async Task GetAsync_NotOk_ThrowsException()
        {
            var factory = _provider.GetService<IHttpClientFactory>();
            var clientHandlerStub = new DelegatingHandlerStub(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.BadGateway,
                Content = null
            });
            var client = new HttpClient(clientHandlerStub);
            factory.CreateClient().Returns(client);
            
            var rest = _provider.GetService<IRestClient>();
            await Assert.ThrowsAsync<RestRequestFailedException>(() => rest.GetAsync<object>(UrlSample));
        }

        
        [Fact]
        public async Task GetAsync_InvalidJson_ThrowsException()
        {
            var factory = _provider.GetService<IHttpClientFactory>();
            var clientHandlerStub = new DelegatingHandlerStub(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("{ssss:}")
            });
            var client = new HttpClient(clientHandlerStub);
            factory.CreateClient().Returns(client);
            
            var rest = _provider.GetService<IRestClient>();
            await Assert.ThrowsAsync<JsonException>(() => rest.GetAsync<object>(UrlSample));
        }

        
        [Fact]
        public void GetAsync_ValidUrl_Success()
        {            
            var factory = _provider.GetService<IHttpClientFactory>();
            var clientHandlerStub = new DelegatingHandlerStub(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("{}")
            });
            var client = new HttpClient(clientHandlerStub);
            factory.CreateClient().Returns(client);
            
            var rest = _provider.GetService<IRestClient>();
            var response = rest.GetAsync<object>(UrlSample);
            Assert.NotNull(response);
        }

        [Fact]
        public async Task GetAsync_ErrorfulUrl_ThrowsException()
        {
            var rest = _provider.GetService<IRestClient>();
            await Assert.ThrowsAsync<RestInvalidUrlException>(() => rest.GetAsync<object>("+--aaa.com"));
        }
    }
    
    class DelegatingHandlerStub : DelegatingHandler
    {
        private readonly HttpResponseMessage _response;

        public DelegatingHandlerStub(HttpResponseMessage response)
        {
            _response = response;
        }
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken token) 
            => Task.FromResult(_response);
    }
}