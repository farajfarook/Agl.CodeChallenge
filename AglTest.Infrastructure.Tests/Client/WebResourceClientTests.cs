using System;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using AglTest.Domain.Exceptions;
using AglTest.Infrastructure.Client;
using AglTest.Infrastructure.Client.Errors;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace AglTest.Infrastructure.Tests.Client
{
    public class ClientTests
    {
        private const string InvalidUrl = "http://example/api.json";
        private const string ValidUrl = "http://ip.jsontest.com/";
        private const string ErrorFulUrl = "--htmple-api-json";
        
        private readonly ILogger<WebResourceClient> _logger = Mock.Of<ILogger<WebResourceClient>>();
        
        [Fact]
        public async Task GetAsync_InvalidUrl_ThrowsException()
        {   
            var client = new WebResourceClient(_logger);            
            await Assert.ThrowsAsync<AglInvalidUrlException>(() => client.GetAsync(InvalidUrl));
        }

        [Fact]
        public void GetAsync_ValidUrl_Success()
        {            
            var client = new WebResourceClient(_logger);
            var response = client.GetAsync(ValidUrl);
            Assert.NotNull(response);
        }

        [Fact]
        public async Task GetAsync_ErrorfulUrl_ThrowsException()
        {            
            var client = new WebResourceClient(_logger);
            await Assert.ThrowsAsync<AglErrorfulUrlException>(() => client.GetAsync(ErrorFulUrl));
        }
    }
}