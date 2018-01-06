using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AglTest.Domain.Models;
using AglTest.Domain.Repositories;
using AglTest.Domain.SeedWork;
using Microsoft.Extensions.Logging;

namespace AglTest.Domain.Services
{
    public class PetService : IPetSortingService, IPetFilteringService
    {
        private readonly IPersonRepository _personRepository;
        private readonly ILogger _logger;

        public PetService(IPersonRepository personRepository, ILogger<PetService> logger)
        {
            _personRepository = personRepository;
            _logger = logger;
        }

        public IEnumerable<Pet> SortByName(IEnumerable<Pet> pets)
        {
            _logger.LogTrace("Sort by name");
            var petArray = pets as List<Pet> ?? pets.ToList();
            petArray.RemoveAll(m => m == null);
            return petArray.OrderBy(m => m?.Name).ToList();
        }

        public async Task<IEnumerable<Pet>> ListPetsByPersonGenderAsync(PersonGender gender)
        {
            var persons = await _personRepository.ListAsync();
            return persons.SelectMany(p => Equals(p?.Gender, gender) ? p?.Pets ?? new Pet[0] : new Pet[0])
                .Distinct().ToList();
        }
    }
}