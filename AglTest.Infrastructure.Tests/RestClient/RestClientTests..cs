using System;
using System.Net;
using System.Net.Http;
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
        public async Task GetAsync_InvalidUrl_ThrowsException()
        {
            var factory = _provider.GetService<IHttpClientFactory>();
            var clientHandlerStub = new DelegatingHandlerStub();
            var client = new HttpClient(clientHandlerStub);
            factory.CreateClient().Returns(client);
            
            var rest = _provider.GetService<IRestClient>();
            await Assert.ThrowsAsync<RestInvalidUrlException>(() => rest.GetAsync<object>(UrlSample));
        }

        [Fact]
        public void GetAsync_ValidUrl_Success()
        {            
            var factory = _provider.GetService<IHttpClientFactory>();
            var clientHandlerStub = new DelegatingHandlerStub();
            var client = new HttpClient(clientHandlerStub);
            factory.CreateClient().Returns(client);
            
            var rest = _provider.GetService<IRestClient>();
            var response = rest.GetAsync<object>(UrlSample);
            Assert.NotNull(response);
        }

        [Fact]
        public async Task GetAsync_ErrorfulUrl_ThrowsException()
        {            
            var factory = _provider.GetService<IHttpClientFactory>();
            var clientHandlerStub = new DelegatingHandlerStubWithException();
            var client = new HttpClient(clientHandlerStub);
            factory.CreateClient().Returns(client);
            
            var rest = _provider.GetService<IRestClient>();
            await Assert.ThrowsAsync<RestRequestFailedException>(() => rest.GetAsync<object>(UrlSample));
        }
    }
    
    class DelegatingHandlerStubWithException : DelegatingHandler {
        private readonly Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> _handlerFunc;
        public DelegatingHandlerStubWithException() {
            _handlerFunc = (request, cancellationToken) => throw new HttpRequestException();
        }
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) {
            return _handlerFunc(request, cancellationToken);
        }
    }
    
    class DelegatingHandlerStub : DelegatingHandler {
        private readonly Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> _handlerFunc;
        public DelegatingHandlerStub() {
            _handlerFunc = (request, cancellationToken) => Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK));
        }
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) {
            return _handlerFunc(request, cancellationToken);
        }
    }
}