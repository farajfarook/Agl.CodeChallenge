using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Enbiso.NLib.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AglTest.Infrastructure.RestClient
{
    [TransientService]
    public class RestClient : IRestClient
    {
        private readonly ILogger _logger;
        private readonly HttpClient _client;
        public RestClient(ILogger<RestClient> logger, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _client = clientFactory.CreateClient();
        }

        public async Task<TResp> GetAsync<TResp>(string url, CancellationToken cancellationToken)
        {
            _logger.LogTrace($"Requesting {url}");
            if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
            {
                _logger.LogError($"Invalid Url {url}");
                throw new RestInvalidUrlException(url);
            }

            try
            {
                var resp = await _client.GetAsync(url, cancellationToken);
                resp.EnsureSuccessStatusCode();
                var content = await resp.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<TResp>(content);
            }
            catch (JsonException e)
            {
                _logger.LogError(e, "Failed to parse response. " + e.Message);
                throw;
            }
            catch (HttpRequestException e)
            {
                _logger.LogError(e, "Invalid Operation");
                throw new RestRequestFailedException(url);
            }
        }
    }
}