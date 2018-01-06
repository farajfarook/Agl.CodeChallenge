using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using AglTest.Domain.Exceptions;
using AglTest.Domain.Models;
using AglTest.Domain.Repositories;
using AglTest.Infrastructure.Client;
using AglTest.Infrastructure.Config;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace AglTest.Infrastructure.Repository
{
    public class PersonRepository: IPersonRepository
    {
        private readonly IResourceClient _client;
        private readonly AppSettings _appSettings;
        private readonly ILogger _logger;

        public PersonRepository(IResourceClient client, IOptions<AppSettings> options, ILogger<PersonRepository> logger)
        {
            _client = client;
            _appSettings = options.Value;
            _logger = logger;
        }

        public async Task<IEnumerable<Person>> ListAsync()
        {
            try
            {
                _logger.LogTrace($"Fetching data from {_appSettings.ResourceUrl}");
                var response = await _client.GetAsync(_appSettings.ResourceUrl);
                
                try
                {
                    return JsonConvert.DeserializeObject<IEnumerable<Person>>(response);
                }
                catch (Exception e)
                {
                    _logger.LogError(e, $"Error parsing - {response}");
                    throw;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to fetch");
                throw new AglFetchFailedException();
            }
        }
    }
}