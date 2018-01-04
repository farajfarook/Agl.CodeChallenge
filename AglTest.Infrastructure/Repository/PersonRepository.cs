using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using AglTest.Domain.Exceptions;
using AglTest.Domain.Models;
using AglTest.Domain.Repositories;
using AglTest.Infrastructure.Config;
using Newtonsoft.Json;

namespace AglTest.Infrastructure.Repository
{
    public class PersonRepository: IPersonRepository
    {
        private readonly HttpClient _client;
        private readonly AppSettings _appSettings;

        public PersonRepository(HttpClient client, AppSettings appSettings)
        {
            _client = client;
            _appSettings = appSettings;
        }

        public async Task<IEnumerable<Person>> ListAsync()
        {
            var response = await _client.GetAsync(_appSettings.ResourceUrl);
            if(!response.IsSuccessStatusCode) 
                throw new AglDomainException("Failed to fetch the people from external resource");
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<Person>>(content);
        }
    }
}