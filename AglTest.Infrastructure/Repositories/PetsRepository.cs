using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AglTest.Domain;
using AglTest.Domain.Models;
using AglTest.Domain.Pets;
using AglTest.Infrastructure.Data;
using AglTest.Infrastructure.Mappers;
using Enbiso.NLib.DependencyInjection;

namespace AglTest.Infrastructure.Repositories
{
    [TransientService]
    public class PetsRepository: IPetsRepository
    {
        private readonly IDataService _dataService;
        private readonly IPetMapper _mapper;

        public PetsRepository(IPetMapper mapper, IDataService dataService)
        {
            _mapper = mapper;
            _dataService = dataService;
        }

        public async Task<IEnumerable<Pet>> ListAsync(string ownerName, CancellationToken cancellationToken)
        {
            var dto = await _dataService.FetchAsync(cancellationToken);
            var personDto = dto.FirstOrDefault(p => p.Name == ownerName)
                            ?? throw new NotFoundException($"Person {ownerName} not found");
            personDto.Pets ??= new List<PetDto>();
            var pets = personDto.Pets.Select(p => _mapper.Map(p)).ToList();
            return pets;
        }
    }
}