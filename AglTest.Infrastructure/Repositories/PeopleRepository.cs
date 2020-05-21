using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AglTest.Domain.People;
using AglTest.Domain.People.Models;
using AglTest.Infrastructure.Data;
using AglTest.Infrastructure.Mappers;
using Enbiso.NLib.DependencyInjection;

namespace AglTest.Infrastructure.Repositories
{
    [TransientService]
    public class PeopleRepository: IPeopleRepository
    {
        private readonly IDataService _dataService;
        private readonly IPersonMapper _mapper;

        public PeopleRepository(IDataService dataService, IPersonMapper mapper)
        {
            _dataService = dataService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Person>> ListAsync(CancellationToken cancellationToken)
        {
            var dto = await _dataService.FetchAsync(cancellationToken);
            var people = dto?.Select(p => _mapper.Map(p)).ToList()
                ?? new List<Person>();
            return people;
        }

        public async Task<IEnumerable<Person>> ListByGenderAsync(PersonGender gender, CancellationToken cancellationToken)
        {
            var dto = await _dataService.FetchAsync(cancellationToken);
            var people = dto.Where(p => p.Gender == gender).Select(p => _mapper.Map(p)).ToList();
            return people;
        }
    }
}