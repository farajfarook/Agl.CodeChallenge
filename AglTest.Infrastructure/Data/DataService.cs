using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AglTest.Infrastructure.RestClient;
using Enbiso.NLib.DependencyInjection;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace AglTest.Infrastructure.Data
{
    public interface IDataService
    {
        Task<List<PersonDto>> FetchAsync(CancellationToken cancellationToken);
    }
    
    [TransientService]
    public class DataService: IDataService
    {
        private const string DataCache = nameof(DataCache);
        
        private readonly IMemoryCache _cache;
        private readonly IRestClient _client;
        private readonly DataOptions _options;

        public DataService(IMemoryCache cache, IRestClient client, IOptions<DataOptions> options)
        {
            _cache = cache;
            _client = client;
            _options = options.Value;
        }

        public async Task<List<PersonDto>> FetchAsync(CancellationToken cancellationToken)
        {
            if(_cache.TryGetValue(DataCache, out var data))
                return data as List<PersonDto>;

            var rawData = await _client.GetAsync<List<PersonDto>>(_options.People.Uri, cancellationToken);
            _cache.Set(DataCache, rawData);
            return rawData;
        }


    }
}