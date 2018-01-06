using System;
using System.Net.Http;
using System.Resources;
using System.Threading.Tasks;
using AglTest.Domain.Exceptions;
using AglTest.Infrastructure.Client.Errors;
using Microsoft.Extensions.Logging;

namespace AglTest.Infrastructure.Client
{
    public class WebResourceClient : IResourceClient
    {
        private readonly ILogger _logger;
        private readonly HttpClient _client;

        public WebResourceClient(ILogger<WebResourceClient> logger)
        {
            _logger = logger;
            _client = new HttpClient();
        }

        public async Task<string> GetAsync(string request)
        {
            if (!Uri.IsWellFormedUriString(request, UriKind.Absolute))
            {
                _logger.LogError($"Errorful Url {request}");
                throw new AglErrorfulUrlException(request);
            }
            _logger.LogTrace($"Requesting {request}");
            try
            {
                return await _client.GetStringAsync(request);
            }
            catch (HttpRequestException e)
            {
                _logger.LogError(e, "Invalid Operation");
                throw new AglInvalidUrlException(request);
            }
        }
    }
}