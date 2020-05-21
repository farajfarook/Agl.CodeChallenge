using System.Collections.Generic;
using AglTest.Domain.People.Models;
using AglTest.Infrastructure.Data;

namespace AglTest.Infrastructure.Tests.Mocks
{
    public static class PeopleDtoMock
    {
        private static readonly PersonDto Sam = new PersonDto()
        {
            Age = 5,
            Gender = PersonGender.Male,
            Name = "Sam",
            Pets = new List<PetDto>
            {
                PetsDtoMock.Max,
                PetsDtoMock.Maxi
            }
        };

        private static readonly PersonDto Anne = new PersonDto()
        {
            Age = 5,
            Gender = PersonGender.Female,
            Name = "Anne",
            Pets = new List<PetDto>
            {
                PetsDtoMock.Nick,
                PetsDtoMock.Hogger
            }
        };
        
        private static readonly PersonDto Jenny = new PersonDto()
        {
            Age = 5,
            Gender = PersonGender.Female,
            Name = "Jenny",
            Pets = new List<PetDto>
            {
                PetsDtoMock.Kite,
                PetsDtoMock.Spooky,
                PetsDtoMock.Mingo,
                PetsDtoMock.Hogger
            }
        };
        
        private static readonly PersonDto Wini = new PersonDto()
        {
            Age = 5,
            Gender = PersonGender.Female,
            Name = "Wini",
            Pets = new List<PetDto>()
        };
        
        private static readonly PersonDto Raju = new PersonDto()
        {
            Age = 5,
            Gender = PersonGender.Male,
            Name = "Raju",
            Pets = null
        };
        
        private static readonly PersonDto Stev = new PersonDto()
        {
            Age = 5,
            Gender = PersonGender.Male,
            Name = "Steve",
            Pets = new List<PetDto>
            {
                PetsDtoMock.Hogger
            }
        };
        
        
        public static List<PersonDto> GetAllValidPeople()
        {
            return new List<PersonDto>() {Sam, Anne, Jenny, Raju, Wini, Stev};
        }
    }
}