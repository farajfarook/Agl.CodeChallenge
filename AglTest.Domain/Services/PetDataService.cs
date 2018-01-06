using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AglTest.Domain.Models;
using AglTest.Domain.Repositories;
using AglTest.Domain.SeedWork;
using Microsoft.Extensions.Logging;

namespace AglTest.Domain.Services
{
    public class PetDataService : IPetFilteringService, IPetCollectionService
    {
        private readonly IPersonRepository _personRepository;
        private readonly IPetSortingService _petSortingService;
        private readonly ILogger _logger;

        public PetDataService(IPersonRepository personRepository, ILogger<PetDataService> logger, IPetSortingService petSortingService)
        {
            _personRepository = personRepository;
            _logger = logger;
            _petSortingService = petSortingService;
        }

        public async Task<IEnumerable<Pet>> ListPetsByPersonGenderAsync(PersonGender gender)
        {
            var persons = await _personRepository.ListAsync();
            return persons.SelectMany(p => Equals(p?.Gender, gender) ? p?.Pets ?? new Pet[0] : new Pet[0])
                .Distinct().ToList();
        }

        public async Task<IEnumerable<Tuple<PersonGender, IEnumerable<Pet>>>> ListSortedPetsByGenderAsync()
        {
            var genders = (PersonGender[])Enum.GetValues(typeof(PersonGender));
            var data = new List<Tuple<PersonGender, IEnumerable<Pet>>>();
            foreach (var gender in genders)
            {
                var pets = await ListPetsByPersonGenderAsync(gender);
                var petArray = pets as Pet[] ?? pets.ToArray();
                if(!petArray.Any()) continue;
                pets = _petSortingService.SortByName(petArray);                
                data.Add(new Tuple<PersonGender, IEnumerable<Pet>>(gender, pets)); 
            }
            return data;
        }
    }
}