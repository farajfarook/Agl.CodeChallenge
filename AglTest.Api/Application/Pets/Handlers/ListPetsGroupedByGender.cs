using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AglTest.Domain.Models;
using AglTest.Domain.People;
using AglTest.Domain.People.Models;
using AglTest.Domain.Pets;
using AglTest.Domain.Pets.Services;
using Enbiso.NLib.Cqrs;
using Enbiso.NLib.DependencyInjection;

namespace AglTest.Api.Application.Pets.Handlers
{
    public class ListPetsGroupedByGender: ICommand<ListPetsGroupByGenderResponse>
    {
    }

    public class ListPetsGroupByGenderResponse : ICommandResponse
    {
        public IEnumerable<GenderGroupedPetsRecord> Records { get; set; }
    }

    public class GenderGroupedPetsRecord
    {
        public PersonGender Gender { get; set; }
        public IEnumerable<Pet> Pets { get; set; }
    }

    [TransientService]
    public class ListPetsGroupedByGenderHandler : ICommandHandler<ListPetsGroupedByGender, ListPetsGroupByGenderResponse>
    {
        private readonly IPetsRepository _petsRepository;
        private readonly IPeopleRepository _peopleRepository;
        private readonly IPetSortingService _sortingService;

        public ListPetsGroupedByGenderHandler(IPetsRepository petsRepository, IPeopleRepository peopleRepository, IPetSortingService sortingService)
        {
            _petsRepository = petsRepository;
            _peopleRepository = peopleRepository;
            _sortingService = sortingService;
        }
        
        public async Task<ListPetsGroupByGenderResponse> Handle(ListPetsGroupedByGender request, CancellationToken cancellationToken)
        {
            var records = new List<GenderGroupedPetsRecord>();
            foreach (var gender in (PersonGender[])Enum.GetValues(typeof(PersonGender)))
            {
                var owners = await _peopleRepository.ListByGenderAsync(gender, cancellationToken)
                             ?? new List<Person>();
                
                var pets = new List<Pet>();
                foreach (var owner in owners)
                {
                    var ownersPets = await _petsRepository.ListAsync(owner.Name, cancellationToken)
                                     ?? new List<Pet>();
                    pets.AddRange(ownersPets);
                }
                records.Add(new GenderGroupedPetsRecord
                {
                    Gender = gender,
                    Pets = _sortingService.SortByName(pets)
                });
            }
            return new ListPetsGroupByGenderResponse
            {
                Records = records
            };
        }
    }
}