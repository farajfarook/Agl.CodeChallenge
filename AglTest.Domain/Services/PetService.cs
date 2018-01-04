using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AglTest.Domain.Models;
using AglTest.Domain.Repositories;
using AglTest.Domain.SeedWork;

namespace AglTest.Domain.Services
{
    public class PetService : IPetSortingService, IPetFilteringService
    {
        private readonly IPersonRepository _personRepository;

        public PetService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public IEnumerable<Pet> SortByName(IEnumerable<Pet> pets)
        {
            return pets?.OrderBy(m => m.Name);
        }

        public async Task<IEnumerable<Pet>> ListPetsByPersonGenderAsync(PersonGender gender)
        {
            var persons = await _personRepository.ListAsync();
            return persons.SelectMany(p => Equals(p.Gender, gender) ? p.Pets : new Pet[0]);
        }
    }
}